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
    [Header("Spawn - Limits")]
    public float startPosX = 0;
    public float topLimit = 15f;
    public float downLimit = 7f;
    
    [Header("Spawn - Energy")]
    public float spawnRate = 10f;
    float startPoint = -50f;

    [Header("Spawn - UpgradeItem")]
    [SerializeField] float spawnTime = 3f; //3초마다 생성
    [SerializeField] float timer = 0;
    [SerializeField] float spawnPosZ = -180f;


    [Space(10f)]
    [Header("ObjectPool - Energy")]
    public int EnergeItemCnt = 15;
    [SerializeField] GameObject EnergeItemPool;
    [SerializeField] GameObject EnergePrefab;
    List<GameObject> energeObjs;

    [Space(10f)]
    [Header("ObjectPool - Item")]
    public GameObject[] ItemPrefabs;
    [SerializeField] int[] spawnCnt = {6,4};
    [SerializeField] GameObject ItemPool;
    [SerializeField] List<GameObject> ItemObjs;
    #endregion

    void Start()
    {
        // count 만큼 루프하면서 발판 생성
        GroundTiles = new GameObject[tileCnt];
        for (int i = 0; i < tileCnt; i++) GroundTiles[i] = Instantiate(GroundTilesPrefab[i%3], new Vector3(0, 0, -100*i), Quaternion.identity);

        // 에너지 오브젝트 풀 생성하고 맵에 배치
        energeObjs = new List<GameObject>();
        for (int i = 0; i < EnergeItemCnt; i++)
        {
            GameObject go = Instantiate(EnergePrefab, EnergeItemPool.transform);
            energeObjs.Add(go);
            go.transform.position = new Vector3(startPosX, Random.Range(downLimit, topLimit), startPoint + -i * spawnRate);
        }

        //다른 아이템 오브젝트 풀 생성
        for (int i = 0; i < ItemPrefabs.Length; i++)
        {
            for(int j = 0; j < spawnCnt[i]; j++)
            {
                GameObject go = Instantiate(ItemPrefabs[i], ItemPool.transform);
                ItemObjs.Add(go);
                go.SetActive(false);
            }
            
        }

        //업그레이트 아이템 스폰을 위한 변수 초기화
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            SpawnItem();
            timer = 0;
        }
    }


    // TODO : 아이템, 타일 스크롤링 연동 
    public void StopScrolling()
    {

    }


    // TODO : [해결]왜 여기서 널 레퍼런스가 뜨지??? -> 맵 매니저가 타일을 생성한 후 게임매니저가 스크롤링을 시작해야하는데 여기서 순서가 꼬이는것 같음 -> 순서를 지정해주자
    // TODO : 근데 왜 안움직이냐ㅋㅋㅋㅋㅋㅋㅋ 
    public void StartScrolling()
    {

        //타일 스크롤링
        foreach(GameObject tile in GroundTiles)
        {
            //Debug.Log(tile.GetComponent<TileScroll>()); // 각 타일에 접근도 잘 하는것 같음 
            tile.GetComponent<TileScroll>().StartTileScolling();
        }

        //TODO : 아이템 스크롤링 
    }

    /// <summary>
    /// 아이템 오브젝트 리스트 중 랜덤으로 하나 스폰 
    /// </summary>
    void SpawnItem()
    {
        Shuffle();
        int idx = 0; while (ItemObjs[idx].activeSelf) idx++;
        GameObject item = ItemObjs[idx];
        item.SetActive(true);
        item.transform.position = new Vector3(startPosX, Random.Range(downLimit, topLimit), spawnPosZ);
    }



    /// <summary>
    /// ItemObjs의 요소를 랜덤으로 섞어주는 함수
    /// </summary>
    void Shuffle()
    {
        int randomIdx;
        for (int i=0;i< ItemObjs.Count; i++)
        {
            randomIdx = Random.Range(0, ItemObjs.Count);

            //swap
            GameObject itme_1 = ItemObjs[i]; GameObject itme_2 = ItemObjs[randomIdx];
            (itme_1, itme_2) = (itme_2, itme_1);
            ItemObjs[i] = itme_1; ItemObjs[randomIdx] = itme_2;
        }
    }

}
