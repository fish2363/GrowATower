using UnityEngine;
using Assets._01.Member.CDH.Code.Events;
using System;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private EventChannelSO uiChannel;


    private List<Test> _resultList;
    private float _chooseTimer;


    private void Awake()
    {
        uiChannel.AddListener<RandomShuffle>(ShuffleHandle);
    }

    private void ShuffleHandle(RandomShuffle obj)
    {
        _resultList = obj.resultList;
        _chooseTimer = obj.chooseTimer;
    }


}
