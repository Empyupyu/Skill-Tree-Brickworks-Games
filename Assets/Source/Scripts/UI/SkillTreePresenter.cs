using Zenject;

public class SkillTreePresenter : IInitializable
{
    private SkillTreeSystem _skillTreeSystem;
    private SkillTreeView _skillTreeView;
    private SaveData _save;
    private SkillSlot _selectedSkillSlot;

    public SkillTreePresenter(SkillTreeSystem model, SkillTreeView view, SaveData save)
    {
        _skillTreeSystem = model;
        _skillTreeView = view;
        _save = save;  
    }

    public void Initialize()
    {
        _skillTreeView.AddPointButton.onClick.AddListener(AddSkillPoint);
        _skillTreeView.AddSkillButton.onClick.AddListener(AddSkill);
        _skillTreeView.RemoveSkillButton.onClick.AddListener(RemoveSkillPoint);
        _skillTreeView.RemoveAllSkillButton.onClick.AddListener(RemoveAllSkillPoint);

        foreach (var slot in _skillTreeView.SkillSlots)
        {
            slot.InitializeColor();
            slot.OnPointerClickEvent += _skillTreeSystem.OnSkillSlotClick;
        }

        _skillTreeSystem.OnSkillSlotSelected += OnSkillSlotSelected;
        _skillTreeSystem.OnSlotActivation += OnSkillSlotAdd;
        _skillTreeSystem.OnSkillRemove += OnSkillSlotRemove;
        UpdatePointCounter();
    }

    private void OnSkillSlotRemove(SkillSlot slot)
    {
        slot.EnableSlot(false);
        UpdatePointCounter();
    }

    private void OnSkillSlotAdd(SkillSlot slot)
    {
        slot.EnableSlot(true);
        UpdatePointCounter();
    }

    private void OnSkillSlotSelected(SkillSlot selectedSkillSlot)
    {
        if(_selectedSkillSlot != null) _selectedSkillSlot.SelectSlot(false);

        _selectedSkillSlot = selectedSkillSlot;
        _selectedSkillSlot.SelectSlot(true);

        UpdateAddSkillSlotButtonInteracble();
        UpdateSelectedSkillSlotPrice();
    }

    private void UpdateAddSkillSlotButtonInteracble()
    {
        _skillTreeView.AddSkillButton.interactable = _selectedSkillSlot != null && _skillTreeSystem.HaveEnoughPoints();
        _skillTreeView.RemoveSkillButton.interactable = _selectedSkillSlot != null && _skillTreeSystem.CheckOnCanRemoveSkillSlot();
    }

    private void UpdateSelectedSkillSlotPrice()
    {
        _skillTreeView.SkillPointPriceText.text = _selectedSkillSlot.Skill.SkillPointPrice.ToString();
    }

    private void AddSkill()
    {
        _skillTreeSystem.ActivateSelectedSkillSlot();
        UpdateAddSkillSlotButtonInteracble();
    }

    private void RemoveSkillPoint()
    {
        _skillTreeSystem.RemoveSelectedSkillSlot();
        UpdateAddSkillSlotButtonInteracble();
    }

    private void RemoveAllSkillPoint()
    {
        _selectedSkillSlot = null;
        _skillTreeSystem.RemoveAllSkillSlots();
        UpdatePointCounter();
        UpdateAddSkillSlotButtonInteracble();
    }

    private void AddSkillPoint()
    {
        _skillTreeSystem.AddPoint();
        UpdatePointCounter();
    }

    private void UpdatePointCounter()
    {
        _skillTreeView.PointCounterText.text = _save.SkillPoints.ToString();
    }
}
