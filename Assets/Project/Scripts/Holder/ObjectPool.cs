namespace ReGaSLZR.EndlessRunner.Holder
{

    using Base;

    using UnityEngine;

    /// <summary>
    /// Pretty straightforward. :)
    /// </summary>
    public class ObjectPool : ReactiveMonoBehaviour
    {

        [SerializeField]
        protected GameObject[] objectsInPool;

        private int cachedIndex = 0;

        protected override void RegisterObservables()
        {
            //TODO
        }

        protected virtual void Start()
        {
            DisableAllSpawnables();
        }

        protected void DisableAllSpawnables()
        {
            foreach (var spawnable in objectsInPool)
            {
                spawnable.SetActive(false);
            }
        }

        public GameObject GetItemFromPool()
        {
            cachedIndex = (cachedIndex == objectsInPool.Length - 1)
                ? 0 : (cachedIndex + 1);
            var spawnable = objectsInPool[cachedIndex];
            spawnable.SetActive(true);
            SetAllChildrenActive(spawnable);
            return spawnable;
        }

        private void SetAllChildrenActive(GameObject parent)
        {
            for (int i = 0; i < parent.transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }

    }

}