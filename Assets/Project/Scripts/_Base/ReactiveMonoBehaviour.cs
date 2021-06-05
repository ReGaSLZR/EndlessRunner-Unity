namespace ReGaSLZR.EndlessRunner.Base
{
   
    using UnityEngine;
    using UniRx;

    public abstract class ReactiveMonoBehaviour : MonoBehaviour
    {

        #region Protected Variables

        protected CompositeDisposable disposables
            = new CompositeDisposable();

        #endregion

        #region Unity Callbacks

        protected virtual void OnEnable()
        {
            RegisterObservables();
        }

        protected virtual void OnDisable()
        {
            disposables.Clear();
        }

        #endregion

        protected abstract void RegisterObservables();

    }

}