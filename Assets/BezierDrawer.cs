using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierDrawer : MonoBehaviour
{
    private LineRenderer line;
    [SerializeField] private int segmentCount = 30; // 곡선 정밀도

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = segmentCount + 1;
        line.enabled = false;
    }

    public void Draw(Vector3 start, Vector3 end)
    {
        // 제어점 (중간 지점 위로 살짝 띄움)
        Vector3 control = (start + end) / 2f + Vector3.up * 4.5f;

        line.enabled = true;
        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            Vector3 pos = CalculateQuadraticBezier(start, control, end, t);
            line.SetPosition(i, pos);
        }
    }

    public void Hide()
    {
        line.enabled = false;
    }

    private Vector3 CalculateQuadraticBezier(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        float u = 1 - t;
        return u * u * a + 2 * u * t * b + t * t * c;
    }
}
