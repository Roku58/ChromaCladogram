using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{

	private Text damageText;
	[SerializeField, Header("フェードアウトするスピード")]
	private float fadeOutSpeed = 1f;
	[SerializeField, Header("移動値")]
	private float moveSpeed = 0.4f;

	void Start()
	{
		damageText = GetComponentInChildren<Text>();
	}

	void LateUpdate()
	{
		transform.rotation = Camera.main.transform.rotation;
		transform.position += Vector3.up * moveSpeed * Time.deltaTime;

		damageText.color = Color.Lerp(damageText.color, new Color(1f, 0f, 0f, 0f), fadeOutSpeed * Time.deltaTime);
		damageText.text = "ダメージ";

		if (damageText.color.a <= 0.1f)
		{
			Destroy(gameObject);
		}
	}
}