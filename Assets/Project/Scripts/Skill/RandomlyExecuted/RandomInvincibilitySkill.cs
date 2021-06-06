namespace ReGaSLZR.EndlessRunner.Skill.Random
{
    
    using UniRx;

    public class RandomInvincibilitySkill : BaseRandomExecSkill
    {

        protected override void RegisterObservables()
        {
            if (!isChosen)
            {
                return;
            }

            base.RegisterObservables();
            AttachRandomExecutionDisposable(
                Observable.Interval(ONE_SECOND)
                .Where(_ => !playerStat.IsPlayerInvincible().Value));
        }

        protected override void TurnOff()
        {
            playerStatSetter.SetInvincibility(false);
        }

        protected override void TurnOn()
        {
            playerStatSetter.SetInvincibility(true);
        }

    }

}