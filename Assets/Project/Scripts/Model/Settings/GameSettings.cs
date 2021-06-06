namespace ReGaSLZR.EndlessRunner.Model.Settings
{

    using UnityEngine;
    using Zenject;

    /// <summary>
    /// Holds together all the Settings and takes care of
    /// uploading these settings as separate entries 
    /// (for abstraction and encapsulation purposes) to
    /// the Dependency Injection framework.
    /// </summary>
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