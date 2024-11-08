using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    private Vector2 _movement;
    
    private Transform _player;
    
    
    
    [SerializeField] private float _speed;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _player.Translate(_movement * (_speed * Time.deltaTime));
    }

    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>().normalized;
    }
}
