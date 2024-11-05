using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageText : MonoBehaviour
{
    private float moveSpeed = 32.0f;
    private float alphaSpeed = 2.0f;
    private float disableTime = 2.0f;
    [SerializeField] TextMeshProUGUI text; //인스펙터에서 할당 
    Color alpha;

    void OnEnable()
    {    
        alpha = text.color;
        alpha.a = 100; //전에 알파 0으로 해놔서 활성화 될때 초기화
        //이거 수치로 조정 안되서 그냥 씬에서 오브젝트 풀 위치 자체를 옮겼는데 수치로 랜덤 배치는 어케하지? -> 부모 리엑트 문제였음 그거 자체를 내림 
        GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-380, 315), Random.Range(-120, 150));
        Invoke("DisableObject", disableTime);
    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 위쪽으로 상승하면서 
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 점점 연하게 
        text.color = alpha;
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }

}
