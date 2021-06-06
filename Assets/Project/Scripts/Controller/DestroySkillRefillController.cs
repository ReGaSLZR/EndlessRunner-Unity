namespace ReGaSLZR.EndlessRunner.Controller
{
    using Base;
    using Model;
    using Model.Settings;

    using UniRx;
    using Zenject;

    /// <summary>
    /// Controls when the Player's Destruction Power is refilled
    /// bit by bit during gameplay.
    /// </summary>
    public class DestroySkillRefillController : ReactiveMonoBehaviour
    {

        [Inject]
        private PlayerStatsGetter playerStatsGetter;

        [Inject]
        private PlayerStatsSetter playerStatsSetter;

        [Inject]
        private SkillSettings skillSettings;

        private int refillTime;

        protected override void RegisterObservables()
        {
            //Set future refill time when the current Destruction Power is below Max
            Observable.Interval(System.TimeSpan.FromSeconds(1))
                .Where(_ => playerStatsGetter.GetGameStatus().Value
                    == GameStatus.InPlay)
                .Where(_ => playerStatsGetter.GetDestructionPowerUseCount().Value
                    < skillSettings.DestructivePowerUseMaxCount)
                .Where(_ => refillTime <
                    playerStatsGetter.GetPlayerTime().Value)
                .Subscribe(_ => refillTime =
                    skillSettings.DestructivePowerRefillCooldown +
                    playerStatsGetter.GetPlayerTime().Value)
                .AddTo(disposablesBasic);

            //Add a Destruction Power usage when the future refill time is met.
            playerStatsGetter.GetPlayerTime()
                .Where(time => time == refillTime)
                .Where(_ => playerStatsGetter.GetDestructionPowerUseCount().Value
                    < skillSettings.DestructivePowerUseMaxCount)
                .Subscribe(_ =>
                    playerStatsSetter.AddDestructionPowerUse())
                .AddTo(disposablesBasic);
        }

    }

}