using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtack : MonoBehaviour
{
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

        _isAtack = true;
    }

    void SpecialAttack()
    {

    }

    void AtackEnd()
    {
        _isAtack = false ;
    }
}
