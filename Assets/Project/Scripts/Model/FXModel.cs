namespace ReGaSLZR.EndlessRunner.Model
{

    using NaughtyAttributes;
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// Holds the reference to globally needed FXs.
    /// Good for repositioning elements regardless of 
    /// parent/caller object's own position.
    /// </summary>
    [System.Serializable]
    public class FXModel : MonoInstaller
    {

        [SerializeField]
        [Required]
        private GameObject fxSpawn;
        public GameObject FXSpawn 
        { get { return fxSpawn; } }

        [SerializeField]
        [Required]
        private GameObject fxDestroy;
        public GameObject FXDestroy
        { get { return fxDestroy; } }

        [SerializeField]
        [Required]
        private GameObject fxDeath;
        public GameObject FXDeath
        { get { return fxDeath; } }

        public override void InstallBindings()
        {
            Container.Bind<FXModel>().FromInstance(this);
        }

    }

}