using Sych_scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class UI_lose : MonoBehaviour
    {

        [SerializeField]
        GameObject Main = null;

        void Start()
        {
            Game_administrator.Singleton.Lose_game_event.AddListener(Activation);
        }

        void Activation()
        {
            Main.SetActive(true);
            Game_Player.Cursor_player(true);
        }
    }
}