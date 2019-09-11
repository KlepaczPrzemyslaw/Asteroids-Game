using System;
using UnityEngine;

[RequireComponent(typeof(Collision2D))]
public class ShipShield : MonoBehaviour, IUpgradable
{
	private bool _active
	{
		get { return _currentState > 0; }
	}

	[SerializeField]
	private Sprite[] _shieldSprites;

	#region IUpgradable

	public int MaxLevel
	{
		get => _shieldSprites.Length - 1;
	}

	public int CurrentLevel { get; private set; }

	public int UpgradeCost
	{
		get => CurrentLevel * 150 + 100;
	}

	public void Upgrade()
	{
		CurrentLevel++;
		RepairShield();
	}

	#endregion

	private int _currentState = 0;
	public int CurrentState
	{
		get
		{
			return _currentState;
		}
		set
		{
			_currentState = Mathf.Clamp(value, 0, MaxLevel);
			UpdateSprite();
		}
	}

	void Awake()
	{
		FindObjectOfType<AsteroidWaveController>().OnWaveStarted += 
			_ => RepairShield();
	}

	void Start()
	{
		RepairShield();
	}

	private void RepairShield()
	{
		CurrentState = CurrentLevel;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.gameObject.CompareTag("Asteroid")) return;
		if (!_active) return;

		--CurrentState;
		Destroy(collision.gameObject.GetComponent<Asteroid>().gameObject);
	}

	private void UpdateSprite()
	{
		GetComponent<SpriteRenderer>().sprite = _shieldSprites[_currentState];
	}
}
