namespace ReGaSLZR.EndlessRunner.Base
{
   
    using UnityEngine;
    using UniRx;

    public abstract class ReactiveMonoBehaviour : MonoBehaviour
    {

        #region Protected Variables

        protected CompositeDisposable disposablesBasic
            = new CompositeDisposable();

        protected CompositeDisposable disposablesTerminal
            = new CompositeDisposable();

        #endregion

        #region Unity Callbacks

        protected virtual void OnEnable()
        {
            RegisterObservables();
        }

        protected virtual void OnDisable()
        {
            disposablesBasic.Clear();
        }

        protected virtual void OnDestroy()
        {
            disposablesTerminal.Clear();
        }

        #endregion

        protected abstract void RegisterObservables();

    }

}