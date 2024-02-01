using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace YNL.RuntimeDebugCommand
{
    public static class DebugCommandConstants
    {
        public static Action<bool, bool> ActivateDebugCommand;
        public static Action OnStartInputCommand;
        public static Action OnCaretPositionChanged;
        public static Action OnChangeNode;
        public static Action OnFinishNode;
        public static Action OnCommandSuccess;
        public static Action OnCommandFail;
        public static Action<ExecuteType, string> OnShowLog;
        public static Action<string, string[]> OnExecuteCommand;

        public static bool IsFocusOnDebugCommand;
    }

    public enum ExecuteType
    {
        Executed, Failed
    }

    public static class Utilities
    {
        public static string ToTitleCase(this string value) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower()).Replace("'", "'");
        public static void SetActive(this Transform transform, bool enable) => transform.gameObject.SetActive(enable);
        public static void SetActiveAllChildren(this Transform gameObject, bool enable)
        {
            foreach (Transform child in gameObject.transform) child.gameObject.SetActive(enable);
        }
        public static bool IsNull<T>(this IList<T> list) => list == null ? true : false;
        public static bool IsEmpty<T>(this IList<T> list) => !list.IsNull() && list.Count <= 0 ? true : false;
        public static bool IsNullOrEmpty<T>(this IList<T> list) => list.IsNull() || list.IsEmpty() ? true : false;
        public static bool IsNullOrEmpty(this string input) => input == null || input == "" || input.Length < 1 ? true : false;
        public static string[] SplitIntoArray(this string inputString, char separator)
        {
            List<string> result = inputString.Split(separator).ToList();
            result.RemoveAll(i => i == "");

            return result.ToArray();
        }
        public static string RemoveDuplicatedChars(this string input, params char[] characters)
        {
            string pattern = "";
            string output = input;

            foreach (char character in characters)
            {
                if (character == '\\') pattern = @"[\\]+";
                else pattern = $"[{character}]+";
                output = Regex.Replace(output, pattern, $"{character}");
            }
            return output;
        }
        public static string RemoveAll(this string inputString, string removedString) => inputString.Replace(removedString, "");
        public static string GetWordAtIndex(this string input, int index)
        {
            var words = input.Split(' ');
            int characterCount = 0;
            for (int wordIndex = 0; wordIndex < words.Length; wordIndex++)
            {
                var word = words[wordIndex];
                if (characterCount + word.Length + wordIndex >= index) return word;
                characterCount += word.Length;
            }
            return "";
        }
        public static string[] GetWordsStartWith(this string[] array, string target, bool ignoreCase = false)
        {
            if (ignoreCase) return array.Where(i => i.StartsWith(target, StringComparison.CurrentCultureIgnoreCase)).ToArray();
            else return array.Where(i => i.StartsWith(target)).ToArray();
        }
        public static string[] GetWordsContain(this string[] array, string target, bool ignoreCase = false)
        {
            if (ignoreCase) return array.Where(i => i.Contains(target, StringComparison.CurrentCultureIgnoreCase)).ToArray();
            else return array.Where(i => i.Contains(target)).ToArray();
        }
        public static string[] FuzzySearch(this string[] array, string target, int maxDistance = 2)
        {
            return array.Where(i => LevenshteinDistance(i, target) <= maxDistance).ToArray();

            int LevenshteinDistance(string s, string t)
            {
                if (s == t) return 0;
                if (s.Length == 0) return t.Length;
                if (t.Length == 0) return s.Length;

                int[,] distance = new int[s.Length + 1, t.Length + 1];

                for (int i = 0; i <= s.Length; i++) distance[i, 0] = i;

                for (int j = 0; j <= t.Length; j++) distance[0, j] = j;

                for (int i = 1; i <= s.Length; i++)
                {
                    for (int j = 1; j <= t.Length; j++)
                    {
                        int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                        distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                    }
                }

                return distance[s.Length, t.Length];
            }
        }
        public static void DestroyAllChildren(this GameObject gameObject)
        {
            foreach (var child in gameObject.transform.Cast<Transform>())
            {
                UnityEngine.Object.Destroy(child.gameObject);
            }
        }
        public static void MoveChildToLast(this Transform transform, int index) => transform.GetChild(index).SetAsLastSibling();
    }
}