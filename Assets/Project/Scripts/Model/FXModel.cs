namespace ReGaSLZR.EndlessRunner.Model
{

    using NaughtyAttributes;
    using UnityEngine;
    using Zenject;

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

        public override void InstallBindings()
        {
            Container.Bind<FXModel>().FromInstance(this);
        }

    }

}