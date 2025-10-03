using Assets._01.Member.CDH.Code.Events;
using UnityEngine;

public class TestKYH : MonoBehaviour
{
    [SerializeField] private EventChannelSO uiChannel;
    [SerializeField] private TESTSOList list;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomShuffle randomShuffle = UIEvents.randomShuffle;
        randomShuffle.chooseTimer = 10f;
        randomShuffle.resultList = RandomShuffle.ShuffleRandomCards(list.tests,6);
        uiChannel.Invok(randomShuffle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
