using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAtack : MonoBehaviour
{
    [SerializeField, Header("�U����")]
    int _atk = 10;
    [SerializeField, Header("�U�����W")]
    Vector3 _attackPosPos ;
    [SerializeField, Header("�U�����W")]
    float _attackRange;
    [SerializeField, Header("�U�����W")]
    LayerMask _enemyLayers;
    Animator _animator;
    bool _isAtack = false;
    public bool IsAtack // �v���p�e�B
    {
        get { return _isAtack; }  // �ʏ̃Q�b�^�[�B�Ăяo��������score���Q�Ƃł���
        set { _isAtack = value; } // �ʏ̃Z�b�^�[�Bvalue �̓Z�b�g���鑤�̐����Ȃǂ𔽉f����
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
        ////�R���C�_�[�o��
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
    /// �Ώۂ̃_���[�W�֐��Ăяo������
    /// </summary>
    /// <param name="collider"></param>
    public void CallDamage(Collider collider)
    {
        //���ɋ��_�̃_���[�W����������X�N���v�g���w��
        if (collider.gameObject.GetComponent<Enemy>())
        {
            collider.gameObject.GetComponent<Enemy>().GetDamage(_atk, collider);
        }
    }

    /// <summary>
    /// �ł��グ�p�̊֐�
    /// </summary>
    //void Launch() 
    //{
    //    _rb.DOMoveY(7f, 0.5f); //Y������0.5�b�����ď㏸
    //    Collider[] hitEnemies = Physics.OverlapSphere(AttackPoint.position, AttackRange); //�R���C�_�[�o��
    //    foreach (Collider enemy in hitEnemies)
    //    {
    //        if(enemy.GetComponent<Enemy>())
    //        {
    //            enemy.GetComponent<Enemy>().Launch(); //�U�����󂯂��G��ł��グ�܂��B
    //        }
    //    }
    //}

}
