using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField,Header("뒤집는 시간")] private float duration = 0.1f;

    [SerializeField] private Image front;
    [SerializeField] private Image back;
    [SerializeField] private Button _button;


    [field:SerializeField] public Test myInfo { get; private set; }

    private Action<Card> _onClickAction;
    private bool isFront = true; // 현재 앞면 상태
    private bool _isSpin;

    private void Start()
    {
        front.rectTransform.localScale = Vector3.one;
        back.rectTransform.localScale = new Vector3(0, 1, 1);
    }

    public void Initialze(Action<Card> onEvent,Test info)
    {
        _onClickAction = onEvent;
        myInfo = info;
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        _onClickAction?.Invoke(this);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    public void SpinAndDisappear()
    {
        _isSpin = true;
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < 3; i++)
        {
            bool flipToFront = (i % 2 == 0); // 번갈아 앞/뒤
            seq.AppendCallback(() => Flip(flipToFront));
            seq.AppendInterval(0.13f); // Flip에서 닫기+열기까지 걸리는 시간 고려
        }

        // 마지막에 작아지면서 뾱!
        seq.Append(transform.DOScale(0, 0.4f).SetEase(Ease.InBack));
        seq.OnComplete(() => gameObject.SetActive(false)); // 완전 사라지게 처리
    }

    private void Flip(bool toFront)
    {
        isFront = toFront;

        back.DOKill();
        front.DOKill();

        (isFront ? back : front).rectTransform
           .DOScaleX(0, duration).SetEase(Ease.OutQuad)
           .OnComplete(() =>
           {
                // 닫힌 뒤 반대쪽 열기
                (isFront ? front : back).rectTransform
                   .DOScaleX(1, duration);
           });
    }

    public void OnPointerEnter(PointerEventData eventData) => Flip(false); // 앞→뒤
    public void OnPointerExit(PointerEventData eventData) => Flip(true);  // 뒤→앞
}
