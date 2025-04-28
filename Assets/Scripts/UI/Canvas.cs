using Sych_scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Canvas : MonoBehaviour
    {
        [SerializeField]
        GameObject ESC_menu = null;

        bool End_game_bool = false;

        private void Start()
        {
            Game_administrator.Singleton.Lose_game_event.AddListener(End_game_listener);
            Game_administrator.Singleton.Win_game_event.AddListener(End_game_listener);
        }

        void End_game_listener()
        {
            End_game_bool = true;
        }


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape) && !End_game_bool)
            {
                Change_active_menu();
            }
        }

        public void Change_active_menu()
        {
            bool active_bool = ESC_menu.gameObject.activeSelf ? false : true;

            Change_active_menu(active_bool);
            
        }

        public void Change_active_menu(bool _change_bool)
        {
            ESC_menu.gameObject.SetActive(_change_bool);

            if (!End_game_bool)
            {
                Game_administrator.Singleton.Player_control_event.Invoke(!_change_bool);
                Game_Player.Cursor_player(_change_bool);
            }

        }
    }
}