namespace ReGaSLZR.EndlessRunner.Model.Settings
{
    using UnityEngine;

    [System.Serializable]
    public class KeySettings
    {

        [SerializeField]
        private MouseButtonOption mouseButtonBreak;
        public MouseButtonOption MouseButtonBreak
        { get { return mouseButtonBreak; } }

        [SerializeField]
        private MouseButtonOption mouseButtonSpawn;
        public MouseButtonOption MouseButtonSpawn
        { get { return mouseButtonSpawn; } }

        [Header("Spawnable Selection Keys")]

        [SerializeField]
        private KeyCode[] keysSpawnableSelect;
        public KeyCode[] KeysSpawnableSelect
        { get { return keysSpawnableSelect; } }

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