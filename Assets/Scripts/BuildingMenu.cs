using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMenu : MonoBehaviour
{
    [SerializeField] private List<BuildingMenuButton> _buttons;
    
    public static event Action<Enums.TowerType> OnTowerBuilt;

    private Vector3Int _tilePosition;
    
    public void Init(Vector3Int tilePos)
    {
        _tilePosition = tilePos;
    }
    
    public void OnButtonClicked(Enums.TowerType towerType)
    {
        OnTowerBuilt?.Invoke(towerType);
        
        switch (towerType)
        {
            case Enums.TowerType.Basic:
                TileManager.Instance.SetTileTourBasic(_tilePosition);
                break;
            case Enums.TowerType.Fast:
                TileManager.Instance.SetTileTourFast(_tilePosition);
                break;
            case Enums.TowerType.Sniper:
                TileManager.Instance.SetTileTourSniper(_tilePosition);
                break;
            case Enums.TowerType.Support:
                TileManager.Instance.SetTileTourSupport(_tilePosition);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(towerType), towerType, null);
        }
    }
}