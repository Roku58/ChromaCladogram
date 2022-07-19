using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    float _moveSpeed;
    [SerializeField, Header("最大体力")]
    int _maxHp;
    [SerializeField, Header("現在体力")]
    int _hp;
    [SerializeField, Header("攻撃力")]
    int _atk;
    [SerializeField, Header("ドロップアイテムの種類（配列の要素）")]
    int _dropType;
    [SerializeField, Header("ドロップアイテムの配列")]
    GameObject[] _drop = default;
    [SerializeField, Header("ダメージUI")]
    private GameObject damageUI;
    [SerializeField, Header("")]
    bool _canMove = true;
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
        if(_canMove)
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
        if (collision.gameObject.tag == "Weapon")
        {
            //↓に拠点のダメージ処理があるスクリプトを指定
            collision.gameObject.GetComponent<PlayerState>().GetDamage(_atk);
        }
    }

    /// <summary> エネミー死亡処理 </summary>
    private void Death()
    {
        if(_drop != null)
        {
            Instantiate(_drop[_dropType]);
        }
        Destroy(this);
    }

    /// <summary> エネミーダメージ処理 </summary>
    public void GetDamage(int damage　, Collider col)
    {
        _hp -= damage;
        Debug.Log(damage + " ダメージを受けてエネミーのHPが " + _hp + " になった");

        var obj = Instantiate<GameObject>(damageUI, col.bounds.center - Camera.main.transform.forward * 0.2f, Quaternion.identity);

        if (_hp <= 0)
        {
            Death();
        }
    }
    //public void Damage(Collider col)
    //{
    //    //　DamageUIをインスタンス化。登場位置は接触したコライダの中心からカメラの方向に少し寄せた位置
    //    var obj = Instantiate<GameObject>(damageUI, col.bounds.center - Camera.main.transform.forward * 0.2f, Quaternion.identity);
    //}
}
