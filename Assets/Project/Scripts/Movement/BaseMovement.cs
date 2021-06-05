namespace ReGaSLZR.EndlessRunner.Movement
{

    using Base;
    using Model;
    using Detector;

    using NaughtyAttributes;
    using UnityEngine;
    using UniRx;
    using Zenject;
    
    public abstract class BaseMovement : ReactiveMonoBehaviour
    {

        [Inject]
        private PlayerStatsGetter playerStats;

        #region Inspector Variables

        [SerializeField]
        protected bool isPlayer;

        [Space]

        [SerializeField]
        [Required]
        protected Rigidbody compRigidbody;

        [SerializeField]
        [Required]
        protected CollisionDetector signalDetector;

        #endregion

        protected virtual void Start()
        {
            if (isPlayer)
            {
                playerStats.GetGameStatus()
                    .Subscribe(status => 
                        this.enabled = (status == GameStatus.InPlay))
                    .AddTo(disposablesTerminal);
            }
        }

    }


}