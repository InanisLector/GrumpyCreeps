using UnityEngine;
using HexGridSystem;
using Zenject;
using ScriptableObjects.Building;

namespace PlayerInteractions
{
    public class TileSelector : MonoBehaviour
    {
        [SerializeField] private LayerMask gridLM;
        [SerializeField] private Camera playerCamera;

        private PlayerControls _inputActions;
        private IHexGrid _hexGrid;

        private Vector2Int? _currentTile;
        private int _selectionDiameter = 0;

        public Vector2Int CurrentTile { get
            {
                if (_currentTile != null)
                    return _currentTile.Value;
                return new(0, 0);
            }
        }

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

        private Vector2Int? GetCurrentTile()
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

        public void SetDiameter(int size)
        {
            if (_currentTile != null)
                _hexGrid.DeselectTilesInDiameter(_currentTile.Value, _selectionDiameter);

            _selectionDiameter = size;

            if(_currentTile != null)
                _hexGrid.SelectTilesInDiameter(_currentTile.Value, _selectionDiameter);
        }

        // Null ref for building means it was deselected
        public void SetDiameterForBuilding(BuildingScriptableObject? building)
        {
            if (building)
                SetDiameter(building.Size);
            else
                SetDiameter(0);
        }
    }
}
