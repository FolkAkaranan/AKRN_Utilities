using System;
using System.Collections.Generic;
using UnityEngine;

namespace AKRN_Utilities
{
    public class EventManager : MonoBehaviourSingleton<EventManager>
    {
        private Dictionary<string, Delegate> eventDictionary = new Dictionary<string, Delegate>();

        public static void StartListening(string eventName, Action listener)
        {
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

        public static void StopListening(string eventName, Action listener)
        {
            if (Instance == null) return;

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
            if (Instance == null) return;

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

        public static void TriggerEvent(string eventName)
        {
            if (Instance.eventDictionary.TryGetValue(eventName, out Delegate thisEvent))
            {
                thisEvent?.DynamicInvoke();
            }
        }

        public static void TriggerEvent<T>(string eventName, T arg)
        {
            if (Instance.eventDictionary.TryGetValue(eventName, out Delegate thisEvent))
            {
                thisEvent?.DynamicInvoke(arg);
            }
        }
    }
}
