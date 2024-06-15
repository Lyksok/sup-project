using System;
using Mirror;
using UnityEngine;
using UnityEngine.Events;
namespace Entities.map
{
    public class cylindre : NetworkBehaviour
    {
        public float speed = 3;
        public UnityEvent unityEvent = new UnityEvent();
        public GameObject door_;
        public float smoothTime = 0.5f;
        public bool key = false;
        public int count = 0;
        private void OnTriggerEnter(Collider collider)
        {
            NewPlayer pb = collider.GetComponentInParent<NewPlayer>();
            if (pb == null || key)
            {
                return; 
            }
            if (pb.Keys >0)
            {
                pb.Keys--;
                key = true;
                
            }
        }

        private void Update()
        {
            if (key && count < 640)
            {
                count++;
                door_.transform.Translate(transform.up * (-1 * speed * Time.deltaTime));
            }
        }
    }
}