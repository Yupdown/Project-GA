using UnityEngine;

namespace Gnome.Isometric.Prototype
{
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

        [SerializeField]
        private Animator _mecanimAnimator;

        private PlayerWeaponHandler _weaponHandler;

        private bool _running;

        public float ViewAngle
        {
            get
            { return viewAngle; }
        }
        private float viewAngle;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _weaponHandler = GetComponent<PlayerWeaponHandler>();

            _running = false;
        }

        private void Update()
        {
            Vector3 velocity = _controller.velocity;
            Vector3 localMoveVelocity = Vector3.zero;

            float finalMoveSpeed = _weaponHandler.ApplyPlayerMoveSpeed(_moveSpeed * (_running ? 2f : 1f), this);

            localMoveVelocity.x = Input.GetAxisRaw("Horizontal");
            localMoveVelocity.z = Input.GetAxisRaw("Vertical");
            localMoveVelocity = localMoveVelocity.normalized * finalMoveSpeed;

            if (Input.GetKeyDown(KeyCode.Space) && _controller.isGrounded)
                localMoveVelocity.y = _jumpForce;

            float cameraAngle = _cameraTransform.localEulerAngles.y;

            Vector3 moveVelocity = Quaternion.AngleAxis(cameraAngle, Vector3.up) * localMoveVelocity;

            velocity = new Vector3(moveVelocity.x, velocity.y + moveVelocity.y + _gravityForce * Time.deltaTime, moveVelocity.z);
            Vector2 flatVelocity = new Vector2(velocity.x, velocity.z);

            _controller.Move(velocity * Time.deltaTime);

            if (velocity.x != 0f && velocity.z != 0f)
                viewAngle = Mathf.Atan2(velocity.z, velocity.x) * Mathf.Rad2Deg;

            if (Input.GetKeyDown(KeyCode.LeftShift))
                _running = true;
            if (localMoveVelocity == Vector3.zero)
                _running = false;

            _mecanimAnimator.SetFloat("MoveSpeed", flatVelocity.magnitude);
        }
    }
}