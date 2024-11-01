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

    //아이템 스폰 
    [Header("SpawnLimits")]
    public float spawnRate = 10f;
    float startPoint = -50f;

    public float startPosX = 0;
    public float topLimit = 15f;
    public float downLimit = 7f;

    [Space(10f)]
    [Header("ObjectPool")]
    [SerializeField] GameObject EnergeItemPool;
    [SerializeField] GameObject EnergePrefab;
    public int EnergeItemCnt = 15;
    
    List<GameObject> energeObjs;

    [SerializeField] GameObject AttackItemPool;
    [SerializeField] int AttackItemCnt = 10;
    [SerializeField] GameObject AttackPrefab;
    List<GameObject> AttackItemObjs;

    

    void Start()
    {
        // count 만큼 루프하면서 발판 생성
        GroundTiles = new GameObject[tileCnt];
        for (int i = 0; i < tileCnt; i++) GroundTiles[i] = Instantiate(GroundTilesPrefab[i%3], new Vector3(0, 0, -100*i), Quaternion.identity);

        //아이템 풀 instantiate
        energeObjs = new List<GameObject>();
        for (int i = 0; i < EnergeItemCnt; i++)
        {
            GameObject go = Instantiate(EnergePrefab, EnergeItemPool.transform);
            energeObjs.Add(go);
            go.transform.position = new Vector3(startPosX, Random.Range(downLimit, topLimit), startPoint + -i * spawnRate);
        }

    }


}
