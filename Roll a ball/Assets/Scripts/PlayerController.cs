using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 500;
	public Text countText;
	public Text winText;

	private int count;

	// Use this for initialization
	void Start () {
		count = 0;
		SetCountText ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "PickUp") {
			other.gameObject.SetActive (false);
			++count;
			SetCountText();
		}
	}

	void SetCountText() {
		countText.text = "Count : " + count.ToString ();
		if (count >= 12) {
			winText.text = "YOU WIN!";
		}
	}
}
