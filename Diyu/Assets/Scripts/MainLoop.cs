using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Abilities;
using Buffs;
using Managers;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLoop : NetworkBehaviour
{
        //public MyNetworkRoomManager NetworkRoomManager;
        [SyncVar] public List<NewPlayer> players; //Contains the current players
        public float roundTime = 0; //Timer for the current round, a 5min every player starts burning, scaling with distance to spawn
        public int curRound = 0; //current round indicator
        public bool activeRound = false; //True if a round is currently going on
        private float _burnCd = 0;
        private int _burnCount = 1;
        private float _downTimer;
        public NewPlayer winner = null;
        public int lootRank = 0;
        private ResourceManager resourceManager => players[0].resources;

        [SyncVar] public bool hasGameStarted = true;

        private void Start()
        {
                DontDestroyOnLoad(transform.gameObject);
                players = FindObjectsOfType<NewPlayer>().ToList();
                Debug.LogError(players.Count);
        }

        private void Awake()
        { 
                DontDestroyOnLoad(transform.gameObject);
                players = FindObjectsOfType<NewPlayer>().ToList();
                Debug.LogError(players.Count);
        }


        private void UpdatePlayers()
        {
                players = FindObjectsOfType<NewPlayer>().ToList();
                Debug.LogError(players.Count);
        }

        public void StartGame()
        {
                hasGameStarted = true;
                players = NetworkServer.connections.Select((pair =>
                {
                        List<(NetworkIdentity identity, GameObject gameObject)> identities =
                                FindObjectsOfType<NetworkIdentity>()
                                        .Where((identity => identity.GetComponent<NewPlayer>() != null))
                                        .Select((identity => (identity, identity.gameObject))).ToList();
                        var res = identities.Where(((tuple) => tuple.identity == pair.Value.identity)).ToList();
                        if (res.Count == 0)
                                throw new Exception("Pas de client avec le bon id");

                        return res[0].gameObject.GetComponent<NewPlayer>();
                })).ToList();
        }
        
        public void OnRoundStart()
        { 
                if (curRound % 2 == 0)
                {
                        lootRank++;
                }
                curRound += 1;
                roundTime = 0;
                activeRound = true;
                foreach (var player in players)
                {
                        player.OnRoundStart();
                        player.roundNumber = curRound;
                }
        }

        public void OnRoundEnd()
        {
                activeRound = false;
                _downTimer = 0;
                foreach (var player in players)
                {
                        player.OnRevive();
                        player.debuffList = new List<Buff>();
                        player.roundTimer = -15;
                        foreach (var ability in player.abilityList)
                        {
                                ability.CurrentCooldown = ability.Cooldown;
                                if (ability.State != States.PASSIVE)
                                {
                                        ability.State = States.READY;
                                }
                        }
                }
                Debug.LogError("Manche terminée");
        }

        private void UpdateCurrentRound()
        {
                roundTime += Time.deltaTime;
                if (_burnCd > 0)
                { 
                        _burnCd -= Time.deltaTime;
                }
                if (roundTime >= 30 && _burnCd <= 0)
                {
                        _burnCount++;
                        _burnCd = 30;
                        foreach (var player in players)
                        {
                                player.AddDebuff(new DebuffMapBurn(5,3,null,100 + _burnCount,player));
                        }
                }
        }

        private void UpdateDownTime()
        {
                _downTimer += Time.deltaTime;
                if (_downTimer >= 15)
                {
                        OnRoundStart();
                }
        }

        private void OnRoundLost(NewPlayer player)
        {
                int _abilityId = RandomNumberGenerator.GetInt32(1, resourceManager.abilityCount + 1);
                Rarities _rarity = resourceManager.GetRarity(RandomNumberGenerator.GetInt32(0, 5));
                player.PickupAbility(resourceManager.GetAbility(_abilityId,_rarity,player));
                
                player.classPassive.ChangeRarity(1);
                
                int _gemId = RandomNumberGenerator.GetInt32(1, 9);
                _rarity = resourceManager.GetRarity(RandomNumberGenerator.GetInt32(0, 5));
                player.PickupGem(player.resources.GetGem(_gemId,_rarity,player));
                _gemId = RandomNumberGenerator.GetInt32(1, 9);
                _rarity = resourceManager.GetRarity(RandomNumberGenerator.GetInt32(0, 5));
                player.PickupGem(player.resources.GetGem(_gemId,_rarity,player));
                _gemId = RandomNumberGenerator.GetInt32(1, 9);
                _rarity = resourceManager.GetRarity(RandomNumberGenerator.GetInt32(0, 5));
                player.PickupGem(player.resources.GetGem(_gemId,_rarity,player));
        }

        private void OnRoundWin(NewPlayer player)
        {
                int slot = RandomNumberGenerator.GetInt32(0, 4);
                player.abilityList[slot].ChangeRarity(1);
                
                player.classPassive.ChangeRarity(1);
                
                int _gemId = RandomNumberGenerator.GetInt32(1, 9);
                Rarities _rarity = resourceManager.GetRarity(RandomNumberGenerator.GetInt32(0, 5));
                player.PickupGem(player.resources.GetGem(_gemId,_rarity,player));
                _gemId = RandomNumberGenerator.GetInt32(1, 9);
                _rarity = resourceManager.GetRarity(RandomNumberGenerator.GetInt32(0, 5));
                player.PickupGem(player.resources.GetGem(_gemId,_rarity,player));
                _gemId = RandomNumberGenerator.GetInt32(1, 9);
                _rarity = resourceManager.GetRarity(RandomNumberGenerator.GetInt32(0, 5));
                player.PickupGem(player.resources.GetGem(_gemId,_rarity,player));
        }
        
        private void CheckEndRound()
        {
                int deadCount = 0;
                foreach (var player in players)
                {
                        if (player.isSpectator)
                        {
                                deadCount++;
                        }
                }

                if (deadCount >= 1)
                {
                        foreach (var player in players)
                        {
                                if (player.isSpectator)
                                {
                                        OnRoundLost(player);
                                }
                                else
                                {
                                        winner = player;
                                        player.score++;
                                        if (player.score >= 3)
                                        {
                                                OnGameWin(player);
                                        }
                                        OnRoundWin(player);
                                }
                        }
                        OnRoundEnd();
                        foreach (var player in players)
                        {
                                player.victor = winner._name;
                        }
                }
        }

        private void OnGameWin(NewPlayer player)
        {
                // transition scene fin
        }
        
        private void Update()
        {
                
                if (SceneManager.GetActiveScene().name != "MapScene")
                        return;
                //Debug.LogError(players.Count);
                if (activeRound)
                {
                        UpdateCurrentRound(); 
                        CheckEndRound();
                }
                else
                {
                        UpdateDownTime();
                }
                players = FindObjectsOfType<NewPlayer>().ToList();
                foreach (var player in players)
                {
                        Debug.LogError(player._name);
                }

                if (players.Count == 0)
                {
                        DontDestroyOnLoad(transform.gameObject);
                        players = FindObjectsOfType<NewPlayer>().ToList();
                        Debug.LogError(players.Count);
                }
        }
}