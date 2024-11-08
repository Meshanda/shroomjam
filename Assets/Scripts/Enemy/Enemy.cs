using System;
using PathCreation;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(EnemyPathController))]
public class Enemy : Entity
{
    [Tooltip("Enemy walk speed")]
    [SerializeField] private float _speed;
    [Tooltip("Enemy health point")]
    [SerializeField] private float _health;

    [Tooltip("Enemy corruption rate")]
    [SerializeField] private float _corruptionRate;
    
    [Tooltip("money point gained after killing the enemy")]
    [SerializeField] private int _earnedPoints = 0;
    
    
    
    
    private EnemyPathController _pathController;
    

    public float Health
    {
        get => _health;
        private set
        {
            if(value >= 0)
            {
                _health = value;
            }
            else
            {
                _health = 0;
            }
        }
    }

    public float CorruptionRate => _corruptionRate;

    public float Speed => _speed;


    private void Update()
    {
    }

    public void Setup(PathCreator pathCreator)
    {
        _pathController.SetPathCreator(pathCreator);
    }

    public void Damage(float damagePoint)
    {
        Health -= damagePoint;
        if (Health <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        // bunch of stuff here
    }

    public void CheckValues()
    {
        // bunch of test here
    }
}
