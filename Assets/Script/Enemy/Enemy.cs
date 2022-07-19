using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")]
    float _moveSpeed;
    [SerializeField, Header("�ő�̗�")]
    int _maxHp;
    [SerializeField, Header("���ݑ̗�")]
    int _hp;
    [SerializeField, Header("�U����")]
    int _atk;
    [SerializeField, Header("�h���b�v�A�C�e���̎�ށi�z��̗v�f�j")]
    int _dropType;
    [SerializeField, Header("�h���b�v�A�C�e���̔z��")]
    GameObject[] _drop = default;
    [SerializeField, Header("�_���[�WUI")]
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

    /// <summary> �G�l�~�[�ړ����� </summary>
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
            //���ɋ��_�̃_���[�W����������X�N���v�g���w��
            collision.gameObject.GetComponent<PlayerState>().GetDamage(_atk);
        }
    }

    /// <summary> �G�l�~�[���S���� </summary>
    private void Death()
    {
        if(_drop != null)
        {
            Instantiate(_drop[_dropType]);
        }
        Destroy(this);
    }

    /// <summary> �G�l�~�[�_���[�W���� </summary>
    public void GetDamage(int damage�@, Collider col)
    {
        _hp -= damage;
        Debug.Log(damage + " �_���[�W���󂯂ăG�l�~�[��HP�� " + _hp + " �ɂȂ���");

        var obj = Instantiate<GameObject>(damageUI, col.bounds.center - Camera.main.transform.forward * 0.2f, Quaternion.identity);

        if (_hp <= 0)
        {
            Death();
        }
    }
    //public void Damage(Collider col)
    //{
    //    //�@DamageUI���C���X�^���X���B�o��ʒu�͐ڐG�����R���C�_�̒��S����J�����̕����ɏ����񂹂��ʒu
    //    var obj = Instantiate<GameObject>(damageUI, col.bounds.center - Camera.main.transform.forward * 0.2f, Quaternion.identity);
    //}
}
