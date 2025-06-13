using UnityEngine;

namespace ScriptableObjects.GameState
{
    [CreateAssetMenu(fileName = "GameState", menuName = "Game state")]
    public class GameState : ScriptableObject
    {
        [Header("Defender")]
        [SerializeField] private int defenderMoney;

        [Header("Attacker")]
        [SerializeField] private int attackerMoney;

        public int DefenderMoney =>
            defenderMoney;

        public int AttackerMoney =>
            attackerMoney;
    }
}
