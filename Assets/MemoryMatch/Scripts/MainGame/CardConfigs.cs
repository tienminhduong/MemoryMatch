using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CardConfigs", menuName = "Configs/CardConfigs")]
public class CardConfigs : ScriptableObject
{
    [SerializeField] List<CardConfig> configs = new List<CardConfig>();
    public List<CardConfig> Stat => configs;
}

[System.Serializable]
public class CardConfig
{
    [SerializeField] int id;
    [SerializeField] CardCategory category;
    [SerializeField] int damage;
    [SerializeField] Sprite icon;
    [SerializeField] CardTarget target;

    public int ID => id;
    public CardCategory Category => category;
    public int Damage => damage;
    public Sprite Icon => icon;
    public CardTarget Target => target;
}

// Enum to represent different types of cards


public enum CardTarget
{
    TurnPlayer, Opponent
}