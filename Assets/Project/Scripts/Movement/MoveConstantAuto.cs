namespace ReGaSLZR.EndlessRunner.Movement
{

    using Model;
    using Holder;

    using NaughtyAttributes;
    using UnityEngine;
    using UniRx;
    using UniRx.Triggers;
    using Zenject;

    /// <summary>
    /// Handles the constant movement as well as the termination
    /// of such movement.
    /// </summary>
    public class MoveConstantAuto : BaseMovement
    {

        [Inject]
        private PlayerStatsSetter playerStats;

        #region Inspector Variables

        [SerializeField]
        [Required]
        private AnimationsHolder animHolder;

        [SerializeField]
        private Vector3 moveDirection;

        [SerializeField]
        private float accelForward;

        #endregion

        #region Class Implementation

        protected override void RegisterObservables()
        {
            this.FixedUpdateAsObservable()
              .Subscribe(_ => compRigidbody.position +=
                  (moveDirection * accelForward *
                  Time.fixedDeltaTime))
              .AddTo(disposablesBasic);

            signalDetector.IsTriggered
                .Where(isDead => isDead)
                .Subscribe(_ => {
                    animHolder.Die();
                    compRigidbody.constraints = RigidbodyConstraints.FreezeAll;

                    if (isPlayer)
                    {
                        playerStats.SetGameStatus(GameStatus.GameOver);
                    }
                })
                .AddTo(disposablesBasic);
        }

        #endregion

    }

}