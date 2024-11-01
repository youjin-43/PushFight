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
            Debug.Log("아이템과 충돌");
            other.gameObject.GetComponent<ItemScroll>().GoStartPos(); // 스폰위치로 이동
            energeCnt++;
            EnergyText.text = energeCnt.ToString();
            

        }

    }
}
