using UnityEngine;

public class Energy : Item
{
    [SerializeField] Vector3 spawnPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPos = transform.position;
    }

    void Update()
    {
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
