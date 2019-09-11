using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShipGun : MonoBehaviour, IUpgradable
{
	private float _lastShootTime = 0f;
	private AudioSource _shootAudioSource;

	[SerializeField]
	private GameObject _bulletPrefab = null;

	[SerializeField]
	private BulletType[] _bulletTypes;

	[SerializeField]
	private AudioClip _shootAudioClip;
	
	private BulletType _bulletType
	{
		get { return _bulletTypes[CurrentLevel]; }
	}

	void Start()
	{
		_shootAudioSource = GetComponent<AudioSource>();
		_shootAudioSource.clip = _shootAudioClip;
	}

	void Update()
	{
		if (!Input.GetMouseButton(0)) return;
		if (!CanShootBullet()) return;

		ShootBullets();
		_lastShootTime = Time.timeSinceLevelLoad;
	}

	#region IUpgradable

	public int MaxLevel
	{
		get { return _bulletTypes.Length - 1; }
	}

	public int CurrentLevel { get; set; }

	public int UpgradeCost
	{
		get => CurrentLevel * 100 + 150;
	}

	public void Upgrade()
	{
		CurrentLevel++;
	}

	#endregion

	private void ShootBullets()
	{
		switch (_bulletType.CannonType)
		{
			case CannonType.Single:
				ShootBullet(Vector3.zero, Vector3.zero);
				break;
			case CannonType.Double:
				ShootBullet(Vector3.left * 0.1f, Vector3.forward * 5f);
				ShootBullet(Vector3.right * 0.1f, Vector3.back * 5f);
				break;
			case CannonType.Quadruple:
				ShootBullet(Vector3.left * 0.1f, Vector3.forward * 15f);
				ShootBullet(Vector3.left * 0.1f, Vector3.zero);
				ShootBullet(Vector3.right * 0.1f, Vector3.zero);
				ShootBullet(Vector3.right * 0.1f, Vector3.back * 15f);
				break;
			default:
				throw new Exception("Default CannonType -> Check ShootBullets() function;");
		}

		_shootAudioSource.Play();
	}

	private void ShootBullet(Vector3 positoin, Vector3 rotation)
	{
		var bullet = Instantiate(
			_bulletPrefab,
			transform.position + positoin + Vector3.up * 0.6f,
			Quaternion.Euler(rotation));

		bullet.GetComponent<Bullet>().Configure(_bulletType);
	}

	private bool CanShootBullet()
	{
		return Time.timeSinceLevelLoad - _lastShootTime >= _bulletType.ShootingDuration;
	}
}
