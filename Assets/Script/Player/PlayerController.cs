using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7;

    //control
    InputAction moveAction;
    InputAction attackAction;
    InputAction jumpAction;

    [SerializeField] CharacterController characterController;
    [SerializeField] Animator animator;

    private void Start()
    {
        InputActionAsset inputActions = GetComponent<PlayerInput>().actions;
        moveAction = inputActions.FindAction("Move");
        //attackAction = inputActions.FindAction("Attack");
        jumpAction = inputActions.FindAction("Jump");

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(-moveVector.x, 0, 0);
        move = move * moveSpeed * Time.deltaTime;
        characterController.Move(move);

        //if (attackAction.WasPressedThisFrame() == true)
        //{
        //    Attack();
        //}else

        if (jumpAction.WasPressedThisFrame() == true)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                DoubleJump();
            }
            else
            {
                Jump();
            }

            
        }

    }


    private void Attack()
    {
        Debug.Log("Attack");
        animator.SetTrigger("Attack");
    }

    private void Jump()
    {
        Debug.Log("Jump");
        animator.SetTrigger("Jump");
    }

    private void DoubleJump()
    {
        Debug.Log("Jump");
        animator.SetTrigger("DoubleJump");
    }

        private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Item")
        {
            Debug.Log("아이템과 충돌");
            other.gameObject.SetActive(false);

        }

    }
}
