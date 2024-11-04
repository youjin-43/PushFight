using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float shotSpeed=10f;
    [SerializeField] Vector3 dir = new Vector3(0, -1, 0);
    void FixedUpdate()
    {
        transform.Translate(dir * shotSpeed * Time.fixedDeltaTime);


        //TODO : 이거 위치 확인해야함 
        //if (transform.position.z < -30) gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "이랑 화살이랑 충돌!1");
        if(other.CompareTag("Monster")) gameObject.SetActive(false); // 몬스터에 맞으면 비활성화 
    }
}
