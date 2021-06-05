namespace ReGaSLZR.EndlessRunner.Movement
{

    using Base;

    using NaughtyAttributes;
    using UnityEngine;
    
    
    public abstract class BaseMovement : ReactiveMonoBehaviour
    {

        #region Inspector Variables

        [SerializeField]
        [Required]
        protected Rigidbody compRigidbody;

        #endregion

    }


}