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

    public GameObject[] GroundTilesPrefab;
    GameObject[] GroundTiles;
    int tileCnt = 6;

    public float scrollSpeed = 2f;

    void Start()
    {
        GroundTiles = new GameObject[tileCnt];

        // count 만큼 루프하면서 발판 생성
        for (int i = 0; i < tileCnt; i++) GroundTiles[i] = Instantiate(GroundTilesPrefab[i%3], new Vector3(0, 0, -100*i), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        //Scrolling

    }
}
