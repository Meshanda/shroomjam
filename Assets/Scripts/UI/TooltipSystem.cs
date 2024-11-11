using UnityEngine;

public class TooltipSystem : Singleton<TooltipSystem>
{
    [SerializeField] private Tooltip _tooltip;
    private Enums.TooltipType _type = Enums.TooltipType.Default;

    public void Show(Enums.TooltipType type, string content, string header = "")
    {
        _type = type;
        _tooltip.SetText(content, header);
        _tooltip.gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        _tooltip.gameObject.SetActive(false);
    }

    public void HideBuildingMenuButton()
    {
        if (_type == Enums.TooltipType.BuildingMenu)
            Hide();
    }
}
