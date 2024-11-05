using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float shotSpeed=60f;
    [SerializeField] Vector3 dir = new Vector3(0, -1, 0);
    void FixedUpdate()
    {
        transform.Translate(dir * shotSpeed * Time.fixedDeltaTime);


        //TODO : 이거 위치 확인해야함 
        if (transform.position.z < -300) gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Monster"))
        {
            gameObject.SetActive(false); // 몬스터에 맞으면 화살 비활성화
            other.GetComponent<Monster>().GetHit();

        }
    }
}
