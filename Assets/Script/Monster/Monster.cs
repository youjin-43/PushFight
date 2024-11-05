using UnityEngine;

public class Monster : MonoBehaviour
{

    
  


    [Header("INFO")]
    
    public int Hp = 100;
    int damage;
    public bool isAlive;


    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        Hp = GameManager.instance.stage * 500 ; // 스테이지 마다 n배로 체력 증가  
        damage = PlayerInfo.instance.attackCnt;//플레이어 인포에서 데미지를 가져옴 
        transform.position = new Vector3(0, 20f, -62); //소환!
        GameManager.instance.currentMonster = this; // 현재 스테이지 몬스터로 지정 
        isAlive = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        //등장해서 땅에 닿았다면 랜딩 
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetTrigger("Land");
            UIManager.instance.MonsterHP_UI.SetActive(true); // HPUI 활성화 //TODO : 이거 좀 자연스럽게 바꾸고 싶은데 
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
            UIManager.instance.ShowDamageUI(damage);
        }
    }


    public void GetDamage(int n)
    {
        Hp -= n;
        GameManager.instance.PushCnt++;
        Debug.Log("남은 HP : " + Hp);
        UIManager.instance.MonsterHP_text.text = Hp.ToString(); // HP text 셋팅
        if (Hp <= 0) Death();
    }

    void Death()
    {
        Debug.Log("몬스터 죽음!");
        animator.SetTrigger("Death"); // 몬스터 죽는 애니메이션 실행
        isAlive = false;
        StartCoroutine(GameManager.instance.VictoryRoutine());
    }
}
