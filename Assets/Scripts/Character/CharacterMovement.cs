using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CharacterMovement : MonoBehaviour
{
    private static readonly int XDir = Animator.StringToHash("xDir");
    private static readonly int YDir = Animator.StringToHash("yDir");
    
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;

    private int _reversed = 1;
    
    private Vector2 _movement;
    private void FixedUpdate()
    {
        transform.Translate(_movement * (_speed * Time.deltaTime));
    }


    public void ReverseInput()
    {
        _reversed *= -1;
    }
    

    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>().normalized * _reversed;
        _animator.SetFloat(XDir, _movement.x);
        _animator.SetFloat(YDir, _movement.y);
    }
}
