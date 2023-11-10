using UnityEngine;

public abstract class SkillBase : ScriptableObject
{
    [field: SerializeField] public int SkillPointPrice { get; private set; }
    public abstract void ApplyEffect();
}
