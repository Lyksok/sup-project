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
            /*if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.LogError("uwu1");
                infoText.inUse = true;
                infoText.displayName.text = describableObject.displayName;
                infoText.displayDesc.text = describableObject.displayDesc;
            }
            else
            {
                Debug.LogError("uwu3");
                infoText.inUse = false;
            }*/
        }

        public void OnMouseEnter()
        {
            //Debug.LogError("uwu1");
            infoText.inUse = true;
            infoText.displayName.text = describableObject.displayName;
            infoText.displayDesc.text = describableObject.displayDesc;
        }

        public void OnMouseExit()
        {
            //Debug.LogError("uwu3");
            infoText.inUse = false;
        }
    }
}