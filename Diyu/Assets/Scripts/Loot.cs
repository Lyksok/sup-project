using System;
using Managers;
using Mirror;
using TMPro;
using UnityEngine;

namespace Entities
{
    public abstract class Loot : NetworkBehaviour
    {
        protected float cooldown = 0f;
        public TextMeshProUGUI text;
        public abstract void OnPickup(NewPlayer player);

        public void Collected()
        {
            Destroy(gameObject);
        }


        public abstract void UpdateInfo();
        
        public void Update()
        {
            cooldown += Time.deltaTime;
            if (cooldown > 10)
            {
                Collected();
            }
        }
        
        public void OnTriggerEnter(Collider other)
        {
            NewPlayer player = other.gameObject.GetComponentInParent<NewPlayer>();
            //Debug.LogError(player != null);
            if (player == null)
            {
                return;
            }
            cooldown = 3;
            OnPickup(player);
        }
    }
}