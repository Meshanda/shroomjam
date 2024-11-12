using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;
    
    private float _damage = 1f;
    private float _corruption = 0.1f;
    private Transform _target;
    private CircleCollider2D _collider;

    private GameObject _owner;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _collider.isTrigger = true;
    }

    private void OnEnable()
    {
        Destroy(gameObject, 5f);
    }

    public void Init(Transform target, float damage, float corruption, GameObject owner)
    {
        _target = target;
        _damage = damage;
        _corruption = corruption;
        _owner = owner;
    }
    
    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }
        
        Vector3 dir = _target.position - transform.position;
        float distanceThisFrame = _speed * Time.deltaTime;
        
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Enemy>().TakeDamage(_damage);
            Destroy(gameObject);
        }
        
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Corruptible")) && other.gameObject != _owner)
        {
            var corruptibleCollided = other.GetComponentInParent<Corruptible>();
            if (corruptibleCollided.Corruption >= corruptibleCollided.MaxCorruption) return;
            
            corruptibleCollided.Corrupt(_corruption);
            Destroy(gameObject);
        }
        
    }
}
