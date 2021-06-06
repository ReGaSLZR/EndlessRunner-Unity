namespace ReGaSLZR.EndlessRunner.Controller
{

    using Base;
    using Model;
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

        [Header("UI Elements")]

        [SerializeField]
        private TextMeshProUGUI[] textTimes;

        [SerializeField]
        private TextMeshProUGUI[] textScores;

        [SerializeField]
        private TextMeshProUGUI[] textDestructivePowerUseCounts;

        [SerializeField]
        private TextMeshProUGUI textRandomSkillName;

        [Header("Components")]

        [SerializeField]
        [Required]
        private RandomSkillChooser randomSkillChooser;

        protected override void RegisterObservables()
        {
            playerStats.GetPlayerTime()
                .Subscribe(time => SetTextOnUI(textTimes, time.ToString()))
                .AddTo(disposablesBasic);

            playerStats.GetPlayerScore()
                .Subscribe(score => SetTextOnUI(textScores, score.ToString()))
                .AddTo(disposablesBasic);

            playerStats.GetDestructionPowerUseCount()
                .Subscribe(useCount => SetTextOnUI(textDestructivePowerUseCounts, useCount.ToString()))
                .AddTo(disposablesBasic);
        }

        private void Start()
        {
            textRandomSkillName.text = randomSkillChooser.GetRandomSkillName();
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