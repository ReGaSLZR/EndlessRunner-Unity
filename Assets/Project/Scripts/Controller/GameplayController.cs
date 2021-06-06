namespace ReGaSLZR.EndlessRunner.Controller
{
    using Base;
    using Model;
    using Model.Settings;
    
    using UnityEngine;
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