/*
using Mirror;
using UnityEngine;

namespace Weapons
{
    public class Fireball : Shooter
    {
        public override string Name => "fireball";
        protected override float Damage => 1f;

        protected override float Cooldown => 0.5f;
        
        [Command]
        public override void CmdAttack(Transform source)
        {
            GameObject newProjectile = Instantiate(projectile, source.position, source.rotation);
            NetworkServer.Spawn(newProjectile);
            RpcAttack(source);
        }
        
        [ClientRpc]
        public override void RpcAttack(Transform source)
        {
            
        }


        public override int? Ammo => null;
        protected override float BulletSpeed => 50f;
        protected override float BulletLifeCycle => 1.5f;
        public override void OnTriggerEnter(Collider other)
        {
            Rigidbody rb = other.attachedRigidbody;
            if (other.gameObject.CompareTag("Walls"))
            {
                //ParticleSystem particleSystem = Instantiate(ded, transform.position, transform.rotation);
                Destroy(gameObject);
            }

            Life life = other.gameObject.GetComponent<Life>();
            if (life!=null)
            {
                life.ChangeHP(-Damage);
                Destroy(gameObject);
            }

            if (!rb && !life)
                return;
            
            Destroy(gameObject);
        }
    }
}
*/
