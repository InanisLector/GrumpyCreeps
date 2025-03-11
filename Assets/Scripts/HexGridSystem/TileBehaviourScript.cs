using DG.Tweening;
using UnityEngine;

namespace HexGridSystem
{
    public class TileBehaviourScript : MonoBehaviour
    {
        [SerializeField] private float timeToMove;
        [SerializeField] private Vector3 offset;

        private Vector3 _startPosition;
        private Vector3 _endPosition;

        private TileLogic _tileLogic;

        private void Awake()
        {
            _startPosition = this.transform.position;
            _endPosition = this.transform.position + offset;
        }
        
        public void SetLogicParent(TileLogic tileLogic)
        {
            if (_tileLogic != null)
            {
                Debug.LogError("Logic script has been already linked", this);
                return;
            }

            _tileLogic = tileLogic;
            _tileLogic.ToggleSelection += ToggleHover;
        }

        private void ToggleHover(bool toggle)
        {
            if (toggle)
            {
                if (!_tileLogic.IsVacant())
                    return;
                transform.DOMove(_endPosition, timeToMove);
            }
            else
            {
                transform.DOMove(_startPosition, timeToMove); 
            }
        }
    }
}
