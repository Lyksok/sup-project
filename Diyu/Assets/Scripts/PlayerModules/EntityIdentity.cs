using UnityEngine;

public class EntityIdentity : Entity
{
    // Constructor
    public EntityIdentity(TypesEnum type, int health) : base(type, health)
    {
    }

    // Check if player is dead
    public bool IsDead()
    {
        return Health <= 0;
    }

    public override void Die()
    {
        //this returns nothing as of now
        return;
    }
}
