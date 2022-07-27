using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    float _moveSpeed = 0;
    [SerializeField, Header("最大体力")]
    int _maxHp = 100;
    [SerializeField, Header("現在体力")]
    int _hp = 0;
    [SerializeField, Header("攻撃力")]
    int _atk = 1;
    [SerializeField, Header("ドロップアイテムの種類（配列の要素）")]
    int _dropType = 0;
    [SerializeField, Header("ドロップアイテムの配列")]
    GameObject[] _drop = default;
    [SerializeField, Header("ダメージUI")]
    private GameObject damageUI = default;
    [SerializeField]
    bool _canMove = true;
    [SerializeField]
    TimeManager _timeManager = default;
    GameObject _player = default; //
    Rigidbody _rb = default;


    void Start()
    {
        _hp = _maxHp;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (_player != null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
        if (_canMove)
        {
            Move();
        }
    }

    /// <summary> エネミー移動処理 </summary>
    private void Move()
    {
        if (_player != null)
        {
            transform.LookAt(_player.transform);
            Vector3 sub = _player.transform.position - transform.position;
            sub.Normalize();
            transform.position += sub * _moveSpeed * Time.deltaTime;
            Vector3 velocity = sub.normalized * _moveSpeed;
            velocity.y = _rb.velocity.y;
            _rb.velocity = velocity;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "Weapon")
        //{
        //    collision.gameObject.GetComponent<PlayerState>().GetDamage(_atk);
        //}
    }

    /// <summary>
    /// 対象のダメージ関数呼び出し処理
    /// </summary>
    /// <param name="collider"></param>
    public void CallDamage(Collider collider)
    {
        if (collider.gameObject.GetComponent<PlayerState>())
        {
            collider.gameObject.GetComponent<PlayerState>().GetDamage(_atk);
        }
    }
    /// <summary>
    /// 攻撃を受けた敵を打ち上げます。
    /// </summary>
    public void Launch()
    {
        _rb.DOMoveY(7f, 0.5f);
    }
    public void OffGrvity()
    {
        _rb.drag = 40; //RigidBodyのDragの数値を弄る
    }
    public void ONGrvity()
    {
        _rb.drag = 0;
    }

    /// <summary> エネミー死亡処理 </summary>
    private void Death()
    {
        //if (_drop[_dropType] != null)
        //{
        //    Instantiate(_drop[_dropType]);
        //}
        //Destroy(gameObject);
    }

    /// <summary> エネミーダメージ処理 </summary>
    public void GetDamage(int damage, Collider col)
    {
        _hp -= damage;
        Debug.Log(damage + " ダメージを受けてエネミーのHPが " + _hp + " になった");

        var obj = Instantiate<GameObject>(damageUI, col.bounds.center - Camera.main.transform.forward * 0.2f, Quaternion.identity);
        obj.GetComponent<DamageUI>().DamageTextUI(damage);

        _timeManager.SlowDown();


        if (_hp <= 0)
        {
            Death();
        }
    }

}
