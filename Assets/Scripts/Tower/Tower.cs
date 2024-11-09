using UnityEngine;

public abstract class Tower : Corruptible
{
    public Enums.TowerType Type { get; protected set; }
    public float Damage { get; protected set; }
    public float Range { get; protected set; }
    public float AttackSpeed { get; protected set; }
    public int Cost { get; protected set; }
}