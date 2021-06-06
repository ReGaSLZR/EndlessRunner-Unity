namespace ReGaSLZR.EndlessRunner.Controller
{

    using Base;
    using Model;
    using Model.Settings;
    using Skill.Random;

    using NaughtyAttributes;
    using UnityEngine;
    using UniRx;
    using TMPro;
    using Zenject;

    public class UITextsController : ReactiveMonoBehaviour
    {
        [Inject]
        private PlayerStatsGetter playerStats;

        [Inject]
        private KeySettings keySettings;

        #region Inspector Variables

        [Header("UI Texts on HUD")]

        [SerializeField]
        private TextMeshProUGUI[] textTimes;

        [SerializeField]
        private TextMeshProUGUI[] textScores;

        [SerializeField]
        private TextMeshProUGUI[] textDestructivePowerUseCounts;

        [SerializeField]
        [Required]
        private TextMeshProUGUI textRandomSkillName;

        [Header("UI Texts - Gameplay Terminal Controls")]

        [SerializeField]
        private TextMeshProUGUI[] textUnpause;

        [SerializeField]
        private TextMeshProUGUI[] textReload;

        [SerializeField]
        private TextMeshProUGUI[] textQuit;

        [Header("Components")]

        [SerializeField]
        [Required]
        private RandomSkillChooser randomSkillChooser;

        #endregion

        protected override void RegisterObservables()
        {
            playerStats.GetPlayerTime()
                .Subscribe(time => SetTextOnUI(textTimes, time.ToString()))
                .AddTo(disposablesBasic);

            playerStats.GetPlayerScore()
                .Subscribe(score => SetTextOnUI(textScores, score.ToString()))
                .AddTo(disposablesBasic);

            playerStats.GetDestructionPowerUseCount()
                .Subscribe(useCount => SetTextOnUI(
                    textDestructivePowerUseCounts, useCount.ToString()))
                .AddTo(disposablesBasic);
        }

        private void Start()
        {
            textRandomSkillName.text = 
                randomSkillChooser.GetRandomSkillName();

            SetTextOnUI(textUnpause, "Unpause (" +
                keySettings.PauseUnpause.ToString() + ")");
            SetTextOnUI(textReload, "Reload (" +
                keySettings.Reload.ToString() + ")");
            SetTextOnUI(textQuit, "Quit (" +
                keySettings.Quit.ToString() + ")"); //TODO make strings static or predefine them somewhere else
        }

        private void SetTextOnUI(TextMeshProUGUI[] texts, string content)
        {
            foreach (var text in texts)
            {
                if (text != null)
                {
                    text.text = content;
                }
            }
        }

    }

}