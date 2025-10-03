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


    private Test _myInfo;
    private Action<Test> _onClickAction;
    private bool isFront = true; // 현재 앞면 상태
    private Button _button;


    private void Start()
    {
        front.rectTransform.localScale = Vector3.one;
        back.rectTransform.localScale = new Vector3(0, 1, 1);
    }

    public void Initialze(Action<Test> onEvent,Test info)
    {
        _onClickAction = onEvent;
        _myInfo = info;
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        _onClickAction?.Invoke(_myInfo);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void Flip(bool toFront)
    {
        isFront = toFront;

        back.DOKill();
        front.DOKill();

        (isFront ? back : front).rectTransform
           .DOScaleX(0, duration)
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
