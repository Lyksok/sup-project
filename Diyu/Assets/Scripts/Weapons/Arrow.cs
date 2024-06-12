using Entities;
using UnityEngine;

namespace Weapons
{
    public class Arrow : Entity
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
            //Debug.LogError(target != null);
            if (target != null)
            {
                target.CmdTakeDamage(damage,DamageType.MAGICAL);
                OnDeath();
            }
            //if (!rb && !target)
            //return;
        }
    }
}