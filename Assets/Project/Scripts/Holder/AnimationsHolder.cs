namespace ReGaSLZR.EndlessRunner.Holder
{

    using NaughtyAttributes;
    using UnityEngine;

    /// <summary>
    /// Serves as the go-to component for
    /// animation executions as it holds the reference
    /// to the Animator and the Parameters.
    /// </summary>
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