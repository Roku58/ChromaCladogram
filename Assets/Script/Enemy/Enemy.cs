using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")]
    float _moveSpeed = 0;
    [SerializeField, Header("�ő�̗�")]
    int _maxHp = 100;
    [SerializeField, Header("���ݑ̗�")]
    int _hp = 0;
    [SerializeField, Header("�U����")]
    int _atk = 1;
    [SerializeField, Header("�h���b�v�A�C�e���̎�ށi�z��̗v�f�j")]
    int _dropType = 0;
    [SerializeField, Header("�h���b�v�A�C�e���̔z��")]
    GameObject[] _drop = default;
    [SerializeField, Header("�_���[�WUI")]
    private GameObject damageUI = default;
    [SerializeField]
    bool _canMove = true;
    [SerializeField]
    TimeManager _timeManager = default;
    GameObject _player = default; //
    Rigidbody _rb = default;
    CinemachineImpulseSource _impulseSource = default;


    void Start()
    {
        _hp = _maxHp;
        _player = GameObject.FindGameObjectWithTag("Player");
        _impulseSource = GetComponent<CinemachineImpulseSource>();
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
        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("a");
            if(_impulseSource)
            {
                _impulseSource.GenerateImpulse();

            }
            //_impulseSource.GenerateImpulseAt();
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
        if (_drop[_dropType] != null)
        {
            Instantiate(_drop[_dropType]);
        }
        //Destroy(gameObject);
    }

    /// <summary> �G�l�~�[�_���[�W���� </summary>
    public void GetDamage(int damage, Collider col)
    {
        _hp -= damage;
        Debug.Log(damage + " �_���[�W���󂯂ăG�l�~�[��HP�� " + _hp + " �ɂȂ���");
        _timeManager.SlowDown();
        {
            //_shake.Shake(0.25f, 0.1f);

            var obj = Instantiate<GameObject>(damageUI, col.bounds.center - Camera.main.transform.forward * 0.2f, Quaternion.identity);
            //obj.GetComponent<DamageUI>().DamageTextUI(damage);
            if (_hp <= 0)
            {
                Death();
            }
        }
    }
}
