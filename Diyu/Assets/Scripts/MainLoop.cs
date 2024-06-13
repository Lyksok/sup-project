using System;
using Buffs;
using Mirror;
using UnityEngine;

public class MainLoop : NetworkBehaviour
{
        public NewPlayer[] players = { }; //Contains the current players
        public float roundTime = 0; //Timer for the current round, a 5min every player starts burning, scaling with distance to spawn
        public int[] score = {0,0,0,0}; //Score of each player, 1 round win = 1 point, 3? points = win
        public int curRound = 0; //current round indicator
        public bool activeRound = false; //True if a round is currently going on

        private void Start()
        {
                curRound = 0;
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
        }
}