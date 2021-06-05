namespace ReGaSLZR.EndlessRunner.Model
{

    using NaughtyAttributes;
    using UniRx;
    using Zenject;

    #region Interface Declarations

    public interface PlayerStatsGetter
    {
        public IReadOnlyReactiveProperty<GameStatus> GetGameStatus();
        public IReadOnlyReactiveProperty<int> GetPlayerTime();
        public IReadOnlyReactiveProperty<int> GetPlayerScore();
    }

    public interface PlayerStatsSetter
    {
        public void SetGameStatus(GameStatus status);
        public void AddScore(int additionalScore);
    }

    #endregion

    public class PlayerModel : MonoInstaller,
        PlayerStatsGetter, PlayerStatsSetter
    {

        #region Private Variables

        private ReactiveProperty<GameStatus> gameStatus
            = new ReactiveProperty<GameStatus>(GameStatus.InPlay);

        private ReactiveProperty<int> time
            = new ReactiveProperty<int>(0);

        private ReactiveProperty<int> score
            = new ReactiveProperty<int>(0);

        #endregion

        #region Callbacks

        public override void InstallBindings()
        {
            Container.Bind<PlayerStatsGetter>().FromInstance(this);
            Container.Bind<PlayerStatsSetter>().FromInstance(this);
        }

        #endregion

        #region Class Implementation

        //[Button]
        //private void StartGame()
        //{
        //    SetGameStatus(GameStatus.InPlay);
        //}

        //[Button]
        //private void EndGame()
        //{
        //    SetGameStatus(GameStatus.GameOver);
        //}

        #endregion

        #region Getter Interface Implementation

        public IReadOnlyReactiveProperty<int> GetPlayerScore()
        {
            return score;
        }

        public IReadOnlyReactiveProperty<GameStatus> GetGameStatus()
        {
            return gameStatus;
        }

        public IReadOnlyReactiveProperty<int> GetPlayerTime()
        {
            return time;
        }

        #endregion

        #region Setter Interface Implementation

        public void AddScore(int additionalScore)
        {
            if (additionalScore < 0)
            {
                return;
            }

            score.Value += additionalScore;
        }

        public void SetGameStatus(GameStatus status)
        {
            gameStatus.Value = status;
        }

        #endregion

    }

}