namespace ReGaSLZR.EndlessRunner.Movement
{

    using Trigger;
    
    using NaughtyAttributes;
    using UnityEngine;
    using UniRx;

    public class PlayerMovementJump : BaseMovement
    {

        #region Inspector Variables

        [SerializeField]
        [Required]
        private Detector jumpSignalDetector;

        [SerializeField]
        private float jumpPower;

        [SerializeField]
        private ForceMode forceMode;

        #endregion

        protected override void RegisterObservables()
        {
            jumpSignalDetector.IsTriggered
                .Where(isTriggered => isTriggered)
                .Subscribe(_ => 
                    compRigidbody.AddForce(Vector3.up * jumpPower, forceMode))
                .AddTo(disposables);
        }

    }

}