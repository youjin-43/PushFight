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
    ItemPool ArrowPool;

    private void Start()
    {
        ArrowPool = FindAnyObjectByType<ItemPool>();
        animator = GetComponent<Animator>();
        currentJumpCnt = 0;
    }

    private void Update()
    {

        //좌우 이동 -> 사실 필요없는뎅;
        //float h = Input.GetAxis("Horizontal");
        //transform.Translate((new Vector3(h, 0, 0) * moveSpeed) * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(GameManager.instance.GameState != GameManager.State.Night)
            {
                //밤이 아닐때는 점프
                currentJumpCnt++;
                Jump();

            }
            else
            {
                //밤에는 공격
                Attack();
            }
        }   

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //Debug.Log("Landed");
            currentJumpCnt = 0;
            animator.SetBool("Land",true);

        }
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


    public void StartRunninng()
    {
        Debug.Log("StartRunninng");
        animator.SetBool("Combat", false);
    }

    public void StopRunning()
    {
        Debug.Log("StopRunning");
        animator.SetBool("Combat", true);
    }

    public void Aim()
    {
        Debug.Log("Aim");
        animator.SetTrigger("Aim");
    }

    Vector3 arrowOffset = new Vector3(0, 1, -1);
    private void Attack()
    {
        //Debug.Log("Attack");
        animator.SetTrigger("Shot");
        GameObject go = ArrowPool.GetArrowObj();
        go.transform.position = transform.position+ arrowOffset; //화살을 플레이어 위치로 
        // 화살이 활성화 되면 알아서 발사 됨 
    }

    public void Victory()
    {
        Debug.Log("Victory!");
        animator.SetTrigger("Victory");
    }


    public void IncreasejumpMaxCnt()
    {
        jumpMaxCnt++;
    }
}
