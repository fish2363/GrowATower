using DG.Tweening;
using UnityEngine;

public class CardArranger : MonoBehaviour
{
    [Header("Card Layout Settings")]
    [SerializeField] private float spacing = 150f;   // ī�� ���� (X��)
    [SerializeField] private float curveHeight = 100f; // ��ġ ���� (Y��)
    [SerializeField] private float angleRange = 60f;   // � ���� ����

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

            // X�� �ܼ��� ���� ��������
            float x = (i - (count - 1) / 2f) * spacing;

            // Y�� ���� ��� ����� (�� ���)
            float angle = startAngle + i * angleStep;
            float rad = angle * Mathf.Deg2Rad;
            float y = Mathf.Cos(rad) * curveHeight;

            rt.anchoredPosition = new Vector2(x, y);

            // ȸ���� �׻� 0
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
        // �����Ϳ��� �� �ٲ� ������ �ٷ� �ݿ�
        ArrangeCards();
    }
#endif

    [ContextMenu("Arrange Cards")]
    public void ArrangeFromMenu()
    {
        ArrangeCards();
    }
}