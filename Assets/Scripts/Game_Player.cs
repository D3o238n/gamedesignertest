//Статический скрипт для функционала игрока
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sych_scripts
{
    public static class Game_Player
    {

        /// <summary>
        /// Активация или выключение курсора игрока
        /// </summary>
        /// <param name="_active"></param>
        public static void Cursor_player(bool _active)
        {
            if (_active)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;

            Cursor.visible = _active;
        }

        public static RaycastHit Raycast_from_camera(Camera _cam, LayerMask _layer)
        {
            

            RaycastHit hit;
            Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out hit, 9999, _layer))
            {


            }

            return hit;
        }

    }
}