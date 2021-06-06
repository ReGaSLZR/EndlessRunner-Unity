namespace ReGaSLZR.EndlessRunner.Skill.Collision
{

    using Detector;

    using NaughtyAttributes;
    using UnityEngine;    
    using UniRx;

    public class CollisionPushOffSkill : BaseSkill
    {

        [SerializeField]
        [Required]
        private CollisionDetector detector;

        [SerializeField]
        private Vector3 pushDirection;

        [SerializeField]
        private float pushForce;

        [SerializeField]
        private ForceMode forceMode;

        protected override void RegisterObservables()
        {
            detector.IsTriggered
                .Where(isTriggered => isTriggered)
                .Select(_ => GetPushableTarget())
                .Where(target => (target != null))
                .Subscribe(target => target.AddForce(
                    pushDirection * pushForce, forceMode))
                .AddTo(disposablesBasic);
        }

        private Rigidbody GetPushableTarget()
        {
            GameObject target = detector.CachedTarget;
            return target.GetComponent<Rigidbody>();
        }

    }

}