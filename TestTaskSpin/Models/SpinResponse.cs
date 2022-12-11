namespace TestTaskSpin.Models
{
    public class SpinResponse
    {
        public int[] Reel { get; set; }

        public int WinScores { get; set; }

        public int PlayerBalance { get; set; }

        public SpinResponse(int[] reel, int winScores, int playerBalance)
        {
            Reel = reel;
            WinScores = winScores;
            PlayerBalance = playerBalance;
        }
    }
}
