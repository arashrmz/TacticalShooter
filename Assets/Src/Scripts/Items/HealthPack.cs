using Src.Scripts.Player;
using UnityEngine;

namespace Src.Scripts.Items
{
    public class HealthPack : Item
    {
        protected override void Pickup()
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerManager>().AddHealth(30);
        }
    }
}