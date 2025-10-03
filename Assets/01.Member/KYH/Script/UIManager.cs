using UnityEngine;
using Assets._01.Member.CDH.Code.Events;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private EventChannelSO uiChannel;
    [SerializeField] private RectTransform parent;  // 카드들이 들어갈 부모 (예: Panel)
    [SerializeField] private Card cardPrefab; // 카드 프리팹
    [SerializeField] private float spacing = 120f;  // 카드 간격
    [SerializeField] private float animDuration = 0.4f; // 들어오는 시간

    private List<Test> _resultList;
    private float _chooseTimer;

    private List<RectTransform> cards = new();
    private List<Test> inventory = new();

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
        float startX = -(tests.Count - 1) * spacing * 0.5f;

        for (int i = 0; i < tests.Count; i++)
        {
            // 프리팹 생성 (부모: parent)
            Card obj = Instantiate(cardPrefab, parent);
            obj.Initialze(SelectButton, tests[i]);
            RectTransform rt = obj.GetComponent<RectTransform>();

            // 처음 위치: 오른쪽 화면 밖
            rt.anchoredPosition = new Vector2(1000f, 0f);

            // 목표 위치
            Vector2 targetPos = new Vector2(startX + i * spacing, 0f);

            // 순차적으로 "척, 척, 척" 들어오게 딜레이 줌
            rt.DOAnchorPos(targetPos, animDuration)
                .SetDelay(i * 0.1f)
                .SetEase(Ease.OutBack);

            cards.Add(rt);
        }
    }
    public void SelectButton(Test test)
    {
        inventory.Add(test);
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
        uiChannel.AddListener<RandomShuffle>(ShuffleHandle);
    }

    private void ShuffleHandle(RandomShuffle obj)
    {
        _resultList = obj.resultList;
        _chooseTimer = obj.chooseTimer;
        SpawnCards(_resultList);
    }

    private void OnDestroy()
    {
        uiChannel.RemoveListener<RandomShuffle>(ShuffleHandle);
    }
}
