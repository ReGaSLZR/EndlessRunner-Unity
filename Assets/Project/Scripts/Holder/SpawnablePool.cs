namespace ReGaSLZR.EndlessRunner.Holder
{

    using Base;
    using Model;
    using Model.Settings;

    using NaughtyAttributes;
    using TMPro;
    using UniRx;
    using UniRx.Triggers;
    using UnityEngine;

    using Zenject;

    public interface SpawnablePoolGetter
    {
        public TextMeshProUGUI GetTextOnUI();
        public GameObject GetItemFromPool();
    }

    public class SpawnablePool : ReactiveMonoBehaviour, SpawnablePoolGetter
    {

        [Inject]
        private PlayerStatsGetter playerStat;

        [Inject]
        private SpawnableSetter spawnableModel;

        [Inject]
        private KeySettings keySettings;

        [SerializeField]
        private bool isDefault;

        [Space]

        [SerializeField]
        private string spawnableName;

        [SerializeField]
        [Required]
        private TextMeshProUGUI textOnUI;

        [Space]

        [SerializeField]
        private int hotKeyIndex;

        [SerializeField]
        private GameObject[] spawnables;

        private int cachedIndex = 0;

        private void Start()
        {
            textOnUI.text = spawnableName + " (" + keySettings.GetHotKeySpawnableSelect(hotKeyIndex) + ")";
            DisableAllSpawnables();

            if (isDefault)
            {
                spawnableModel.SetSelectedPool(this);
            }
        }

        protected override void RegisterObservables()
        {
            this.UpdateAsObservable()
                .Where(_ => playerStat.GetGameStatus().Value
                    == GameStatus.InPlay)
                .Where(_ => Input.GetKeyDown(
                    keySettings.GetHotKeySpawnableSelect(hotKeyIndex)))
                .Subscribe(_ => 
                    spawnableModel.SetSelectedPool(this))
                .AddTo(disposablesBasic);
        }

        private void DisableAllSpawnables()
        {
            foreach (var spawnable in spawnables)
            {
                spawnable.SetActive(false);
            }
        }

        public TextMeshProUGUI GetTextOnUI()
        {
            return textOnUI;
        }

        public GameObject GetItemFromPool()
        {
            cachedIndex = (cachedIndex == spawnables.Length - 1) 
                ? 0 : (cachedIndex + 1);
            var spawnable = spawnables[cachedIndex];
            spawnable.SetActive(true);
            return spawnable;
        }

    }


}