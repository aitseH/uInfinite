using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName="New Quest", menuName="Quest", order=999)]
public class QuestTemplate : ScriptableObject {

    [Header("General")]
    [TextArea(1, 30)] public string tooltip;

    [Header("Requirements")]
    public QuestTemplate predecessor;

    [Header("Rewards")]
    public long rewardGold;
    public long rewardExp;
    public ItemTemplate rewardItem;

    [Header("Fulfillment")]
    public MonsterTemplate killTarget;
    public int killAmount;
    public ItemTemplate gatherItem;
    public int gatherAmount;

    static Dictionary<string, QuestTemplate> cache = null;
    public static Dictionary<string, QuestTemplate> dict {
        get {
            // load if not loaded yet
            return cache ?? (cache = Resources.LoadAll<QuestTemplate>("").ToDictionary(
                quest => quest.name, quest => quest)
            );
        }
    }

}
