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

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _collider.isTrigger = true;
    }

    public void Init(Transform target, float damage, float corruption)
    {
        _target = target;
        _damage = damage;
        _corruption = corruption;
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
        
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Corruptible")))
        {
            other.GetComponent<Corruptible>().Corrupt(_corruption);
        }
    }
}
