namespace ReGaSLZR.EndlessRunner.Movement
{

    using Trigger;

    using NaughtyAttributes;
    using UniRx;
    using UnityEngine;

    public class MoveSlideAuto : BaseMovement
    {

        #region Inspector Variables

        [SerializeField]
        [Required]
        private Detector slideSignalDetector;



        #endregion

        protected override void RegisterObservables()
        {
            slideSignalDetector.IsTriggered
                .Where(isTriggered => isTriggered)
                .Subscribe()
                .AddTo(disposables);
        }

    }

}