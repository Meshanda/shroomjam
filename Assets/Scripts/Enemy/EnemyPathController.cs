using PathCreation;
using PathCreation.Examples;
using UnityEngine;

public class EnemyPathController : MonoBehaviour
{
    
#if UNITY_EDITOR
    [SerializeField] private PathCreator _debugPathCreator;
#endif
    
    [SerializeField] private PathFollower _pathFollower;
    

    private Enemy _enemyData;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyData = GetComponent<Enemy>();
        
#if UNITY_EDITOR
        _pathFollower.pathCreator = _debugPathCreator;
#endif
        
        _pathFollower.speed = _enemyData.Speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPathCreator(PathCreator pathCreator)
    {
        _pathFollower.pathCreator = pathCreator;
    }
}
