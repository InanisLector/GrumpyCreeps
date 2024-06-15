using UnityEngine;

namespace GameFlow.GameManager
{
    public class GameManager : MonoBehaviour
    {
        #region Data

        [HideInInspector] public static GameManager Instance
            => _instance;
        private static GameManager _instance;

        public int DefenderMoney
            => _defenderMoney;
        private int _defenderMoney = 0;
        public int AttackerMoney
            => _attackerMoney;
        private int _attackerMoney = 0;

        #endregion

        #region Unity methods

        private void Awake()
        {
            SetInstance();
        }

        #endregion

        #region Private Implementations

        private void SetInstance()
        {
            if (_instance != null)
            {
                Debug.LogError("Another instance of GameManager already exists!", this);
                return;
            }

            _instance = this;
        }

        #endregion
    }
}