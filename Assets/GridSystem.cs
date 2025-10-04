using UnityEngine;
using UnityEngine.EventSystems;

public class GridSystem : MonoBehaviour
{
    [Header("Grid Reference")]
    public Grid grid; // UnityEngine.Grid
    public LayerMask placementMask;

    [Header("Ghost Settings")]
    public GameObject ghostPrefab;
    public Material validMat;
    public Material invalidMat;

    [Header("Building Settings")]
    public GameObject buildingPrefab;
    public Vector3Int buildingSize = Vector3Int.one; // 타워 크기 셀 단위

    private GameObject ghostInstance;
    private bool deleteMode = false;

    // Occupied cell tracking
    private bool[,] occupiedCells;

    public bool isGridStart;

    private void Awake()
    {
        if (grid)
        {
            int width = 20; // Grid 범위에 맞게 조정 가능
            int height = 20;
            occupiedCells = new bool[width, height];
        }
    }
    public void SetGrid(bool isDrag)
    {
        isGridStart = isDrag;
    }
    void Update()
    {
        if (!grid || !isGridStart) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, 500, placementMask)) return;

        Vector3Int cell = grid.WorldToCell(hit.point);

        // Vector3Int → Vector3 변환 후 셀 중심 계산
        Vector3 cellOffset = new Vector3(buildingSize.x, 0, buildingSize.z) * 0.5f;
        Vector3 targetPos = grid.CellToWorld(cell) + cellOffset;

        // Ghost 생성
        if (!ghostInstance && ghostPrefab)
        {
            ghostInstance = Instantiate(ghostPrefab, targetPos, Quaternion.identity);
        }

        if (ghostInstance)
        {
            ghostInstance.transform.position = targetPos;

            bool canPlace = CanPlace(cell);
            Renderer[] rends = ghostInstance.GetComponentsInChildren<Renderer>();
            foreach (var r in rends)
                r.material = canPlace ? validMat : invalidMat;

            // 배치
            if (Input.GetMouseButtonDown(0) && !deleteMode && canPlace)
            {
                Instantiate(buildingPrefab, targetPos, Quaternion.identity);
                SetOccupied(cell, true);
            }

            // 삭제
            if (deleteMode && Input.GetMouseButtonDown(0))
            {
                if (IsOccupied(cell))
                {
                    Collider[] hits = Physics.OverlapBox(targetPos, new Vector3(buildingSize.x, 1, buildingSize.z) * 0.5f);
                    foreach (var col in hits)
                    {
                        Destroy(col.gameObject);
                    }
                    SetOccupied(cell, false);
                }
            }

            // 삭제 모드 토글
            if (Input.GetKeyDown(KeyCode.C))
                deleteMode = !deleteMode;
        }
    }

    private bool CanPlace(Vector3Int cell)
    {
        if (!grid) return false;
        if (IsOccupied(cell)) return false;

        Vector3 cellOffset = new Vector3(buildingSize.x, 0, buildingSize.z) * 0.5f;
        Vector3 worldPos = grid.CellToWorld(cell) + cellOffset;
        Collider[] hits = Physics.OverlapBox(worldPos, new Vector3(buildingSize.x, 1, buildingSize.z) * 0.5f, Quaternion.identity, placementMask);
        return hits.Length == 0;
    }

    private bool IsOccupied(Vector3Int cell)
    {
        if (cell.x < 0 || cell.z < 0 || cell.x >= occupiedCells.GetLength(0) || cell.z >= occupiedCells.GetLength(1))
            return false;
        return occupiedCells[cell.x, cell.z];
    }

    private void SetOccupied(Vector3Int cell, bool value)
    {
        if (cell.x < 0 || cell.z < 0 || cell.x >= occupiedCells.GetLength(0) || cell.z >= occupiedCells.GetLength(1))
            return;
        occupiedCells[cell.x, cell.z] = value;
    }
}
