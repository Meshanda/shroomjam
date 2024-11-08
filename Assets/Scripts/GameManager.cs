using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public EnemyDatabaseSO EnemyDatabase;
    
    protected override void SingletonAwake()
    {
        Init();
    }

    private void Init()
    {
        EnemyDatabase.CheckDatabaseIntegrity();
    }
}