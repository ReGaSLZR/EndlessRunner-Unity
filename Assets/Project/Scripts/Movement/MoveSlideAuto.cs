namespace ReGaSLZR.EndlessRunner.Movement
{

    using Holder;
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

        [SerializeField]
        [Required]
        private AnimationsHolder animHolder;

        [SerializeField]
        [Required]
        private new BoxCollider collider;

        [SerializeField]
        private float scaleYNormal;

        [SerializeField]
        private float scaleYOnSlide;

        #endregion

        protected override void RegisterObservables()
        {
            slideSignalDetector.IsTriggered
                .Subscribe(isTriggered => {
                    animHolder.Slide(isTriggered);
                    collider.size = new Vector3(
                        collider.size.x,
                        isTriggered ? scaleYOnSlide : scaleYNormal,
                        collider.size.z);
                })
                .AddTo(disposables);
        }

    }

}