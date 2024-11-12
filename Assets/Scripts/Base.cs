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
            
            // Enemy Hit The Base
            float corruptionPercentage = Corruption / MaxCorruption;
            CameraShake.OnEnemyHitBase?.Invoke(corruptionPercentage);
            GameManager.OnEnemyHitBase?.Invoke(corruptionPercentage);
        
            // Check if it's game over
            if (Corruption >= MaxCorruption)
            {
                // Game Over : Call the event for "Game Over"
                GameManager.OnGameOver?.Invoke(Enums.GameOverType.BaseDestroyed);
            }
        }
    }
}
