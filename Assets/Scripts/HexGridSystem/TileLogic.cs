using ScriptableObjects.Grid;
using System;
using UnityEngine;

namespace HexGridSystem
{
    public class TileLogic
    {
        private TileState _tileState;
        private GameObject _building;

        public Action<bool> ToggleSelection;

        public void ClickedOnIt()
        {
            if (_tileState == TileState.unavailable)
                return;

            if (_tileState == TileState.unprocessed)
            {
                ToggleSelection.Invoke(true);
                // Do dropdown to purchase tile
                return;
            }

            if (_tileState == TileState.occupied)
                // Select a building
                return;
        }

        public void ProcessTile()
        {
            if (_tileState != TileState.unprocessed)
            {
                Debug.LogError("Processed unprocessable tile");
            }

            _tileState = TileState.processed;
        }

        public void StartedHoveringOnIt()
        {
            if (_tileState == TileState.processed)
            {
                ToggleSelection.Invoke(true);

                _tileState = TileState.hover;
            }
        }


        public void StoppedHoveringOnIt()
        {
            if (_tileState == TileState.hover)
            {
                ToggleSelection.Invoke(false);

                _tileState = TileState.processed;
            }
        }

        public void AddBuilding(GameObject building)
        {
            if (_tileState != TileState.occupied)
                return;

            _building = building;
            // Subscribe to building deletion event

            _tileState = TileState.occupied;
        }

        private void BuildingGetsDeleted()
        {
            if (_building == null)
            {
                Debug.LogError("Building deletiong continueity error: Tried deleting non-existent building");
                return;
            }

            _building = null;

            _tileState = TileState.processed;
        }

        public TileLogic(InitialTileState initialTileState)
        {
            switch (initialTileState)
            {
                case InitialTileState.unavailable:
                    _tileState = TileState.unavailable;
                    break;
                case InitialTileState.unprocessed:
                    _tileState = TileState.unprocessed;
                    break;
                case InitialTileState.processed:
                    _tileState = TileState.processed;
                    break;
                default:
                    Debug.LogError("unimplemented initial tile state");
                    _tileState = TileState.unavailable;
                    break;
            }
        }

        private enum TileState
        {
            unavailable, // Cannot be interacted with
            unprocessed, // Requires additional purchase of a tile before using it
            selected, // Is selected for purchase
            processed, // Can be used to put a building on it
            hover, // Is being selected actively being selected
            occupied, // Has a building on it
        }
    }

    public enum InitialTileState
    {
        unavailable, // Cannot be interacted with
        unprocessed, // Requires additional purchase of a tile before using it
        processed, // Can be used to put a building on it
    }
}
