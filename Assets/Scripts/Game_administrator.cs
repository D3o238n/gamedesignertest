using Sych_scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Test
{
    public class Game_administrator : MonoBehaviour
    {

        internal UnityEvent Win_game_event = new UnityEvent();

        internal UnityEvent Lose_game_event = new UnityEvent();

        internal UnityEvent<bool> Player_control_event = new UnityEvent<bool>();



        internal static Game_administrator Singleton;

        private void Awake()
        {

            Game_Player.Cursor_player(false);

            if (Singleton != null && Singleton != this)
            {
                Destroy(this);
            }
            else
            {
                Singleton = this;
            }
        }


        public void Win_game()
        {
            Win_game_event.Invoke();
            Player_control_event.Invoke(false);
        }

        public void Lose_game()
        {
            Lose_game_event.Invoke();
            Player_control_event.Invoke(false);
        }

    }
}