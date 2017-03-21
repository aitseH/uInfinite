using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

[System.Serializable]
public struct Monster{

    public string name;

    public Monster(MonsterTemplate template) {
        name = template.name;
    }


    public bool TemplateExists() {
        return name != null && MonsterTemplate.dict.ContainsKey(name);
    }

    public MonsterTemplate template {
        get { return MonsterTemplate.dict[name]; }
    }

    public int hpMax {
        get { return template.hpMax; }
    }

    public int mpMax {
        get { return template.mpMax; }
    }

    public int damage {
        get { return template.damage; }
    }

    public int defense {
        get { return template.defense; }
    }

    public float block {
        get { return template.block; }
    }

    public float crit {
        get { return template.crit; }
    }

    public float moveDist {
        get { return template.moveDist; }
    }

    public float followDist {
        get { return template.followDist; }
    }

    public int lootGoldMin {
        get { return template.lootGoldMin; }
    }

    public int lootGoldMax {
        get { return template.lootGoldMax; }
    }

    public SyncListMonster lootItems{
        get { return template.lootItems; }
    }

    public float deathTime {
        get { return template.deathTime; }
    }

    public bool respawn {
        get { return template.respawn; }
    }

    public float respawnTime {
        get { return template.respawnTime; }
    }

    public Sprite image {
        get { return template.image; }
    }

    public GameObject model {
        get { return template.model; }
    }
        
    public string Tooltip() {
        string tip = template.tooltip;
        tip = tip.Replace("{NAME}", name);
        tip = tip.Replace("{HPMAX}", hpMax.ToString());
        tip = tip.Replace("{MPMAX}", mpMax.ToString());
        tip = tip.Replace("{DAMAGE}", damage.ToString());
        tip = tip.Replace("{DEFENSE}", defense.ToString());
        tip = tip.Replace("{BLOCK}", block.ToString());
        tip = tip.Replace("{CRIT}", crit.ToString());
        tip = tip.Replace("{MOVEDIST}", moveDist.ToString());
        return tip;
    }
}

public class SyncListMonster : SyncListStruct<Monster> { }
