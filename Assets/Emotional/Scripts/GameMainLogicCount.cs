using EmotionaL.Common;
using UnityEngine;
using System;

public class GameMainLogicCount
{
    private readonly int JUST_COUNT;
    private readonly int MAX_PLAYER_COUNTER;
    private int _currentCount;
    private PlayerType _currentPlayerType;

    public GameMainLogicCount(int justCnt)
    {
        JUST_COUNT = justCnt;

        _currentCount = 0;
        _currentPlayerType = PlayerType.None;
    }
}
