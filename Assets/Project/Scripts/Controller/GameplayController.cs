namespace ReGaSLZR.EndlessRunner.Controller
{
    using Base;
    using Model;
    using Model.Settings;

    using UniRx;
    using Zenject;

    public class GameplayController : ReactiveMonoBehaviour
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
            Observable.Interval(System.TimeSpan.FromSeconds(1))
                .Where(_ => playerStatsGetter.GetGameStatus().Value
                    == GameStatus.InPlay)
                .Where(_ => playerStatsGetter
                    .GetDestructionPowerUseCount().Value < skillSettings.DestructivePowerUseMaxCount)
                .Where(_ => refillTime < 
                    playerStatsGetter.GetPlayerTime().Value)
                .Subscribe(_ => refillTime = skillSettings.DestructivePowerRefillCooldown + playerStatsGetter.GetPlayerTime().Value)
                .AddTo(disposablesBasic);

            playerStatsGetter.GetPlayerTime()
                .Where(time => time == refillTime)
                .Where(_ => playerStatsGetter.GetDestructionPowerUseCount().Value < skillSettings.DestructivePowerUseMaxCount)
                .Subscribe(_ => 
                    playerStatsSetter.AddDestructionPowerUse())
                .AddTo(disposablesBasic);
        }

        private void Start()
        {
            playerStatsSetter.SetDestructionPowerUseCount(skillSettings.DestructivePowerUseMaxCount);
        }
    }

}