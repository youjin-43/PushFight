using System.Collections;
using UnityEngine;

public class TileScroll : MonoBehaviour
{
    float scrollSpeed;
    IEnumerator coroutine;


    private void Awake()
    {
        //TODO : 따로 객체 할당하는게 아니라 그냥 함수 이름으로 넣어버리면 안되나? 
        //coroutine = TileScrollCoroutine(); //게임 매니저 start에서 ChangeStateToDay();를 호출하기 때문에 그 전에 할당해놔야함 
    }
    private void Start()
    {
        scrollSpeed = MapManager.instance.scrollSpeed;
    }

    // TODO : update 말고 코루틴을 이용해서 특정상황에서만 스크롤링 하도록 
    //private void Update()
    //{
    //    transform.position += Vector3.forward * scrollSpeed * Time.deltaTime;
    //    if (transform.position.z > 100)
    //    {
    //        transform.position = new Vector3(0, 0, -500f);
    //    }
    //}


    IEnumerator TileScrollCoroutine()
    {
        Debug.Log("코루틴 실행됨");
        while (true)
        {
            transform.position += Vector3.forward * scrollSpeed * Time.deltaTime;
            if (transform.position.z > 100)
            {
                transform.position = new Vector3(0, 0, -500f);
            }
            yield return null;
        }
    }

    public void StopTileScolling()
    {
        StopCoroutine(TileScrollCoroutine());
    }


    // 맵 매니저 StartScrolling()에서 호출됨 
    public void StartTileScolling()
    {

        //Debug.Log("tileScroll에서 StartTileScolling 함수 호출됨"); 호출은 잘 됨 
        StartCoroutine(TileScrollCoroutine());
    }
}
