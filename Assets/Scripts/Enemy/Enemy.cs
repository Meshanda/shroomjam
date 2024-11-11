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

    public static event Action<Enemy> OnDeath;

    public float Health
    {
        get => _health;
        private set => _health = value >= 0 ? value : 0;
    }

    public float CorruptionRate => _corruptionRate;

    public float Speed => _speed;
    

    public void Setup(BezierSpline spline, Tilemap tilemap)
    {
        GetComponent<EnemyPathController>().SetPathCreator(spline);
        Tilemap = tilemap;
    }

    private void Die()
    {
		OnDeath?.Invoke(this);
        
        TileManager.Instance.EnemyDead(transform.position, _corruptionRate);

        DestroySelf();
        
    }

    public void DestroySelf()
    {
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
            if (other.gameObject.GetComponent<Corruptible>() is { } corruptibleEntity)
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
        if (other.gameObject.layer.CompareTo(LayerMask.NameToLayer("Corruptible")) == 0)
        {
            if (other.gameObject.GetComponent<Corruptible>() is { } corruptibleEntity)
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
            var corruptiblesAroundCopy = new List<Corruptible>(_corruptiblesAround);

            foreach (var corruptibleEntity in corruptiblesAroundCopy)
            {
                corruptibleEntity.Corrupt(_corruptionRate);
            }

            yield return new WaitForSeconds(_corruptionSpeed);
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        
        if (Health <= 0) 
            Die();
    }
}
