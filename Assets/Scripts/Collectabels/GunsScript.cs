using Unity.Cinemachine;
using UnityEngine;

public class GunsScript : CollectableScript {

	[HideInInspector] public Transform firePoint;
	public GameObject bulletPrefab;
	public GunHolder gunHolder;

	[SerializeField] private float fireRate;
	[SerializeField] private float fireSpeed;
	[SerializeField] private float maxPitch;
	[SerializeField] private float minPitch;
	[SerializeField] private float maxRange;
	[SerializeField] private float reloadTime;
	[SerializeField] private int maxBullets;
	private float nextReloadTime = 0f;
	private float fireTime;
	private int bulletsCount;
	private CinemachineImpulseSource impulseSource;

	public enum GunHolder {
		player,
		enemy
	}

	public void Shoot() {
		if(gunHolder == GunHolder.player) CameraShakeManager.instance.CameraShake(impulseSource);
		FindFirstObjectByType<AudioManager>().Play("GunShoot",Random.Range(minPitch,maxPitch));
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

		rb.AddForce(firePoint.right * fireSpeed, ForceMode2D.Impulse);
		BulletScript bulletScript = bullet.GetComponent<BulletScript>();
		bulletScript.maxRange = maxRange;
		bulletsCount--;
	}

	private void Start() {
		bulletsCount = maxBullets;
		firePoint = transform.Find("FirePoint");
		player = GameObject.FindWithTag("Player");
		collectTransform = player.transform.Find("Collect");
		impulseSource = GetComponent<CinemachineImpulseSource>();
	}


	private void Update() {
		CheckClosestCollectable();
		HandleCollectionInput();
		HandleCollection();

		if (collected) {
			if(Input.GetButton("Fire1") && Time.time >= fireTime && bulletsCount > 0) {
				Shoot();
				fireTime = Time.time + 1f / fireRate;
			} else {
				if (nextReloadTime == 0f) {
					nextReloadTime = Time.time + reloadTime;
				}

				if (Time.time >= nextReloadTime) {
					bulletsCount = maxBullets;
					nextReloadTime = 0f;
				}
			}
		}
	}

}
