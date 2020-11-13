using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStat
{
    static int _currentLevel;
    static int _totalPoints;

    static void IncreaseTotalNumberPoints(int numberPoints)
    {
        _totalPoints += numberPoints;
    }
}
