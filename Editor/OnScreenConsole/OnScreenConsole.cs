using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AKRN_Utilities
{
    public class OnScreenConsole : MonoBehaviour
    {
        public Text consoleText;
        public int maxLines = 50;

        private Queue<string> logQueue = new Queue<string>();

        int i = 0;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(i);
                i++;
            }
        }

        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType type)
        {
            string newLog = $"{type}: {logString}";
            logQueue.Enqueue(newLog);
            if (logQueue.Count > maxLines)
            {
                logQueue.Dequeue();
            }
            consoleText.text = string.Join("\n", logQueue.ToArray());
        }
    }
}