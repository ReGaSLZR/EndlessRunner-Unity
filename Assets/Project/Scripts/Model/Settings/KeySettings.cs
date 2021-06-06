namespace ReGaSLZR.EndlessRunner.Model.Settings
{
    using UnityEngine;

    [System.Serializable]
    public class KeySettings
    {

        #region Variables

        [Header("Primary Mouse-based Skill Buttons")]

        [SerializeField]
        private MouseButtonOption mouseButtonBreak;
        public MouseButtonOption MouseButtonBreak
        { get { return mouseButtonBreak; } }

        [SerializeField]
        private MouseButtonOption mouseButtonSpawn;
        public MouseButtonOption MouseButtonSpawn
        { get { return mouseButtonSpawn; } }

        [Header("Selection Keys for Spawnable Platforms")]

        [SerializeField]
        private KeyCode[] keysSpawnableSelect;
        public KeyCode[] KeysSpawnableSelect
        { get { return keysSpawnableSelect; } }

        [SerializeField]
        private KeyCode pauseUnpauseGameplay;
        public KeyCode PauseUnpause
        { get { return pauseUnpauseGameplay; } }

        #endregion

        public KeyCode GetHotKeySpawnableSelect(int index)
        {
            if ((index < 0) || (index >= keysSpawnableSelect.Length))
            {
                return KeyCode.Q;  
            }

            return keysSpawnableSelect[index];
        }

    }

}