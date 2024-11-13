using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Power : MonoBehaviour
{
    [SerializeField] protected PowerDataSO _dataSo;
    protected List<Collider2D> _colliders;

    protected PowerEffect _powerEffect;
    
    public abstract void UseAbility();
    
    private void Awake()
    {
        _dataSo.InitData();
    }
    
    public float GetRange()
    {
        return _dataSo.Range;
    }

    public void SetPowerEffect(PowerEffect powerEffect)
    {
        _powerEffect = powerEffect;
    }
    
    protected List<Collider2D> CheckOverlapCircle(float range, string tag = null)
    {
        List<Collider2D> results = new List<Collider2D>();
        Collider2D[] overlapResults = Physics2D.OverlapCircleAll(transform.position, range / 2);
        if (overlapResults.Length > 0)
        {
            foreach (Collider2D collider2d in overlapResults)
            {
                if (collider2d.CompareTag("Player")) 
                    continue;
                
                if (collider2d.gameObject.layer == LayerMask.NameToLayer("Corruptible"))
                {
                    if (tag != null)
                    {
                        if(collider2d.CompareTag(tag))
                        {
                            results.Add(collider2d);
                        }
                    }
                    else
                    {
                        results.Add(collider2d);
                    }
                }
            }
        }
        return results;
    }

    protected List<T> GetGenericTypeList<T>()
    {
        List<T> corruptibles = new List<T>();
        foreach (Collider2D collider in _colliders)
        {
            T corruptible = collider.GetComponentInParent<T>();
            corruptibles.Add(corruptible);
        }
        return corruptibles;
    }
    
        
    // Handle the Cooldown of the Spells
    protected IEnumerator CooldownCoroutine(PowerDataSO ability)
    {
        ability.IsAvailable = false;
        
        float elapsedTime = 0f;

        while (elapsedTime < ability.Cooldown)
        {
            elapsedTime += Time.deltaTime;
            
            ability.RemainingTimeCooldown = ability.Cooldown - elapsedTime;
            
            yield return null;
        }
        
        ability.IsAvailable = true;
    }
}
