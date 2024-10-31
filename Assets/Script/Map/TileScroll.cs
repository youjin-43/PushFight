using UnityEngine;

public class TileScroll : MonoBehaviour
{
    float scrollSpeed;

    private void Start()
    {
        scrollSpeed = MapManager.Instance.scrollSpeed;
    }
    void Update()
    {
        transform.position += Vector3.forward * scrollSpeed * Time.deltaTime;
        if (transform.position.z > 100)
        {
            transform.position = new Vector3(0,0,-500f);
        }
    }
}
