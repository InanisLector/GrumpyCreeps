using UnityEngine;
using HexGridSystem;
using Zenject;
using ScriptableObjects.Building;
using GameFlow.GameManager;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

namespace PlayerInteractions.DefenderInteractions
{
    [RequireComponent(typeof(TileSelector))]
    public class Builder : MonoBehaviour
    {
        [SerializeField] private float tileHeight;

        private PlayerControls _inputActions;
        private IHexGrid _hexGrid;
        private GameManager _gameManager;
        private TileSelector _tileSelector;

        private BuildingScriptableObject _buildingScriptableObject;

        private void Start()
        {
            _tileSelector = gameObject.GetComponent<TileSelector>();

            _inputActions.Defender.AcceptSelection.performed += (InputAction.CallbackContext context) 
                => Build();
        }

        [Inject]
        private void Construct(PlayerControls inputAction, IHexGrid hexManager, GameManager gameManager)
        {
            _inputActions = inputAction;
            _hexGrid = hexManager;
            _gameManager = gameManager;
        }

        private void Build()
        {
            if (!HasSelection())
                return;

            if (!TryBuy(_buildingScriptableObject))
                return;

            if (!TryPlace(_buildingScriptableObject, _tileSelector.CurrentTile))
                return;
        }

        private bool HasSelection()
            => _buildingScriptableObject != null && _tileSelector.CurrentTile != null;

        private bool TryBuy(BuildingScriptableObject building)
        {
            if (building.Cost < _gameManager.DefenderMoney)
            {
                // Add feedback
                return false;
            }

            return true;
        }

        private bool TryPlace(BuildingScriptableObject building, Vector2Int centerTilePosition)
        {
            if (!_hexGrid.CheckForVacancyInRadius(centerTilePosition, building.Size))
                return false;

            Vector3 instPosition = _hexGrid.TileToWorldPosition(centerTilePosition);
            instPosition.y += tileHeight;

            var buildingGameObject = Instantiate(building.Prefab, instPosition, Quaternion.identity);

            _hexGrid.AssignBuildingToTilesInRadius(centerTilePosition, building.Size, buildingGameObject);

            return true;
        }

        public void AssignNewBuildingForConstruction(BuildingScriptableObject building)
            => _buildingScriptableObject = building;
    }
}