using DG.Tweening;
using UnityEngine;

public class CardArranger : MonoBehaviour
{
    [Header("Card Layout Settings")]
    [SerializeField] private float spacing = 150f;   // 카드 간격 (X축)
    [SerializeField] private float curveHeight = 100f; // 아치 높이 (Y축)
    [SerializeField] private float angleRange = 60f;   // 곡선 각도 범위

    public void ArrangeCards(RectTransform exclude = null)
    {
        int count = transform.childCount;
        if (count == 0) return;

        float angleStep = count == 1 ? 0 : angleRange / (count - 1);
        float startAngle = -angleRange / 2f;

        for (int i = 0; i < count; i++)
        {
            RectTransform rt = transform.GetChild(i) as RectTransform;
            if (rt == null || rt == exclude) continue;

            // X는 단순히 일정 간격으로
            float x = (i - (count - 1) / 2f) * spacing;

            // Y는 각도 기반 곡선으로 (∩ 모양)
            float angle = startAngle + i * angleStep;
            float rad = angle * Mathf.Deg2Rad;
            float y = Mathf.Cos(rad) * curveHeight;

            rt.anchoredPosition = new Vector2(x, y);

            // 회전은 항상 0
            rt.localRotation = Quaternion.identity;
        }
    }

    private void OnTransformChildrenChanged()
    {
        ArrangeCards();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // 에디터에서 값 바꿀 때마다 바로 반영
        ArrangeCards();
    }
#endif

    [ContextMenu("Arrange Cards")]
    public void ArrangeFromMenu()
    {
        ArrangeCards();
    }
}