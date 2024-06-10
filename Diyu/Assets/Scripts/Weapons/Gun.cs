/*using System;
using Mirror;
using UnityEngine;

namespace Weapons
{
    public class Gun
    {
        public GameObject hook;
        private GameObject _bulletPrefab;
        private float _diecounter;
        
        private void Start()
        {
            if (!isLocalPlayer)
                return;

            CmdInstantiateHook(GetComponent<NetworkIdentity>().netId);
        }
        
        void Update()
        {
            // Check for collisions
            _diecounter += Time.deltaTime;
        }

        private void OnTriggerEnter(Collider collider)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb)
            {
                // Nothing
            }

            if (collider.gameObject.tag == "Walls")
            {
                // ParticleSystem particleSystem = Instantiate(ded, transform.position, transform.rotation);
                Destroy(gameObject);
                return;
            }

            Life life = collider.gameObject.GetComponent<Life>();

            if (life != null) 
            {
                life.ChangeHP(10);
                Destroy(gameObject);
                return;
            }

            if (!rb && !life)
                return;

            Destroy(gameObject);
        }

        [Command]
        void CmdInstantiateHook(uint playerNetID)
        {
            hook = Instantiate(_bulletPrefab, this.transform.position, Quaternion.identity);
            NetworkServer.Spawn(hook, connectionToClient);
        }
    }
}*/