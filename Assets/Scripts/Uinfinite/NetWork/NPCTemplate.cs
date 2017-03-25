using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;


[CreateAssetMenu(fileName="New NPC", menuName="NPC", order=999)]
public class NPCTemplate : ScriptableObject {

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

    [Header("Items")]
    public ItemTemplate[] objects;

    static Dictionary<string, NPCTemplate> cache = null;
    public static Dictionary<string, NPCTemplate> dict {
        get {
            return cache ?? (cache = Resources.LoadAll<NPCTemplate>("").ToDictionary(
                npc => npc.name, npc => npc
            ));
        }
    }

}
