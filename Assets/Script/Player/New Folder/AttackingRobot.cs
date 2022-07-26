using UnityEngine;

/// <summary>
/// 敵キャラクターを制御するコンポーネント。
/// トリガーに Rigidbody が入ったら、攻撃する。
/// 攻撃が Rigidbody に当たったら、弾き飛ばす。
/// 攻撃範囲は _attackRangeCenter, _attackRangeRadius で設定する。
/// </summary>
public class AttackingRobot : MonoBehaviour
{
    /// <summary>攻撃範囲の中心</summary>
    [SerializeField] Vector3 _attackRangeCenter = default;
    /// <summary>攻撃範囲の半径</summary>
    [SerializeField] float _attackRangeRadius = 1f;
    /// <summary></summary>
    [SerializeField] float _attackPower = 10f;
    GameObject _player = default;
    Animator _anim = default;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        // プレイヤーの方を見る
        if (_player)
        {
            this.transform.forward = _player.transform.position - this.transform.position;
        }
    }

    void OnDrawGizmosSelected()
    {
        // 攻撃範囲を赤い線でシーンビューに表示する
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetAttackRangeCenter(), _attackRangeRadius);
    }

    /// <summary>
    /// 攻撃範囲の中心を計算して取得する
    /// </summary>
    /// <returns>攻撃範囲の中心座標</returns>
    Vector3 GetAttackRangeCenter()
    {
        Vector3 center = this.transform.position + this.transform.forward * _attackRangeCenter.z
            + this.transform.up * _attackRangeCenter.y
            + this.transform.right * _attackRangeCenter.x;
        return center;
    }

    void OnTriggerEnter(Collider other)
    {
        // トリガーに入った GameObject が Rigidbody を持っていたら Animator Controller のパラメータを操作する
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            _anim.SetTrigger("AttackTrigger");
        }
    }

    /// <summary>
    /// 攻撃をする。アニメーションイベントから呼ばれることを想定している。
    /// </summary>
    void Attack()
    {
        // 攻撃範囲に入っているコライダーを取得する
        var cols = Physics.OverlapSphere(GetAttackRangeCenter(), _attackRangeRadius);
        
        // 各コライダーに対して、それが Rigidbody を持っていたら力を加える
        foreach (var c in cols)
        {
            Rigidbody rb = c.gameObject.GetComponent<Rigidbody>();

            if (rb)
            {
                rb.AddForce(this.transform.forward * _attackPower, ForceMode.Impulse);
            }
        }
    }
}
