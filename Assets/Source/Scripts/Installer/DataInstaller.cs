using UnityEngine;
using Zenject;

public class DataInstaller : MonoInstaller
{
    private const string SAVE_DATA_KEY = "SaveDataKey";
    private SaveData _saveData;

    public override void InstallBindings()
    {
        LoadSaveDatas();
        Container.Bind<SaveData>().FromInstance(_saveData).AsSingle();
        Container.Bind<GameData>().FromNew().AsSingle();
        Container.Bind<IInitializable>().To<SkillTreePresenter>().AsSingle();
    }

    private void LoadSaveDatas()
    {
        var save = SaveUtility.Instance();
        if (!save.HasSave(SAVE_DATA_KEY)) return;

        _saveData = JsonUtility.FromJson<SaveData>(save.Load(SAVE_DATA_KEY));

        if (_saveData == null) _saveData = new();
    }

    private void OnDestroy()
    {
        var save = JsonUtility.ToJson(_saveData);
        SaveUtility.Instance().Save(SAVE_DATA_KEY, save);
    }
}

