using UnityEngine;
//using DG.Tweening;

/// <summary>
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class MoveP : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 7.5f;
    [SerializeField] float _jumpPower = 5f;
    [SerializeField] float _stepPower = 30f;

    int jumpCount;
    Rigidbody _rb = default;
    Animator _anim = default;
    /// <summary>接地フラグ</summary>
    bool _isGrounded;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        _isGrounded = CheckGrounded();
 
    }

    private void FixedUpdate()
    {
        if (_isGrounded)
        {
            Move();
            Jump();
        }
    }
    void LateUpdate()
    {
        // アニメーションの処理
        if (_anim)
        {
            _anim.SetBool("IsGrounded", _isGrounded);
            Vector3 walkSpeed = _rb.velocity;
            walkSpeed.y = 0;
            _anim.SetFloat("Speed", walkSpeed.magnitude);
        }
    }

    void Move()
    {
        // 入力を受け付ける
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 入力された方向を「カメラを基準とした XZ 平面上のベクトル」に変換する
        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        // キャラクターを「入力された方向」に向ける
        if (dir != Vector3.zero)
        {
            this.transform.forward = dir;
        }

        // Y 軸方向の速度を保ちながら、速度ベクトルを求めてセットする
        Vector3 velocity = dir.normalized * _moveSpeed;
        velocity.y = _rb.velocity.y;
        //_rb.velocity = velocity;
        //_rb.AddForce(dir * _stepPower, ForceMode.Force);
        _rb.AddForce(dir * _moveSpeed);

        //入力がないときは減速
        if (h == 0 && v == 0 && _isGrounded)
        {
            _rb.AddForce(_moveSpeed * (dir - _rb.velocity　* 0.1f));
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            _rb.AddForce(dir * _stepPower, ForceMode.VelocityChange);
            Debug.Log("Step");
            _anim.SetTrigger("Step");

        }
    }

    void Jump()
    {
        // ジャンプ処理
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            if (jumpCount <= 1)//  もし、Groundedがtrueなら、
            {
                if (_isGrounded == true)
                {
                    _isGrounded = false;
                    _anim.SetTrigger("Jump");
                    _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
                }
            }
        }
    }


    bool CheckGrounded()
    {
        //放つ光線の初期位置と姿勢
        var ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        //光線の距離(今回カプセルオブジェクトに設定するのでHeight/2 + 0.1以上を設定)
        var distance = 1.2f;
        //Raycastがhitするかどうかで判定レイヤーを指定することも可能
        return Physics.Raycast(ray, distance);
    }
}
