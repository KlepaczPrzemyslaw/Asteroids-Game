using UnityEngine;

public enum CannonType { Single, Double, Quadruple }

[System.Serializable]
public class BulletType
{
	public Sprite Sprite = null;
	public float ShootingDuration = 0.5f;
	public float Power = 1f;

	public CannonType CannonType = CannonType.Single;

	[Range(8f, 18f)]
	public float Speed = 8f;

	[Range(0.6f, 1.6f)]
	public float LifeTime = 1.6f;
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
	private Rigidbody2D _rigidbody;

	public float BulletPower = 1f;

	public void Configure(BulletType bulletType)
	{
		BulletPower = bulletType.Power;

		GetComponent<SpriteRenderer>().sprite = bulletType.Sprite;
		GetComponent<Rigidbody2D>().velocity = transform.rotation * Vector3.up * bulletType.Speed;
		Destroy(gameObject, bulletType.LifeTime);
	}
}
