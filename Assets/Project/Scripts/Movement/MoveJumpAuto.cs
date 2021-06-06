namespace ReGaSLZR.EndlessRunner.Movement
{

    using UnityEngine;
    using UniRx;

    public class MoveJumpAuto : BaseMovement
    {

        protected override void RegisterObservables()
        {
            signalDetector.IsTriggered
                .Where(isTriggered => isTriggered)
                .Subscribe(_ => 
                    compRigidbody.AddForce(Vector3.up * 
                    playerSettings.NormalJumpForce,
                    playerSettings.NormalJumpForceMode))
                .AddTo(disposablesBasic);
        }

    }

}