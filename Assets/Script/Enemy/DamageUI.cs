using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class DamageUI : MonoBehaviour
{

	//private TextMeshPro damageText;
	//private Text damageText;
	private TextMeshProUGUI damageText;

	[SerializeField, Header("�t�F�[�h�A�E�g����X�s�[�h")]
	private float fadeOutSpeed = 1f;
	[SerializeField, Header("�ړ��l")]
	private float moveSpeed = 0.4f;
	[SerializeField, Header("�����x���ς��܂ł̑҂�����")]
	private float _waitTime = 1;

	void Start()
	{
	}

	void LateUpdate()
	{
		StartCoroutine("TextChange");
	}

	IEnumerator TextChange()
    {
		transform.rotation = Camera.main.transform.rotation;
		transform.position += Vector3.up * moveSpeed * Time.deltaTime;

		yield return _waitTime;

		damageText.color = Color.Lerp(damageText.color, new Color(1f, 0f, 0f, 0f), fadeOutSpeed * Time.deltaTime);

		if (damageText.color.a <= 0.1f)
		{
			Destroy(gameObject);
		}
	}
	public void DamageTextUI(int damage)
    {
		//damageText = GetComponentInChildren<Text>();
		damageText = GetComponentInChildren<TextMeshProUGUI>();

		//string dmg;
		//dmg = damage.ToString("000");
		damageText.text = ($"{damage}");

	}
}