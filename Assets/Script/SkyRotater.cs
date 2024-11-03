using UnityEngine;

public class SkyRotater : MonoBehaviour
{
    float scrollSpeed = 5f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        scrollSpeed = GameManager.instance.SkyScrollSpeed;
    }

    float offset;
    void Update()
    {
        offset = (Time.time * scrollSpeed / 200)%1f;
        rend.material.mainTextureOffset = new Vector2(offset, 0);
        
    }
}
