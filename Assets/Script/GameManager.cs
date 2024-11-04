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

    PlayerController playerController;

    [Header("Camera Control")]
    GameObject mainCamera;
    [SerializeField] Vector3 cameraRunningModePos;
    [SerializeField] Vector3 cameraRunningModeAngle;
    [Space(4f)]
    [SerializeField] Vector3 cameraAttackModePos = new Vector3(-3.5f,10f,-10f); 
    [SerializeField] Vector3 cameraAttackModeAngle = new Vector3(0.1f,179f,359f);

    
    [Space(10f)]
    [Header("TimeState Control")]
    public State GameState = State.Day;
    [SerializeField] float SkyScrollSpeed = 0.02f;
    float Offset_DayStart = 0.7f;
    [SerializeField] Renderer SkyRend;
    [SerializeField] float offset;

    [Header("Monster")]
    public GameObject currentMonster;

    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        mainCamera = GameObject.Find("MainCamera");
        SkyRend = GameObject.Find("SkyDome").GetComponent<Renderer>();

        MapManager.instance.GenTiles();// 맵 생성 
        MapManager.instance.GenEnergy(); // 에너지 맵에 배치

        cameraRunningModePos = mainCamera.transform.position;
        cameraRunningModeAngle = mainCamera.transform.rotation.eulerAngles;
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



    // TODO : 카메라 포지션, 각도 정상화 
    void ChangeStateToDay()
    {
        Debug.Log("낮이 되었습니다~"+ offset);
        GameState = State.Day;
        StartCoroutine(CameraMove(cameraRunningModePos, cameraRunningModeAngle)); //카메라 포지션, 각도 정상화 
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
    void ChangeStateToNight()
    {
        Debug.Log("밤이 되었습니다~"+ offset);
        GameState = State.Night;
        MapManager.instance.Stop_TileScrolling(); // 타일 스크롤링이 멈추고
        playerController.StopRunning(); // 플레이어가 달리기를 멈추고 전투준비
        StartCoroutine(CameraMove(cameraAttackModePos,cameraAttackModeAngle)); // 전투모드로 카메라 위치 이동
        MapManager.instance.Monsters.transform.GetChild(0).gameObject.SetActive(true); // 몬스터 등장
        UIManager.instance.MonsterHP_UI.SetActive(true); // HPUI 활성화 
        UIManager.instance.MonsterHP_text.text = currentMonster.GetComponent<Monster>().Hp.ToString(); // HP text 셋팅

        playerController.Aim(); // 캐릭터가 조준

        
    }


    void ChangeState_ToTwilight()
    {
        Debug.Log("새벽이 되었습니다~" + offset);
        GameState = State.Twilight;

        //몹을 죽였는지 확인
        if (currentMonster.GetComponent<Monster>().isAlive)
        {
            //몹을 죽이지 못한경우 
            //todo : 남은 몹 채력 만큼 에너지 소비

            //todo : 에너지를 다 썼는데도 죽이지 못했다면 게임 오버 

            //todo : 그랬는데도 죽이지 못한경우 몹이 플레이어를 공격하며 게임 오버
            GameOver();
        }
        else
        {
            // TODO : 죽이면 빅토리 뜨고 플레이어가 뒤돌며 승리 모션
            // todo : 보상선택 
            Debug.Log("몹을 죽이는데 성공했습니다");

        }




    }



    // TODO : 게임 오버 구현 
    void GameOver()
    {
        //게임오버
        Debug.Log("GameOver");
    }


    /// <summary>
    /// 1초동안 카메라를 공겨 모드 위치로 이동 
    /// </summary>
    /// <returns></returns>
    IEnumerator CameraMove(Vector3 pos,Vector3 rotate)
    {
        float delta = 0;
        float duration = 1f;
        while (delta <= duration)
        {
            float t = delta / duration;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, pos, t);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, Quaternion.Euler(rotate), t);

            delta += Time.deltaTime;
            yield return null;
        }
    }
}
