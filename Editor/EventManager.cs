using System;
using System.Collections.Generic;
using UnityEngine;

namespace AKRN_Utilities
{
    public class EventManager : MonoBehaviourSingleton<EventManager>
    {
        private Dictionary<string, Delegate> eventDictionary = new Dictionary<string, Delegate>();

        private static bool IsInstanceAvailable()
        {
            return Instance != null;
        }

        // Start Listening for Event
        public static void StartListening(string eventName, Action listener)
        {
            if (!IsInstanceAvailable()) return;

            if (Instance.eventDictionary.TryGetValue(eventName, out Delegate thisEvent))
            {
                thisEvent = Delegate.Combine(thisEvent, listener);
                Instance.eventDictionary[eventName] = thisEvent;
            }
            else
            {
                Instance.eventDictionary.Add(eventName, listener);
            }
        }

        public static void StartListening<T>(string eventName, Action<T> listener)
        {
            if (!IsInstanceAvailable()) return;

            if (Instance.eventDictionary.TryGetValue(eventName, out Delegate thisEvent))
            {
                thisEvent = Delegate.Combine(thisEvent, listener);
                Instance.eventDictionary[eventName] = thisEvent;
            }
            else
            {
                Instance.eventDictionary.Add(eventName, listener);
            }
        }

        // Stop Listening for Event
        public static void StopListening(string eventName, Action listener)
        {
            if (!IsInstanceAvailable()) return;

            if (Instance.eventDictionary.TryGetValue(eventName, out Delegate thisEvent))
            {
                thisEvent = Delegate.Remove(thisEvent, listener);
                if (thisEvent == null)
                {
                    Instance.eventDictionary.Remove(eventName);
                }
                else
                {
                    Instance.eventDictionary[eventName] = thisEvent;
                }
            }
        }

        public static void StopListening<T>(string eventName, Action<T> listener)
        {
            if (!IsInstanceAvailable()) return;

            if (Instance.eventDictionary.TryGetValue(eventName, out Delegate thisEvent))
            {
                thisEvent = Delegate.Remove(thisEvent, listener);
                if (thisEvent == null)
                {
                    Instance.eventDictionary.Remove(eventName);
                }
                else
                {
                    Instance.eventDictionary[eventName] = thisEvent;
                }
            }
        }

        // Trigger Event without parameter
        public static void TriggerEvent(string eventName)
        {
            if (!IsInstanceAvailable()) return;

            if (Instance.eventDictionary.TryGetValue(eventName, out Delegate thisEvent))
            {
                thisEvent?.DynamicInvoke();
            }
        }

        // Trigger Event with a parameter
        public static void TriggerEvent<T>(string eventName, T arg)
        {
            if (!IsInstanceAvailable()) return;

            if (Instance.eventDictionary.TryGetValue(eventName, out Delegate thisEvent))
            {
                thisEvent?.DynamicInvoke(arg);
            }
        }
    }
}
