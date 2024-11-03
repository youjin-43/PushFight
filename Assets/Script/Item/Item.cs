using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    float scrollSpeed;
    Coroutine coroutine;

    /// <summary>
    /// 1은 에너지, 2는 공격, 3은 증강 
    /// </summary>
    [SerializeField] int type; //인스펙터에서 할당

    private void Start()
    {
        scrollSpeed = MapManager.instance.scrollSpeed;
    }

    IEnumerator ItemScrollCoroutine()
    {
        //Debug.Log(gameObject.name + "의 코루틴 실행");
        while (true)
        {
            transform.position += Vector3.forward * scrollSpeed * Time.deltaTime; // 매 프레임 스크롤 스피드 만큼 이동
            if (transform.position.z > 0) gameObject.SetActive(false);
            yield return null;
        }
    }


    // 오브젝트가 비활성화되면 코루틴이 알아서 멈추는지 확인 -> 지피티는 즉시 종료된다고 함~ 개꿀. 
    public void StartItemScolling()
    {
        coroutine = StartCoroutine(ItemScrollCoroutine());
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
 