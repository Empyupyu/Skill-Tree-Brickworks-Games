using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour, IPointerClickHandler
{
    public event Action<SkillSlot> OnPointerClickEvent;
    public bool RemoveMark { get; set; }

    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public bool IsRootSkillSlot { get; private set; }
    [field: SerializeField] public TextMeshProUGUI SkillIDText { get; private set; }
    [field: SerializeField] public SkillBase Skill { get; private set; }
    [field: SerializeField] public List<SkillSlot> NearSlots { get; private set; }
    [field: SerializeField] public List<SkillSlot> ConnectedSlots { get; private set; }
    [field: SerializeField] public Image SlotImage { get; private set; }
    [field: SerializeField] public Color ActiveColor { get; private set; }
    [field: SerializeField] public Color DisableColor { get; private set; }
    [field: SerializeField] public Color SelectColor { get; private set; }
    [field: SerializeField] public bool Active { get; set; }

    private Color _currentColor;

    public void InitializeColor()
    {
        _currentColor = DisableColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickEvent?.Invoke(this);     
    }

    public void EnableSlot(bool isActive)
    {
        _currentColor = isActive ? ActiveColor : DisableColor;
        SlotImage.color = _currentColor;
    }

    public void SelectSlot(bool selected)
    {
        if (selected)
        {
            SlotImage.color = SelectColor;
            return;
        }
        SlotImage.color = _currentColor;
    }
}
