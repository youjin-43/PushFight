using UnityEngine;

public class Energy : Item
{
    float spawnPos;

    protected override void Start()
    {
        base.Start();
        spawnPos = MapManager.instance.spawnPosZ;
    }

    protected override void Update()
    {
        base.Update();
        // TODO: 0? ????? ?????? ??? ?? ?????? ?? ??? 
        //if (transform.position.z > 0) GoStartPos();
        if (transform.position.z > 0) gameObject.SetActive(false);
    }


    public void GoStartPos()
    {
        transform.position = new Vector3(startPosX, Random.Range(downLimit, topLimit), spawnPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInfo>().IncreseEnergeCnt();
            //GoStartPos();
            gameObject.SetActive(false);
        }
    }
}
