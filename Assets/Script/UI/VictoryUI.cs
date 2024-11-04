using UnityEngine;

public class VictoryUI : MonoBehaviour
{

    public void ShowVictoryUI()
    {
        gameObject.SetActive(true);
    }

    public void CloseVictoryUI()
    {
        gameObject.SetActive(false);
    }

    public void SetCloseTrigger()
    {
        GetComponent<Animator>().SetTrigger("Close");
    }
}
