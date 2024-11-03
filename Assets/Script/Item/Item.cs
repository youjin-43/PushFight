using UnityEngine;

public class Item : MonoBehaviour
{
    float scrollSpeed;
    /// <summary>
    /// 1은 에너지, 2는 공격, 3은 증강 
    /// </summary>
    [SerializeField] int type; //인스펙터에서 할당

    private void Start()
    {
        scrollSpeed = MapManager.instance.scrollSpeed;
    }

    protected virtual void Update()
    {
        transform.position += Vector3.forward * scrollSpeed * Time.deltaTime; // 매 프레임 스크롤 스피드 만큼 이동
        if (transform.position.z > 0) gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {               
            gameObject.SetActive(false); //비활성화 

            switch (type)
            {
                case 1:
                    other.GetComponent<PlayerInfo>().IncreseEnergeCnt();
                    break;
                case 2:
                    other.GetComponent<PlayerInfo>().IncreseAttackCnt();
                    break;
                default:
                    break;
            }

        }
    }
}
 