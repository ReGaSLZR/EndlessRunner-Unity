namespace ReGaSLZR.EndlessRunner.Model
{

    using Holder;

    using NaughtyAttributes;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

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
        [Required]
        private RawImage imageSpawnableIcon;

        [Space]

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

            imageSpawnableIcon.texture = cachedPool.GetIcon();
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