using UnityEngine;


public class GameManager : MonoBehaviour
{
    //싱글턴으로
    public static GameManager instance; // 싱글톤을 할당할 전역 변수 -> 이 instance 자체는 게임 오브젝트를 얘기하는것 같고 

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake()
    {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
            Debug.Log("게임매니저가 생성됐습니다");
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 삭제되지 않도록 
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
            Debug.Log("게임매니저를 삭제합니다");
        }
    }

    public enum State
    {
        Day,
        Night,
        GameOver
    }


    public State GameState = State.Day;
    
    public float SkyScrollSpeed = 5f;

    void Start()
    {
        MapManager.instance.GenTiles();// 맵 생성 
        MapManager.instance.GenEnergy(); // 에너지 맵에 배치
        ChangeStateToDay(); //게임 상태 낮으로 시작 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (GameState == State.Day) {
                ChangeStateToNight();
            }
            else
            {
                ChangeStateToDay();
            }
        }
    }
    
    // TODO : 플레이어 전투 애니메이션으로  전환
    // TODO : 카메라 위치 전환 
    void ChangeStateToNight()
    {
        Debug.Log("밤이 되었습니다~");
        GameState = State.Night;
        MapManager.instance.Stop_ItemSpawnRepeatedly();
        MapManager.instance.Stop_TileScrolling();
    }

    void ChangeStateToDay()
    {
        Debug.Log("낮이 되었습니다~");
        GameState = State.Day;
        MapManager.instance.Start_ItemSpawnRepeatedly();
        MapManager.instance.Start_TileScrolling();
    }

    void GameOver()
    {
        //게임오버 
    }
}
