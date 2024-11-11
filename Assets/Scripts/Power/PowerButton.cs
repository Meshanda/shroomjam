using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PowerButton : MonoBehaviour
{
    [SerializeField] private Enums.PowerType _powerType;
    
    public void OnButtonHoverEnter(BaseEventData eventData)
    {
        CharacterPower.OnPointerHoverSpellButton(true, _powerType);
    }

    public void OnButtonHoverExit(BaseEventData eventData)
    {
        CharacterPower.OnPointerHoverSpellButton(false, _powerType);
    }

    public void OnClick()
    {
        CharacterPower.OnClickSpellButton(_powerType);
    }
}
