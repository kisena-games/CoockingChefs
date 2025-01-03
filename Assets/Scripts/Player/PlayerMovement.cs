using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

[RequireComponent(typeof(CharacterController), typeof(InputHandler))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Move Parameters")]
    [SerializeField] private float moveSpeed = 4.0f;
    [SerializeField] private float rotateSpeed = 10.0f;

    private InputHandler _inputHandler;
    private CharacterController _characterController;
    private Vector3 _moveDirection = Vector3.zero;    

    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _moveDirection = new Vector3(_inputHandler.MoveInput.x, 0f, _inputHandler.MoveInput.y);

        if (_moveDirection != Vector3.zero)
        {
            Move();
            Rotate();
        }
    }

    private void Move()
    {
        _characterController.Move(_moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        gameObject.transform.forward = Vector3.Slerp(transform.forward, _moveDirection, rotateSpeed * Time.deltaTime);
    }
}
