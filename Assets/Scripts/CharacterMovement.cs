using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInputs playerInput;
    private Vector3 moveDirection;
    [SerializeField] float speed = 5f;
    [SerializeField]private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float smoothTurnVelocity = 0.2f;
    private float smoothTurnVel;
    Transform mainCamera;
    private float targetAngle;
    private Vector3 moveVec;
    private bool canJump = false;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInputs>();
        playerInput.Jumping += Jump;
        mainCamera = Camera.main.transform;
    }

    private void Jump()
    {
        if (controller.isGrounded)
        {
            Debug.Log("isGrounded");
            canJump = true;
            //controller.Move(new Vector3(moveDirection.x * Time.deltaTime, moveDirection.y, moveDirection.z * Time.deltaTime));
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection  = playerInput.moveInput.x * Vector3.right + playerInput.moveInput.y * Vector3.forward;
        if (moveDirection.magnitude != 0) {
            targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVel, smoothTurnVelocity);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        }
        moveDirection = moveDirection.normalized;
        moveVec = new Vector3(moveDirection.x, 0f, moveDirection.z);
        moveVec.y = controller.isGrounded? 0f : moveDirection.y + gravity * Time.deltaTime;
        if (canJump)
        {
            moveVec.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canJump = false;
        }
        moveVec = new Vector3(moveVec.x,0,moveVec.z) * speed + Vector3.up* moveVec.y;
        controller.Move(Time.deltaTime * moveVec);
        
    }
}

