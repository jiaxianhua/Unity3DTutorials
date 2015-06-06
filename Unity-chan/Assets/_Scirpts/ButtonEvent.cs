using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour {
	private Button button;

	void Awake() {	 
		button = GetComponent<Button> ();
		button.onClick.AddListener (() => Application.Quit());
	}
}
