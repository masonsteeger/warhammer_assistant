namespace WarhammerAssistant;

public class Utility
{
    public static int FormatAddNumberEntry(int currentAmount, int additionEntry)
    {
        return Int32.Parse(currentAmount.ToString() + additionEntry.ToString());
    }
    public static int FormatRemoveNumberEntry(int currentAmount)
    {
        return Int32.Parse(currentAmount.ToString().Length > 1 ? currentAmount.ToString().Substring(0, currentAmount.ToString().Length - 1) : "0");
    }
}
