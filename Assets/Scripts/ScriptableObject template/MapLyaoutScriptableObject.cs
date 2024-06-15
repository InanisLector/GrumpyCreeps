using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Grid
{
    [CreateAssetMenu(fileName = "MapLayout", menuName = "Grid/Mapping")]
    public class MapLyaoutScriptableObject : ScriptableObject, IMapLayout
    {
        [SerializeField] private ITileSettings[,] m_TileSettings;

        public IEnumerator<ITileSettings> GetEnumerator()
        {
            throw new System.NotImplementedException();
            //return m_TileSettings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }
    }
}
