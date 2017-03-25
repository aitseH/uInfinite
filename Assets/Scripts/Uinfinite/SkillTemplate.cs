using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName="New Skill", menuName="Skill", order=999)]
public class SkillTemplate : ScriptableObject{

    [Header("Base Stats")]

    public string category;
    public bool followupDefaultAttack;
    [TextArea(1, 30)]public string tooltip;
    public Sprite image;
    public bool learnDefault;

    [System.Serializable]
    public struct Skilllevel {
        public int damage;
        public float castTime;
        public float cooldown;
        public float castRange;
        public float aoeRadius;
        public int manaCosts;
        public int healsHp;
        public int healsMp;
        public float buffTime;
        public int buffsHpMax;
        public int buffsMpMax;
        public int buffsDamage;
        public int buffsDefense;
        [Range(0, 1)] public float buffsBlock;
        [Range(0, 1)] public float buffsCrit;
        public float buffsHpPercentPerSecond;
        public float buffsMpPercentPerSecond;
        public Projectile projectile;

        public int requiredLevel;
        public long requiredSkillExp;
    }

    [Header("Skill levels")]
    public Skilllevel[] levels = new Skilllevel[]{ new Skilllevel() };

    static Dictionary<string, SkillTemplate> cache = null;
    public static Dictionary<string, SkillTemplate> dict {
        get {
            return cache ?? (cache = Resources.LoadAll<SkillTemplate>("").ToDictionary(
                skill => skill.name, skill => skill
            ));
        }
    }

}
