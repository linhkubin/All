using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enums
{
    public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public static List<T> SortOrder<T>(List<T> list)
    {
        return list.OrderBy(d => System.Guid.NewGuid()).Take(list.Count).ToList();
    } 
    
    
    public static T[] SortOrder<T>(T[] list)
    {
        return list.OrderBy(d => System.Guid.NewGuid()).Take(list.Length).ToArray();
    }
}

public enum GameMode { Normal, Challenge }

public enum GameState { MainMenu, Pause, GamePlay }

public enum GameResult { Win, Lose }

public enum UIID{ GamePlay, BlockRaycast, MainMenu, Skin, Setting, Counting, ScoreBoard, LeaderBoard, IAP }

public enum ButtonState { Buy, Equip, Equipped, Ads, CommingSoon }

public enum PriceType { Ads, Gem}

public enum QuizID {
    Wiring,
    Audition,
    Caculator,
    Memory,
    RollingConnect,
    SortOrder,
    Random
}

[System.Serializable]
public class SkinPrice
{
    public PriceType priceType;
    public int value;
}

