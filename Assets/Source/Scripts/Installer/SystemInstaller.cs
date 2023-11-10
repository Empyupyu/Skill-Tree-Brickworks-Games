using UnityEngine;
using Zenject;

public class SystemInstaller : MonoInstaller
{
    [SerializeField] private SkillTreeSystem _skillTreeSystem;

    public override void InstallBindings()
    {
        Container.Bind<SkillTreeSystem>().FromInstance(_skillTreeSystem).AsSingle();
    }
}

