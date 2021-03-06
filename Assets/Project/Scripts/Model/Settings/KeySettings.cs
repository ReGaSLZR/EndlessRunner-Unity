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

        [Header("Game Terminal Keys")]

        [SerializeField]
        private KeyCode pauseUnpauseGameplay;
        public KeyCode PauseUnpause
        { get { return pauseUnpauseGameplay; } }

        [SerializeField]
        private KeyCode reload;
        public KeyCode Reload
        { get { return reload; } }

        [SerializeField]
        private KeyCode quit;
        public KeyCode Quit
        { get { return quit; } }

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