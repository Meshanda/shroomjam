using UnityEngine;

public class Base : Corruptible
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();

            if (enemy == null) return;
            
            Corrupt(enemy.CorruptionRate);
            enemy.DestroySelf();
        
            // Check if it's game over
            if (Corruption > MaxCorruption)
            {
                // Game Over : Call the event for "Game Over"
                GameManager.OnGameOver?.Invoke(Enums.GameOverType.BaseDestroyed);
            }
            else
            {
                // Call something to tell the corruption has changed (To modify camera / etc. )
                GameManager.OnEnemyHitBase?.Invoke(Corruption / MaxCorruption);
            }
        }
    }
}
