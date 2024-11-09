using UnityEngine;

public class CharacterPower : MonoBehaviour
{
    [SerializeField] private GameObject _powerVisualizer;

    [Header("Data")]
    [SerializeField] private PowerData _cleanData;
    [SerializeField] private PowerData _shieldData;
    [SerializeField] private PowerData _boostData;
    
    

    private void Awake()
    {
        DisplayPowerVisualize(false);
    }

    public void OnClean()
    {
        Collider2D[] overlapResults = Physics2D.OverlapCircleAll(transform.position, _cleanData.Range / 2);
        if (overlapResults.Length > 0)
        {
            // Some results
            foreach (Collider2D collider in overlapResults)
            {
                if (collider.gameObject.layer == LayerMask.NameToLayer("Corruptible"))
                {
                    Corruptible corruptible = collider.GetComponentInParent<Corruptible>();
                    corruptible.DeCorrupt(_cleanData.Value);
                }
            }
        }
    }

    public void OnShield()
    {
        Debug.Log("Shield");
    }

    public void OnBoost()
    {
        Debug.Log("Boost");
    }

    public void OnClickButton(Enums.PowerType powerType)
    {
        switch(powerType)
        {
            case Enums.PowerType.Heal:
                OnClean();
                break;
            case Enums.PowerType.Shield:
                OnShield();
                break;
            case Enums.PowerType.Boost:
                OnBoost();
                break;
        }
    }
    
    public void OnButtonHoverEnter(Enums.PowerType powerType)
    {
        float value = powerType switch
        {
            Enums.PowerType.Heal => _cleanData.Range,
            Enums.PowerType.Shield => _shieldData.Range,
            Enums.PowerType.Boost => _boostData.Range,
        };

        DisplayAreaOfEffect(value);
    }
    
    public void OnButtonHoverExit()
    {
        DisplayPowerVisualize(false);
    }

    private void DisplayAreaOfEffect(float value)
    {
        DisplayPowerVisualize(true);
        ModifyCircleVisualisation(value);
    }

    private void DisplayPowerVisualize(bool value)
    {
        _powerVisualizer.SetActive(value);
    }

    private void ModifyCircleVisualisation(float value)
    {
        _powerVisualizer.transform.localScale = new Vector3(value, value, 1);
    }
}
