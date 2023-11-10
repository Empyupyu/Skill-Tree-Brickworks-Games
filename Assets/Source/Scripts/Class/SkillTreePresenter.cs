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
        _skillTreeView.AddSkillPointButton.onClick.AddListener(() =>
        {
            AddSkillPoint();
            UpdateSkillPointCounter();
            UpdateActivateSkillSlotButtonInteractable();
        });
        _skillTreeView.ActivateSkillSlotButton.onClick.AddListener(() =>
        {
            ActivateSkillSlot();
            UpdateActivateSkillSlotButtonInteractable();
            UpdateDeactivateSkillSlotButtonInteractable();
        });
        _skillTreeView.DeactivateSkillSlotButton.onClick.AddListener(() =>
        {
            DeactivateSkillSlot();
            UpdateActivateSkillSlotButtonInteractable();
            UpdateDeactivateSkillSlotButtonInteractable();
        });
        _skillTreeView.DeactivateAllSkillSlotsButton.onClick.AddListener(() =>
        {
            DeactivateAllSkillSlots();
            UpdateSkillPointCounter();
            UpdateActivateSkillSlotButtonInteractable();
            UpdateDeactivateSkillSlotButtonInteractable();
        });

        foreach (var slot in _skillTreeView.SkillSlots)
        {
            slot.InitializeColor();
            slot.OnPointerClickEvent += _skillTreeSystem.OnSkillSlotClick;
        }

        _skillTreeSystem.OnSkillSlotSelected += OnSkillSlotSelected;
        _skillTreeSystem.OnSlotActivation += OnSkillSlotActivation;
        _skillTreeSystem.OnSkillRemove += OnSkillSlotDeactivation;

        UpdateSkillPointCounter();
    }

    private void OnSkillSlotDeactivation(SkillSlot slot)
    {
        slot.EnableSlot(false);
        _selectedSkillSlot.SelectSlot(true);
        UpdateSkillPointCounter();
    }

    private void OnSkillSlotActivation(SkillSlot slot)
    {
        slot.EnableSlot(true);
        UpdateSkillPointCounter();
    }

    private void OnSkillSlotSelected(SkillSlot selectedSkillSlot)
    {
        if(_selectedSkillSlot != null) _selectedSkillSlot.SelectSlot(false);

        _selectedSkillSlot = selectedSkillSlot;
        _selectedSkillSlot.SelectSlot(true);

        UpdateActivateSkillSlotButtonInteractable();
        UpdateDeactivateSkillSlotButtonInteractable();
        UpdateSelectedSkillSlotPrice();
    }

    private void UpdateActivateSkillSlotButtonInteractable()
    {
        _skillTreeView.ActivateSkillSlotButton.interactable = _selectedSkillSlot != null && _skillTreeSystem.HaveEnoughPoints();
    }

    private void UpdateDeactivateSkillSlotButtonInteractable()
    {
        _skillTreeView.DeactivateSkillSlotButton.interactable = _selectedSkillSlot != null && _skillTreeSystem.CanDeactivateSkillSlot();
    }

    private void UpdateSelectedSkillSlotPrice()
    {
        _skillTreeView.SkillPointPriceText.text = _selectedSkillSlot.Skill.SkillPointPrice.ToString();
    }

    private void ActivateSkillSlot()
    {
        _skillTreeSystem.ActivateSelectedSkillSlot();
    }

    private void DeactivateSkillSlot()
    {
        _skillTreeSystem.DeactivateSelectedSkillSlot(_selectedSkillSlot);
    }

    private void DeactivateAllSkillSlots()
    {
        _skillTreeSystem.DeactivateAllSkillSlots();
        _selectedSkillSlot.SelectSlot(false);
        _skillTreeView.SkillPointPriceText.text = "0";
        _selectedSkillSlot = null;
    }

    private void AddSkillPoint()
    {
        _skillTreeSystem.AddPoint();
    }

    private void UpdateSkillPointCounter()
    {
        _skillTreeView.SkillPointCounterText.text = _save.SkillPoints.ToString();
    }
}
