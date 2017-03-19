using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName="New Item", menuName="Item", order=999)]
public class ItemTemplate : ScriptableObject{
    [Header("Base Stats")]

    public string category;
    public int maxStack;
    public long buyPrice;
    public long sellPrice;
    public int minLevel;
    public bool sellable;
    public bool tradable;
    public bool destroyable;
    [TextArea(1, 30)] public string tooltip;
    public Sprite image;

    [Header("Usage Boosts")]
    public bool usageDestroy;
    public int usageHp;
    public int usageMp;
    public int usageExp;

    [Header("Equipment Boosts")]
    public int equipHpBonus;
    public int equipMpBonus;
    public int equipDamageBonus;
    public int equipDefenseBonus;
    [Range(0, 1)] public float equipBlockBonus;
    [Range(0, 1)] public float equipCritBonus;
    public GameObject model;

    static Dictionary<string, ItemTemplate> cache = null;
    public static Dictionary<string, ItemTemplate> dict {
        get {
            return cache ?? (cache = Resources.LoadAll<ItemTemplate>("").ToDictionary(
                item => item.name, item => item)
            );
        }
    }

    void OnValidate() {
        sellPrice = Math.Min(sellPrice, buyPrice);
    }
}
