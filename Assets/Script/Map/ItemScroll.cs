using UnityEngine;

public class ItemScroll : MonoBehaviour
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
            transform.position = new Vector3(0, 0, -500f);
        }

        
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("아이템과 충돌");
    //}
}
