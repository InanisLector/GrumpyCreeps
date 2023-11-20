using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoader : MonoBehaviour
{
    public void LoadMap(MapPrefab prefab)
    {
        GlobalSpawnerSystem.MapPrefab = prefab;

        SceneManager.LoadScene("Testing");
    }
}
