using Unity.Cinemachine;
using UnityEngine;

public class GunsScript : CollectableScript {

	[HideInInspector] public Transform firePoint;
	public float fireRate;
	public float fireSpeed;
	public float maxPitch;
	public float minPitch;
	public GameObject bulletPrefab;

	public GunHolder gunHolder;
	private float fireTime;
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

		Vector2 shootDirection = firePoint.right;
		rb.AddForce(shootDirection * fireSpeed, ForceMode2D.Impulse);
	}

	private void Start() {
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
			if(Input.GetButton("Fire1") && Time.time >= fireTime) {
				Shoot();
				fireTime = Time.time + 1f / fireRate;
			}
		}
	}

}
