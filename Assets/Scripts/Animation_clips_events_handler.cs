//—крипт обрабатывающий эвенты в аниматоре (работает от эвенетов просвоеные в клипах)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sych_scripts
{
    [AddComponentMenu("Sych scripts / Game / Handlers / Animation clips events handler")]
    [DisallowMultipleComponent]
    public class Animation_clips_events_handler : MonoBehaviour
    {
        [Tooltip("Ёвенты дл€ аниматора")]
        [SerializeField]
        Event_class[] Animator_event_array = new Event_class[0];

        /// <summary>
        /// јктивировать необходимый эвент по имени
        /// </summary>
        /// <param name="_event_name">»м€ эвента</param>
        public void Activation_animation_event(string _event_name)
        {
            bool result = false;

            if (Animator_event_array.Length > 0)
            {
                for (int x = 0; x < Animator_event_array.Length; x++)
                {
                    if (Animator_event_array[x].Event_name == _event_name)
                    {
                        Animator_event_array[x].Animation_event.Invoke();
                        result = true;
                        break;
                    }
                }

                if (!result)
                {
                    Debug.LogError("Ёвент с именем  " + _event_name + "  не существует!");
                }
            }
            else
            {
                Debug.LogError("Ќету эвентов от слова совсем!");
            }
        }

    }

    [System.Serializable]
    class Event_class
    {
        [Tooltip("Ёвент дл€ аниматора")]
        public UnityEvent Animation_event = new UnityEvent();

        public string Event_name = "»м€ эвента";
    }
}