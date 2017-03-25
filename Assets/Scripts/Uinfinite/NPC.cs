using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public struct NPC {

    public string name;

    public NPC(NPCTemplate template) {
        name = template.name;
    }

    public bool TemplateExists() {
        return NPCTemplate.dict.ContainsKey(name);
    }

    public NPCTemplate template {
        get { return NPCTemplate.dict[name]; }
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

    public ItemTemplate[] objects {
        get { return template.objects; }
    }
}

public class SyncListNPC : SyncListStruct<NPC> { }