using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAtack : MonoBehaviour
{
    [SerializeField, Header("攻撃力")]
    int _atk = 10;
    [SerializeField, Header("攻撃座標")]
    Vector3 _attackPosPos ;
    [SerializeField, Header("攻撃座標")]
    float _attackRange;
    [SerializeField, Header("攻撃座標")]
    LayerMask _enemyLayers;
    Animator _animator;
    bool _isAtack = false;
    public bool IsAtack // プロパティ
    {
        get { return _isAtack; }  // 通称ゲッター。呼び出した側がscoreを参照できる
        set { _isAtack = value; } // 通称セッター。value はセットする側の数字などを反映する
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        AttackManager();
    }

    void AttackManager()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            NormalAttack();
        }

    }

    void NormalAttack()
    {
        _animator.SetTrigger("Attack1");
        ////コライダー出現
        //Collider[] hitEnemies = Physics.OverlapSphere(_attackPosPos, _attackRange, _enemyLayers);
        //foreach (Collider enemy in hitEnemies)
        //{
        //    if (enemy.gameObject.tag == "Enemy")
        //    {
        //        CallDamage(enemy);
        //    }
        //}

    }

    void SpecialAttack()
    {

    }

    void AtackEnd()
    {
        _isAtack = false ;
    }
    void AtackStart()
    {
        _isAtack = true;
    }

    /// <summary>
    /// 対象のダメージ関数呼び出し処理
    /// </summary>
    /// <param name="collider"></param>
    public void CallDamage(Collider collider)
    {
        //↓に拠点のダメージ処理があるスクリプトを指定
        if (collider.gameObject.GetComponent<Enemy>())
        {
            collider.gameObject.GetComponent<Enemy>().GetDamage(_atk, collider);
        }
    }

    /// <summary>
    /// 打ち上げ用の関数
    /// </summary>
    //void Launch() 
    //{
    //    _rb.DOMoveY(7f, 0.5f); //Y方向に0.5秒かけて上昇
    //    Collider[] hitEnemies = Physics.OverlapSphere(AttackPoint.position, AttackRange); //コライダー出現
    //    foreach (Collider enemy in hitEnemies)
    //    {
    //        if(enemy.GetComponent<Enemy>())
    //        {
    //            enemy.GetComponent<Enemy>().Launch(); //攻撃を受けた敵を打ち上げます。
    //        }
    //    }
    //}

}
