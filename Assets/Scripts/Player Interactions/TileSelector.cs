using UnityEngine;
using HexGridSystem;
using Zenject;
using ScriptableObjects.Building;

namespace PlayerInteractions.TileSelector
{
    public class TileSelector : MonoBehaviour
    {
        [SerializeField] private LayerMask gridLM;
        [SerializeField] private Camera playerCamera;

        private PlayerControls _inputActions;
        private IHexGrid _hexGrid;

        private Vector3Int? _currentTile;
        private int _selectionDiameter = 0;

        private void Update()
        {
            SelectTile();
        }

        [Inject]
        private void Construct(PlayerControls inputAction, IHexGrid hexManager)
        {
            _inputActions = inputAction;
            _hexGrid = hexManager;
        }


        private void SelectTile()
        {
            var previousFrameTile = _currentTile;
            _currentTile = GetCurrentTile();

            if (previousFrameTile != _currentTile)
            {
                if (previousFrameTile != null)
                    _hexGrid.DeselectTilesInDiameter(previousFrameTile.Value, _selectionDiameter);

                if (_currentTile != null)
                    _hexGrid.SelectTilesInDiameter(_currentTile.Value, _selectionDiameter);
            }
        }

        private Vector3Int? GetCurrentTile()
        {
            var plane = new Plane(Vector3.up, new Vector3(0, 0.2f, 0));
            var ray = playerCamera.ScreenPointToRay(_inputActions.Defender.MousePosition.ReadValue<Vector2>());


            if (plane.Raycast(ray, out float distance))
            {
                var hitPoint = ray.GetPoint(distance);

                return _hexGrid.WorldPositionToTile(hitPoint);
            }


            return null;
        }

        public void SetDiameter(BuildingBase building)
        {
            if (_currentTile != null)
                _hexGrid.DeselectTilesInDiameter(_currentTile.Value, _selectionDiameter);

            if (building == null)
            {
                _selectionDiameter = 0;

                return;
            }

            _selectionDiameter = building.Size;

            if(_currentTile != null)
                _hexGrid.SelectTilesInDiameter(_currentTile.Value, _selectionDiameter);
        }
    }
}
