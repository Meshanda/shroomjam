using UnityEngine;

[CreateAssetMenu(menuName = "CustomTile/MainBaseTile")]
public class MainBaseTile : CorruptibleTile
{
   
    public override void OnTile(Entity entity)
    {
        if (entity is Enemy enemy)
        {
            EnemyTouched(enemy.CorruptionRate);
        }
    }
    
    public void EnemyTouched(float corruption)
    {
        Corrupt(corruption);
        
        // Modify MainBase Sprite to Show Corruption
        
        
        // Check if it's game over
        if (Corruption > _maxCorruption)
        {
            // Game Over : Call the event for "Game Over"
            GameManager.OnGameOver.Invoke(GameManager.GameOverType.BaseDestroyed);
        }
        else
        {
            // Call something to tell the corruption has changed (To modify camera / etc. )
            GameManager.OnEnemyHitBase.Invoke(Corruption);
        }
    }


}