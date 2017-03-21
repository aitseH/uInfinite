using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName="New Monster", menuName="Monster", order=999)]
public class MonsterTemplate : ScriptableObject {
    [Header("Base Stats")]
    public int hpMax;
    public int mpMax;
    public int damage;
    public int defense;

    [Header("Sepcial")]
    [Range(0, 1)]public float block;
    [Range(0, 1)]public float crit;

    [Header("MoveMent")]
    public float moveDist;
    public float followDist;

    [Header("Loot")]
    public int lootGoldMin;
    public int lootGoldMax;
    public SyncListMonster lootItems;

    [Header("Respawn")]
    public float deathTime;
    public bool respawn;
    public float respawnTime;

    [TextArea(1, 30)] public string tooltip;
    public Sprite image;
    public GameObject model; 


    static Dictionary<string, MonsterTemplate> cache=null;
    public static Dictionary<string, MonsterTemplate> dict {
        get {
            return cache ?? (cache = Resources.LoadAll<MonsterTemplate>("").ToDictionary(
                monster => monster.name, monster => monster
            ));
        }
    }
}