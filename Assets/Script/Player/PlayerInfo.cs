using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Energy")
        {
            //Debug.Log("에너지와 충돌");
            other.gameObject.GetComponent<Energy>().GoStartPos(); // 스폰위치로 이동
            energeCnt++;
            EnergyText.text = energeCnt.ToString();
            
        }else if (other.gameObject.tag == "UpgradeItem")
        {
            Debug.Log("업그레이드 아이템 먹음!!");
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "AttackItem")
        {
            Debug.Log("공격 아이템 먹음!!");
            attackCnt++;
            AttackText.text = attackCnt.ToString();
            other.gameObject.SetActive(false);
        }

    }
}
