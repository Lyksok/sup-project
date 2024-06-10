namespace Abilities
{
    public class AbilityNone_0 : Ability
    {
        public AbilityNone_0()
        {
            Name = "No Ability";
            State = States.PASSIVE;
        }
        public override int id { get => 0; }
        public override void OnEnd(){}
        public override void PassiveEffect()
        {
            
        }

        public override void ActiveEffect()
        {
            
        }

        public override void SetupEffect()
        {
            
        }

        public override void SetRarity(Rarities rarity)
        {
            
        }
    }
}