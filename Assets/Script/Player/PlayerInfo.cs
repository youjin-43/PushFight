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
