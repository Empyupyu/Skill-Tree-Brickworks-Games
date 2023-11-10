using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SkillTreeView : MonoBehaviour
{
    [field : SerializeField] public TextMeshProUGUI SkillPointCounterText { get; private set; }
    [field : SerializeField] public TextMeshProUGUI SkillPointPriceText { get; private set; }
    [field : SerializeField] public Button AddSkillPointButton { get; private set; }
    [field : SerializeField] public Button ActivateSkillSlotButton { get; private set; }
    [field : SerializeField] public Button DeactivateSkillSlotButton { get; private set; }
    [field : SerializeField] public Button DeactivateAllSkillSlotsButton { get; private set; }
    [field : SerializeField] public List<SkillSlot> SkillSlots { get; private set; }
}
