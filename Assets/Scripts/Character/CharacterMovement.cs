using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private int _reversed = 1;
    
    private Vector2 _movement;
    private void Update()
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
    }
}
