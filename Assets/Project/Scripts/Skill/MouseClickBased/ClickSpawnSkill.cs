namespace ReGaSLZR.EndlessRunner.Skill.Click
{

    using Model;
    using Model.Settings;

    using UniRx;
    using UniRx.Triggers;
    using UnityEngine;

    using Zenject;

    public class ClickSpawnSkill : BaseSkill
    {

        [Inject]
        private SpawnableGetter spawnableModel;

        [Inject]
        private KeySettings keySettings;

        [SerializeField]
        private float zPosition;

        protected override void RegisterObservables()
        {
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(
                    (int)keySettings.MouseButtonSpawn))
                .Select(_ => GetWorldPosition())
                .Subscribe(worldPos => {
                    var obj = spawnableModel.GetSpawnableObject();
                    if (obj != null)
                    {
                        obj.SetActive(true);
                        obj.transform.position = worldPos;
                    }
                })
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