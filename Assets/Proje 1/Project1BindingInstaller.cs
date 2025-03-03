using UnityEngine;
using Zenject;

namespace Proje_1
{
    public class Project1BindingInstaller : MonoInstaller
    {
        public override void InstallBindings() {
            Container.Bind<Xgrid>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UiManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}