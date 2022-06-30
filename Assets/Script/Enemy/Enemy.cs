using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _hp = 100;
    [SerializeField] int _maxhp = 100;
    [SerializeField] float _movespeed = 10;

    NavMeshAgent _agent = null;
    GameObject _player = null;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        _agent.SetDestination(_player.transform.position);

    }

    public void GetDamage(int dmg)
    {

    }

    void Death()
    {

    }
}
