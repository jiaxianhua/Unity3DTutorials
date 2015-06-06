using UnityEngine;
using System.Collections;

public class ChanCtrl : MonoBehaviour {
	public float waitTime = 3f;
	public bool isRandom = true;
	public AnimationClip[] _faceClips;
	public string[] _faceMotionName;
	public AudioClip[] _chanClips;
	public AudioClip[] _hourClips;

	private Animator _animator;
	private AnimatorStateInfo _currentStateInfo;
	private AnimatorStateInfo _preStateInfo;
	private AudioSource _audio;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
		_currentStateInfo = _animator.GetCurrentAnimatorStateInfo (0);
		_preStateInfo = _currentStateInfo;
		_audio = GetComponent<AudioSource> ();

		_faceClips = Resources.LoadAll<AnimationClip>("FaceMotion");
		_chanClips = Resources.LoadAll<AudioClip> ("ChanVoice");
		_hourClips = Resources.LoadAll<AudioClip> ("HourClips");
		_faceMotionName = new string[_faceClips.Length];

		for (int i = 0; i < _faceClips.Length; i++) {
			_faceMotionName[i] = _faceClips[i].name;
		}

		StartCoroutine (RandomChangeMotion());
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hitInfo;

		if (Input.GetMouseButtonDown(0)) {
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity)) {
				if (hitInfo.collider.tag == "face") {
					ChangeFace();
				}
			}
		}

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

	void ChangeFace() {
		_animator.SetLayerWeight (1, 1);
		int index = Random.Range (0, _faceClips.Length);
		_animator.CrossFade (_faceMotionName [index], 0);

		if (_audio.isPlaying) {
			_audio.Stop();
		}
		_audio.clip = _chanClips [index];
		_audio.Play ();
	}

	public void OnAskTime() {
		int hour = System.DateTime.Now.Hour;
		if (_audio.isPlaying) {
			_audio.Stop();
		}
		_audio.clip = _hourClips [hour];
		_audio.Play ();
	}
}
