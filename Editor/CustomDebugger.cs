using UnityEngine;

namespace AKRN_Utilities
{
    public static class CustomDebugger
    {
        public enum LogImportance
        {
            Important,
            Normal,
            Test,
            Default
        }

        public static LogImportance Important => LogImportance.Important;
        public static LogImportance Normal => LogImportance.Normal;
        public static LogImportance Test => LogImportance.Test;

        public static void Log(string message, LogImportance importance = LogImportance.Default)
        {
            string color = GetColor(importance);
            Debug.Log($"<color={color}>{message}</color>");
        }

        private static string GetColor(LogImportance importance)
        {
            switch (importance)
            {
                case LogImportance.Important:
                    return "red";
                case LogImportance.Normal:
                    return "yellow";
                case LogImportance.Test:
                    return "green";
                default:
                    return "white";
            }
        }
    }
}