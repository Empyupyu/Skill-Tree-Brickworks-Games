using UnityEngine;
using Zenject;

public class GameSystem : MonoBehaviour
{
    [Inject] protected GameData _game;
    [Inject] protected SaveData _save;

    public virtual void OnAwake() { }
    public virtual void OnUpdate() { }
}
