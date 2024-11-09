using BezierSolution;
using UnityEngine;

public class EnemyPathController : MonoBehaviour
{

    [SerializeField] private BezierWalkerWithSpeed _splineFollower;
    

    private Enemy _enemyData;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyData = GetComponent<Enemy>();

        _splineFollower.speed = _enemyData.Speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPathCreator(BezierSpline spline)
    {
        _splineFollower.spline = spline;
    }
}
