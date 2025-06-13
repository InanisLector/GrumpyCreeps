using GameFlow;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.GeneralDefendersUI
{
    public class GeneralDefendersUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmp;
        private string _text;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            _text = tmp.text;
            gameManager.Defender.OnMoneyChange.AddListener(ChangeText);
            ChangeText(gameManager.Defender.PlayerMoney);
        }

        private void ChangeText(int amount)
            => tmp.text = _text + amount;
    }
}