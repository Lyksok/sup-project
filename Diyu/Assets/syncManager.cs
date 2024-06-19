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
                go = Instantiate(resourceManager.lootList[0],pos,Quaternion.identity);
                go.GetComponent<AbilityOrb>()._rarity = rarities;
                go.GetComponent<AbilityOrb>().UpdateInfo();
                break;
            default:
                go = Instantiate(resourceManager.lootList[2],pos,Quaternion.identity);
                go.GetComponent<GemOrb>()._rarity = rarities;
                go.GetComponent<GemOrb>().UpdateInfo();
                break;
        }
            
    }

    [Command(requiresAuthority = false)]
    public void CmdSpawnFireball(int type, Vector3 pos,float damage,Vector3 orientation)
    {
        SpawnFireballRpc(type, pos, damage,orientation);
            
    }
    
    [Command(requiresAuthority = false)]
    public void CmdSpawnAoe(int type, Vector3 pos)
    {
        SpawnAoeRpc(type, pos);
            
    }
    
    [ClientRpc]
    public void SpawnAoeRpc(int type, Vector3 pos)
    {
        GameObject newFireball;
        switch (type)
        {
            case 1:
                newFireball = Object.Instantiate(resourceManager.projectileList[type], pos, Quaternion.identity);
                break;
            case 7:
                newFireball = Object.Instantiate(resourceManager.projectileList[type], pos, Quaternion.identity);
                break;
            case 8:
                newFireball = Object.Instantiate(resourceManager.projectileList[type], pos, Quaternion.identity);
                break;
            case 9:
                newFireball = Object.Instantiate(resourceManager.projectileList[type], pos, Quaternion.identity);
                break;
            default:
                newFireball = Object.Instantiate(resourceManager.projectileList[1], pos, Quaternion.identity);
                break;
        }
            
    }
    
    [ClientRpc]
    public void SpawnFireballRpc(int type, Vector3 pos,float damage,Vector3 orientation)
    {
        GameObject newFireball;
        switch (type)
        {
            case 0:
                newFireball = Object.Instantiate(resourceManager.projectileList[type], pos, Quaternion.identity);
                newFireball.GetComponent<Fireball>().damage = damage;
                break;
            case 3:
                newFireball = Object.Instantiate(resourceManager.projectileList[type], pos, Quaternion.identity);
                newFireball.GetComponent<Energyball>().damage = damage;
                break;
            case 4:
                newFireball = Object.Instantiate(resourceManager.projectileList[type], pos, Quaternion.identity);
                newFireball.GetComponent<Elementball>().damage = damage;
                break;
            case 5:
                newFireball = Object.Instantiate(resourceManager.projectileList[type], pos, Quaternion.identity);
                newFireball.GetComponent<Arrow>().damage = damage;
                break;
            case 6:
                newFireball = Object.Instantiate(resourceManager.projectileList[type], pos, Quaternion.identity);
                newFireball.GetComponent<ThrownDagger>().damage = damage;
                break;
            default:
                newFireball = Object.Instantiate(resourceManager.projectileList[type], pos, Quaternion.identity);
                newFireball.GetComponent<Fireball>().damage = damage;
                break;
                
        }
        Rigidbody rb = newFireball.GetComponent<Rigidbody>();
        rb.AddForce(30 * orientation, ForceMode.VelocityChange);
            
    }
}
