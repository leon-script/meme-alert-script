namespace MemeAlertsScript.Core.Models
{
    public class CreateMemeReward
    {
        public string? Name { get; set; }
        public string? Prompt { get; set; }
        public int TwitchCost { get; set; }
        public int MemeCost { get; set; }
    }
}
