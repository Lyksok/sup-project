using System;
using Mirror;
using TMPro;
using UnityEngine;

namespace Entities
{
    public class InfoText : NetworkBehaviour
    {
        public bool inUse;
        public TextMeshProUGUI displayName;
        public TextMeshProUGUI displayDesc;
        
        private void Start()
        {
            inUse = false;
        }

        private void Update()
        {
            if (inUse)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}