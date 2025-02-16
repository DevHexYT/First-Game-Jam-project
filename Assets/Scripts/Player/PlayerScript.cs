using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerScript : MonoBehaviour{

	[HideInInspector] public bool slowMotion = false;
	[HideInInspector] public bool loseLevel = false;
	[HideInInspector] public bool winLevel = false;
	[HideInInspector] public bool esc = false;
	[HideInInspector] public StaminaScript staminaScript;
	[HideInInspector] public Rigidbody2D rb;
	public const float SPEED = 4.5F;

	[Header("Game Objects")]
	public GameObject collect;
	public GameObject slowMoPost;
	public GameObject resumeCanvas;
	
	private Transform staminaBarPos;
	private ChromaticAberration chromatic;
	private LensDistortion lensDistortion;
	private WhiteBalance whiteBalance;
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		FindFirstObjectByType<AudioManager>().Play("Theme");
		staminaScript = GetComponent<StaminaScript>();
		rb = GetComponent<Rigidbody2D>();
		staminaBarPos = transform.Find("StaminaBarPosistion");
	}

	// Update is called once per frame
	void Update() {
		PlayerMovement(rb, collect, SPEED);
		SlowmotionEffect(0.3f, ref slowMotion, staminaScript.curStamina);
		StaminaBarHandle(slowMotion, ref staminaScript.curStamina, 50f);
		Resume();
		PostProcess();
		if (staminaScript.staminaCanvas != null)
		staminaScript.SetBarPosition(staminaBarPos.position,staminaBarPos.rotation);
	}

	#region PlayerMovement
	private void PlayerMovement(Rigidbody2D rb, GameObject collect, float speed) {
		float inputX = Input.GetAxisRaw("Horizontal");
		float inputY = Input.GetAxisRaw("Vertical");
        
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 dir = mousePos - rb.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		rb.rotation = angle;

		collect.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));

		rb.linearVelocity = new Vector2(inputX, inputY).normalized * speed;
	}

	#endregion

	#region PlayerAbillty

	public void SlowmotionEffect(float timeScale,ref bool slowMotion,float curStamina) {
		if (Input.GetKeyDown(KeyCode.Q) && !slowMotion && curStamina > 0 && !esc && !loseLevel) {
			Time.timeScale = timeScale;
			slowMotion = true;
		} else if (Input.GetKeyDown(KeyCode.Q) && slowMotion || curStamina <= 0 && !esc && !loseLevel) {
			Time.timeScale = 1f;
			slowMotion = false;
		} else if (slowMotion && !loseLevel) Time.timeScale = timeScale; else return;
	}

	public void StaminaBarHandle(bool slowMotion, ref float curStamina, float staminaLoseRate) {
		if (slowMotion) {
			curStamina -= staminaLoseRate * Time.deltaTime;
			curStamina = Mathf.Max(curStamina, 0);
			staminaScript.SetValue(curStamina);
		}
	}


	#endregion

	#region CheckTriggers
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("EnemyBullet") && !winLevel) {
			resumeCanvas.SetActive(true);
			Time.timeScale = 0f;
			loseLevel = true;
		}
	}
	#endregion

	#region Other
	private void Resume() {
		if (resumeCanvas == null)
			resumeCanvas = GameObject.Find("UIMannge").transform.Find("ResumeCanvas").gameObject;
		if (Input.GetKeyDown(KeyCode.Escape) && !esc) {
			resumeCanvas.SetActive(true);
			staminaScript.staminaCanvas.SetActive(false);
			Time.timeScale = 0f;
			esc = true;
		} else if (Input.GetKeyDown(KeyCode.Escape) && esc) {
			resumeCanvas.SetActive(false);
			staminaScript.staminaCanvas.SetActive(true);
			Time.timeScale = 1f;
			esc = false;
		}
	}
	private void PostProcess() {
		if (slowMoPost == null)
			slowMoPost = GameObject.Find("PostProcessing").transform.Find("SlowMoPost").gameObject;
		Volume volume = slowMoPost.GetComponent<Volume>();
		float[] targetIntensities = new float[4];
		targetIntensities[0] = slowMotion ? 0.4f : 0f;
		targetIntensities[1] = slowMotion ? 0.25f : 0f;
		targetIntensities[2] = slowMotion ? -30 : 0f;
		targetIntensities[3] = slowMotion ? 20f : 0f;
		float lerpSpeed = 1f - Mathf.Exp(-5f * Time.deltaTime);

		if (volume.profile.TryGet(out chromatic))
			chromatic.intensity.value = Mathf.Lerp(chromatic.intensity.value, targetIntensities[0], lerpSpeed);

		if (volume.profile.TryGet(out lensDistortion))
			lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, targetIntensities[1], lerpSpeed);

		if (volume.profile.TryGet(out whiteBalance)) {
			whiteBalance.temperature.value = Mathf.Lerp(whiteBalance.temperature.value, targetIntensities[2], lerpSpeed);
			whiteBalance.tint.value = Mathf.Lerp(whiteBalance.tint.value, targetIntensities[3], lerpSpeed);
		}
	}
	#endregion
}
