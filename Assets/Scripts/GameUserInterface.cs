using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameUserInterface : MonoBehaviour
{
	private AsteroidWaveController _asteroidWaveController;

	[SerializeField]
	private Text _waveCounter;

	[SerializeField]
	private Text _moneyCounter;

	[SerializeField]
	private Button _gunUpgradeButton;

	[SerializeField]
	private Button _shieldUpgradeButton;

	[SerializeField]
	private Button _shipSpeedUpgradeButton;

	[SerializeField]
	private Button _profitUpgradeButton;

	void Awake()
	{
		_asteroidWaveController = FindObjectOfType<AsteroidWaveController>();

		_asteroidWaveController.OnBreakStarted +=
			waveNumber =>
			{
				_waveCounter.text = $"Wave: {waveNumber}";
				_waveCounter.gameObject.SetActive(true);
			};

		_asteroidWaveController.OnPrepareStarted += () =>
			_waveCounter.gameObject.SetActive(false);

		FindObjectOfType<GameManager>().OnMoneyChanged +=
			money => _moneyCounter.text = $"Money: {money}";
	}

	void Start()
	{
		var shield = FindObjectOfType<ShipShield>();
		_shieldUpgradeButton.GetComponent<UpgradeButton>().Configure(shield);

		var gun = FindObjectOfType<ShipGun>();
		_gunUpgradeButton.GetComponent<UpgradeButton>().Configure(gun);

		var speed = FindObjectOfType<ShipSpeed>();
		_shipSpeedUpgradeButton.GetComponent<UpgradeButton>().Configure(speed);

		var profit = FindObjectOfType<ShipMoneyProfit>();
		_profitUpgradeButton.GetComponent<UpgradeButton>().Configure(profit);
	}
}
