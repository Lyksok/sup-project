using UnityEngine;

namespace Managers.DataSets
{
    public class DataCharacter
    {
        public GameObject CharacterPrefab;
        public int Id;

        public DataCharacter(int id, GameObject prefab)
        {
            CharacterPrefab = prefab;
            Id = id;
        }
    }
}
