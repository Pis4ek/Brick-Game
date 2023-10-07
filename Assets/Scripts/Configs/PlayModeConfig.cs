using UnityEngine;

public enum GameComplexity
{
    Easy,
    Normal,
    Difficult
}

public struct PlayModeConfig
{
    public GameComplexity complexity;
}