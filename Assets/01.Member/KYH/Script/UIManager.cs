using UnityEngine;
using Assets._01.Member.CDH.Code.Events;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private EventChannelSO uiChannel;
    [SerializeField] private RectTransform parent;  // ī����� �� �θ� (��: Panel)
    [SerializeField] private Card cardPrefab; // ī�� ������
    [SerializeField] private float spacing = 120f;  // ī�� ����
    [SerializeField] private float animDuration = 0.4f; // ������ �ð�

    private List<Test> _resultList;
    private float _chooseTimer;

    private List<RectTransform> cards = new();
    private List<Test> inventory = new();

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
        float startX = -(tests.Count - 1) * spacing * 0.5f;

        for (int i = 0; i < tests.Count; i++)
        {
            // ������ ���� (�θ�: parent)
            Card obj = Instantiate(cardPrefab, parent);
            obj.Initialze(SelectButton, tests[i]);
            RectTransform rt = obj.GetComponent<RectTransform>();

            // ó�� ��ġ: ������ ȭ�� ��
            rt.anchoredPosition = new Vector2(1000f, 0f);

            // ��ǥ ��ġ
            Vector2 targetPos = new Vector2(startX + i * spacing, 0f);

            // ���������� "ô, ô, ô" ������ ������ ��
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
