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
    
    
    private List<Corruptible> _corruptiblesAround = new List<Corruptible>();

    public Action<Enemy> OnDeath;

    public float Health
    {
        get => _health;
        private set => _health = value >= 0 ? value : 0;
    }

    public float CorruptionRate => _corruptionRate;

    public float Speed => _speed;

    private void OnEnable()
    {
        StartCoroutine(Corrupt());
    }

    private void OnDisable()
    {
        StopCoroutine(Corrupt());
    }


    public void Setup(BezierSpline spline, Tilemap tilemap)
    {
        GetComponent<EnemyPathController>().SetPathCreator(spline);
        Tilemap = tilemap;
    }

    private void Die()
    {
        TileManager.Instance.EnemyDead(transform.position, _corruptionRate);

        MoneyManager.AddMoney?.Invoke(_earnedPoints);
        DestroySelf();
        
    }
    
    

    public void DestroySelf()
    {
        OnDeath?.Invoke(this);

        Destroy(gameObject);
    }

    public void CheckValues()
    {
        // bunch of test here
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.CompareTo(LayerMask.NameToLayer("Corruptible")) == 0)
        {
            if (other.gameObject.GetComponentInParent<Corruptible>() is { } corruptibleEntity)
            {
                _corruptiblesAround.Add(corruptibleEntity);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer.CompareTo(LayerMask.NameToLayer("Corruptible")) == 0)
        {
            if (other.gameObject.GetComponentInParent<Corruptible>() is { } corruptibleEntity)
            {
                _corruptiblesAround.Remove(corruptibleEntity);
            }
        }
    }

    private IEnumerator Corrupt()
    {
        while (true)
        {
            yield return new WaitForSeconds(_corruptionSpeed);

            if (_corruptiblesAround.Count == 0) continue;
            
            var corruptiblesAroundCopy = new List<Corruptible>(_corruptiblesAround);

            foreach (var corruptibleEntity in corruptiblesAroundCopy)
            {
                corruptibleEntity.Corrupt(_corruptionRate);
            }

        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        
        if (Health <= 0) 
            Die();
    }
}
