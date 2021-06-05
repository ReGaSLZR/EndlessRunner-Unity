namespace ReGaSLZR.EndlessRunner.Skill
{

    using Base;
    using Model;

    using UnityEngine;
    using UniRx;
    using Zenject;

    public abstract class BaseSkill : ReactiveMonoBehaviour
    {
        [Inject]
        private PlayerStatsGetter playerStat;

        protected virtual void Start()
        {
            playerStat.GetGameStatus()
                .Subscribe(status => enabled = 
                    (status == GameStatus.InPlay))
                .AddTo(disposablesTerminal);
        
        }

    }

}