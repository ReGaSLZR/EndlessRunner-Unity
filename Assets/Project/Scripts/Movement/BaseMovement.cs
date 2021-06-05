namespace ReGaSLZR.EndlessRunner.Movement
{

    using Base;
    using Trigger;

    using NaughtyAttributes;
    using UnityEngine;
    
    
    public abstract class BaseMovement : ReactiveMonoBehaviour
    {

        #region Inspector Variables

        [SerializeField]
        [Required]
        protected Rigidbody compRigidbody;

        [SerializeField]
        [Required]
        protected Detector signalDetector;

        #endregion

    }


}