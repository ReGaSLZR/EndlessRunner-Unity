namespace ReGaSLZR.EndlessRunner.Model
{

    using Holder;

    using TMPro;
    using UnityEngine;

    using Zenject;

    public interface SpawnableGetter
    {
        public GameObject GetSpawnableObject();
    }

    public interface SpawnableSetter
    {
        public void SetSelectedPool(SpawnablePoolGetter pool);
    }

    public class SpawnableModel : MonoInstaller, 
        SpawnableGetter, SpawnableSetter
    {

        [SerializeField]
        private TextMeshProUGUI[] textsSpawnables;

        [SerializeField]
        private float textAlphaOnActive = 1f;
        [SerializeField]
        private float textAlphaOnInactive = 0.3f;

        private SpawnablePoolGetter cachedPool;

        public override void InstallBindings()
        {
            Container.Bind<SpawnableGetter>().FromInstance(this);
            Container.Bind<SpawnableSetter>().FromInstance(this);
        }

        private void DisableAllTexts()
        {
            foreach (var text in textsSpawnables)
            {
                if (text != null)
                {
                    text.CrossFadeAlpha(textAlphaOnInactive, 0f, true);
                }
            }
        }

        public void SetSelectedPool(SpawnablePoolGetter pool)
        {
            cachedPool = pool;
            DisableAllTexts();
            cachedPool.GetTextOnUI().CrossFadeAlpha(
                textAlphaOnActive, 0f, true);
        }

        public GameObject GetSpawnableObject()
        {
            if (cachedPool == null)
            {
                return null;
            }

            return cachedPool.GetItemFromPool();
        }

    }

}