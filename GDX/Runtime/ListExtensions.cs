﻿// dotBunny licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GDX
{
    /// <summary>
    ///     <see cref="System.Collections.Generic.List{T}" /> Based Extension Methods
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        ///     <para>Does <paramref name="targetList" /> contain <paramref name="targetItem" />?</para>
        /// </summary>
        /// <remarks>Avoids using <see cref="System.Collections.Generic.EqualityComparer{T}" />.</remarks>
        /// <param name="targetList">The <see cref="System.Collections.Generic.List{T}" /> to look in.</param>
        /// <param name="targetItem">The target object to look for.</param>
        /// <typeparam name="T">The type of the <see cref="System.Collections.Generic.List{T}" />.</typeparam>
        /// <returns>true/false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsItem<T>(this List<T> targetList, T targetItem) where T : class
        {
            int length = targetList.Count;
            for (int i = 0; i < length; i++)
            {
                if (targetList[i] == targetItem)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     <para>
        ///         Replaces the object found at the provided <paramref name="index" /> with the last object in
        ///         <paramref name="targetList" />, then removes the last item from the <paramref name="targetList" />.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     This make sure that you are always removing from the end of a
        ///     <see cref="System.Collections.Generic.List{T}" />.
        /// </remarks>
        /// <param name="targetList">The target <see cref="System.Collections.Generic.List{T}" />.</param>
        /// <param name="index">The index of the item to remove.</param>
        /// <typeparam name="T">The type of the <see cref="System.Collections.Generic.List{T}" />.</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveItemSwap<T>(this List<T> targetList, int index)
        {
            int lastIndex = targetList.Count - 1;
            targetList[index] = targetList[lastIndex];
            targetList.RemoveAt(lastIndex);
        }

        /// <summary>
        ///     <para>Removes <paramref name="targetItem" /> from the provided <paramref name="targetList" />.</para>
        /// </summary>
        /// <remarks>Avoids using <see cref="System.Collections.Generic.EqualityComparer{T}" />.</remarks>
        /// <param name="targetList">The target <see cref="System.Collections.Generic.List{T}" />.</param>
        /// <param name="targetItem">The target object to remove from the <paramref name="targetList" />.</param>
        /// <typeparam name="T">The type of the <see cref="System.Collections.Generic.List{T}" />.</typeparam>
        /// <returns>true/false if the item was removed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RemoveItem<T>(this List<T> targetList, T targetItem) where T : class
        {
            int length = targetList.Count;
            for (int i = 0; i < length; i++)
            {
                if (targetList[i] != targetItem)
                {
                    continue;
                }

                targetList.RemoveAt(i);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Shuffle the items in the <paramref name="targetList" />.
        /// </summary>
        /// <param name="targetList">The target <see cref="System.Collections.Generic.List{T}" />.</param>
        /// <typeparam name="T">The type of the <see cref="System.Collections.Generic.List{T}" />.</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Shuffle<T>(this List<T> targetList)
        {
            int length = targetList.Count;
            for (int i = 0; i < length; i++)
            {
                T t = targetList[i];
                int r = Random.Range(i, length);
                targetList[i] = targetList[r];
                targetList[r] = t;
            }
        }
    }
}