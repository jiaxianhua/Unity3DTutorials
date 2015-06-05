using UnityEngine;
using System.Collections;

public class ChanCtrl : MonoBehaviour {
	public float waitTime = 3f;
	public bool isRandom = true;

	private Animator _animator;
	private AnimatorStateInfo _currentStateInfo;
	private AnimatorStateInfo _preStateInfo;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
		_currentStateInfo = _animator.GetCurrentAnimatorStateInfo (0);
		_preStateInfo = _currentStateInfo;

		StartCoroutine (RandomChangeMotion());
	}
	
	// Update is called once per frame
	void Update () {
		if (_animator.GetBool("Next")) {
			_currentStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

			if (_preStateInfo.shortNameHash != _currentStateInfo.shortNameHash) {
				_animator.SetBool("Next", false);
				_preStateInfo = _currentStateInfo;
			}
		}
	}

	IEnumerator RandomChangeMotion() {
		while (true) {
			if (isRandom) {
				_animator.SetBool("Next", true);
			}
			yield return new WaitForSeconds(waitTime);
		}
	}
}
