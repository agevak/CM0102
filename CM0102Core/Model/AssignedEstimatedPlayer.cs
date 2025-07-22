namespace CM.Model
{
    public class AssignedEstimatedPlayer
    {
        public EstimatedPlayer EstPlayer { get; set; }
        public PlayerPosition[] AssignedPosition { get; set; } = new PlayerPosition[2];
        public float[] AssignedPositionRating { get => new float[] { EstPlayer.RatingByPosition[0][(int)AssignedPosition[0]], EstPlayer.RatingByPosition[1][(int)AssignedPosition[1]] }; }

        public AssignedEstimatedPlayer(EstimatedPlayer estPlayer)
        {
            EstPlayer = estPlayer;
        }
    }
}
