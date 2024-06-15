using DG.Tweening;
using UnityEngine;

namespace HexGridSystem
{
    public class HexTile : MonoBehaviour, IHexTile
    {
        [SerializeField] private float timeToMove;
        [SerializeField] private Vector3 offset;
        [SerializeField] private bool immovable;

        private Vector3 _startPosition;
        private Vector3 _endPosition;

        [HideInInspector]
        public bool IsFree
            => true;

        private IHexGrid _manager;
        private Vector2Int _gridPosition;

        private void Awake()
        {
            _manager = this.gameObject.GetComponentInParent<HexManager>();
            _startPosition = this.transform.position;
            _endPosition = this.transform.position + offset;
        }

        public void SetGridPosition(int x, int y)
            => _gridPosition = new(x, y);

        public void Select()
        {
            if(IsFree && !immovable)
                transform.DOMove(_endPosition, timeToMove);
        }

        public void Deselect()
        {
            if(!immovable)
                transform.DOMove(_startPosition, timeToMove);
        }
    }
}