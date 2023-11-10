using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SkillTreeView : MonoBehaviour
{
    [field : SerializeField] public TextMeshProUGUI PointCounterText { get; private set; }
    [field : SerializeField] public TextMeshProUGUI SkillPointPriceText { get; private set; }
    [field : SerializeField] public Button AddPointButton { get; private set; }
    [field : SerializeField] public Button AddSkillButton { get; private set; }
    [field : SerializeField] public Button RemoveSkillButton { get; private set; }
    [field : SerializeField] public Button RemoveAllSkillButton { get; private set; }
    [field : SerializeField] public List<SkillSlot> SkillSlots { get; private set; }
}
