namespace ReGaSLZR.EndlessRunner.Movement
{

    using Model;
    using Utils;

    using UniRx;
    using UniRx.Triggers;
    using UnityEngine;

    public class MoveGravityMultiplier : BaseMovement
    {
        protected override void RegisterObservables()
        {
            this.FixedUpdateAsObservable()
                .Where(_ => playerStats.GetGameStatus().Value
                    == GameStatus.InPlay)
               .Select(_ => compRigidbody.velocity)
               .Where(velocity => (velocity.y < 0))
               .Subscribe(_ => {
                   compRigidbody.velocity += PhysicsUtil
                       .GetGravityMultiplier(Vector3.up,
                           playerSettings.GravityMultiplierFallDown);
               })
               .AddTo(disposablesBasic);

            this.FixedUpdateAsObservable()
                .Where(_ => playerStats.GetGameStatus().Value
                    == GameStatus.InPlay)
                .Select(_ => compRigidbody.velocity)
                .Where(velocity => (velocity.y > 0))
                .Subscribe(_ => {
                    compRigidbody.velocity += PhysicsUtil
                        .GetGravityMultiplier(Vector3.up,
                            playerSettings.GravityMultiplierJumpUp);
                })
                .AddTo(disposablesBasic);
        }

    }

}