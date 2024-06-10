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

using Entities;
using UnityEngine;

namespace Weapons
{
    public class Fireball : Entity
    {
        [SerializeField]
        private float lifespan = 0.0f;

        [SerializeField]
        private float limit = 1.5f;

        [SerializeField]
        public float damage { get; set; }

        [SerializeField]
        public ParticleSystem ded;

        void Start()
        {

        }

        void Update()
        {
            lifespan += Time.deltaTime;
            if (lifespan >= limit)
            {
                //ParticleSystem particleSystem = Instantiate(ded, transform.position, transform.rotation);
                OnDeath();
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            Debug.LogError(collider.gameObject.name);
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb)
            {
                //rien
            }

            if (collider.gameObject.CompareTag("Walls"))
            {
                //ParticleSystem particleSystem = Instantiate(ded, transform.position, transform.rotation);
                OnDeath();
            }

            Entity target = collider.gameObject.GetComponentInParent<Entity>();
            Debug.LogError(target != null);
            if (target != null)
            {
                target.CmdTakeDamage(damage,DamageType.MAGICAL);
                OnDeath();
            }
            //if (!rb && !target)
                //return;
            OnDeath();
        }
    }
}