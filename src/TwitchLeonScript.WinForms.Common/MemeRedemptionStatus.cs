namespace TwitchLeonScript.WinForms.Common
{
    public static class MemeRedemptionStatus
    {
        public const string Init = "Initialization"; // 0
        public const string InProgress = "In progress"; // 1
        public const string Resolved = "Bonus {0} points sent"; // 2
        public const string Error = "Error: {0}"; // 3
        public const string UserNotFound = "User not found, bonus returned"; // 4

        //public const string TwitchAuthPending = "Twitch Authorization Pending";
        //public const string MemeAuthPending = "Meme Authorization Pending";
    }
}
