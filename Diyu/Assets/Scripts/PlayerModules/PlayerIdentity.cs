using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdentity : Entity
{
    // Implement abstract methods from Entity
    public override void Die()
    {
        Debug.Log($"Player {Team} died");
    }
}
