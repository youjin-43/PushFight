using UnityEngine;

public class Energy : Item
{
    [SerializeField] Vector3 spawnPos;

    protected override void Start()
    {
        base.Start();
        spawnPos = transform.position;
    }

    protected override void Update()
    {
        base.Update();
        if (transform.position.z > 0) GoStartPos();
    }

    public void GoStartPos()
    {
        transform.position = new Vector3(startPosX, Random.Range(downLimit, topLimit), spawnPos.z);
    }
}
