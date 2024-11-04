using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageText : MonoBehaviour
{
    private float moveSpeed = 32.0f;
    private float alphaSpeed = 2.0f;
    private float destroyTime = 2.0f;
    [SerializeField] TextMeshProUGUI text; //인스펙터에서 할당 
    Color alpha;

    void OnEnable()
    {    
        alpha = text.color;
        Invoke("DisableObject", destroyTime);
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
