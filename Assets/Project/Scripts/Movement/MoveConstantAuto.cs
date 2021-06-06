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
        private PlayerStatsSetter playerStatsSetter;

        #region Inspector Variables

        [SerializeField]
        [Required]
        private AnimationsHolder animHolder;

        [SerializeField]
        private Vector3 moveDirection;

        #endregion

        #region Class Implementation

        protected override void RegisterObservables()
        {
            this.FixedUpdateAsObservable()
              .Subscribe(_ => compRigidbody.position +=
                  (moveDirection * playerSettings.ConstantAccel *
                  Time.fixedDeltaTime))
              .AddTo(disposablesBasic);

            signalDetector.IsTriggered
                .Where(isDead => isDead)
                .Subscribe(_ => OnMeetDeathInTheFace())
                .AddTo(disposablesBasic);
        }

        private void OnMeetDeathInTheFace()
        {
            if (playerStats.IsPlayerInvincible().Value)
            {
                signalDetector.CachedTarget.SetActive(false);
            }
            else
            {
                animHolder.Die();
                compRigidbody.constraints = RigidbodyConstraints.FreezeAll;

                if (isPlayer)
                {
                    playerStatsSetter.SetGameStatus(GameStatus.GameOver);
                }
            }
        }

        #endregion

    }

}