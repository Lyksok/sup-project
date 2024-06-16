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
        public GameObject background;
        
        private void Start()
        {
            inUse = false;
        }

        public void Update()
        {
            if (inUse)
            {
                background.SetActive(true);
            }
            else
            {
                background.SetActive(false);
                displayDesc.text = "";
                displayName.text = "";
            }
        }
    }
}