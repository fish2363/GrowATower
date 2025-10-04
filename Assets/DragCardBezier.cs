using UnityEngine;
using UnityEngine.EventSystems;

public class DragCardBezier : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private BezierDrawer bezierDrawer;
    [SerializeField] private Camera mainCamera;

    private bool isDragging;

    void Start()
    {
        bezierDrawer = FindAnyObjectByType<BezierDrawer>();
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        // UI → 월드 좌표 변환
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            transform as RectTransform,
            eventData.position,
            mainCamera,
            out Vector3 worldMousePos
        );

        // 카메라에서 시작 → 마우스 위치까지 곡선
        bezierDrawer.Draw(new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y-2f, mainCamera.transform.position.z), new Vector3(worldMousePos.x,worldMousePos.y+0.4f,worldMousePos.z));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        bezierDrawer.Hide();
    }
}
