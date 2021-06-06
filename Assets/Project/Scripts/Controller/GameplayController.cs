namespace ReGaSLZR.EndlessRunner.Controller
{
    using Base;
    using Model;
    using Model.Settings;
    using Utils;
    
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UniRx;
    using UniRx.Triggers;
    using Zenject;

    public class GameplayController : ReactiveMonoBehaviour
    {

        [Inject]
        private KeySettings keySettings;

        [Inject]
        private PlayerStatsGetter playerStatsGetter;

        [Inject]
        private PlayerStatsSetter playerStatsSetter;

        protected override void RegisterObservables()
        {
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(keySettings.PauseUnpause))
                .Select(_ => playerStatsGetter.GetGameStatus().Value)
                .Where(status => (status == GameStatus.InPlay) ||
                        (status == GameStatus.Paused))
                .Subscribe(status => PauseUnpauseGame(status))
                .AddTo(disposablesBasic);

            this.UpdateAsObservable()
              .Where(_ => Input.GetKeyDown(keySettings.Reload))
              .Subscribe(_ => {
                  playerStatsSetter.SetGameStatus(GameStatus.Loading);
                  SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
              })
              .AddTo(disposablesBasic);

            this.UpdateAsObservable()
              .Where(_ => Input.GetKeyDown(keySettings.Quit))
              .Subscribe(_ => {
                  playerStatsSetter.SetGameStatus(GameStatus.Loading);
                  LogUtil.PrintInfo(gameObject, GetType(),
                      "Quitting the game... Thanks for playing. -Ren");
                  Application.Quit();
              })
              .AddTo(disposablesBasic);
        }

        private void PauseUnpauseGame(GameStatus status)
        {
            playerStatsSetter.SetGameStatus(
                        (status == GameStatus.InPlay)
                        ? GameStatus.Paused : GameStatus.InPlay);
            Time.timeScale = (status == GameStatus.InPlay) ? 0 : 1;
        }

    }

}