using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController _contoller;
    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private float _walkSpeed =10f,_shiftSpeed=5f;
    [SerializeField]
    private float _jumpHeight = 5f;

    private float _gravity = -9.81f;
    private Vector3 _velocity;

    private float _groundDistance = 0.25f;
    private bool _isGrounded;

    private float xMovement;
    private float zMovement;
    private float _speed;

    private void Awake()
    {
        _speed = _walkSpeed;
    }
    private void Update()
    {
        Move();
        Jump();
        Gravity();
    }

    private void Move()
    {
        xMovement = Input.GetAxis("Horizontal");
        zMovement = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xMovement + transform.forward * zMovement;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = _shiftSpeed;
        }
        else
        {
            _speed = _walkSpeed;
        }
        _contoller.Move(move * _speed * Time.deltaTime);

    }

    private void Jump()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundLayer);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }

    private void Gravity()
    {
        _velocity.y += _gravity * Time.deltaTime;
        _contoller.Move(_velocity * Time.deltaTime);
    }
}
