using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

/// <summary>
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayreMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 7.5f;
    [SerializeField] float _atackmoveSpeed = 0.1f;
    [SerializeField] float _jumpPower = 5f;
    [SerializeField] float _stepPower = 30f;
    int jumpCount;
    bool _isGrounded;
    Rigidbody _rb = default;
    Animator _anim = default;
    PlayerAtack _playerAtack;
    Vector2 _position;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _playerAtack = GetComponent<PlayerAtack>();
    }

    void Update()
    {
        //if(!_playerAtack.IsAtack)
        //{
        //    Move();
        //}
        Move();
        Jump();
        _isGrounded = CheckGrounded();

        if (_playerAtack.IsAtack && !_isGrounded)
        {
            OffGrvity();
        }
        else
        {
            ONGrvity();

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
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        float h = _position.x;
        float v = _position.y;

        // 入力された方向を「カメラを基準とした XZ 平面上のベクトル」に変換する
        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        // キャラクターを「入力された方向」に向ける
        if (dir != Vector3.zero)
        {
            this.transform.forward = dir;
        }

        //if(_playerAtack.IsAtack)
        //{
        //    _rb.velocity = Vector3.zero;
        //    return;
        //}

        // Y 軸方向の速度を保ちながら、速度ベクトルを求めてセットする
        Vector3 velocity = dir.normalized * _moveSpeed;
        velocity.y = _rb.velocity.y;
        if (_playerAtack.IsAtack)
        {
            _rb.velocity = dir.normalized * _moveSpeed * _atackmoveSpeed;
        }
        else
        {
            _rb.velocity = velocity;

        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            //if (dir != Vector3.zero)
            //{
            //    FrontStep();
            //    //_rb.velocity = dir.normalized * _stepPower;


            //}
            //else
            //{
            //    BackStep();
            //    //_rb.velocity = -transform.forward * _stepPower;
                


            //}
            _anim.SetTrigger("Step");

        }
    }
    /// <summary>
    /// ジャンプ処理
    /// </summary>
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            if (jumpCount <= 2)//  もし、Groundedがtrueなら、
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
        var distance = 0.25f;
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, 0.1f, false);
        //Raycastがhitするかどうかで判定レイヤーを指定することも可能
        return Physics.Raycast(ray, distance);
    }

    /// <summary>
    /// InputSystemイベント受付処理
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        _position = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 攻撃中の移動処理
    /// </summary>
    /// <param name="addspeed"></param>
    //void AtackMove(AnimationEvent animationEvent)
    //{
    //    float addspeed = animationEvent.floatParameter;
    //    int vec_x = animationEvent.intParameter;
    //    int vec_y = animationEvent.intParameter;
    //    int vec_z = animationEvent.intParameter;
    //    _rb.velocity = transform.rotation * new Vector3(vec_x, vec_y, vec_z) * addspeed;
    //}
    void AtackMove(float addspeed)
    {

        //_rb.velocity = transform.forward * addspeed;
        this.transform.DOLocalMove(this.transform.position + transform.forward, 2f);
        //_rb.DOMove(this.transform.position+ 1, addspeed);
    }
    //void FrontStep(float _addPower)
    //{
    //    this.transform.DOLocalMove(this.transform.position + transform.forward * _addPower, 0.8f);
    //}

    void FrontStep(AnimationEvent animationEvent)
    {
        int _addPower = animationEvent.intParameter;
        float _count = animationEvent.floatParameter;

        this.transform.DOLocalMove(this.transform.position + transform.forward * _addPower, _count);
    }

    void BackStep(AnimationEvent animationEvent)
    {
        int _addPower = animationEvent.intParameter;
        float _count = animationEvent.floatParameter;
        this.transform.DOLocalMove(this.transform.position + -transform.forward * _addPower, _count);
    }

    /// <summary>
    /// 最も近いエネミーを探す処理
    /// </summary>
    void EnemySearch()
    {

    }

    /// <summary>
    /// オブジェクトに向けての移動処理
    /// </summary>
    /// <param name="target"></param>
    /// <param name="speed"></param>
    void TargetMove(GameObject target, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
    }

    /// <summary>
    /// 打ち上げ用の関数
    /// </summary>
    public void OffGrvity()
    {
        _rb.drag = 50; //RigidBodyのDragの数値を弄る
    }
    public void ONGrvity()
    {
        _rb.drag = 0; //RigidBodyのDragの数値を弄る
    }

    //void Launch()
    //{
    //    OffGrvity(); //OffGrvityを実行
    //    _rb.DOMoveY(7f, 0.5f);
    //    Collider[] hitEnemies = Physics.OverlapSphere(AttackPoint.position, AttackRange, enemyLayers); //コライダー出現
    //    foreach (Collider enemy in hitEnemies)
    //    {
    //        enemy.GetComponent<Enemy>().Launch(); //敵を打ち上げる
    //        enemy.GetComponent<Enemy>().OffGrvity(); //敵側のOffGravityを実行
    //    }
    //}
}
