using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public struct Quest {

    public string name;

    public int killed;
    public bool completed;

    public Quest(QuestTemplate template){
        name = template.name;
        killed = 0;
        completed = false;
    }

    public bool TemplateExists() {
        return QuestTemplate.dict.ContainsKey(name);
    }

    public QuestTemplate template {
        get { return QuestTemplate.dict[name]; }
    }
    public string predecessor {
        get { return template.predecessor != null ? template.predecessor.name : ""; }
    }
    public long rewardGold {
        get { return template.rewardGold; }
    }
    public long rewardExp {
        get { return template.rewardExp; }
    }
    public ItemTemplate rewardItem {
        get { return template.rewardItem; }
    }
    public string killName {
        get { return template.killTarget != null ? template.killTarget.name : ""; }
    }
    public int killAmount {
        get { return template.killAmount; }
    }
    public string gatherName {
        get { return template.gatherItem != null ? template.gatherItem.name : ""; }
    }
    public int gatherAmount {
        get { return template.gatherAmount; }
    }

    public string Tooltip(int gathered = 0) {
        string tip = template.tooltip;
        tip = tip.Replace("{NAME}", name);
        tip = tip.Replace("{KILLNAME}", killName);
        tip = tip.Replace("{KILLAMOUNT}", killAmount.ToString());
        tip = tip.Replace("{GATHERNAME}", gatherName);
        tip = tip.Replace("{GATHERAMOUNT}", gatherAmount.ToString());
        tip = tip.Replace("{REWARDGOLD}", rewardGold.ToString());
        tip = tip.Replace("{REWARDEXP}", rewardExp.ToString());
        tip = tip.Replace("{REWARDITEM}", rewardItem != null ? rewardItem.name : "");
        tip = tip.Replace("{KILLED}", killed.ToString());
        tip = tip.Replace("{GATHERED}", gathered.ToString());
        tip = tip.Replace("{STATUS}", IsFulfilled(gathered) ? "<i>Completed!</i>" : "");
        return tip;
    }
        
    public bool IsFulfilled(int gathered) {
        return killed >= killAmount || gathered >= gatherAmount;
    }
}

public class SyncListQuest : SyncListStruct<Quest> { }