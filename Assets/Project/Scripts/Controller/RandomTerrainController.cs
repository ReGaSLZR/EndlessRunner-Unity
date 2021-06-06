namespace ReGaSLZR.EndlessRunner.Controller
{

    using Base;
    using Holder;
    using Model;
    using Model.Settings;

    using System;
    using UnityEngine;
    using UniRx;
    using Zenject;

    /// <summary>
    /// Handles the randomization of the terrain during gameplay.
    /// Spawning happens in a given time span.
    /// Uses ObjectPools for optimized spawning operation.
    /// TIP: This controller only manipulates the x position of 
    /// items from the pools.
    /// </summary>
    public class RandomTerrainController : ReactiveMonoBehaviour
    {

        [Inject]
        private PlayerStatsGetter playerStats;

        [Inject]
        private TerrainSettings terrainSettings;

        #region Inspector Variables

        [SerializeField]
        private ObjectPool[] pools;

        #endregion

        private float latestPositionX;
        protected override void RegisterObservables()
        {
            latestPositionX = terrainSettings.StartingPositionX;

            //After a given delay, spawn an item from a random ObjectPool
            //at every time span tick.
            Observable.Interval(
                    TimeSpan.FromSeconds(terrainSettings.SpawnTimeSpan))
                .Delay(TimeSpan.FromSeconds(
                    terrainSettings.DelayFirstSpawn))
                .Where(_ => playerStats.GetGameStatus().Value
                    == GameStatus.InPlay)
                .Subscribe(_ => PositionObjectFromPool())
                .AddTo(disposablesBasic);
        }

        private void PositionObjectFromPool()
        {
            var randomPool = pools[UnityEngine.Random.Range(
                0, pools.Length - 1)];
            latestPositionX += terrainSettings.DistanceElementsX;

            var randomObject = randomPool.GetItemFromPool();
            randomObject.transform.position = new Vector3(
                    latestPositionX,
                    randomObject.transform.position.y,
                    randomObject.transform.position.z);
        }

    }

}