﻿// dotBunny licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using UnityEditor;
using UnityEngine;

namespace GDX.Editor
{
    /// <summary>
    /// An author-time helper class to access the GDX specific configuration file in a project.
    /// </summary>
    public static class ConfigProvider
    {
        /// <summary>
        /// Get/Create an instance of the <see cref="GDXConfig"/> <see cref="UnityEngine.ScriptableObject"/>.
        /// </summary>
        /// <returns>A fully realized <see cref="GDXConfig"/>.</returns>
        public static GDXConfig Get()
        {
            // Attempt to load the settings file from the asset database.
            GDXConfig settings = AssetDatabase.LoadAssetAtPath<GDXConfig>("Assets/Resources/GDX/GDXConfig.asset");

            // If it worked, send it back!
            if (settings != null)
            {
                return settings;
            }

            // Looks like we need to make one
            settings = ScriptableObject.CreateInstance<GDXConfig>();

            // Ensure the folder structure is in place before we manually make the asset
            Platform.EnsureFileFolderHierarchyExists(
                System.IO.Path.Combine(Application.dataPath, "Resources/GDX/GDXConfig.asset"));

            // Create and save the asset
            AssetDatabase.CreateAsset(settings, "Assets/Resources/GDX/GDXConfig.asset");
            AssetDatabase.SaveAssets();

            // Send it back!
            return settings;
        }

        /// <summary>
        /// Get a <see cref="UnityEditor.SerializedObject"/> for raw editing of the <see cref="GDXConfig"/>.
        /// </summary>
        /// <returns>A <see cref="UnityEditor.SerializedObject"/>.</returns>
        public static SerializedObject GetSerializedConfig()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            return new SerializedObject(Get());
        }
    }
}