using ScriptableObjects.Building;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerInteractions.DefenderInteractions
{
    public class BuildingForConstrtuctionSelector : MonoBehaviour
    {
        public UnityEvent<BuildingScriptableObject?> OnBuildingForConstructionSelectionToggle;

        private BuildingScriptableObject? _currentBuilding;

        /// <summary>
        /// Select building for building
        /// </summary>
        public void SelectBuilding(BuildingScriptableObject building)
        {
            if (building == _currentBuilding)
                _currentBuilding = null;
            else
                _currentBuilding = building;

            OnBuildingForConstructionSelectionToggle.Invoke(_currentBuilding);
        }
    }
}
