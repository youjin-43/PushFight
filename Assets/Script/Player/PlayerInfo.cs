using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{

    //싱글턴으로
    public static PlayerInfo instance; // 싱글톤을 할당할 전역 변수 -> 이 instance 자체는 게임 오브젝트를 얘기하는것 같고 

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake()
    {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
            Debug.Log(" 플레이어 인포가 생성됐습니다");
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 플레이어 인포 가 존재합니다!");
            Destroy(gameObject);
            Debug.Log("플레이어 인포를 삭제합니다");
        }
    }

    public int energeCnt = 0;
    public int attackCnt = 1;


    [Header("UI obs")]
    public TMP_Text EnergyText;
    public TMP_Text AttackText;

    private void Start()
    {
        EnergyText.text = energeCnt.ToString(); 
        AttackText.text = attackCnt.ToString();
    }

    public void IncreseEnergeCnt()
    {
        energeCnt++;
        EnergyText.text = energeCnt.ToString();
    }

    public void IncreseAttackCnt()
    {
        attackCnt++;
        AttackText.text = attackCnt.ToString();
    }
}
