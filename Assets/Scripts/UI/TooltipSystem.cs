using UnityEngine;

public class TooltipSystem : Singleton<TooltipSystem>
{
    [SerializeField] private Tooltip _tooltip;

    public void Show(string content, string header = "")
    {
        _tooltip.SetText(content, header);
        _tooltip.gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        _tooltip.gameObject.SetActive(false);
    }
}
