using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStat
{
    public static int currentLevel;
    public static int totalPoints;

    public static void IncreaseTotalNumberPoints(int numberPoints)
    {
        totalPoints += numberPoints;
    }
}
