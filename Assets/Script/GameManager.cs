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
        Day, // 0.7 ~ 0.1
        SunSet, // 0.1 ~ 0.2
        Night, // 0.3 ~ 0.6
        Twilight, // 0.6 ~ 0.7
        GameOver
    }


    public State GameState = State.Day;

    [Header("TimeState Control")]
    public float Daytime = 20;
    public float SkyScrollSpeed = 5f;
    float Offset_DayStart = 0.7f;
    [SerializeField] Renderer SkyRend;
    [SerializeField] float offset;

    void Start()
    {
        MapManager.instance.GenTiles();// 맵 생성 
        MapManager.instance.GenEnergy(); // 에너지 맵에 배치
    }
   
    void Update()
    {
        offset = Offset_DayStart + (Time.time * SkyScrollSpeed / 200) % 1f;
        SkyRend.material.mainTextureOffset = new Vector2(offset, 0);

        if(0.8f<offset && offset <= 0.9)
        {
            // 저녁
            if(GameState != State.SunSet) ChangeState_ToSunSet();

        }
        else if (0.9 < offset && offset <= 1.3)
        {
            // 밤 
            if (GameState != State.Night) ChangeStateToNight();
        }
        else if (1.3 < offset && offset <= 1.4)
        {
            //황혼 
            if (GameState != State.Twilight) ChangeState_ToTwilight();
        }
        else
        {
            //낮 
            if (GameState != State.Day) ChangeStateToDay();
        }
    }

    /// <summary>
    /// 5가 디폴트 
    /// </summary>
    void SetTimeSpeed(float speed)
    {
        SkyScrollSpeed = speed;
    }

    void ChangeStateToDay()
    {
        Debug.Log("낮이 되었습니다~");
        GameState = State.Day;
        MapManager.instance.Start_ItemSpawnRepeatedly();
        MapManager.instance.Start_TileScrolling();
    }


    /// <summary>
    /// 저녁 : 아이템 스폰이 멈춤 
    /// </summary>
    void ChangeState_ToSunSet()
    {
        Debug.Log("저녁이 되었습니다~");
        GameState = State.SunSet;
        SetTimeSpeed(2);
        MapManager.instance.Stop_ItemSpawnRepeatedly();
    }


    /// <summary>
    /// 밤 : 전투 준비 + 몹 스폰 + 전투 진행
    /// </summary>
    
    // TODO : 캐릭터 달리기 모션 멈추기 
    void ChangeStateToNight()
    {
        Debug.Log("밤이 되었습니다~");
        GameState = State.Night;
        SetTimeSpeed(5);
        MapManager.instance.Stop_TileScrolling();
    }

    void ChangeState_ToTwilight()
    {
        Debug.Log("새벽이 되었습니다~");
        GameState = State.Twilight;
    }




    void GameOver()
    {
        //게임오버 
    }
}
