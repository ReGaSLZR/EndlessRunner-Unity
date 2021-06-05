namespace ReGaSLZR.EndlessRunner.Movement
{
    
    using Holder;

    using NaughtyAttributes;
    using UnityEngine;
    using UniRx;
    using UniRx.Triggers;

    /// <summary>
    /// Handles the constant movement as well as the termination
    /// of such movement.
    /// </summary>
    public class MoveConstantAuto : BaseMovement
    {

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
              .AddTo(disposables);

            signalDetector.IsTriggered
                .Where(isDead => isDead)
                .Subscribe(_ => {
                    animHolder.Die();
                    this.enabled = false;
                })
                .AddTo(disposables);
        }

        #endregion

    }

}