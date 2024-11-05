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

    //[Header("Camera Control")]
    GameObject mainCamera;
    Vector3 cameraRunningModePos;
    Vector3 cameraRunningModeAngle;
    Vector3 cameraAttackModePos = new Vector3(-3.5f,10f,-10f); 
    Vector3 cameraAttackModeAngle = new Vector3(0.1f,179f,359f);


    [Space(10f)]
    [Header("TimeState Control")]
    public bool TimeRunning = true;
    public State GameState = State.Day;
    [SerializeField] float SkyScrollSpeed = 0.02f;
    float Offset_DayStart = 0.7f;
    Renderer SkyRend;
    [SerializeField] float offset=0;

    [Header("Monster")]
    public int stage=0;
    public Monster currentMonster;

    [Header("GameOver")]
    public int DamageSum = 0;


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
        TimeRunning = true;

    }
   
    void Update()
    {
        if (TimeRunning)
        {
            //todo : 시간이 흐를 때 안 흐를떄를 제어하는 변수하나 만들어서 안흐를 때는 offset이 증가하지 않도록 해도 될것 같음
            //todo : 이거 계속 안되는데 그냥 넘길때는 아예 옵셋을 지정해버려야하나 
            offset = (Time.time * SkyScrollSpeed) % 1f;
            SkyRend.material.mainTextureOffset = new Vector2(Offset_DayStart + offset, 0);

            if (offset <= 0.45f)
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

                //todo : 밤이 끝나기 전에 일찍 몹을 죽인경우 새벽으로 빠르게 타임 워프 해야함 
            }
            else
            {
                //Debug.Log("황혼 " + offset);
                //황혼 
                if (GameState != State.Twilight) ChangeState_ToTwilight();
            }
        }

    }

    void ChangeStateToDay()
    {
        Debug.Log("낮이 되었습니다~"+ offset);
        GameState = State.Day;
        stage++;

        StartCoroutine(CameraMove(cameraRunningModePos, cameraRunningModeAngle)); //카메라 포지션, 각도 정상화 
        MapManager.instance.Start_ItemSpawnRepeatedly();
        MapManager.instance.Start_TileScrolling();
        playerController.StartRunninng();// 플레이어 다시 달리기 시작

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

        //ReadyToFight
        MapManager.instance.Stop_TileScrolling(); // 타일 스크롤링이 멈추고
        playerController.StopRunning(); // 플레이어가 달리기를 멈추고 전투준비
        StartCoroutine(CameraMove(cameraAttackModePos,cameraAttackModeAngle)); // 전투모드로 카메라 위치 이동
        MapManager.instance.Monsters.transform.GetChild(0).gameObject.SetActive(true); // 몬스터 등장
        UIManager.instance.MonsterHP_text.text = currentMonster.Hp.ToString(); // HP text 셋팅

        playerController.Aim(); // 캐릭터가 조준
    }



    //몬스터 스크립트에서 몬스터가 죽으면 실행됨 
    public IEnumerator VictoryRoutine()
    {
        UIManager.instance.MonsterHP_UI.SetActive(false); //HP UI 끄기 
        currentMonster.gameObject.SetActive(false);// 몬스터 씬에서 없애고
        playerController.Victory(); // 플레이어가 뒤돌며 승리 모션
        TimeRunning = false; //잠시 게임 정지
        UIManager.instance.ShowVictoryUI(); // todo : 보상선택 - 잠깐 타임 스케일 0
        yield return null;
    }

    void ChangeState_ToTwilight()
    {
        Debug.Log("새벽이 되었습니다~" + offset);
        GameState = State.Twilight;
        //if (SkyScrollSpeed != 0.02) SkyScrollSpeed /= 2;//몹 죽였을때 빨라지게 했던 스크롤 스피드 정상화 

        //몹을 죽였는지 확인
        if (currentMonster.isAlive)
        {
            //몹을 죽이지 못한경우 
            StartCoroutine("UseEnergy"); //남은 몹 채력 만큼 에너지 소비
            if (currentMonster.isAlive) GameOver(); //에너지를 다 썼는데도 죽이지 못했다면 게임 오버
        }
        else
        {
            Debug.Log("몹을 죽이는데 성공했습니다");
            
        }
    }


    //todo : 에너지가 부족한경우 
    IEnumerator UseEnergy()
    {
        float timer = 0.1f; //0,1초마다 닳음
        float delta = 0;
        while (currentMonster.Hp > 0)
        {
            delta += Time.deltaTime;
            if(delta> timer)
            {
                delta = 0;
                PlayerInfo.instance.DecreaseEnergeCnt();
                currentMonster.GetDamage(1);
            }
            yield return null;
        }

        yield return null;
    }



    void CloseVictoryUI()
    {
        UIManager.instance.Victory_UI.GetComponent<VictoryUI>().SetCloseTrigger();
    }



    /// <summary>
    /// 1초동안 카메라를 공겨 모드 위치로 이동 
    /// </summary>
    /// <returns></returns>
    IEnumerator CameraMove(Vector3 pos,Vector3 rotate)
    {
        float delta = 0;
        float duration = 1.5f;
        while (delta <= duration)
        {
            float t = delta / duration;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, pos, t);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, Quaternion.Euler(rotate), t);

            delta += Time.deltaTime;
            yield return null;
        }
    }





    // TODO : 게임 오버 구현 
    void GameOver()
    {
        //게임오버
        Debug.Log("GameOver");
        UIManager.instance.GameOverUI.SetActive(true);

    }
}
