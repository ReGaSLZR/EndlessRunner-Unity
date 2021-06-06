namespace ReGaSLZR.EndlessRunner.Skill.Random
{

    using UniRx;

    public class RandomDestructivePowerRefillSkill : BaseRandomExecSkill
    {

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
            //NOTE: Nothing to do here...
        }

        protected override void TurnOn()
        {
            playerStatSetter.SetDestructionPowerUseCount(
                skillSettings.DestructivePowerUseMaxCount);
        }

    }

}