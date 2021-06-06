namespace ReGaSLZR.EndlessRunner.Skill.Random
{

    using Model;
    using Model.Settings;

    using NaughtyAttributes;
    using System.Collections;
    using UnityEngine;
    using UniRx;
    using Zenject;

    public abstract class BaseRandomExecSkill : BaseSkill
    {

        [Inject]
        protected PlayerStatsSetter playerStatSetter;

        [Inject]
        protected SkillSettings skillSettings;

        [SerializeField]
        protected string skillName;

        [SerializeField]
        protected bool hasDuration;

        [SerializeField]
        protected bool isChosen;

        [SerializeField]
        [Required]
        protected GameObject fxHolder;

        protected readonly System.TimeSpan ONE_SECOND
            = System.TimeSpan.FromSeconds(1);

        protected int executeTime;

        protected IEnumerator CorSetInvincibility()
        {
            int randomDuration = hasDuration ? Random.Range(
                skillSettings.BonusSkillEffectDurationMin,
                    skillSettings.BonusSkillEffectDurationMax) : 1;

            fxHolder.SetActive(true);
            TurnOn();

            yield return new WaitForSeconds(randomDuration);

            TurnOff();
            fxHolder.SetActive(false);
        }

        protected abstract void TurnOff();
        protected abstract void TurnOn();

        protected override void Start()
        {
            base.Start();
            TurnOff();
        }

        protected override void RegisterObservables()
        {
            if (!isChosen)
            {
                return;
            }

            playerStat.GetPlayerTime()
                .Where(time => time == executeTime)
                .Subscribe(_ => {
                    StopAllCoroutines();
                    StartCoroutine(CorSetInvincibility());
                })
                .AddTo(disposablesBasic);
        }

        protected void AttachRandomExecutionDisposable(System.IObservable<long> observable)
        {
            observable.Where(_ =>
                playerStat.GetPlayerTime().Value > executeTime)
                .Subscribe(_ => executeTime = Random.Range(
                        skillSettings.BonusSkillEffectDelayMin,
                        skillSettings.BonusSkillEffectDelayMax) +
                    playerStat.GetPlayerTime().Value)
                .AddTo(disposablesBasic);
        }

        public void SetIsChosen(bool isChosen)
        {
            this.isChosen = isChosen;
        }

        public string GetSkillName()
        {
            return skillName;
        }

    }

}