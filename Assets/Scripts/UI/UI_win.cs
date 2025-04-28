using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sych_scripts;

namespace Test
{
    public class UI_win : MonoBehaviour
    {

        [SerializeField]
        GameObject Main = null;

        void Start()
        {
            Game_administrator.Singleton.Win_game_event.AddListener(Activation);
        }

        void Activation()
        {
            Main.SetActive(true);
            Game_Player.Cursor_player(true);
        }
    }
}