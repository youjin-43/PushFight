using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<MapManager>();
                if (!_instance)
                {
                    GameObject obj = new GameObject();
                    obj.name = "MapManager";
                    _instance = obj.AddComponent(typeof(MapManager)) as MapManager;
                }
            }
            return _instance;
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
