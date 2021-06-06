namespace ReGaSLZR.EndlessRunner.Model.Settings
{

    using UnityEngine;
    using Zenject;

    [CreateAssetMenu(fileName = "GameSettings", 
        menuName = "Installers/GameSettings")]
    public class GameSettings : ScriptableObjectInstaller<GameSettings>
    {

        [SerializeField]
        private GeneralSettings generalSettings;

        [SerializeField]
        private PlayerSettings playerSettings;

        [SerializeField]
        private SkillSettings skillSettings;

        [SerializeField]
        private TerrainSettings terrainSettings;

        [SerializeField]
        private KeySettings keySettings;

        public override void InstallBindings()
        {
            Container.BindInstances(
                generalSettings, playerSettings, 
                skillSettings, keySettings, 
                terrainSettings);
        }

    }

}