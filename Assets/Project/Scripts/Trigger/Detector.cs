namespace ReGaSLZR.EndlessRunner.Trigger
{

    using Base;
    using Utils;

    using NaughtyAttributes;
    using System;
    using System.Collections;
    using UnityEngine;
    using UniRx;
    using UniRx.Triggers;

    public enum SingleTriggerType
    { 
        Enter,
        Stay,
        Exit,
    }

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
        private SingleTriggerType triggerType;

        [SerializeField]
        [Tag]
        protected string tagTarget;

        [SerializeField]
        private bool isSingleUse = false;

        [SerializeField]
        private bool doesSelfReset = false;

        [SerializeField]
        [ShowIf("doesSelfReset")]
        private float selfResetDelay = 2.5f;

        #endregion

        #region Private Variables

        private ReactiveProperty<bool> isTriggered
            = new ReactiveProperty<bool>(false);

        private GameObject cachedDetectedTarget;

        #endregion

        #region Public Variables

        public IReadOnlyReactiveProperty<bool> IsTriggered
        {
            get
            {
                return isTriggered;
            }
        }

        public GameObject DetectedTarget
        { 
            get 
            {
                return cachedDetectedTarget;
            } 
        }

        #endregion

        protected override void RegisterObservables()
        {

            if (string.IsNullOrEmpty(tag))
            {
                LogUtil.PrintWarning(this, GetType(), 
                    "No tag specified. Skipping RegisterObservables() call...");
                return;
            }

            GetCollisionObservable()
              .Where(collision => collision.gameObject.CompareTag(tagTarget))
              .Subscribe(collision => ExecuteAndCheckUse(collision.gameObject))
              .AddTo(disposables);

            GetTriggerObservable()
                .Where(collider => collider.CompareTag(tagTarget))
                .Subscribe(collider => ExecuteAndCheckUse(collider.gameObject))
                .AddTo(disposables);

            if (doesSelfReset)
            {
                isTriggered.Where(val => val)
                    .Subscribe(_ => {
                            StopAllCoroutines();
                            StartCoroutine(CorResetTrigger());
                        })
                    .AddTo(disposables);
            }
        }

        private IEnumerator CorResetTrigger()
        {
            yield return new WaitForSeconds(selfResetDelay);
            isTriggered.Value = false;
        }

        private void ExecuteAndCheckUse(GameObject target)
        {
            isTriggered.Value = true;
            cachedDetectedTarget = target;

            if (isSingleUse)
            {
                gameObject.SetActive(false);
            }
        }

        private IObservable<Collider> GetTriggerObservable()
        {
            switch (triggerType)
            {
                case SingleTriggerType.Enter:
                    {
                        return this.OnTriggerEnterAsObservable();
                    }
                case SingleTriggerType.Stay:
                    {
                        return this.OnTriggerStayAsObservable();
                    }
                case SingleTriggerType.Exit:
                default:
                    {
                        return this.OnTriggerExitAsObservable();
                    }
            }
        }

        private IObservable<Collision> GetCollisionObservable()
        {
            switch (triggerType)
            {
                case SingleTriggerType.Enter:
                    {
                        return this.OnCollisionEnterAsObservable();
                    }
                case SingleTriggerType.Stay:
                    {
                        return this.OnCollisionStayAsObservable();
                    }
                case SingleTriggerType.Exit:
                default:
                    {
                        return this.OnCollisionExitAsObservable();
                    }
            }
        }

    }

}