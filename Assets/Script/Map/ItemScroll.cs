using UnityEngine;

public class ItemScroll : MonoBehaviour
{
    float scrollSpeed;

    [SerializeField] float startPosX = 0;
    [SerializeField] Vector3 spawnPos;
    

    [Header("SpawnLimits")]
    [SerializeField] float topLimit;
    [SerializeField] float downLimit;
    

    private void Start()
    {
        spawnPos = transform.position;

        scrollSpeed = MapManager.Instance.scrollSpeed;
        startPosX = MapManager.Instance.startPosX;
        topLimit = MapManager.Instance.topLimit;
        downLimit = MapManager.Instance.downLimit;
    }

    void Update()
    {
        transform.position += Vector3.forward * scrollSpeed * Time.deltaTime;
        if (transform.position.z > 0)
        {
            GoStartPos();
        }
    }

    public void GoStartPos()
    {
        transform.position = new Vector3(startPosX, Random.Range(downLimit, topLimit), spawnPos.z);
    }
}
