namespace ReGaSLZR.EndlessRunner.Controller
{
    using Base;
    using Model;
    using Model.Settings;

    using NaughtyAttributes;
    using System.Collections;
    using TMPro;
    using UnityEngine;
    using UniRx;
    using Zenject;

    /// <summary>
    /// Controls the execution of gameplay sequences 
    /// [1] Cutscene
    /// [2] Countdown to availability of Destructive Power (Skill)
    /// </summary>
    public class GameplaySequenceController : ReactiveMonoBehaviour
    {

        [Inject]
        private PlayerStatsGetter playerStatsGetter;

        [Inject]
        private PlayerStatsSetter playerStatsSetter;

        [Inject]
        private GeneralSettings generalSettings;

        [Inject]
        private SkillSettings skillSettings;

        [SerializeField]
        [Required]
        private CanvasGroup groupUIFirstBreakSkillRefill;

        [SerializeField]
        [Required]
        private TextMeshProUGUI textFirstBreakSkillRefillCountdown;

        protected override void RegisterObservables()
        {
            playerStatsGetter.GetGameStatus()
                .Where(status => status == GameStatus.InPlay)
                .Where(_ => playerStatsGetter.GetPlayerTime().Value == 0)
                .Subscribe(_ => StartCoroutine(CorStartCountdownToBreakSkillUsage()))
                .AddTo(disposablesBasic);
        }

        private IEnumerator CorStartCountdownToBreakSkillUsage()
        {
            playerStatsSetter.SetDestructionPowerUseCount(0);

            groupUIFirstBreakSkillRefill.gameObject.SetActive(true);
            groupUIFirstBreakSkillRefill.alpha = 1f;
            int countdown = generalSettings.FirstBreakSkillRefillCountdown;

            while (countdown >= 0)
            {
                textFirstBreakSkillRefillCountdown.text = 
                    (countdown > 0) ? countdown.ToString() : "GO!";
                yield return new WaitForSeconds(1);
                countdown--;
                playerStatsSetter.SetDestructionPowerUseCount(0);
            }

            playerStatsSetter.SetDestructionPowerUseCount(
                skillSettings.DestructivePowerUseMaxCount);

            groupUIFirstBreakSkillRefill.alpha = 0f;
            groupUIFirstBreakSkillRefill.gameObject.SetActive(false);
            enabled = false;
        }
    }

}