namespace ReGaSLZR.EndlessRunner.Model.Settings
{

    using UnityEngine;

    [System.Serializable]
    public class GeneralSettings
    {

        [SerializeField]
        private int preGamePlayCountdown = 3;
        public int PreGamePlayCountdown 
            { get { return preGamePlayCountdown; } }

    }

}