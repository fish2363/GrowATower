using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GridSystem gridSystem;  // GridSystem 참조
    [SerializeField] private GameObject towerPrefab; // 포탑 프리팹

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryPlaceTower();
    }

    void TryPlaceTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
        }
    }
}
