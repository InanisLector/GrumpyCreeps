using ScriptableObjects.Building;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerInteractions.DefenderInteractions.BuildingSelector
{
    public class BuildingSelector : MonoBehaviour
    {
        public UnityEvent<BuildingBase?> OnBuildingSelectionToggle;

        private BuildingBase? _currentBuilding;

        public void SelectBuilding(BuildingBase building)
        {
            if (building == _currentBuilding)
                _currentBuilding = null;
            else
                _currentBuilding = building;

            OnBuildingSelectionToggle.Invoke(_currentBuilding);
        }
    }
}
