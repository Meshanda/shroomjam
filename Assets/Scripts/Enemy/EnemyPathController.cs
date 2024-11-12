using BezierSolution;
using UnityEngine;

public class EnemyPathController : MonoBehaviour
{

    [SerializeField] private BezierWalkerWithSpeed _splineFollower;
    

    private Enemy _enemyData;
    
    
    private void Start()
    {
        _enemyData = GetComponent<Enemy>();

        _splineFollower.speed = _enemyData.Speed;
    }

    public void ChangeSpeed(float newSpeed)
    {
        _splineFollower.speed = newSpeed;
    }
    

    public void SetPathCreator(BezierSpline spline)
    {
        _splineFollower.spline = spline;
    }
}
