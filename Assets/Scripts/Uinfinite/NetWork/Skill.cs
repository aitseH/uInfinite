using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public struct Skill {

    public string name;

    public bool learned;
    public int level;
    public float castTimeEnd;
    public float cooldownEnd;
    public float buffTimeEnd;

    public Skill(SkillTemplate template) {
        name = template.name;

        learned = template.learnDefault;
        level = 1;

        castTimeEnd = cooldownEnd = buffTimeEnd = Time.time;
    }

    public bool TemplateExists() {
        return SkillTemplate.dict.ContainsKey(name);
    }

    public SkillTemplate template {
        get { return SkillTemplate.dict[name]; }
    }

    public string category {
        get { return template.category; }
    }
    public int damage {
        get { return template.levels[level-1].damage; }
    }
    public float castTime {
        get { return template.levels[level-1].castTime; }
    }
    public float cooldown {
        get { return template.levels[level-1].cooldown; }
    }
    public float castRange {
        get { return template.levels[level-1].castRange; }
    }
    public float aoeRadius {
        get { return template.levels[level-1].aoeRadius; }
    }
    public int manaCosts {
        get { return template.levels[level-1].manaCosts; }
    }
    public int healsHp {
        get { return template.levels[level-1].healsHp; }
    }
    public int healsMp {
        get { return template.levels[level-1].healsMp; }
    }
    public float buffTime {
        get { return template.levels[level-1].buffTime; }
    }
    public int buffsHpMax {
        get { return template.levels[level-1].buffsHpMax; }
    }
    public int buffsMpMax {
        get { return template.levels[level-1].buffsMpMax; }
    }
    public int buffsDamage {
        get { return template.levels[level-1].buffsDamage; }
    }
    public int buffsDefense {
        get { return template.levels[level-1].buffsDefense; }
    }
    public float buffsBlock {
        get { return template.levels[level-1].buffsBlock; }
    }
    public float buffsCrit {
        get { return template.levels[level-1].buffsCrit; }
    }
    public float buffsHpPercentPerSecond {
        get { return template.levels[level-1].buffsHpPercentPerSecond; }
    }
    public float buffsMpPercentPerSecond {
        get { return template.levels[level-1].buffsMpPercentPerSecond; }
    }
    public bool followupDefaultAttack {
        get { return template.followupDefaultAttack; }
    }
    public Sprite image {
        get { return template.image; }
    }
    public Projectile projectile {
        get { return template.levels[level-1].projectile; }
    }
    public bool learnDefault {
        get { return template.learnDefault; }
    }
    public int requiredLevel {
        get { return template.levels[level-1].requiredLevel; }
    }
    public long requiredSkillExp {
        get { return template.levels[level-1].requiredSkillExp; }
    }
    public int maxLevel {
        get { return template.levels.Length; }
    }
    public int upgradeRequiredLevel {
        get { return (level < maxLevel) ? template.levels[level].requiredLevel : 0; }
    }
    public long upgradeRequiredSkillExp {
        get { return (level < maxLevel) ? template.levels[level].requiredSkillExp : 0; }
    }

    public string Tooltip(bool showRequirements = false) {
        string tip = template.tooltip;
        tip = tip.Replace("{NAME}", name);
        tip = tip.Replace("{CATEGORY}", category);
        tip = tip.Replace("{LEVEL}", level.ToString());
        tip = tip.Replace("{DAMAGE}", damage.ToString());
        tip = tip.Replace("{CASTTIME}", Utils.PrettyTime(castTime));
        tip = tip.Replace("{COOLDOWN}", Utils.PrettyTime(cooldown));
        tip = tip.Replace("{CASTRANGE}", castRange.ToString());
        tip = tip.Replace("{AOERADIUS}", aoeRadius.ToString());
        tip = tip.Replace("{HEALSHP}", healsHp.ToString());
        tip = tip.Replace("{HEALSMP}", healsMp.ToString());
        tip = tip.Replace("{BUFFTIME}", Utils.PrettyTime(buffTime));
        tip = tip.Replace("{BUFFSHPMAX}", buffsHpMax.ToString());
        tip = tip.Replace("{BUFFSMPMAX}", buffsMpMax.ToString());
        tip = tip.Replace("{BUFFSDAMAGE}", buffsDamage.ToString());
        tip = tip.Replace("{BUFFSDEFENSE}", buffsDefense.ToString());
        tip = tip.Replace("{BUFFSBLOCK}", Mathf.RoundToInt(buffsBlock * 100).ToString());
        tip = tip.Replace("{BUFFSCRIT}", Mathf.RoundToInt(buffsCrit * 100).ToString());
        tip = tip.Replace("{BUFFSHPPERCENTPERSECOND}", Mathf.RoundToInt(buffsHpPercentPerSecond * 100).ToString());
        tip = tip.Replace("{BUFFSMPPERCENTPERSECOND}", Mathf.RoundToInt(buffsMpPercentPerSecond * 100).ToString());
        tip = tip.Replace("{MANACOSTS}", manaCosts.ToString());

        // only show requirements if necessary
        if (showRequirements) {
            tip += "\n<b><i>Required Level: " + requiredLevel + "</i></b>\n" +
                "<b><i>Required Skill Exp.: " + requiredSkillExp + "</i></b>\n";
        }
        // only show upgrade if necessary (not if not learned yet etc.)
        if (learned && level < maxLevel) {
            tip += "\n<i>Upgrade:</i>\n" +
                "<i>  Required Level: " + upgradeRequiredLevel + "</i>\n" +
                "<i>  Required Skill Exp.: " + upgradeRequiredSkillExp + "</i>\n";
        }

        return tip;
    }

    public float CastTimeRemaining() {
        // how much time remaining until the casttime ends? (using server time)
        return NetworkTime.time >= castTimeEnd ? 0 : castTimeEnd - NetworkTime.time;
    }

    public bool IsCasting() {
        // we are casting a skill if the casttime remaining is > 0
        return CastTimeRemaining() > 0;
    }

    public float CooldownRemaining() {
        // how much time remaining until the cooldown ends? (using server time)
        return NetworkTime.time >= cooldownEnd ? 0 : cooldownEnd - NetworkTime.time;
    }

    public float BuffTimeRemaining() {
        // how much time remaining until the buff ends? (using server time)
        return NetworkTime.time >= buffTimeEnd ? 0 : buffTimeEnd - NetworkTime.time;        
    }

    public bool IsReady() {
        return CooldownRemaining() == 0;
    }    
}

public class SyncListSkill : SyncListStruct<Skill> { }
