namespace ReGaSLZR.EndlessRunner.Movement
{

    using UnityEngine;
    using UniRx;
    using UniRx.Triggers;

    public class PlayerMovementConstant : BaseMovement
    {

        #region Inspector Variables

        [Header("Calibration")]

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
        }

        #endregion

    }

}