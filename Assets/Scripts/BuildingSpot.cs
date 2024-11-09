using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BuildingSpot : MonoBehaviour
{
    [SerializeField] private GameObject _buildingMenuPfb;
    
    private BuildingMenu _buildingMenuInstance;
    private CircleCollider2D _collider;
    private Vector3Int _tilePos;

    private void OnEnable()
    {
        BuildingMenu.OnTowerBuilt += OnTowerBuiltHandler;
    }

    private void OnDisable()
    {
        BuildingMenu.OnTowerBuilt -= OnTowerBuiltHandler;
    }

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _collider.isTrigger = true;
        _collider.radius = 10f;
    }

    private void Start()
    {
        _tilePos = TileManager.Instance.SpecialTilemap.WorldToCell(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        OpenBuildingMenu();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        CloseBuildingMenu();
    }

    private void OnTowerBuiltHandler(Enums.TowerType towerType)
    {
        CloseBuildingMenu();
    }
    
    private void OpenBuildingMenu()
    {
        if (_buildingMenuInstance != null) return;
        
        
        _buildingMenuInstance = Instantiate(_buildingMenuPfb, transform.position, Quaternion.identity).GetComponent<BuildingMenu>();
        _buildingMenuInstance.Init(_tilePos);
    }
    
    private void CloseBuildingMenu()
    {
        if (_buildingMenuInstance == null) return;
        
        Destroy(_buildingMenuInstance.gameObject);
        _buildingMenuInstance = null;
    }
}