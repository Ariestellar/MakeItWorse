using System;

public interface IGameLogic
{
    void SetActionResultsGame(Action<StatusGame> action);
}

public interface IChecker
{
    void Check();
}