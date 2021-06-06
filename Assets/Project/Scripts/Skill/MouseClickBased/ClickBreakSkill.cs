namespace ReGaSLZR.EndlessRunner.Skill.Click
{

    using Model;
    using Model.Settings;

    using NaughtyAttributes;
    using UniRx;
    using UniRx.Triggers;
    using UnityEngine;

    using Zenject;

    public class ClickBreakSkill : BaseSkill
    {

        [Inject]
        private PlayerStatsSetter playerStatSetter;

        [Inject]
        private KeySettings keySettings;

        [SerializeField]
        [Layer]
        private int[] layersTarget; 

        private RaycastHit hit;

        protected override void RegisterObservables()
        {
            this.UpdateAsObservable()
                .Where(_ => playerStat.GetGameStatus().Value 
                    == GameStatus.InPlay)
                .Where(_ => Input.GetMouseButtonDown(
                    (int)keySettings.MouseButtonBreak))
                .Where(_ => playerStat.GetDestructionPowerUseCount().Value > 0)
                .Select(_ => Camera.main.ScreenPointToRay(Input.mousePosition))
                .Where(ray => Physics.Raycast(ray, out hit))
                .Where(_ => IsTargetLayer(hit.collider.gameObject.layer))
                .Subscribe(_ => {
                    hit.collider.gameObject.SetActive(false);
                    playerStatSetter.DecreaseDestructionPowerUse();
                })
                .AddTo(disposablesBasic);
        }

        private bool IsTargetLayer(int layer)
        {
            foreach (var check in layersTarget)
            {
                if (check == layer)
                {
                    return true;
                }
            }

            return false;
        }

    }

}