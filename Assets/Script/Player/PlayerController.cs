using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    //[SerializeField] float moveSpeed = 7f;// for 좌우이동 -> 근데 안씀~
    [SerializeField] float jumpPower = 20f;
    [SerializeField] float DjumpPower = 10f;
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

        //점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentJumpCnt++;
            Jump();


        }   

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Landed");
            currentJumpCnt = 0;
            animator.SetBool("Land",true);

        }
    }

    private void Attack()
    {
        Debug.Log("Attack");
        animator.SetTrigger("Attack");
    }

    private void Jump()
    {
        if (currentJumpCnt <= jumpMaxCnt)
        {
            if (currentJumpCnt==1) // 1단점프
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                animator.SetTrigger("Jump");
            }   
            else //2단 이상 
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * DjumpPower, ForceMode.Impulse);
                animator.SetTrigger("DJump");
            }
            
            animator.SetBool("Land", false);
        }
    }



}
