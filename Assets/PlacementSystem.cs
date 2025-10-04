using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GridSystem gridSystem;  // GridSystem ����
    [SerializeField] private GameObject towerPrefab; // ��ž ������

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
