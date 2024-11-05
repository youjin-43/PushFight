using UnityEngine;

public class VictoryUI : MonoBehaviour
{

    public void ShowVictoryUI()
    {
        gameObject.SetActive(true);
    }

    //버튼에 할당돼있음 
    public void CloseVictoryUI()
    {
        gameObject.SetActive(false);

        //시간 다시 가도록 -> 새벽이 되기 전 죽였다면 빠르게 새벽으로 이동
        GameManager.instance.TimeRunning = true;
        GameManager.instance.TimeJumptoTwilight();
    }

    public void SetCloseTrigger()
    {
        GetComponent<Animator>().SetTrigger("Close");
    }
}
