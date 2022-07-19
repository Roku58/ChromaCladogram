using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeTime = 0.5f;
    public Vector3 shakeRange = new Vector3(0.2f, 0.2f, 0f);

    private float _shakeTime;
    private float _timer;

    private Vector3 _originPos;
    private bool _onShakeEnd;

    void Start()
    {
        //init
        _shakeTime = -1f;
        _timer = 0f;
        _originPos = transform.position;
        _onShakeEnd = false;
    }

    void Update()
    {
        if (_timer <= _shakeTime)
        {
            _onShakeEnd = true;
            _timer += Time.deltaTime;
            transform.position = _originPos + MulVector3(shakeRange, Random.insideUnitSphere);
        }
        else
        {
            if (_onShakeEnd)
            {
                transform.position = _originPos;
                _onShakeEnd = false;
            }
            _originPos = transform.position;
        }
    }
    public void Shake()
    {
        _timer = 0f;
        _shakeTime = shakeTime;
    }

    private Vector3 MulVector3(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
}