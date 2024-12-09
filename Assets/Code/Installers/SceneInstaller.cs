using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private PrefabsContainer _prefabsContainer;

    public override void InstallBindings()
    {
        Container.Bind<PrefabsContainer>().FromInstance(_prefabsContainer).AsSingle();
    }
}
