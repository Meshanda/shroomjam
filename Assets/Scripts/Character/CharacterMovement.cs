using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Vector2 _movement;
    private void Update()
    {
        transform.Translate(_movement * (_speed * Time.deltaTime));
    }

    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>().normalized;
    }
}
