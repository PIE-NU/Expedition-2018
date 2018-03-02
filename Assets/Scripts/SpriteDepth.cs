using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (SpriteRenderer))]
public class SpriteDepth : MonoBehaviour {

	public float Y_SortOffset = 0;
	public bool AutoOffset = true;
	public bool CastShadows = true;
	public bool ReceiveShadows = true;
	public bool AutoGenerateSideShadows = false;
	SpriteRenderer m_spr;

	void Start () {
		m_spr = GetComponent<SpriteRenderer> ();
		GetComponent<Renderer> ().receiveShadows = ReceiveShadows;
		if (CastShadows)
			GetComponent<Renderer> ().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
		if (AutoGenerateSideShadows && GetComponent<BoxCollider2D>() && GetComponent<SpriteRenderer>()) {
			Debug.Log ("Autogenerating side shaodows");
			BoxCollider2D bc = GetComponent<BoxCollider2D> ();
			SpriteRenderer sr = GetComponent<SpriteRenderer> ();
			GameObject t = Instantiate (FindObjectOfType<GameManager> ().SideShadowTemplate, transform);
			t.GetComponent<SpriteRenderer> ().sprite = m_spr.sprite;
			Vector3 sc = transform.localScale;
			t.transform.Translate (new Vector3 (bc.size.x / 2, 0f, bc.size.y/4f));
			t.transform.localScale = new Vector3 (sc.x * (bc.size.y / bc.size.x)/2f, sc.y, sc.z);
			t.transform.Rotate (0f, 90f, 0f);
			Debug.Log (t);

			GameObject t2 = Instantiate (FindObjectOfType<GameManager> ().SideShadowTemplate, transform);
			t2.GetComponent<SpriteRenderer> ().sprite = m_spr.sprite;
			t2.transform.Translate (new Vector3 (-bc.size.x / 2, 0f, bc.size.y/4f));
			t2.transform.localScale = new Vector3 (sc.x * (bc.size.y / bc.size.x)/2f, sc.y, sc.z);
			t2.transform.Rotate (0f, 90f, 0f);
			Debug.Log (t2);

			GameObject t3 = Instantiate (FindObjectOfType<GameManager> ().SideShadowTemplate, transform);
			t3.GetComponent<SpriteRenderer> ().sprite = m_spr.sprite;
			t3.transform.Translate (new Vector3 (0f, 0f, bc.size.y/2));
			t3.transform.localScale = new Vector3 (sc.x , sc.y, sc.z);
			Debug.Log (t3);
		}
		if (AutoOffset && GetComponent<BoxCollider2D>()) {
			Y_SortOffset = GetComponent<BoxCollider2D> ().offset.y;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//m_spr.sortingOrder = Mathf.CeilToInt(transform.position.y * 100f) * -1 + Y_SortOffset;
		Vector3 pos = m_spr.transform.position;
		m_spr.transform.position = new Vector3 (pos.x, pos.y, pos.y + Y_SortOffset);
	}
}
