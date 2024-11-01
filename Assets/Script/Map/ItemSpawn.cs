using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [Header("SpawnLimits")]
    [SerializeField] float spawnRate = 10f;
    float startPoint = -50f;

    [SerializeField] float startPosX = 0;
    [SerializeField] float topLimit = 20f;
    [SerializeField] float downLimit = 7f;


    [Header("ObjectPool")]
    public GameObject EnergeItemPool; [SerializeField] int EnergeItemCnt = 30; public GameObject EnergePrefab;
    List<GameObject> energeObjs;

    public GameObject AttackItemPool; [SerializeField] int AttackItemCnt = 10; public GameObject AttackPrefab;
    List<GameObject> AttackItemObjs;


    void Start()
    {
        //아이템 풀 instantiate
        energeObjs = new List<GameObject>();
        for (int i = 0; i < EnergeItemCnt; i++)
        {
            GameObject go = Instantiate(EnergePrefab, EnergeItemPool.transform);
            energeObjs.Add(go);
            go.transform.position = new Vector3(startPosX, Random.Range(downLimit, topLimit), startPoint + -i * spawnRate);
        }
        //for (int i = 0; i < AttackItemCnt; i++) AttackItemObjs.Add(Instantiate(AttackPrefab, AttackItemPool.transform));
    }

    // Update is called once per frame 
    void Update()
    {
        
    }
}
