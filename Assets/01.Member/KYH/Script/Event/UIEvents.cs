using System.Collections.Generic;
using System;
using Assets._01.Member.CDH.Code.Events;

public static class UIEvents
{
    public static RandomShuffle randomShuffle = new();
}

public class RandomShuffle : GameEvent
{
    public List<Test> resultList;
    public float chooseTimer;

    private Random _random = new();

    /// <summary>
    /// ����Ʈ���� �����ϰ� shuffleCnt ������ �̾� ��ȯ�մϴ�.
    /// </summary>
    public List<Test> ShuffleRandomCards(List<Test> source, int shuffleCnt)
    {
        if (source == null || source.Count == 0)
            throw new ArgumentException("Source ����Ʈ�� ��� �ֽ��ϴ�.");

        if (shuffleCnt <= 0)
            throw new ArgumentException("shuffleCnt�� 1 �̻��̾�� �մϴ�.");

        if (shuffleCnt > source.Count)
            shuffleCnt = source.Count;

        List<Test> copy = new List<Test>(source);
        List<Test> result = new List<Test>();

        for (int i = 0; i < shuffleCnt; i++)
        {
            int index = _random.Next(copy.Count);
            result.Add(copy[index]);
            copy.RemoveAt(index); // �ߺ� ����
        }

        return result;
    }
}
