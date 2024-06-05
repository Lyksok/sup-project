using System;
using UnityEngine;

public class SightZone : MonoBehaviour
{
    //i feel like all of this is pretty explicit
    public event Action<GameObject> onEnter = null;
    public event Action<GameObject> onStay = null;
    public event Action<GameObject> onExit = null;

    private void OnTriggerEnter(Collider other)
    {
        onEnter?.Invoke(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        NewPlayer pb = other.GetComponent<NewPlayer>();
        if (pb != null)
            onStay?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        onExit?.Invoke(other.gameObject);
    }
}