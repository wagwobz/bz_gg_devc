using Cinemachine;
using UnityEngine;
using Zenject;

namespace Proje_2
{
    public class Project2Installer : MonoInstaller
    {
        public override void InstallBindings() {
            Container.Bind<PlatformController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Chibi>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CinemachineVirtualCamera>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AudioManager>().FromComponentInHierarchy().AsSingle();
            
            PreviousPlatform existingInstance = FindObjectOfType<PreviousPlatform>();

            if (existingInstance != null)
            {
                Container.Bind<PreviousPlatform>().FromInstance(existingInstance).AsSingle();
            }
            else
            {
                Container.Bind<PreviousPlatform>().FromComponentInHierarchy().AsSingle();
            }
            
        }
    }
}