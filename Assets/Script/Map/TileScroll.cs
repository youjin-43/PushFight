using System.Collections;
using UnityEngine;

public class TileScroll : MonoBehaviour
{
    float scrollSpeed;
    Coroutine coroutine;

    private void Start()
    {
        scrollSpeed = MapManager.instance.scrollSpeed;
    }

    IEnumerator TileScrollCoroutine()
    {
        //Debug.Log("코루틴 실행됨");
        while (true)
        {
            transform.position += Vector3.forward * scrollSpeed * Time.deltaTime;
            if (transform.position.z > 100) transform.position = new Vector3(0, 0, -500f);
            yield return null;
        }
    }

    public void StopTileScolling()
    {
        StopCoroutine(coroutine);
    }

    public void StartTileScolling()
    {
        coroutine = StartCoroutine(TileScrollCoroutine());
    }
}
