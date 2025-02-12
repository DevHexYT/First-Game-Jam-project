using UnityEngine;

public class EnemyScript : MonoBehaviour {
	[SerializeField] private Transform player;
	[SerializeField] private GameObject gunHoldingPrefab;
	[SerializeField] private Transform gunHoldParent;
	[SerializeField] private float distance;
	[SerializeField] private ParticleSystem deathParticle;

	[Header("Shooting Settings")]
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private float shootCooldown = 1.5f;
	[SerializeField] private float shootRange = 10f;
	[SerializeField] private float angleTolerance = 10f; 

	private float lastShootTime;
	private Rigidbody2D rb;
	private GameObject gunHolding;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		gunHolding = Instantiate(gunHoldingPrefab);
		gunHolding.transform.SetParent(gunHoldParent);
		gunHolding.transform.localPosition = Vector3.zero;

		gunHolding.GetComponent<GunsScript>().bulletPrefab = bulletPrefab;

		rb = GetComponent<Rigidbody2D>();

		GameObject.Find("UIMannge").GetComponent<GameManngeScript>().AddEnemy();
	}

	// Update is called once per frame
	void Update() {
		if (Vector2.Distance(transform.position, player.position) <= shootRange) {

			Vector2 dir = player.position - transform.position;
			float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			float smoothedAngle = Mathf.LerpAngle(rb.rotation, targetAngle, Time.deltaTime * 10);
			rb.rotation = smoothedAngle;

			if (Time.time >= lastShootTime + shootCooldown) {
				float angleDifference = Mathf.Abs(Mathf.DeltaAngle(rb.rotation, targetAngle));

				if (angleDifference <= angleTolerance) {
					gunHolding.GetComponent<GunsScript>().Shoot();
					lastShootTime = Time.time;
				}
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("PlayerBullet")) {
			StaminaScript staminaScript = player.GetComponent<StaminaScript>();
			GameObject.Find("UIMannge").GetComponent<GameManngeScript>().RemoveEnemy();
			if (staminaScript) {
				staminaScript.curStamina += 25f;
				staminaScript.SetValue(staminaScript.curStamina);
			}
			Destroy(gameObject);
			ParticleSystem newDeathParticle = Instantiate(deathParticle, transform.position, transform.rotation);
			newDeathParticle.Play();
			Destroy(newDeathParticle,1.5f);
			Destroy(other.gameObject);
			Debug.Log(GameObject.Find("UIMannge").GetComponent<GameManngeScript>().enemysCount);
		}
	}
}
