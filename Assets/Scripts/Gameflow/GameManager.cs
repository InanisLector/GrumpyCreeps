using ScriptableObjects.GameState;
using UnityEngine;

namespace GameFlow
{
    public class GameManager
    {
        private PlayerManager _defender;
        public PlayerManager Defender
            => _defender;

        private PlayerManager _attacker;
        public PlayerManager Attacker
            => _attacker;

        public GameManager(GameState gameState)
        {
            _defender = new(gameState.DefenderMoney);
            _attacker = new(gameState.AttackerMoney);
        } 
    }
}