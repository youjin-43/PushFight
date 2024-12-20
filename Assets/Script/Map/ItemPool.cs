using UnityEngine;
using System.Collections.Generic;

public class ItemPool : MonoBehaviour
{

    [Header("Energy")]
    public int EnergeItemPoolCnt = 25;
    [SerializeField] GameObject EnergeItemPool; //에너지 오브젝트를 모아놓을 엠티 오브젝트 -> 인스펙터에서 할당 
    [SerializeField] GameObject EnergePrefab; //생성할 에너지 프리팹 -> 인스펙터에서 할당
    public List<GameObject> energeObjs;
    


    [Space(10f)]
    [Header("UpgradeItem")]
    public GameObject[] ItemPrefabs;
    [SerializeField] int[] spawnCnt = { 3, 7 }; //ItemPrefabs에 있는 아이템 별 몇개 생성해놓을건지 
    [SerializeField] GameObject UpgradeItemPool;
    public List<GameObject> ItemObjs;


    [Space(10f)]
    [Header("Arrow")]
    public GameObject ArrowrePrefabs;
    [SerializeField] int ArrowPoolCnt = 40; //ItemPrefabs에 있는 아이템 별 몇개 생성해놓을건지 
    [SerializeField] GameObject ArrowPool;
    public List<GameObject> ArrowObjs;


    // TODO :인스펙터에서 하나하나 할당하지 않아도 되도록 변경하자 ... 
    void Start()
    {
        // 에너지 오브젝트 풀 생성
        energeObjs = new List<GameObject>();
        for (int i = 0; i < EnergeItemPoolCnt; i++)
        {
            GameObject go = Instantiate(EnergePrefab, EnergeItemPool.transform);
            energeObjs.Add(go);
            go.SetActive(false);
        }

        //업그레이드 아이템 오브젝트 풀 생성
        for (int i = 0; i < ItemPrefabs.Length; i++)
        {
            for (int j = 0; j < spawnCnt[i]; j++)
            {
                GameObject go = Instantiate(ItemPrefabs[i], UpgradeItemPool.transform);
                ItemObjs.Add(go);
                go.SetActive(false);
            }
        }

        //화살 오브젝트 풀 생성
        ArrowObjs = new List<GameObject>();
        for (int i = 0; i < ArrowPoolCnt; i++)
        {
            GameObject go = Instantiate(ArrowrePrefabs, ArrowPool.transform);
            ArrowObjs.Add(go);
            go.SetActive(false);
        }
    }

    /// <summary>
    /// 에너지 오브젝트하나를 활성화해서 넘겨주는 함수 
    /// </summary>
    public GameObject GetEnergyObj()
    {
        foreach(GameObject energe in energeObjs)
        {
            if (!energe.activeSelf)
            {
                energe.SetActive(true);
                return energe;
            }
        }

        //여기까지 함수가 실행됐다면 오브젝트풀의 사이즈가 부족했다는건데 그러면 새로 만들어서 return;
        GameObject go = Instantiate(EnergePrefab, EnergeItemPool.transform);
        energeObjs.Add(go);
        go.SetActive(true);
        return go;
    }

    /// <summary>
    /// ItemObjs의 요소를 랜덤으로 섞어주는 함수
    /// </summary>
    public void Shuffle()
    {
        int randomIdx;
        for (int i = 0; i < ItemObjs.Count; i++)
        {
            randomIdx = Random.Range(0, ItemObjs.Count);

            //swap
            GameObject itme_1 = ItemObjs[i]; GameObject itme_2 = ItemObjs[randomIdx];
            (itme_1, itme_2) = (itme_2, itme_1);
            ItemObjs[i] = itme_1; ItemObjs[randomIdx] = itme_2;
        }
    }


    /// <summary>
    /// 화살 오브젝트하나를 활성화해서 넘겨주는 함수 
    /// </summary>
    public GameObject GetArrowObj()
    {
        foreach (GameObject arrow in ArrowObjs)
        {
            if (!arrow.activeSelf)
            {
                arrow.SetActive(true);
                return arrow;
            }
        }

        //여기까지 함수가 실행됐다면 오브젝트풀의 사이즈가 부족했다는건데 그러면 새로 만들어서 return;
        GameObject go = Instantiate(EnergePrefab, EnergeItemPool.transform);
        energeObjs.Add(go);
        go.SetActive(true);
        return go;
    }
}
