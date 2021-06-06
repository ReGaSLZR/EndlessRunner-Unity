namespace ReGaSLZR.EndlessRunner.Holder
{

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
        public Texture GetIcon();
        public GameObject GetItemFromPool();
    }

    public class SpawnablePool : ObjectPool, SpawnablePoolGetter
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

        [SerializeField]
        [Required]
        private Texture icon;

        [Space]

        [SerializeField]
        private int hotKeyIndex;

        protected override void Start()
        {
            base.Start();

            textOnUI.text = spawnableName + " (" + 
                keySettings.GetHotKeySpawnableSelect(hotKeyIndex) + ")";

            if (isDefault)
            {
                spawnableModel.SetSelectedPool(this);
            }
        }

        protected override void RegisterObservables()
        {
            base.RegisterObservables();

            this.UpdateAsObservable()
                .Where(_ => playerStat.GetGameStatus().Value
                    == GameStatus.InPlay)
                .Where(_ => Input.GetKeyDown(
                    keySettings.GetHotKeySpawnableSelect(hotKeyIndex)))
                .Subscribe(_ => 
                    spawnableModel.SetSelectedPool(this))
                .AddTo(disposablesBasic);
        }

        public TextMeshProUGUI GetTextOnUI()
        {
            return textOnUI;
        }

        public Texture GetIcon()
        {
            return icon;
        }

    }

}