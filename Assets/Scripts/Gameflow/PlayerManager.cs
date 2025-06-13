using ScriptableObjects.GameState;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameFlow
{
    public class PlayerManager
    {
        private int _playerMoney = 0;
        public int PlayerMoney
            => _playerMoney;

        public UnityEvent<int> OnMoneyChange { get; private set; }

        public PlayerManager(int startingMoney)
        {
            OnMoneyChange = new();
            _playerMoney = startingMoney;
        }

        /// <summary>
        /// If player has enough money - spends it
        /// </summary>
        /// <returns> true if successful and false if not </returns>
        public bool TrySpendMoney(int amount)
        {
            if (amount > _playerMoney)
                return false;

            _playerMoney -= amount;
            OnMoneyChange.Invoke(_playerMoney);

            return true;
        }

        public void AddMoney(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Cannot add negative amount. Use TrySpendMoney(int) instead");
                return;
            }

            _playerMoney += amount;
            OnMoneyChange.Invoke(_playerMoney);
        }
    }
}