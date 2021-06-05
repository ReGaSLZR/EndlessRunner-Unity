namespace ReGaSLZR.EndlessRunner.Trigger
{

    using Base;
    using Utils;

    using NaughtyAttributes;
    using System;
    using UnityEngine;
    using UniRx;
    using UniRx.Triggers;

    /// <summary>
    /// Uses a collider's OnCollisionX and OnTriggerX to detect
    /// interaction with a specified tag. Notifies any subscriber
    /// for the change in value.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class Detector : ReactiveMonoBehaviour
    {

        #region Inspector Variables

        [SerializeField]
        [Tag]
        protected string[] tagTargets;

        #endregion

        #region Private Variables

        private ReactiveProperty<bool> isTriggered
            = new ReactiveProperty<bool>(false);

        #endregion

        #region Public Variables

        public IReadOnlyReactiveProperty<bool> IsTriggered
        {
            get
            {
                return isTriggered;
            }
        }

        #endregion

        protected override void RegisterObservables()
        {

            if (tagTargets == null || tagTargets.Length == 0)
            {
                LogUtil.PrintWarning(this, GetType(),
                    "No tag specified. Skipping RegisterObservables() call...");
                return;
            }

            GetDisposableCollision(
                this.OnCollisionEnterAsObservable(), true)
                .AddTo(disposablesBasic);

            GetDisposableCollision(
                this.OnCollisionExitAsObservable(), false)
              .AddTo(disposablesBasic);

            GetDisposableTrigger(
                this.OnTriggerEnterAsObservable(), true)
                .AddTo(disposablesBasic);

            GetDisposableTrigger(
                this.OnTriggerExitAsObservable(), false)
                .AddTo(disposablesBasic);
        }

        private IDisposable GetDisposableCollision(
            IObservable<Collision> observable, bool isTriggered)
        {
            return observable
                .Where(collision => StringUtil.HasEntry(tagTargets, collision.gameObject.tag))
                .Subscribe(collision =>
                    this.isTriggered.Value = isTriggered);
        }

        private IDisposable GetDisposableTrigger(
            IObservable<Collider> observable, bool isTriggered)
        {
            return observable
                .Where(collider => StringUtil.HasEntry(tagTargets, collider.tag))
                .Subscribe(collision =>
                    this.isTriggered.Value = isTriggered);
        }


    }

}