namespace ReGaSLZR.EndlessRunner.Model
{

    using UniRx;
    using Zenject;

    #region Interface Declarations

    public interface PlayerStatsGetter
    {
        public IReadOnlyReactiveProperty<GameStatus> GetGameStatus();
        public IReadOnlyReactiveProperty<bool> IsPlayerInvincible();
        public IReadOnlyReactiveProperty<int> GetPlayerTime();
        public IReadOnlyReactiveProperty<int> GetPlayerScore();
        public IReadOnlyReactiveProperty<int> GetDestructionPowerUseCount();
    }

    public interface PlayerStatsSetter
    {
        public void SetGameStatus(GameStatus status);
        public void AddScore(int additionalScore);
        public void AddTime(int additionalTime);

        public void SetInvincibility(bool isInvincible);
        public void AddDestructionPowerUse(); 
        public void DecreaseDestructionPowerUse();
        public void SetDestructionPowerUseCount(int count);
    }

    #endregion

    public class GameplayModel : MonoInstaller,
        PlayerStatsGetter, PlayerStatsSetter
    {

        #region Private Variables

        private ReactiveProperty<GameStatus> gameStatus
            = new ReactiveProperty<GameStatus>(GameStatus.NotStarted);

        private ReactiveProperty<bool> isInvincible
            = new ReactiveProperty<bool>(false);

        private ReactiveProperty<int> time
            = new ReactiveProperty<int>(0);

        private ReactiveProperty<int> score
            = new ReactiveProperty<int>(0);

        private ReactiveProperty<int> destructivePowerUseCount
            = new ReactiveProperty<int>(0);

        private CompositeDisposable disposables
            = new CompositeDisposable();

        private readonly System.TimeSpan ONE_SECOND
            = System.TimeSpan.FromSeconds(1);

        #endregion

        #region Callbacks

        public override void InstallBindings()
        {
            Container.Bind<PlayerStatsGetter>().FromInstance(this);
            Container.Bind<PlayerStatsSetter>().FromInstance(this);
        }

        private void OnEnable()
        {
            Observable.Interval(ONE_SECOND)
                .Where(_ => GetGameStatus().Value == GameStatus.InPlay)
                .Subscribe(_ => time.Value += 1)
                .AddTo(disposables);
        }

        private void OnDisable()
        {
            disposables.Clear();
        }

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

        public IReadOnlyReactiveProperty<bool> IsPlayerInvincible()
        {
            return isInvincible;
        }

        public IReadOnlyReactiveProperty<int> GetDestructionPowerUseCount()
        {
            return destructivePowerUseCount;
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

        public void AddTime(int additionalTime)
        {
            if (additionalTime < 0)
            {
                return;
            }

            time.Value += additionalTime;
        }

        public void SetGameStatus(GameStatus status)
        {
            gameStatus.Value = status;
        }

        public void SetInvincibility(bool isInvincible)
        {
            this.isInvincible.Value = isInvincible;
        }

        public void AddDestructionPowerUse()
        {
            destructivePowerUseCount.Value += 1;
        }

        public void DecreaseDestructionPowerUse()
        {
            if (destructivePowerUseCount.Value <= 0)
            {
                return;
            }

            destructivePowerUseCount.Value -= 1;
        }

        public void SetDestructionPowerUseCount(int count)
        {
            destructivePowerUseCount.Value = count;
        }

        #endregion

    }

}