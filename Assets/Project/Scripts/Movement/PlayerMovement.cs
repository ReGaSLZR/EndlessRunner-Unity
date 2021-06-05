namespace ReGaSLZR.Movement
{

    using NaughtyAttributes;
    using UnityEngine;
    using UniRx;
    using UniRx.Triggers;
    using Zenject;

    public class PlayerMovement : MonoBehaviour
    {

        #region Inspector Variables

        [Header("Components")]

        [SerializeField]
        [Required]
        private Rigidbody compRigidbody;

        [Header("Calibration")]

        [SerializeField]
        private Vector3 moveDirection;

        [SerializeField]
        private float accelForward;

        #endregion

        #region Private Variables

        private CompositeDisposable disposables = new CompositeDisposable();

        #endregion

        #region Unity Callbacks

        private void OnEnable()
        {
            RegisterObservables();   
        }

        private void OnDisable()
        {
            disposables.Clear();
        }

        #endregion

        #region Class Implementation

        private void RegisterObservables()
        {
            this.FixedUpdateAsObservable()
              .Subscribe(_ => compRigidbody.position +=
                  (moveDirection *
                  accelForward *
                  Time.fixedDeltaTime))
              .AddTo(disposables);

        }

        #endregion

    }

}