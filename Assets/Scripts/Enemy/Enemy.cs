using System;
using System.Collections;
using System.Collections.Generic;
using BezierSolution;
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
    [Range(0.1f, 10f)]
    [SerializeField] private float _corruptionRate = 0.1f;

    [Range(0.1f, 3f)]
    [SerializeField] private float _corruptionSpeed = 0.1f;
    
    [Tooltip("money point gained after killing the enemy")]
    [SerializeField] private int _earnedPoints = 0;
    
    
    private List<CorruptibleEntity> _corruptiblesAround = new List<CorruptibleEntity>();
    
    

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
    

    public void Setup(BezierSpline spline, Tilemap tilemap)
    {
        GetComponent<EnemyPathController>().SetPathCreator(spline);
        Tilemap = tilemap;
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
        if (CurrentTile is RoadTile roadTile)
        {
            roadTile.Corrupt(_corruptionRate);
        }

        // bunch of stuff here
    }

    public void CheckValues()
    {
        // bunch of test here
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("bouh");
        if (other.gameObject.CompareTag("CorruptibleEntity"))
        {
            if (other.gameObject.GetComponent<CorruptibleEntity>() is { } corruptibleEntity)
            {
                _corruptiblesAround.Add(corruptibleEntity);
            }
            
            if(_corruptiblesAround.Count == 1)
            {
                StartCoroutine(Corrupt());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CorruptibleEntity"))
        {
            if (other.gameObject.GetComponent<CorruptibleEntity>() is { } corruptibleEntity)
            {
                _corruptiblesAround.Remove(corruptibleEntity);
            }
            
            if(_corruptiblesAround.Count == 0)
            {
                StopCoroutine(Corrupt());
            }
        }
    }


    private IEnumerator Corrupt()
    {
        while (_corruptiblesAround.Count >0)
        {
            var corruptiblesAroundCopy = new List<CorruptibleEntity>(_corruptiblesAround);

            foreach (var corruptibleEntity in corruptiblesAroundCopy)
            {
                corruptibleEntity.Corrupt(_corruptionRate);
            }

            yield return new WaitForSeconds(_corruptionSpeed);
        }
    }
}
