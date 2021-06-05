namespace ReGaSLZR.EndlessRunner.Skill
{

    using NaughtyAttributes;
    using Model;

    using UniRx;
    using UniRx.Triggers;
    using UnityEngine;

    public class SpawnSkill : BaseSkill
    {

        [SerializeField]
        private MouseButtonOption mouseButton;

        [SerializeField]
        [Required]
        private GameObject platform;

        [SerializeField]
        private float zPosition;

        protected override void RegisterObservables()
        {
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown((int)mouseButton))
                .Select(_ => GetWorldPosition())
                .Subscribe(worldPos => Instantiate(
                    platform, worldPos, Quaternion.identity))
                .AddTo(disposablesBasic);

            
        }

        private Vector3 GetWorldPosition()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = zPosition;
            return mousePos;
        }
    }

}