using Src.Scripts.Player;
using UnityEngine;

namespace Src.Scripts.Items
{
    public class BulletPack : Item
    {
        protected override void Pickup()
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerShooting>().AddAmmo(30);
        }
    }
}