using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    private Vector2 _movement;
    
    [SerializeField] private float _speed;

    [SerializeField] private float _corruption = 0.0f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_movement * (_speed * Time.deltaTime));
    }

    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>().normalized;
    }
}
