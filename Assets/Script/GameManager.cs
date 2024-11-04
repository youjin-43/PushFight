using UnityEngine;
using System.Collections;

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

    [SerializeField] PlayerController playerController; //인스펙터에서 할당
    [SerializeField] GameObject mainCamera;
    [SerializeField] Vector3 cameraAttackModePos = new Vector3(-3.5f,10f,-2.6f);
    [SerializeField] Vector3 cameraAttackModeAngle = new Vector3(0.1f,179f,359f);

    public State GameState = State.Day;

    [Header("TimeState Control")]
    public float Daytime = 20;
    public float SkyScrollSpeed = 0.0015f;
    float Offset_DayStart = 0.7f;
    [SerializeField] Renderer SkyRend;
    [SerializeField] float offset;

    void Start()
    {
        MapManager.instance.GenTiles();// 맵 생성 
        MapManager.instance.GenEnergy(); // 에너지 맵에 배치
        ChangeStateToDay(); //처음 낮으로 게임 시작 



    }
   
    void Update()
    {
        offset = ( Time.time * SkyScrollSpeed ) % 1f;
        SkyRend.material.mainTextureOffset = new Vector2(Offset_DayStart + offset, 0);

        if(offset <= 0.4)
        {
            //Debug.Log("낮 " + offset);
            //낮 
            if (GameState != State.Day) ChangeStateToDay();

        }
        else if (offset <= 0.5)
        {
            //Debug.Log("저녁 " + offset);
            // 저녁
            if (GameState != State.SunSet) ChangeState_ToSunSet();

        }
        else if (offset <= 0.9)
        {
            //Debug.Log("밤 " + offset);
            // 밤 
            if (GameState != State.Night) ChangeStateToNight();
        }
        else
        {
            //Debug.Log("황혼 " + offset);
            //황혼 
            if (GameState != State.Twilight) ChangeState_ToTwilight();
        }
    }

    void ChangeStateToDay()
    {
        Debug.Log("낮이 되었습니다~"+ offset);
        GameState = State.Day;
        MapManager.instance.Start_ItemSpawnRepeatedly();
        MapManager.instance.Start_TileScrolling();
    }


    /// <summary>
    /// 저녁 : 아이템 스폰이 멈춤 
    /// </summary>
    void ChangeState_ToSunSet()
    {
        Debug.Log("저녁이 되었습니다~"+offset);
        GameState = State.SunSet;
        MapManager.instance.Stop_ItemSpawnRepeatedly();
    }


    /// <summary>
    /// 밤 : 전투 준비 + 몹 스폰 + 전투 진행
    /// </summary>


   
    // TODO : 몹 등장 -> 캐릭터가 조준
    // TODO : 스페이스바를 누르면 화살 나감 -> 몹이 맞으면 피격 모션 -> 체력 바 닳기
    // TODO : 몹 체력 0 되면 사망 모션 
    void ChangeStateToNight()
    {
        Debug.Log("밤이 되었습니다~"+ offset);
        GameState = State.Night;
        MapManager.instance.Stop_TileScrolling(); // 타일 스크롤링이 멈추고
        playerController.StopRunning(); // 플레이어가 달리기를 멈추고 전투준비
        StartCoroutine(CameraMovetoAttack());
    }

    void ChangeState_ToTwilight()
    {
        Debug.Log("새벽이 되었습니다~" + offset);
        GameState = State.Twilight;
    }

    void GameOver()
    {
        //게임오버 
    }


    /// <summary>
    /// 1초동안 카메라를 공겨 모드 위치로 이동 
    /// </summary>
    /// <returns></returns>
    IEnumerator CameraMovetoAttack()
    {
        float delta = 0;
        float duration = 1f;
        while (delta <= duration)
        {
            float t = delta / duration;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraAttackModePos, t);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, Quaternion.Euler(cameraAttackModeAngle), t);

            delta += Time.deltaTime;
            yield return null;
        }
    }
}
