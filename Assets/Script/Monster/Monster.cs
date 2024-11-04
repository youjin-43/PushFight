using UnityEngine;

public class Monster : MonoBehaviour
{

    
    // TODO : 죽이면 빅토리 뜨고 플레이어가 뒤돌며 승리 모션


    [Header("INFO")]
    //TODO : 스테이지 지날떄마다 2배로 늘어나면 되려나?? 
    int hp = 100;
    int damage;
    public bool isAlive;


    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        damage = PlayerInfo.instance.attackCnt;//플레이어 인포에서 데미지를 가져옴 
        transform.position = new Vector3(0, 20f, -62); //소환!
        GameManager.instance.currentMonster = gameObject; // 현재 스테이지 몬스터로 지정 
        isAlive = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        //등장해서 땅에 닿았다면 랜딩 
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetTrigger("Land");
        }
    }

    //화살에서 호출 
    public void GetHit() 
    {
        //살아있는 경우에만 맞은 모션을 실행하도록 
        if (isAlive)
        {
            GetDamage(damage);
            animator.SetTrigger("Hit");
        }
    }


    // TODO : 체력 바 닳기 -> 체력 바 말고 텍스트로 크게 써놓고 랜덤위치에 -damage 나오도록 해야겠다
    void GetDamage(int n)
    {
        hp -= n;
        Debug.Log("남은 HP : " + hp);
        if (hp < 0) Death();
    }


    //TODO : 페이드 아웃으로 사라지게 하고 싶은데 되려나?
    void Death()
    {
        Debug.Log("몬스터 죽음!");
        animator.SetTrigger("Death"); // 몬스터 죽는 애니메이션 실행
        isAlive = false;
    }
}
