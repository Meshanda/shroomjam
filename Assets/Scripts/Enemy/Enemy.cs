using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(EnemyPathController))]
public class Enemy : MonoBehaviour
{
    [Tooltip("Enemy walk speed")]
    [SerializeField] private float _speed;
    [Tooltip("Enemy health point")]
    [SerializeField] private float _health;

    [Tooltip("Enemy corruption rate")]
    [SerializeField] private float _corruptionRate;
    
    [Tooltip("money point gained after killing the enemy")]
    [SerializeField] private int _earnedPoints = 0;
    
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

    public float Speed => _speed;

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
}
