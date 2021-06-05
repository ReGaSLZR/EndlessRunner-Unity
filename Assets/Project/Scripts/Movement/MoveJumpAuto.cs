namespace ReGaSLZR.EndlessRunner.Movement
{

    using UnityEngine;
    using UniRx;

    public class MoveJumpAuto : BaseMovement
    {

        #region Inspector Variables

        [SerializeField]
        private float jumpPower;

        [SerializeField]
        private ForceMode forceMode;

        #endregion

        protected override void RegisterObservables()
        {
            signalDetector.IsTriggered
                .Where(isTriggered => isTriggered)
                .Subscribe(_ => 
                    compRigidbody.AddForce(Vector3.up * jumpPower, forceMode))
                .AddTo(disposables);
        }

    }

}