namespace ReGaSLZR.EndlessRunner.Model.Settings
{

    using UnityEngine;

    [System.Serializable]
    public class GeneralSettings
    {

        [SerializeField]
        private int firstBreakSkillRefillCountdown = 3;
        public int FirstBreakSkillRefillCountdown 
            { get { return firstBreakSkillRefillCountdown; } }

    }

}