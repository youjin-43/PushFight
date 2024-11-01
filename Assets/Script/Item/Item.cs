using UnityEngine;

public class Item : MonoBehaviour
{
    protected float scrollSpeed;
    protected float startPosX = 0;
    protected float topLimit;
    protected float downLimit;


    protected virtual void Start()
    {
        scrollSpeed = MapManager.Instance.scrollSpeed;
        startPosX = MapManager.Instance.startPosX;
        topLimit = MapManager.Instance.topLimit;
        downLimit = MapManager.Instance.downLimit;
    }

    protected virtual void Update()
    {
        transform.position += Vector3.forward * scrollSpeed * Time.deltaTime; // 매 프레임 스크롤 스피드 만큼 이동 
    }
}
 