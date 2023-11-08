namespace WarhammerAssistant;

public class AttackInstance
{
    public Unit Attacker { get; set; }
    public Unit Defender { get; set; }
    public int NumberOfAttacks { get; set; }
    public int NumberOfHits { get; set; }
    public int NumberOfSaves { get; set; } // This is the number of successful saves made by the defender
    public int NumberOfRegularWounds { get; set; } // Regular wounds can only be applied to one model and do not overflow to other models in the unit
    public int NumberOfMortalWounds { get; set; } // Mortal wounds are wounds that ignore saves and overflow to other models in the unit
}
