using UnityEngine;

public class Item : MonoBehaviour
{
    protected float scrollSpeed;
    protected float startPosX = 0;
    protected float topLimit;
    protected float downLimit;

    protected virtual void Start()
    {
        scrollSpeed = MapManager.instance.scrollSpeed;
        startPosX = MapManager.instance.startPosX;
        topLimit = MapManager.instance.topLimit;
        downLimit = MapManager.instance.downLimit;
    }

    protected virtual void Update()
    {

        transform.position += Vector3.forward * scrollSpeed * Time.deltaTime; // 매 프레임 스크롤 스피드 만큼 이동 
        
        
    }
}
 