using System;
using UnityEngine;

public class TowerBuff : MonoBehaviour
{
    [SerializeField] private GameObject HealIcon; 
    [SerializeField] private GameObject ShieldIcon; 
    [SerializeField] private GameObject AttackSpeedIcon;

    private void Awake()
    {
        HealIcon.SetActive(false);
        ShieldIcon.SetActive(false);
        AttackSpeedIcon.SetActive(false);
    }

    public void Shield(bool value)
    {
        ShieldIcon.SetActive(value);
    }

    public void Heal(bool value)
    {
        HealIcon.SetActive(value);
    }

    public void AttackSpeed(bool value)
    {
        AttackSpeedIcon.SetActive(value);
    }
}
