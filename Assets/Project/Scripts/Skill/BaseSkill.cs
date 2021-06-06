namespace ReGaSLZR.EndlessRunner.Skill
{

    using Base;
    using Model;

    using UniRx;
    using Zenject;

    public abstract class BaseSkill : ReactiveMonoBehaviour
    {
        [Inject]
        protected PlayerStatsGetter playerStat;

        protected virtual void Start()
        {
            playerStat.GetGameStatus()
                .Subscribe(status => enabled = 
                    (status == GameStatus.InPlay))
                .AddTo(disposablesTerminal);
        }

    }

}