namespace WarhammerAssistant;

public class Player
{
    public List<Unit> Units { get; }
    public int CommandPoints { get; set; }
    public int VictoryPoints { get; set; }
    public List<Unit> UnitsToAct { get; }
    public string PlayerName { get; }

    public Player(string PlayerName, List<Unit> Units)
    {
        this.PlayerName = PlayerName;
        this.Units = Units;
        CommandPoints = 0;
        VictoryPoints = 0;
        this.UnitsToAct = new List<Unit>();
    }
    public void AddUnitsToAct(List<Unit> units)
    {
        UnitsToAct.AddRange(units);
    }
}
