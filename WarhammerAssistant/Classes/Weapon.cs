namespace WarhammerAssistant;

public class Weapon
{
    public string WeaponName { get; }
    public bool IsMelee { get; }
    public List<string> Keywords { get; }
    public int Range { get; }
    public string Attacks { get; }
    public int Skill { get; } //WS (Weapon Skill) for melee, BS (Ballistic Skill) for ranged
    public int Strength { get; }
    public int ArmorPenetration { get; }
    public string Damage { get; }
    public Weapon(
        string WeaponName,
        bool IsMelee,
        List<string> Keywords,
        int Range,
        string Attacks,
        int Skill,
        int Strength,
        int ArmorPenetration,
        string Damage
    )
    {
        this.WeaponName = WeaponName;
        this.IsMelee = IsMelee;
        this.Keywords = Keywords;
        this.Range = Range;
        this.Attacks = Attacks;
        this.Skill = Skill;
        this.Strength = Strength;
        this.ArmorPenetration = ArmorPenetration;
        this.Damage = Damage;
    }
}
