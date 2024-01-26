public abstract class Entity
{
    /*
    An entity is a game object that has some 
    attributs that are common to all game objects
    It manages the health and team of the game object
    while storing the type of the game object
    */

    
    private int _health;
    public int Health { get; private set; }

    private string _type;
    public string Type { get; private set; }

    // Class methods to set entity health and team
    public void SetHealth(int health)
    {
        _health = health;
    }

    public abstract void Die();

}