using UnityEngine;

[CreateAssetMenu(menuName = "CustomTile/MainBaseTile")]
public class MainBaseTile : Corruptible
{
    [SerializeField] private float _corruptionMax;

    public void EnemyTouched(float corruption)
    {
        CorruptionState += corruption;
        
        // Modify MainBase Sprite ? 
        
        
        // Check if it's game over
        if (CorruptionState > _corruptionMax)
        {
            // Game Over : Call the event for "Game Over"
            GameManager.OnGameOver.Invoke(GameManager.GameOverType.BaseDestroyed);
        }
        else
        {
            // Call something to tell the corruption has changed (To modify camera / etc. )
            
        }
    }
}