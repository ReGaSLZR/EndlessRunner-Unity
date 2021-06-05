namespace ReGaSLZR.EndlessRunner.Holder
{

    using NaughtyAttributes;
    using UnityEngine;

    public class AnimationsHolder : MonoBehaviour
    {

        #region Inspector Variables

        [SerializeField]
        [Required]
        private Animator compAnimator;

        [SerializeField]
        [AnimatorParam("compAnimator")]
        private string paramOnDeathTrigger;

        [SerializeField]
        [AnimatorParam("compAnimator")]
        private string paramSlide;

        #endregion

        public void Slide(bool isSliding)
        {
            compAnimator.SetBool(paramSlide, isSliding);
        }

        public void Die()
        {
            compAnimator.SetTrigger(paramOnDeathTrigger);
        }

    }

}