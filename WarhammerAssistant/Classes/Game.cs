namespace WarhammerAssistant;

public class Game
{
    public int CurrentRound { get; set; }
    public Phase Phase { get; set; }
    public int CurrentPlayersTurn { get; set; } // index of PlayerOrder list
    public List<Player> PlayerOrder { get; }

    public Game(Player Player1, Player Player2)
    {
        PlayerOrder = new List<Player>
        {
            Player1,
            Player2
        };
        CurrentPlayersTurn = 0;
        CurrentRound = 1;
        Phase = Phase.Command;
    }
}
