using UnityEngine;

public class UpgradeItem : Item
{
    public int itmeType; //1은 공격 증가 , 2는 증강스택
    void Update()
    {
        if (transform.position.z > 0)
        {
            gameObject.SetActive(false);
            
        }
    }
}
