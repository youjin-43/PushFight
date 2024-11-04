using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("INFO")]
    //TODO : 스테이지 지날떄마다 2배로 늘어나면 되려나?? 
    int hp = 100;
    int damage;


    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        //등장해서 땅에 닿았다면 랜딩 
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetTrigger("Land");
        }


        //TODO : HP가 깎이고 hit 애니메이션 실행
        //TODO : 플레이어 인포를 어떻게 받아오지? 
        if (collision.gameObject.CompareTag("Arrow"))
        {
            GetDamage(damage);
        }

    }


    private void OnEnable()
    {
        damage = PlayerInfo.instance.attackCnt;//플레이어 인포에서 데미지를 가져옴 
        transform.position = new Vector3(0, 20f, -62); //소환!
    }

    void GetDamage(int n)
    {
        hp -= n;
        if (hp < 0) Death();
    }


    //TODO : 몬스터 죽는 애니메이션 실행
    //TODO : 페이드 아웃으로 사라지게 하고 싶은데 되려나? 
    void Death()
    {
        Debug.Log("몬스터 죽음!");
    }
}
