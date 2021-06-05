namespace ReGaSLZR.EndlessRunner.Skill
{

    using Model;

    using NaughtyAttributes;
    using UniRx;
    using UniRx.Triggers;
    using UnityEngine;

    using Zenject;

    public class BreakSkill : BaseSkill
    {

        [Inject]
        private PlayerStatsGetter playerStat;

        [SerializeField]
        private MouseButtonOption mouseButton;

        [SerializeField]
        [Layer]
        private int layerTarget; 

        private RaycastHit hit;

        protected override void RegisterObservables()
        {
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown((int)mouseButton))
                .Select(_ => Camera.main.ScreenPointToRay(Input.mousePosition))
                .Where(ray => Physics.Raycast(ray, out hit))
                .Where(_ => hit.collider.gameObject.layer == layerTarget)
                .Subscribe(_ => hit.collider.gameObject.SetActive(false))
                .AddTo(disposablesBasic);
        }
    }

}