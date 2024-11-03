using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //싱글턴으로
    public static MapManager instance; // 싱글톤을 할당할 전역 변수 -> 이 instance 자체는 게임 오브젝트를 얘기하는것 같고 

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake()
    {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
            Debug.Log("맵매니저가 생성됐습니다");
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 삭제되지 않도록 
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 맵 매니저가 존재합니다!");
            Destroy(gameObject);
            Debug.Log("맵 매니저를 삭제합니다");
        }
    }


    public float scrollSpeed = 20f;
    [Space(10f)]

    //Tile 생성 
    public GameObject[] GroundTilesPrefab;
    GameObject[] GroundTiles;
    int tileCnt = 6;


    #region 아이템 스폰 관련 변수들
    [SerializeField] ItemPool itemPool; // 인스펙터에서 할당해줌 

    [Header("Spawn - Limits")]
    public float startPosX = 0;
    public float topLimit = 15f;
    public float downLimit = 7f;
    [SerializeField] public float spawnPosZ = -180f;
    //TODO: spawnPosZ 스폰되는거 확인하고 조정해줘야함

    [Header("Spawn - Energy")]
    public float startSpawnRate = 10f;
    float startPoint = -50f;
    [SerializeField] int maxSpawnCnt_energy = 15;
    [SerializeField] float spawnTime_energy = 0.3f; //0.3초마다 생성
    [SerializeField] float timer_energy = 0;

    [Header("Spawn - UpgradeItem")]
    [SerializeField] float spawnTime_upgradeItem = 3f; //3초마다 생성
    [SerializeField] float timer_upgradeItem = 0;
    //[SerializeField] float spawnPosZ = -180f;
    #endregion


    //TODO : 에너지 스폰 방식 변경 -> set active 이용하는 방식으로
    


    void Start()
    {
        // count 만큼 루프하면서 발판 생성
        GroundTiles = new GameObject[tileCnt];
        for (int i = 0; i < tileCnt; i++) GroundTiles[i] = Instantiate(GroundTilesPrefab[i%3], new Vector3(0, 0, -100*i), Quaternion.identity);

        // 에너지 맵에 배치
        for(int i = 0; i < maxSpawnCnt_energy; i++)
        {
            GameObject go = itemPool.energeObjs[i];
            go.SetActive(true);
            go.transform.position = new Vector3(startPosX, Random.Range(downLimit, topLimit), startPoint+startSpawnRate*-i);
        }

        timer_upgradeItem = 0;//업그레이트 아이템 스폰을 위한 변수 초기화
    }

    private void Update()
    {
       
        timer_upgradeItem += Time.deltaTime;
        if (timer_upgradeItem > spawnTime_upgradeItem)
        {
            SpawnItem();
            timer_upgradeItem = 0;
        }

        // TODO : 에너지 일정하게 무한 스폰 -> 플레이어가 먹거나 맵을 넘어가면 비활성화되고, 스폰 포인트에서 스폰 시간마다 비활성화된 에너지 중 하나를 활성화 시키는 방식으로
        timer_energy+= Time.deltaTime;
        if (timer_energy > spawnTime_energy)
        {
            SpawnEnergy();
            timer_energy = 0;
        }


    }

    void SpawnEnergy()
    {
        GameObject energy = itemPool.GetEnergyObj();
        energy.transform.position= new Vector3(startPosX, Random.Range(downLimit, topLimit), spawnPosZ);
    }

    // TODO : 아이템, 타일 스크롤링 연동 
    public void StopScrolling()
    {

        //타일 스크롤링
        foreach (GameObject tile in GroundTiles)
        {
            //Debug.Log(tile.GetComponent<TileScroll>()); // 각 타일에 접근도 잘 하는것 같음 
            tile.GetComponent<TileScroll>().StopTileScolling();
        }
    }

    public void StartScrolling()
    {

        //타일 스크롤링
        foreach(GameObject tile in GroundTiles)
        {
            tile.GetComponent<TileScroll>().StartTileScolling();
        }

        //TODO : 아이템 스크롤링 
    }

    /// <summary>
    /// 아이템 오브젝트 리스트 중 랜덤으로 하나 스폰 
    /// </summary>
    void SpawnItem()
    {
        itemPool.Shuffle();
        int idx = 0; while (itemPool.ItemObjs[idx].activeSelf) idx++;
        GameObject item = itemPool.ItemObjs[idx];
        item.SetActive(true);
        item.transform.position = new Vector3(startPosX, Random.Range(downLimit, topLimit), spawnPosZ);
    }



   

}
