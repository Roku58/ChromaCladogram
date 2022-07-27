using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using TMPro;

public class PlayerAtack : MonoBehaviour
{
    [SerializeField, Header("攻撃力")]
    int _atk = 10;
    [SerializeField, Header("攻撃倍率")]
    float _atkanim = 0;

    /// <summary>攻撃範囲の中心</summary>
    [SerializeField, Header("攻撃範囲の中心")]
    Vector3 _attackRangeCenter = default;
    /// <summary>攻撃範囲の半径</summary>
    [SerializeField, Header("攻撃範囲の半径")]
    float _attackRangeRadius = 1f;

    Animator _animator;
    CinemachineImpulseSource _impulseSource = default;

    bool _isAtack = false;
    public bool IsAtack // プロパティ
    {
        get { return _isAtack; }  // 通称ゲッター。呼び出した側がscoreを参照できる
        set { _isAtack = value; } // 通称セッター。value はセットする側の数字などを反映する
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();

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
        float a = _atk * _atkanim;
        int b = (int)a;
        if (collider.gameObject.GetComponent<Enemy>())
        {
            collider.gameObject.GetComponent<Enemy>().GetDamage(b, collider);
            if (_impulseSource)
            {
                _impulseSource.GenerateImpulse();
                //_impulseSource.GenerateImpulseAt(Vector3.zero, Vector3.up);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // 攻撃範囲を赤い線でシーンビューに表示する
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetAttackRangeCenter(), _attackRangeRadius);
    }

    /// <summary>
    /// 攻撃範囲の中心を計算して取得する
    /// </summary>
    /// <returns>攻撃範囲の中心座標</returns>
    Vector3 GetAttackRangeCenter()
    {
        Vector3 center = this.transform.position + this.transform.forward * _attackRangeCenter.z
            + this.transform.up * _attackRangeCenter.y
            + this.transform.right * _attackRangeCenter.x;
        return center;
    }

    void Attack()
    {
        // 攻撃範囲に入っているコライダーを取得する
        var hitObj = Physics.OverlapSphere(GetAttackRangeCenter(), _attackRangeRadius);

        // 各コライダーに対して、それが Rigidbody を持っていたら力を加える
        foreach (Collider enemy in hitObj)
        {
            if (enemy.gameObject.tag == "Enemy")
            {
                CallDamage(enemy);
            }
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
