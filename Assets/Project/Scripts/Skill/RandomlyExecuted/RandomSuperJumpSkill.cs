namespace ReGaSLZR.EndlessRunner.Skill.Random
{

    using NaughtyAttributes;
    using UniRx;
    using UnityEngine;

    public class RandomSuperJumpSkill : BaseRandomExecSkill
    {

        [SerializeField]
        [Required]
        private Rigidbody compRigidbody;

        protected override void RegisterObservables()
        {
            if (!isChosen)
            {
                return;
            }

            base.RegisterObservables();
            AttachRandomExecutionDisposable(
                Observable.Interval(ONE_SECOND));
        }

        protected override void TurnOff()
        {
            //NOTE: Nothing to do here.
        }

        protected override void TurnOn()
        {
            compRigidbody.AddForce(Vector3.up * 
                skillSettings.SuperJumpForce, 
                skillSettings.SuperJumpForceMode);   
        }

    }

}