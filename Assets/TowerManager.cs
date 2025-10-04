using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObject[] towerPrefabs;
    private List<GameObject> placedTowers = new();

    public void PlaceTower(GameObject prefab, Vector3 pos)
    {
        var tower = Instantiate(prefab, pos, Quaternion.identity);
        placedTowers.Add(tower);
    }
}
