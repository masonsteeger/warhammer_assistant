﻿namespace WarhammerAssistant;

public class Unit
{
    public string UnitName { get; }
    public int TotalModels { get; }
    public int RemainingModels { get; private set; }
    public int AdditionalCurrentWounds { get; private set; }
    public bool IsDestroyed { get; private set; }
    public int WoundsPerModel { get; }
    public bool IsBattleShocked { get; private set; }
    public List<Unit> IsLockedInCombat { get; private set; }
    public bool CanShoot { get; private set; }
    public bool CanCharge { get; private set; }
    public bool IsInReserves { get; private set; }
    public bool IsAttached { get; private set; }
    public List<Unit>? AttachedUnits { get; }
    public List<Weapon> RangedWeapons { get; }
    public List<Weapon> MeleeWeapons { get; }
    public int Movement { get; }
    public int Toughness { get; }
    public int Save { get; }
    public int Leadership { get; }
    public int ObjectiveControl { get; }
    public bool IsWarlord { get; }
    public bool IsLeader { get; }
    public bool HasFightFirst { get; }
    public Unit(
        string UnitName,
        int TotalModels,
        List<Weapon> RangedWeapons,
        List<Weapon> MeleeWeapons,
        int Movement,
        int Toughness,
        int Save,
        int WoundsPerModel,
        int Leadership,
        int ObjectiveControl,
        bool IsWarlord,
        bool IsLeader,
        bool HasFightFirst
    )
    {
        this.UnitName = UnitName;
        this.TotalModels = TotalModels;
        RemainingModels = TotalModels;
        this.AdditionalCurrentWounds = 0;
        this.IsDestroyed = false;
        this.WoundsPerModel = WoundsPerModel;
        IsBattleShocked = false;
        IsLockedInCombat = null;
        CanShoot = true;
        CanCharge = true;
        IsLockedInCombat = new List<Unit>();
        this.RangedWeapons = RangedWeapons;
        this.MeleeWeapons = MeleeWeapons;
        this.Movement = Movement;
        this.Toughness = Toughness;
        this.Save = Save;
        this.Leadership = Leadership;
        this.ObjectiveControl = ObjectiveControl;
        this.IsWarlord = IsWarlord;
        this.IsLeader = IsLeader;
        this.HasFightFirst = HasFightFirst;
    }
    public Unit(
        string UnitName,
        int TotalModels,
        int WoundsPerModel,
        List<Weapon> RangedWeapons,
        List<Weapon> MeleeWeapons,
        int Movement,
        int Toughness,
        int Save,
        int Leadership,
        int ObjectiveControl,
        bool IsWarlord,
        bool IsLeader,
        bool HasFightFirst,
        List<Unit> AttachedUnits
    ) : this(
        UnitName,
        TotalModels,
        RangedWeapons,
        MeleeWeapons,
        Movement,
        Toughness,
        Save,
        WoundsPerModel,
        Leadership,
        ObjectiveControl,
        IsWarlord,
        IsLeader,
        HasFightFirst)
    {
        this.AttachedUnits = AttachedUnits;
        foreach (Unit AttachedUnit in AttachedUnits)
        {
            AttachedUnit.IsAttached = true;
        }
    }
    public void Moved()
    {
        this.IsLockedInCombat = null;
        this.CanShoot = true;
        this.CanCharge = true;
    }
    public void Advanced()
    {
        this.IsLockedInCombat = null;
        this.CanShoot = false;
        this.CanCharge = false;
    }
    public void FellBack()
    {
        this.IsLockedInCombat = null;
        this.CanShoot = false;
        this.CanCharge = false;
    }
    public void RemainedStationary()
    {
        this.CanShoot = true;
        this.CanCharge = true;
    }

    public void EngageInMelee(Unit unit)
    {
        this.IsLockedInCombat.Add(unit);
    }
    public void LeaveMeleeCombat(Unit unit)
    {
        this.IsLockedInCombat.Remove(unit);
    }
    public void ResolveAttack(int modelsDestroyed, int additionalWounds)
    {
        this.RemainingModels -= modelsDestroyed;
        if (this.RemainingModels <= 0)
        {
            this.IsDestroyed = true;
            this.RemainingModels = 0;
            this.AdditionalCurrentWounds = 0;
        }
        else
        {
            if (additionalWounds + this.AdditionalCurrentWounds < WoundsPerModel)
            {
                this.AdditionalCurrentWounds += additionalWounds;
            }
            else if (additionalWounds + this.AdditionalCurrentWounds >= WoundsPerModel)
            {
                this.AdditionalCurrentWounds = WoundsPerModel;
                this.RemainingModels -= 1;
                if (this.RemainingModels <= 0)
                {
                    this.RemainingModels = 0;
                    this.IsDestroyed = true;
                }

            }
            else
            {
                this.AdditionalCurrentWounds = 0;
            }
        }

    }
}
