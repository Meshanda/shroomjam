using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PowerButton : MonoBehaviour
{
    [SerializeField] private CharacterPower _characterPower;
    [SerializeField] private Enums.PowerType _powerType;
    
    public void OnButtonHoverEnter(BaseEventData eventData)
    {
        _characterPower.OnButtonHoverEnter(_powerType);
    }

    public void OnButtonHoverExit(BaseEventData eventData)
    {
        _characterPower.OnButtonHoverExit();
    }

    public void OnClick()
    {
        _characterPower.OnClickButton(_powerType);
    }
}
