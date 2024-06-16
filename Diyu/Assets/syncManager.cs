using System;
using System.Collections;
using System.Collections.Generic;
using Abilities;
using Entities;
using Mirror;
using UnityEngine;
using Weapons;
using Object = UnityEngine.Object;
using ResourceManager = Managers.ResourceManager;

public class syncManager : NetworkBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public ResourceManager resourceManager;
    
    [Command(requiresAuthority = false)]
    public void CmdSpawnLoot(Rarities rarities, Vector3 pos,int index)
    {
        SpawnLootRpc(rarities, pos, index);
    }

    [ClientRpc]
    public void SpawnLootRpc(Rarities rarities, Vector3 pos,int index)
    {
        GameObject go;
        switch (index)
        {
            case 0:
                go = Instantiate(resourceManager.lootList[0],pos,Quaternion.identity);
                go.GetComponent<AbilityOrb>()._rarity = rarities;
                go.GetComponent<AbilityOrb>().UpdateInfo();
                break;
            case 1:
                go = Instantiate(resourceManager.lootList[1],pos,Quaternion.identity);
                go.GetComponent<WeaponOrb>()._rarity = rarities;
                go.GetComponent<WeaponOrb>().UpdateInfo();
                break;
            default:
                go = Instantiate(resourceManager.lootList[2],pos,Quaternion.identity);
                go.GetComponent<GemOrb>()._rarity = rarities;
                go.GetComponent<GemOrb>().UpdateInfo();
                break;
        }
            
    }

    [Command(requiresAuthority = false)]
    public void CmdSpawnFireball(GameObject prefab, Vector3 pos,float damage,Vector3 orientation)
    {
        GameObject newFireball = Object.Instantiate(prefab, pos, Quaternion.identity);
        newFireball.GetComponent<Fireball>().damage = damage;
        //newFireball.GetComponent<Fireball>().attacker = User;
        Rigidbody rb = newFireball.GetComponent<Rigidbody>();

        rb.AddForce(30 * orientation, ForceMode.VelocityChange);
        //_firelaunch.transform.position = position;
        //_firelaunch.Play();
        NetworkServer.Spawn(newFireball);
            
    }
}
