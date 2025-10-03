using UnityEngine;
using Assets._01.Member.CDH.Code.Events;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField,Header("�� �� �ִ� �ѵ�")] private int _maxSelectCnt;

    [SerializeField] private EventChannelSO _uiChannel;
    [SerializeField] private RectTransform _parent;  // ī����� �� �θ� (��: Panel)
    [SerializeField] private Card _cardPrefab; // ī�� ������
    [SerializeField] private float _spacing = 120f;  // ī�� ����
    [SerializeField] private float _animDuration = 0.4f; // ������ �ð�
    [SerializeField] private RectTransform _inventory;

    private int _currentSelectCnt;
    private List<Test> _resultList;
    private float _chooseTimer;
    private List<RectTransform> cards = new();

    /// <summary>
    /// �߰� ����
    /// </summary>
    public void SpawnCards(List<Test> tests)
    {
        // ���� ī�� ����
        foreach (var c in cards)
            Destroy(c.gameObject);
        cards.Clear();

        // �߾� ���� �������� ī�� ��ġ�� X��ǥ ���
        float startX = -(tests.Count - 1) * _spacing * 0.5f;

        for (int i = 0; i < tests.Count; i++)
        {
            // ������ ���� (�θ�: parent)
            Card obj = Instantiate(_cardPrefab, _parent);
            obj.Initialze(SelectButton, tests[i]);
            RectTransform rt = obj.GetComponent<RectTransform>();

            // ó�� ��ġ: ������ ȭ�� ��
            rt.anchoredPosition = new Vector2(1000f, 0f);

            // ��ǥ ��ġ
            Vector2 targetPos = new Vector2(startX + i * _spacing, 0f);

            // ���������� "ô, ô, ô" ������ ������ ��
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
    [SerializeField] private float delay = 0.1f; // ī�� ���� �ð�
    [SerializeField] private float jumpAmount = 50f; // ������ ���ȴٰ� �ö���� ����
    [SerializeField] private float flyHeight = 1200f; // ���� ���ư� �Ÿ�
    [SerializeField] private float flyDuration = 0.8f; // ���� ���ư��� �ð�
    private void FinishChoose()
    {
        _currentSelectCnt = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            RectTransform card = cards[i];

            // ī�� �ϳ��� ���� �ð� ��¦ ������
            float startDelay = i * delay;

            Sequence seq = DOTween.Sequence();

            seq.AppendInterval(startDelay);

            // 1. ��¦ ������ ���ȴٰ�
            seq.Append(card.DOAnchorPosY(card.anchoredPosition.y - jumpAmount, 0.15f)
                .SetEase(Ease.InQuad));

            // 2. �ǿ�~ �ϰ� ���� Ƣ��ö� ȭ�� ������ ���ư�
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

    #region ���� ����
    //y��ǥ 279
    //public void SpawnCards(int count)
    //{
    //    // ���� ī�� ����
    //    foreach (var c in cards)
    //        Destroy(c.gameObject);
    //    cards.Clear();

    //    int perRow = 3;               // �� �ٿ� �� ���� ��ġ����
    //    float rowSpacing = -500f;     // �� ���� (�Ʒ��� �������ϱ� ������)

    //    int rowCount = Mathf.CeilToInt(count / (float)perRow);

    //    for (int i = 0; i < count; i++)
    //    {
    //        // ������ ����
    //        GameObject obj = Instantiate(cardPrefab, parent);
    //        RectTransform rt = obj.GetComponent<RectTransform>();

    //        // ó�� ��ġ (������ ��)
    //        rt.anchoredPosition = new Vector2(2000f, 0f);

    //        // i��° ī���� ��(row), ��(col)
    //        int row = i / perRow;
    //        int col = i % perRow;

    //        // �̹� ���� ���� X (�߾� ����)
    //        int colCountThisRow = Mathf.Min(perRow, count - row * perRow);
    //        float startX = -(colCountThisRow - 1) * spacing * 0.5f;

    //        // ��ǥ ��ġ
    //        Vector2 targetPos = new Vector2(startX + col * spacing, row * rowSpacing);

    //        // "ôôô" �ִϸ��̼�
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
