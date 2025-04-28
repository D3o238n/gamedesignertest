using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Dead_zone : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<ThirdPersonController>())
            {
                Game_administrator.Singleton.Lose_game();
            }
        }

    }
}