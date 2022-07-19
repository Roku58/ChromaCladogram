using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class DamageUI : MonoBehaviour
{

	//private TextMeshPro damageText;
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

		if (damageText.color.a <= 0.1f)
		{
			Destroy(gameObject);
		}
	}

	public void DamageTextUI(int damage)
    {
		//string dmg;
		//dmg = damage.ToString("000");
		damageText.text = ($"{damage}");

	}
}