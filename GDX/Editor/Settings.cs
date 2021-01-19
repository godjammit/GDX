﻿// dotBunny licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// ReSharper HeapView.ObjectAllocation.Evident

namespace GDX.Editor
{
    /// <summary>
    ///     GDX Assembly Settings
    /// </summary>
    public static class Settings
    {
        /// <summary>
        ///     The key used by <see cref="EditorPrefs" /> to store <see cref="UpdateDayCountSetting" />.
        /// </summary>
        private const string UpdateDayCountKey = "GDX.UpdateProvider.UpdateDayCount";

        /// <summary>
        ///     A list of keywords to flag when searching project settings.
        /// </summary>
        private static readonly HashSet<string> s_keywords = new HashSet<string>(new[] {"gdx", "update"});

        /// <summary>
        ///     Settings content for <see cref="GDXConfig.updateProviderCheckForUpdates" />.
        /// </summary>
        private static readonly GUIContent s_contentCheckForUpdates = new GUIContent(
            "",
            "Should the package check the GitHub repository to see if there is a new version?");

        /// <summary>
        ///     Settings content for <see cref="UpdateDayCountSetting" />.
        /// </summary>
        private static readonly GUIContent s_contentUpdateDayCount = new GUIContent(
            "Update Timer (Days)",
            "After how many days should updates be checked for?");

        /// <summary>
        ///     The number of days between checks for updates.
        /// </summary>
        /// <remarks>We use a property over methods in this case so that Unity's UI can be easily tied to this value.</remarks>
        public static int UpdateDayCountSetting
        {
            get => EditorPrefs.GetInt(UpdateDayCountKey, 7);
            private set => EditorPrefs.SetInt(UpdateDayCountKey, value);
        }

        /// <summary>
        ///     Get <see cref="SettingsProvider" /> for GDX assembly.
        /// </summary>
        /// <returns>A provider for project settings.</returns>
        [SettingsProvider]
        public static SettingsProvider SettingsProvider()
        {
            return new SettingsProvider("Project/GDX", SettingsScope.Project)
            {
                label = "GDX",
                guiHandler = searchContext =>
                {
                    SerializedObject settings = ConfigProvider.GetSerializedConfig();
                    EditorGUILayout.BeginVertical(SettingsStyles.WrapperStyle);

                    #region Package Updates

                    bool packageSectionEnabled = SettingsStyles.SectionHeader(
                        "Automatic Package Updates",
                        settings.FindProperty("updateProviderCheckForUpdates"),
                        s_contentCheckForUpdates);

                    EditorGUILayout.BeginHorizontal(SettingsStyles.InfoBoxStyle);
                    if (UpdateProvider.LocalPackage.Definition != null)
                    {
                        GUILayout.Label(
                            UpdateProvider.UpdatePackageDefinition != null
                                ? $"Local version: {UpdateProvider.LocalPackage.Definition.version} ({UpdateProvider.LocalPackage.InstallationMethod.ToString()})\nGitHub version: {UpdateProvider.UpdatePackageDefinition.version}\nLast checked on {UpdateProvider.GetLastChecked().ToString(Localization.LocalTimestampFormat)}."
                                : $"Local version: {UpdateProvider.LocalPackage.Definition.version} ({UpdateProvider.LocalPackage.InstallationMethod.ToString()})\nGitHub version: Unknown\nLast checked on {UpdateProvider.GetLastChecked().ToString(Localization.LocalTimestampFormat)}.",
                            EditorStyles.boldLabel);

                        // Force things to the right
                        GUILayout.FlexibleSpace();

                        EditorGUILayout.BeginVertical();
                        if (UpdateProvider.HasUpdate(UpdateProvider.UpdatePackageDefinition))
                        {
                            if (GUILayout.Button("Changelog", SettingsStyles.ButtonStyle))
                            {
                                Application.OpenURL(UpdateProvider.GitHubChangelogUri);
                            }

                            if (GUILayout.Button("Update", SettingsStyles.ButtonStyle))
                            {
                                UpdateProvider.AttemptUpgrade();
                            }
                        }
                        else
                        {
                            if (GUILayout.Button("Manual Check", SettingsStyles.ButtonStyle))
                            {
                                UpdateProvider.CheckForUpdates();
                            }
                        }

                        EditorGUILayout.EndVertical();
                    }
                    else
                    {
                        GUILayout.Label(
                            $"An error occured trying to find the package definition.\nPresumed Root: {UpdateProvider.LocalPackage.PackageAssetPath}\nPresumed Manifest:{UpdateProvider.LocalPackage.PackageManifestPath})",
                            EditorStyles.boldLabel);
                    }

                    EditorGUILayout.EndHorizontal();


                    // Disable based on if we have this enabled
                    GUI.enabled = packageSectionEnabled;

                    UpdateDayCountSetting =
                        EditorGUILayout.IntSlider(s_contentUpdateDayCount, UpdateDayCountSetting, 1, 31);

                    // Restore regardless
                    GUI.enabled = true;

                    #endregion

                    settings.ApplyModifiedPropertiesWithoutUndo();
                    EditorGUILayout.EndVertical();
                },
                keywords = s_keywords
            };
        }
    }
}