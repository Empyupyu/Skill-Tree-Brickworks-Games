using UnityEngine;
using Zenject;

public class ViewInstaller : MonoInstaller
{
    [SerializeField] private SkillTreeView _skillTreeView;

    public override void InstallBindings()
    {
        Container.Bind<SkillTreeView>().FromInstance(_skillTreeView).AsSingle();
    }
}

