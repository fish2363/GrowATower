using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image front;
    [SerializeField] private Image back;

    private bool isFront = true; // 현재 앞면 상태
    private float duration = 0.3f;

    private void Start()
    {
        front.rectTransform.localScale = Vector3.one;
        back.rectTransform.localScale = new Vector3(0, 1, 1);
    }

    private void Flip(bool toFront)
    {
        isFront = toFront;

        // 먼저 현재 보이는 쪽 닫기
        //(toFront ? back : front).rectTransform
        //    .DOScaleX(0, duration)
        //    .OnComplete(() =>
        //    {
        //        // 닫힌 뒤 반대쪽 열기
        //        (toFront ? front : back).rectTransform
        //            .DOScaleX(1, duration);
        //    });
    }

    public void OnPointerEnter(PointerEventData eventData) => Flip(false); // 앞→뒤
    public void OnPointerExit(PointerEventData eventData) => Flip(true);  // 뒤→앞
}
