using System;
using System.Collections.Generic;
using Buffs;
using Mirror;
using UnityEngine;

public class MainLoop : NetworkBehaviour
{
        public List<NewPlayer> players = new List<NewPlayer>(); //Contains the current players
        public float roundTime = 0; //Timer for the current round, a 5min every player starts burning, scaling with distance to spawn
        public int[] score = {0,0,0,0}; //Score of each player, 1 round win = 1 point, 3? points = win
        public int curRound = 0; //current round indicator
        public bool activeRound = false; //True if a round is currently going on

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
        }

        public void OnRoundEnd()
        {
                activeRound = false;
                foreach (var player in players)
                {
                        player.OnRevive();
                }
        }
        
        private void Update()
        {
                if (activeRound)
                {
                        roundTime += Time.deltaTime;
                }

                if (roundTime >= 30)
                {
                        foreach (var player in players)
                        {
                                player.AddBuff(new DebuffMapBurn(5,3,null,100,player));
                        }
                }
                //Debug.LogError($"{players.Count}");
        }
}