using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int _atk = 10;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //↓に拠点のダメージ処理があるスクリプトを指定
            other.gameObject.GetComponent<Enemy>().GetDamage(_atk, other);
        }
    }
}
