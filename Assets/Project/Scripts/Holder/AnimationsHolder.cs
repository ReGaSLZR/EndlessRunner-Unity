namespace ReGaSLZR.EndlessRunner.Holder
{

    using NaughtyAttributes;
    using System.Collections;
    using UnityEngine;

    public class AnimationsHolder : MonoBehaviour
    {

        #region Inspector Variables

        [SerializeField]
        [Required]
        private Animator compAnimator;

        //[SerializeField]
        //[AnimatorParam("compAnimator")]
        //private string paramJump;

        [SerializeField]
        [AnimatorParam("compAnimator")]
        private string paramSlide;

        [SerializeField]
        private float slideDuration = 2f;

        #endregion

        public void Slide(bool isSliding)
        {
            compAnimator.SetBool(paramSlide, isSliding);
        }

    }

}