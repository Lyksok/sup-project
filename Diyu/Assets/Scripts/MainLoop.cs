using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Abilities;
using Buffs;
using Managers;
using Mirror;
using UnityEngine;

public class MainLoop : NetworkBehaviour
{
        public List<NewPlayer> players = new List<NewPlayer>(); //Contains the current players
        public float roundTime = 0; //Timer for the current round, a 5min every player starts burning, scaling with distance to spawn
        public int curRound = 0; //current round indicator
        public bool activeRound = false; //True if a round is currently going on
        private float _burnCd = 0;
        private int _burnCount = 1;
        public ResourceManager resourceManager;
        private float _downTimer;
        public NewPlayer winner = null;
        
        private void Awake()
        { 
                DontDestroyOnLoad(transform.gameObject);
        }

        public void OnRoundStart()
        { 
                curRound += 1;
                roundTime = 0;
                activeRound = true;
                foreach (var player in players)
                {
                        player.OnRoundStart();
                }
                //transition map scene
        }

        public void OnRoundEnd()
        {
                activeRound = false;
                _downTimer = 30;
                foreach (var player in players)
                {
                        player.OnRevive();
                }
                //transition autre scene
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
                                player.AddBuff(new DebuffMapBurn(5,3,null,100 + _burnCount,player));
                        }
                }
        }

        private void UpdateDownTime()
        {
                _downTimer -= Time.deltaTime;
                if (_downTimer <= 0)
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

                if (deadCount >= 3)
                {
                        foreach (var player in players)
                        {
                                if (player.isSpectator)
                                {
                                        OnRoundLost(player);
                                }
                                else
                                {
                                        player.score++;
                                        if (player.score >= 3)
                                        {
                                                OnGameWin(player);
                                        }
                                        OnRoundWin(player);
                                }
                        }
                        OnRoundEnd();
                }
        }

        private void OnGameWin(NewPlayer player)
        {
                winner = player;
                // transistion scene fin
        }
        
        private void Update()
        {
                if (activeRound)
                {
                        UpdateCurrentRound(); 
                        CheckEndRound();
                }
                else
                {
                        UpdateDownTime();
                }
                
                
        }
}