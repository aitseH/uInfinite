using UnityEngine.Networking;
using UnityEngine;

[System.Serializable]
public struct Item{
   
    public string name;

    public bool valid;
    public int amount;

    public Item(ItemTemplate template, int _amount=1) {
        name = template.name;
        amount = _amount;
        valid = true;
    }
    public bool TemplateExists() {
        return name != null && ItemTemplate.dict.ContainsKey(name);
    }
    public ItemTemplate template {
        get { return ItemTemplate.dict[name]; }
    }
    public string category {
        get { return template.category; }
    }
    public int maxStack {
        get { return template.maxStack; }
    }
    public long buyPrice {
        get { return template.buyPrice; }
    }
    public long sellPrice {
        get { return template.sellPrice; }
    }
    public int minLevel {
        get { return template.minLevel; }
    }
    public bool sellable {
        get { return template.sellable; }
    }
    public bool tradable {
        get { return template.tradable; }
    }
    public bool destroyable {
        get { return template.destroyable; }
    }
    public Sprite image {
        get { return template.image; }
    }
    public bool usageDestroy {
        get { return template.usageDestroy; }
    }
    public int usageHp {
        get { return template.usageHp; }
    }
    public int usageMp {
        get { return template.usageMp; }
    }
    public int usageExp {
        get { return template.usageExp; }
    }
    public int equipHpBonus {
        get { return template.equipHpBonus; }
    }
    public int equipMpBonus {
        get { return template.equipMpBonus; }
    }
    public int equipDamageBonus {
        get { return template.equipDamageBonus; }
    }
    public int equipDefenseBonus {
        get { return template.equipDefenseBonus; }
    }
    public float equipBlockBonus {
        get { return template.equipBlockBonus; }
    }
    public float equipCritBonus {
        get { return template.equipCritBonus; }
    }
    public GameObject model {
        get { return template.model; }
    }


    public string Tooltip() {
        string tip = template.tooltip;
        tip = tip.Replace("{NAME}", name);
        tip = tip.Replace("{CATEGORY}", category);
        tip = tip.Replace("{EQUIPDAMAGEBONUS}", equipDamageBonus.ToString());
        tip = tip.Replace("{EQUIPDEFENSEBONUS}", equipDefenseBonus.ToString());
        tip = tip.Replace("{EQUIPHPBONUS}", equipHpBonus.ToString());
        tip = tip.Replace("{EQUIPMPBONUS}", equipMpBonus.ToString());
        tip = tip.Replace("{EQUIPBLOCKBONUS}", Mathf.RoundToInt(equipBlockBonus * 100).ToString());
        tip = tip.Replace("{EQUIPCRITBONUS}", Mathf.RoundToInt(equipCritBonus * 100).ToString());
        tip = tip.Replace("{USAGEHP}", usageHp.ToString());
        tip = tip.Replace("{USAGEMP}", usageMp.ToString());
        tip = tip.Replace("{USAGEEXP}", usageExp.ToString());
        tip = tip.Replace("{DESTROYABLE}", (destroyable ? "Yes" : "No"));
        tip = tip.Replace("{SELLABLE}", (sellable ? "Yes" : "No"));
        tip = tip.Replace("{TRADABLE}", (tradable ? "Yes" : "No"));
        tip = tip.Replace("{MINLEVEL}", minLevel.ToString());
        tip = tip.Replace("{BUYPRICE}", buyPrice.ToString());
        tip = tip.Replace("{SELLPRICE}", sellPrice.ToString());
        tip = tip.Replace("{AMOUNT}", amount.ToString());
        return tip;
    }
}


public class SyncListItem : SyncListStruct<Item> { }