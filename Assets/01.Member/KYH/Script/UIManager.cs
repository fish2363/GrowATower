using UnityEngine;
using Assets._01.Member.CDH.Code.Events;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField,Header("고를 수 있는 한도")] private int _maxSelectCnt;

    [SerializeField] private EventChannelSO _uiChannel;
    [SerializeField] private RectTransform _parent;  // 카드들이 들어갈 부모 (예: Panel)
    [SerializeField] private Card _cardPrefab; // 카드 프리팹
    [SerializeField] private float _spacing = 120f;  // 카드 간격
    [SerializeField] private float _animDuration = 0.4f; // 들어오는 시간
    [SerializeField] private RectTransform _inventory;

    private int _currentSelectCnt;
    private List<Test> _resultList;
    private float _chooseTimer;
    private List<RectTransform> cards = new();

    /// <summary>
    /// 중간 생성
    /// </summary>
    public void SpawnCards(List<Test> tests)
    {
        // 기존 카드 제거
        foreach (var c in cards)
            Destroy(c.gameObject);
        cards.Clear();

        // 중앙 정렬 기준으로 카드 배치할 X좌표 계산
        float startX = -(tests.Count - 1) * _spacing * 0.5f;

        for (int i = 0; i < tests.Count; i++)
        {
            // 프리팹 생성 (부모: parent)
            Card obj = Instantiate(_cardPrefab, _parent);
            obj.Initialze(SelectButton, tests[i]);
            RectTransform rt = obj.GetComponent<RectTransform>();

            // 처음 위치: 오른쪽 화면 밖
            rt.anchoredPosition = new Vector2(1000f, 0f);

            // 목표 위치
            Vector2 targetPos = new Vector2(startX + i * _spacing, 0f);

            // 순차적으로 "척, 척, 척" 들어오게 딜레이 줌
            rt.DOAnchorPos(targetPos, _animDuration)
                .SetDelay(i * 0.1f)
                .SetEase(Ease.OutBack);

            cards.Add(rt);
        }
    }
    public void SelectButton(Card test)
    {
        Card obj = Instantiate(_cardPrefab, _inventory);
        obj.Initialze(ClickButton, test.myInfo);
        ChooseHandler(test);
    }

    public void ClickButton(Card test)
    {

    }

    public void ChooseHandler(Card card)
    {
        card.SpinAndDisappear();
        _currentSelectCnt++;

        if (_maxSelectCnt <= _currentSelectCnt)
            FinishChoose();
    }
    [SerializeField] private float delay = 0.1f; // 카드 간격 시간
    [SerializeField] private float jumpAmount = 50f; // 밑으로 눌렸다가 올라오는 정도
    [SerializeField] private float flyHeight = 1200f; // 위로 날아갈 거리
    [SerializeField] private float flyDuration = 0.8f; // 위로 날아가는 시간
    private void FinishChoose()
    {
        _currentSelectCnt = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            RectTransform card = cards[i];

            // 카드 하나당 시작 시간 살짝 딜레이
            float startDelay = i * delay;

            Sequence seq = DOTween.Sequence();

            seq.AppendInterval(startDelay);

            // 1. 살짝 밑으로 눌렸다가
            seq.Append(card.DOAnchorPosY(card.anchoredPosition.y - jumpAmount, 0.15f)
                .SetEase(Ease.InQuad));

            // 2. 뽀용~ 하고 위로 튀어올라서 화면 밖으로 날아감
            seq.Append(card.DOAnchorPosY(card.anchoredPosition.y + flyHeight, flyDuration)
                .SetEase(Ease.OutQuad));
        }
        ShowInventory(true);
    }

    private void ShowInventory(bool isUp)
    {
        if(isUp)
            _inventory.DOAnchorPosY(-477f, 1f);
        else
            _inventory.DOAnchorPosY(-1000f, 1f);
    }

    #region 상하 생성
    //y좌표 279
    //public void SpawnCards(int count)
    //{
    //    // 기존 카드 제거
    //    foreach (var c in cards)
    //        Destroy(c.gameObject);
    //    cards.Clear();

    //    int perRow = 3;               // 한 줄에 몇 개씩 배치할지
    //    float rowSpacing = -500f;     // 줄 간격 (아래로 내려가니까 음수값)

    //    int rowCount = Mathf.CeilToInt(count / (float)perRow);

    //    for (int i = 0; i < count; i++)
    //    {
    //        // 프리팹 생성
    //        GameObject obj = Instantiate(cardPrefab, parent);
    //        RectTransform rt = obj.GetComponent<RectTransform>();

    //        // 처음 위치 (오른쪽 밖)
    //        rt.anchoredPosition = new Vector2(2000f, 0f);

    //        // i번째 카드의 행(row), 열(col)
    //        int row = i / perRow;
    //        int col = i % perRow;

    //        // 이번 줄의 시작 X (중앙 정렬)
    //        int colCountThisRow = Mathf.Min(perRow, count - row * perRow);
    //        float startX = -(colCountThisRow - 1) * spacing * 0.5f;

    //        // 목표 위치
    //        Vector2 targetPos = new Vector2(startX + col * spacing, row * rowSpacing);

    //        // "척척척" 애니메이션
    //        rt.DOAnchorPos(targetPos, animDuration)
    //            .SetDelay(i * 0.1f)
    //            .SetEase(Ease.OutBack);

    //        cards.Add(rt);
    //    }
    //}
    #endregion

    private void Awake()
    {
        _uiChannel.AddListener<RandomShuffle>(ShuffleHandle);
    }

    private void ShuffleHandle(RandomShuffle obj)
    {
        _resultList = obj.resultList;
        _chooseTimer = obj.chooseTimer;
        SpawnCards(_resultList);
    }

    private void OnDestroy()
    {
        _uiChannel.RemoveListener<RandomShuffle>(ShuffleHandle);
    }
}
