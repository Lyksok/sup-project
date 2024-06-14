using System;
using Entities;
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class InfoTextManager : NetworkBehaviour
    {
        public InfoText infoText;
        public DescribableObject describableObject;

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.LogError("uwu");
                infoText.inUse = true;
                var transform1 = infoText.transform;
                transform1.position = gameObject.transform.position;
                var transformPosition = transform1.position;
                transformPosition.y += 100;
                infoText.displayName.text = describableObject.displayName;
                infoText.displayDesc.text = describableObject.displayDesc;
            }
        }

        public void OnMouseOver()
        {
            Debug.LogError("uwu");
            infoText.inUse = true;
            var transform1 = infoText.transform;
            transform1.position = gameObject.transform.position;
            var transformPosition = transform1.position;
            transformPosition.y += 100;
            infoText.displayName.text = describableObject.displayName;
            infoText.displayDesc.text = describableObject.displayDesc;
        }

        public void OnMouseExit()
        {
            infoText.inUse = false;
        }
    }
}