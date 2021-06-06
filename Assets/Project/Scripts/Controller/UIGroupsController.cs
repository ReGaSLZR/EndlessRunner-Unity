namespace ReGaSLZR.EndlessRunner.Controller
{

    using Base;
    using Model;

    using NaughtyAttributes;
    using UnityEngine;
    using UniRx;
    using Zenject;

    public class UIGroupsController : ReactiveMonoBehaviour
    {

        [Inject]
        private PlayerStatsGetter playerStats;

        [SerializeField]
        [Required]
        private CanvasGroup groupHUD;

        [SerializeField]
        [Required]
        private CanvasGroup groupGameOver;

        [SerializeField]
        [Required]
        private CanvasGroup groupLoading;

        [SerializeField]
        [Required]
        private CanvasGroup groupPaused;

        protected override void RegisterObservables()
        {
            playerStats.GetGameStatus()
                .Subscribe(status => ChangeActiveGroup(status))
                .AddTo(disposablesBasic);
        }

        private void ChangeActiveGroup(GameStatus status)
        {
            DisableAllGroups();

            switch (status)
            {
                case GameStatus.GameOver:
                    {
                        groupGameOver.gameObject.SetActive(true);
                        break;
                    }
                case GameStatus.InPlay:
                    {
                        groupHUD.gameObject.SetActive(true);
                        break;
                    }
                case GameStatus.Loading:
                    {
                        groupLoading.gameObject.SetActive(true);
                        break;
                    }
                case GameStatus.Paused:
                    {
                        groupPaused.gameObject.SetActive(true);
                        break;
                    }
            }
        }

        private void DisableAllGroups()
        {
            groupHUD.gameObject.SetActive(false);
            groupGameOver.gameObject.SetActive(false);
            groupLoading.gameObject.SetActive(false);
            groupPaused.gameObject.SetActive(false);
        }

    }

}