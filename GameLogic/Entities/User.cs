namespace GameLogic.Entities
{
    public class User
    {
        public User(int gameBalance)
        {
            GameBalance = gameBalance;
        }

        public int GameBalance { get; set; }
    }
}
