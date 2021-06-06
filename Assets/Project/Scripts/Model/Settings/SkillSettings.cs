namespace ReGaSLZR.EndlessRunner.Model.Settings
{
    using UnityEngine;

    [System.Serializable]
    public class SkillSettings
    {

        [Header("Skill - Destructive Power Use")]

        [SerializeField]
        private int destructivePowerUseMaxCount = 5;
        public int DestructivePowerUseMaxCount
        { get { return destructivePowerUseMaxCount; } }

        [SerializeField]
        [Tooltip("Every x seconds, the destructive power gets a +1.")]
        private int destructivePowerRefillCooldown = 5;
        public int DestructivePowerRefillCooldown
        { get { return destructivePowerRefillCooldown; } }

        [Header("Skill - Super Jump")]

        [SerializeField]
        private float superJumpForce = 5f;
        public float SuperJumpForce
        { get { return superJumpForce; } }

        [SerializeField]
        private ForceMode superJumpForceMode = ForceMode.Impulse;
        public ForceMode SuperJumpForceMode
        { get { return superJumpForceMode; } }

        [Header("Bonus Skill - Effect Delay")]

        [SerializeField]
        private int bonusSkillEffectDelayMin = 5;
        public int BonusSkillEffectDelayMin
        { get { return bonusSkillEffectDelayMin; } }

        [SerializeField]
        private int bonusSkillEffectDelayMax = 5;
        public int BonusSkillEffectDelayMax
        { get { return bonusSkillEffectDelayMax; } }

        [Header("Bonus Skill - Effect Duration")]

        [SerializeField]
        private int bonusSkillEffectDurationMin = 5;
        public int BonusSkillEffectDurationMin
        { get { return bonusSkillEffectDurationMin; } }

        [SerializeField]
        private int bonusSkillEffectDurationMax = 5;
        public int BonusSkillEffectDurationMax
        { get { return bonusSkillEffectDurationMax; } }


    }

}