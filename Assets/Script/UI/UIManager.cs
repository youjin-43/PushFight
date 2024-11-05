using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class UIManager : MonoBehaviour
{
    //싱글턴으로
    public static UIManager instance; // 싱글톤을 할당할 전역 변수 -> 이 instance 자체는 게임 오브젝트를 얘기하는것 같고 

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake()
    {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
            Debug.Log("UIManager가 생성됐습니다");
        }
        else
        {
            // instance에 이미 다른 UIManager 오브젝트가 할당되어 있는 경우 씬에 두개 이상의 UIManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 UIManager가 존재합니다!");
            Destroy(gameObject);
            Debug.Log("UIManager를 삭제합니다");
        }
    }

    public GameObject WarningUI;//인스펙터에서 할당 
    public GameObject MonsterHP_UI; //인스펙터에서 할당 
    public TextMeshProUGUI MonsterHP_text; //인스펙터에서 할당
    public GameObject Victory_UI; //인스펙터에서 할당
    public GameObject GameOverUI;//인스펙터에서  할당
    public GameObject PauseUI;

    

    [Header("DamageText")]
    public GameObject DamagetextPrefab; //인스펙터에서 할당 
    [SerializeField] int DamagetextPoolCnt = 30; 
    [SerializeField] GameObject DamagetextPool; //인스펙터에서 할당 
    public List<GameObject> DamagetextObjs;


    private void Start()
    {
        // 텍스트 오브젝트 풀 생성
        DamagetextObjs = new List<GameObject>();
        for (int i = 0; i < DamagetextPoolCnt; i++)
        {
            GameObject go = Instantiate(DamagetextPrefab, DamagetextPool.transform);
            DamagetextObjs.Add(go);
            go.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseUI.activeSelf)
            {
                DisablePause();
            }
            else
            {
                PauseUI.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void DisablePause()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// 데미지 텍스DamagetextObjs 오브젝트하나를 활성화해서 넘겨주는 함수 
    /// </summary>
    public GameObject GetDamageTextObj()
    {
        foreach (GameObject t in DamagetextObjs)
        {
            if (!t.activeSelf)
            {
                t.SetActive(true);
                return t;
            }
        }

        GameObject go = Instantiate(DamagetextPrefab, DamagetextPool.transform);
        DamagetextObjs.Add(go);
        go.SetActive(false);
        return go;
    }

    public void ShowDamageUI(int damage)
    {
        GameObject d = GetDamageTextObj();
       
        d.GetComponent<TextMeshProUGUI>().text = "-" + damage.ToString();
    }

    public void ShowVictoryUI()
    {
        Victory_UI.SetActive(true);
    }

    //증강 버튼에 할당돼있음 
    public void CloseVictoryUI()
    {
        Victory_UI.SetActive(false);
        GameManager.instance.TimeRunning = true;
        //GameManager.instance.TimeJumptoTwilight();
        //Todo : jump to twilight
        if (GameManager.instance.GameState != GameManager.State.Twilight) GameManager.instance.SkyScrollSpeed = 0.08f;
    }


}
