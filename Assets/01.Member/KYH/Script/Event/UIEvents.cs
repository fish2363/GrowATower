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
    /// 리스트에서 랜덤하게 shuffleCnt 개수를 뽑아 반환합니다.
    /// </summary>
    public List<Test> ShuffleRandomCards(List<Test> source, int shuffleCnt)
    {
        if (source == null || source.Count == 0)
            throw new ArgumentException("Source 리스트가 비어 있습니다.");

        if (shuffleCnt <= 0)
            throw new ArgumentException("shuffleCnt는 1 이상이어야 합니다.");

        if (shuffleCnt > source.Count)
            shuffleCnt = source.Count;

        List<Test> copy = new List<Test>(source);
        List<Test> result = new List<Test>();

        for (int i = 0; i < shuffleCnt; i++)
        {
            int index = _random.Next(copy.Count);
            result.Add(copy[index]);
            copy.RemoveAt(index); // 중복 방지
        }

        return result;
    }
}
