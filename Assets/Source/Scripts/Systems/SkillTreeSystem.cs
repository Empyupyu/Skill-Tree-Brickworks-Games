using System;
using System.Collections.Generic;

public sealed class SkillTreeSystem : GameSystem
{
    public event Action<SkillSlot> OnSkillSlotSelected;
    public event Action<SkillSlot> OnSlotActivation;
    public event Action<SkillSlot> OnSkillRemove;

    private readonly List<SkillSlot> _visitedSlots = new ();
    private readonly List<SkillSlot> _activeSlots = new ();
    private SkillSlot _selectedSkillSlot;

    public void AddPoint()
    {
        ChangeSkillPoints(1);
    }

    public void OnSkillSlotClick(SkillSlot slot)
    {
        if (slot.IsRootSkillSlot) return;

        ResetVisitedSlots();

        if (!HasPathToRoot(slot.ConnectedSlots, slot)) return;

        _selectedSkillSlot = slot;
        OnSkillSlotSelected?.Invoke(_selectedSkillSlot);
    }

    public void ActivateSelectedSkillSlot()
    {
        _selectedSkillSlot.Active = true;
        _activeSlots.Add(_selectedSkillSlot);
        ChangeSkillPoints(-_selectedSkillSlot.Skill.SkillPointPrice);
        OnSlotActivation?.Invoke(_selectedSkillSlot);

        _selectedSkillSlot.ConnectedSlots.Clear();
        _selectedSkillSlot.ConnectedSlots.AddRange(_selectedSkillSlot.NearSlots);

        foreach (var slot in _selectedSkillSlot.ConnectedSlots)
        {
            if (slot.ConnectedSlots.Contains(_selectedSkillSlot)) continue;

            slot.ConnectedSlots.Add(_selectedSkillSlot);
        }
    }

    public bool HaveEnoughPoints()
    {
        bool haveEnoughPoints = _save.SkillPoints >= _selectedSkillSlot.Skill.SkillPointPrice;
        return _selectedSkillSlot != null && !_selectedSkillSlot.Active && haveEnoughPoints;
    }

    public bool CheckOnCanRemoveSkillSlot()
    {
        if (!_selectedSkillSlot.Active) return false;
        _selectedSkillSlot.RemoveMark = true;

        ResetVisitedSlots();

        foreach (var connectedSlot in _selectedSkillSlot.ConnectedSlots)
        {
            if (!connectedSlot.Active) continue;

            var deleteCheckList = new List<SkillSlot>();
            deleteCheckList.AddRange(connectedSlot.ConnectedSlots);

            for (int i = 0; i < connectedSlot.ConnectedSlots.Count; i++)
            {
                if (connectedSlot.ConnectedSlots[i].Active) continue;

                deleteCheckList.Remove(connectedSlot.ConnectedSlots[i]);
            }
            deleteCheckList.Add(connectedSlot);
            deleteCheckList.Remove(_selectedSkillSlot);

            if (!HasPathToRoot(deleteCheckList, connectedSlot) || deleteCheckList.Count == 0)
            {
                _selectedSkillSlot.RemoveMark = false;
                return false;
            }
        }
        _selectedSkillSlot.RemoveMark = false;
        return true;
    }

    public void RemoveSelectedSkillSlot()
    {
        RemoveSkill(_selectedSkillSlot);
    }

    public void RemoveAllSkillSlots()
    {
        for (int i = 0; i < _activeSlots.Count;)
        {
            var skill = _activeSlots[i];
            RemoveSkill(skill);
        }
        _selectedSkillSlot = null;
    }

    private void RemoveSkill(SkillSlot removeSkill)
    {
        removeSkill.Active = false;

        for (int i = 0; i < removeSkill.ConnectedSlots.Count; i++)
        {
            var slot = removeSkill.ConnectedSlots[i];

            if (slot.IsRootSkillSlot) continue;
            if (slot.Active) continue;

            slot.ConnectedSlots.Remove(removeSkill);
            removeSkill.ConnectedSlots.Remove(slot);
            i--;
        }

        if (_activeSlots.Contains(removeSkill)) _activeSlots.Remove(removeSkill);

        ChangeSkillPoints(removeSkill.Skill.SkillPointPrice);
        OnSkillRemove?.Invoke(removeSkill);
    }

    private bool HasPathToRoot(List<SkillSlot> connectSlots, SkillSlot currentSlot)
    {
        if (currentSlot.IsRootSkillSlot) return true;
        if (_visitedSlots.Contains(currentSlot)) return false;

        _visitedSlots.Add(currentSlot);

        foreach (var nearSlot in connectSlots)
        {
            bool target = HasPathToRoot(connectSlots, nearSlot);

            if (target) return true;

            foreach (var connectedSlot in nearSlot.ConnectedSlots)
            {
                if (!connectedSlot.Active) continue;
                if (connectedSlot.RemoveMark) continue;

                target = HasPathToRoot(nearSlot.ConnectedSlots, connectedSlot);

                if (target) return true;
            }
        }
        return false;
    }

    private void ResetVisitedSlots()
    {
        _visitedSlots.Clear();
    }

    private void ChangeSkillPoints(int value)
    {
        _save.SkillPoints += value;
    }
}
