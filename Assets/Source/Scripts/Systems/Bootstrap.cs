using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private readonly List<GameSystem> _gameSystems = new ();

    private void Awake()
    {
        AddSystems();

        foreach (var system in _gameSystems) system.OnAwake();
    }

    private void AddSystems()
    {
        _gameSystems.AddRange(transform.GetComponentsInChildren<GameSystem>());
    }

    private void Update()
    {
        foreach (var system in _gameSystems) system.OnUpdate();
    }
}
