using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _gravityForce;

    [SerializeField]
    private Transform _cameraTransform;

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 velocity = _controller.velocity;
        Vector3 localMoveVelocity = new Vector3();

        localMoveVelocity.x = Input.GetAxis("Horizontal") * _moveSpeed;
        localMoveVelocity.z = Input.GetAxis("Vertical") * _moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && _controller.isGrounded)
            localMoveVelocity.y = _jumpForce;

        float viewAngle = _cameraTransform.localEulerAngles.y;

        Vector3 moveVelocity = Quaternion.AngleAxis(viewAngle, Vector3.up) * localMoveVelocity;

        velocity = new Vector3(moveVelocity.x, velocity.y + moveVelocity.y + _gravityForce * Time.deltaTime, moveVelocity.z);

        _controller.Move(velocity * Time.deltaTime);
    }
}
