using System;
using Managers;
using Mirror;
using TMPro;
using UnityEngine;

namespace Entities
{
    public abstract class Loot : NetworkBehaviour
    {
        protected float cooldown = 0;
        public TextMeshProUGUI text;
        public abstract void OnPickup(NewPlayer player);

        public void Collected()
        {
            Destroy(gameObject);
        }


        public abstract void UpdateInfo();
        
        public void Update()
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
        }
        
        public void OnCollisionEnter(Collision other)
        {
            NewPlayer player = other.gameObject.GetComponentInParent<NewPlayer>();
            //Debug.LogError(player != null);
            if (player == null || cooldown > 0)
            {
                return;
            }
            cooldown = 3;
            OnPickup(player);
        }
    }
}