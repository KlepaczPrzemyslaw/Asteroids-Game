using UnityEngine;

[System.Serializable]
public class AsteroidType
{
	public Sprite Sprite = null;
	public float Durability = 1f;

	public float MinSpeed = 4f;
	public float MaxSpeed = 8f;

	public int Points = 100;
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Asteroid : MonoBehaviour
{
	private ShipMoneyProfit _shipMoneyProfit;
	private SpriteRenderer _spriteRenderer = null;
	private GameManager _gameManager = null;
	private float _durability = 1f;
	private float _minSpeed = 4f;
	private float _maxSpeed = 8f;
	private int _points;

	[SerializeField]
	public GameObject _destroyingParticles = null;

	[SerializeField]
	public GameObject _destroyedParticles = null;

	void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_gameManager = FindObjectOfType<GameManager>();
	}

	void Start()
	{
		_shipMoneyProfit = FindObjectOfType<ShipMoneyProfit>();
	}

	public void Configure(AsteroidType asteroidType)
	{
		GetComponent<Rigidbody2D>().velocity =
			Vector2.down * Random.Range(asteroidType.MinSpeed, asteroidType.MaxSpeed);

		_spriteRenderer.sprite = asteroidType.Sprite;
		_points = asteroidType.Points;
		_durability = asteroidType.Durability;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var obj = collision.gameObject;
		var bullet = obj.GetComponent<Bullet>();

		if (bullet != null)
		{
			CreateParticle(_destroyingParticles, collision.transform.position);
			DecreaseDurability(bullet.BulletPower);
			Destroy(obj);
		}
	}

	private void DecreaseDurability(float bulletPower)
	{
		_durability -= bulletPower;

		if (_durability <= 0)
		{
			CreateParticle(_destroyedParticles, transform.position);
			_gameManager.Money += _points * _shipMoneyProfit.GetMoneyMultiplier();
			Destroy(gameObject);
		}
	}

	private void CreateParticle(GameObject prefab, Vector3 position)
	{
		var particles = Instantiate(prefab, position, Quaternion.identity);

		particles.GetComponent<ParticleSystemRenderer>().material.mainTexture =
			_spriteRenderer.sprite.texture;

		Destroy(particles.gameObject, 1.5f);
	}
}
