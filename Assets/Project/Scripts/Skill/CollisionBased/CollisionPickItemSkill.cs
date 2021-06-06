namespace ReGaSLZR.EndlessRunner.Skill.Collision
{

    using Base;
    using Model;
    using Detector;

    using NaughtyAttributes;
    using UnityEngine;
    using UniRx;
    using Zenject;

    public enum ItemBoon
    { 
        Time,
        Score,
    }

    public class CollisionPickItemSkill : ReactiveMonoBehaviour
    {

        [Inject]
        private PlayerStatsSetter playerStats;

        [SerializeField]
        [Required]
        private CollisionDetector signalDetector;

        [SerializeField]
        private ItemBoon itemBoon;

        [SerializeField]
        private int boonValue = 1;

        protected override void RegisterObservables()
        {
            signalDetector.IsTriggered
                .Where(isTriggered => isTriggered)
                .Subscribe(_ => {
                    switch (itemBoon)
                    {
                        case ItemBoon.Score:
                            {
                                playerStats.AddScore(boonValue);
                                break;
                            }
                        case ItemBoon.Time:
                            {
                                playerStats.AddTime(boonValue);
                                break;
                            }
                    }

                    signalDetector.CachedTarget.SetActive(false);
                    signalDetector.ForceResetIsTriggered();
                })
                .AddTo(disposablesBasic);
        }
    }


}