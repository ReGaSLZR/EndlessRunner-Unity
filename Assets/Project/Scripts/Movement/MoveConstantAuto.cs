namespace ReGaSLZR.EndlessRunner.Movement
{

    using Model;
    using Holder;
    using Utils;

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
        [Layer]
        private int invincibilityExceptionLayer;

        [SerializeField]
        private AnimationsHolder animHolder;

        [SerializeField]
        private Vector3 moveDirection;

        #endregion

        #region Class Implementation

        protected override void RegisterObservables()
        {
            this.FixedUpdateAsObservable()
              .Where(_ => playerStats.GetGameStatus().Value
                         == GameStatus.InPlay)
              .Subscribe(_ => compRigidbody.position +=
                  (moveDirection * playerSettings.ConstantAccel *
                  Time.fixedDeltaTime))
              .AddTo(disposablesBasic);

            if (signalDetector != null)
            {
                signalDetector.IsTriggered
                    .Where(isDead => isDead)
                    .Subscribe(_ => OnMeetDeathInTheFace())
                    .AddTo(disposablesBasic);
            }
        }

        private void OnMeetDeathInTheFace()
        {
            if (playerStats.IsPlayerInvincible().Value)
            {
                if (signalDetector.CachedTarget.layer !=
                    invincibilityExceptionLayer)
                {
                    signalDetector.CachedTarget.SetActive(false);
                }
                else 
                {
                    LogUtil.PrintInfo(gameObject, GetType(),
                        "Player is Invincible but hit exception layer. Not disabling collided object.");
                }
            }
            else
            {
                if (animHolder != null)
                {
                    animHolder.Die();
                }
                
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