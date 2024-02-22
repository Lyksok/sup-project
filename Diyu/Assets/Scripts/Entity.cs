public class Entity
{
    /*
    An entity is a game object that has some 
    attributs that are common to all game objects
    It manages the health and team of the game object
    while storing the type of the game object
    */

    public Entity(TypesEnum type, int health)
    {
        Type = type;
        Health = health;
        MaxHealth = health;
    }

    public int Health { get; set; }

    public int MaxHealth { get; set; }

    public TypesEnum Type { get; }

}