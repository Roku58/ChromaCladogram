using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] int _hp = 100;
    [SerializeField] int _maxhp = 100;

    [SerializeField] int _redPoint = 0;
    [SerializeField] int _bruePoint = 0;
    [SerializeField] int _colorRatio = 50;

    [SerializeField] int _colorPoint = 0;
    [SerializeField] bool _red = false;
    [SerializeField] bool _brue = false;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void Color()
    {
        _colorPoint = _redPoint + _bruePoint; //ëçó åvéZ
        _colorRatio = _redPoint / _colorPoint;Å@//äÑçáåvéZ

        if(_colorRatio <= 50)
        {
            _brue = false;
            _red = true;
        }
        else if( _colorRatio > 50)
        {
            _red = false;
            _brue = true;
        }
    }

    public void GetDamage(int dmg)
    {

    }

    void Death()
    {

    }
}
