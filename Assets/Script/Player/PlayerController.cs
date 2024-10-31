using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    //[SerializeField] float moveSpeed = 7f;// for 좌우이동 -> 근데 안씀~
    [SerializeField] float jumpPower = 20f;
    [SerializeField] int jumpMaxCnt = 2;
    [SerializeField] int currentJumpCnt;
    

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentJumpCnt = 0;
    }

    private void Update()
    {

        //좌우 이동 -> 사실 필요없는뎅;
        //float h = Input.GetAxis("Horizontal");
        //transform.Translate((new Vector3(h, 0, 0) * moveSpeed) * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space)) Jump(); //점프


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Landed");
            currentJumpCnt = 0;
            animator.SetTrigger("Land");

        }
    }


    private void Attack()
    {
        Debug.Log("Attack");
        animator.SetTrigger("Attack");
    }

    private void Jump()
    {
        //Debug.Log("Space is pushed");
        if (currentJumpCnt < jumpMaxCnt)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            currentJumpCnt++;
            animator.SetTrigger("Jump");
        }
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
