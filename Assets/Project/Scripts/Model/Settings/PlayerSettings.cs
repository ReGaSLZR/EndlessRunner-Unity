namespace ReGaSLZR.EndlessRunner.Model.Settings
{
    using UnityEngine;

    [System.Serializable]
    public class PlayerSettings
    {

        [SerializeField]
        private float constantAccel = 5f;
        public float ConstantAccel
        { get { return constantAccel; } }

        [Space]

        [SerializeField]
        private float normalJumpForce = 5f;
        public float NormalJumpForce
        { get { return normalJumpForce; } }

        [SerializeField]
        private ForceMode normalJumpForceMode = ForceMode.Impulse;
        public ForceMode NormalJumpForceMode
        { get { return normalJumpForceMode; } }

        [Space]

        [SerializeField]
        [Range(1f, 10f)]
        private float gravityMultiplierJumpUp = 1.1f;
        public float GravityMultiplierJumpUp
        { get { return gravityMultiplierJumpUp; } }

        [SerializeField]
        [Range(1f, 10f)]
        private float gravityMultiplierFallDown = 1.1f;
        public float GravityMultiplierFallDown
        { get { return gravityMultiplierFallDown; } }


    }

}