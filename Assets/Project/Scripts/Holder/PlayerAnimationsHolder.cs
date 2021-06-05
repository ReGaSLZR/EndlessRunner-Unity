namespace ReGaSLZR.EndlessRunner.Holder
{

    using NaughtyAttributes;
    using System.Collections;
    using UnityEngine;

    public class PlayerAnimationsHolder : MonoBehaviour
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

        public void Slide()
        {
            StopAllCoroutines();
            StartCoroutine(CorSlide());
        }

        private IEnumerator CorSlide()
        {
            compAnimator.SetBool(paramSlide, true);
            yield return new WaitForSeconds(slideDuration);
            compAnimator.SetBool(paramSlide, false);
        }

    }

}