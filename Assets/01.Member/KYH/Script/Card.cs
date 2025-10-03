using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image front;
    [SerializeField] private Image back;

    private bool isFront = true; // ���� �ո� ����
    private float duration = 0.3f;

    private void Start()
    {
        front.rectTransform.localScale = Vector3.one;
        back.rectTransform.localScale = new Vector3(0, 1, 1);
    }

    private void Flip(bool toFront)
    {
        isFront = toFront;

        // ���� ���� ���̴� �� �ݱ�
        //(toFront ? back : front).rectTransform
        //    .DOScaleX(0, duration)
        //    .OnComplete(() =>
        //    {
        //        // ���� �� �ݴ��� ����
        //        (toFront ? front : back).rectTransform
        //            .DOScaleX(1, duration);
        //    });
    }

    public void OnPointerEnter(PointerEventData eventData) => Flip(false); // �ա��
    public void OnPointerExit(PointerEventData eventData) => Flip(true);  // �ڡ��
}
