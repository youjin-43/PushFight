using UnityEngine;
using TMPro;
public class GameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;

    [SerializeField] TextMeshProUGUI stageInfo;
    [SerializeField] TextMeshProUGUI DamageInfo;
    private void OnEnable()
    {
        stageInfo.text += GameManager.instance.stage;
        DamageInfo.text += GameManager.instance.PushCnt;

        Invoke("ShowStageInfo", 1f);
        Invoke("ShowDamageInfo", 1f);
    }

    void ShowStageInfo()
    {
        stageInfo.gameObject.SetActive(true);
    }

    void ShowDamageInfo()
    {
        DamageInfo.gameObject.SetActive(true);
    }
}
