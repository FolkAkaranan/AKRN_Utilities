using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AKRN_Utilities
{
    public class OnScreenConsole : MonoBehaviour
    {
        public Text consoleText;
        private Queue<string> logQueue = new Queue<string>();
        private const int maxLogs = 50;
        private static OnScreenConsole instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            string newLog = type == LogType.Error || type == LogType.Exception
                ? $"{type}: {logString}\n{stackTrace}"
                : $"{type}: {logString}";

            if (logQueue.Count >= maxLogs)
            {
                logQueue.Dequeue();
            }

            logQueue.Enqueue(newLog);

            if (consoleText != null)
            {
                consoleText.text = string.Join("\n", logQueue.ToArray());
            }
        }

        public void CopyConsoleToClipboard()
        {
            if (consoleText == null || logQueue.Count == 0)
            {
                Debug.LogWarning("No logs available to copy.");
                return;
            }

            GUIUtility.systemCopyBuffer = string.Join("\n", logQueue.ToArray());
            Debug.Log("Console text copied to clipboard.");
        }
    }
}
