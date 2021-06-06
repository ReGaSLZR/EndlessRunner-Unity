namespace ReGaSLZR.EndlessRunner.Model.Settings
{

    using UnityEngine;

    [System.Serializable]
    public class TerrainSettings
    {

        [SerializeField]
        private float startingPositionX;
        public float StartingPositionX
        { get { return startingPositionX; } }
        
        [SerializeField]
        private float distanceElementsX;
        public float DistanceElementsX
        { get { return distanceElementsX; } }

        [SerializeField]
        private float spawnTimeSpan;
        public float SpawnTimeSpan
        { get { return spawnTimeSpan; } }

        [SerializeField]
        private float delayFirstSpawn;
        public float DelayFirstSpawn
        { get { return delayFirstSpawn; } }

    }

}