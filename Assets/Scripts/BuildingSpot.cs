using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BuildingSpot : MonoBehaviour
{
    [SerializeField] private GameObject _buildingMenuPfb;
    
    private GameObject _buildingMenuInstance;
    private CircleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _collider.isTrigger = true;
        _collider.radius = 10f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        if (_buildingMenuInstance == null) 
            _buildingMenuInstance = Instantiate(_buildingMenuPfb, transform.position, Quaternion.identity);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        CloseBuildingMenu();
    }
    
    private void CloseBuildingMenu()
    {
        if (_buildingMenuInstance == null) return;
        
        Destroy(_buildingMenuInstance);
        _buildingMenuInstance = null;
    }
}