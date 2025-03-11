using UnityEngine;

namespace GameFlow.GameManager
{
    public class GameManager
    {
        #region Data

        public int DefenderMoney
            => _defenderMoney;
        private int _defenderMoney = 0;
        public int AttackerMoney
            => _attackerMoney;
        private int _attackerMoney = 0;

        #endregion
    }
}