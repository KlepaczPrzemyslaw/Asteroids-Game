using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UpgradeButton : MonoBehaviour
{
	private AudioSource _upgradeSoundSource;
	private GameManager _gamemanager;
	private Button _button;

	[SerializeField]
	private string _text;

	public IUpgradable Upgradable;

	void Awake()
	{
		var waveController = FindObjectOfType<AsteroidWaveController>();
		_gamemanager = FindObjectOfType<GameManager>();

		waveController.OnBreakStarted += _ => gameObject.SetActive(true);
		waveController.OnPrepareStarted += () => gameObject.SetActive(false);
		_gamemanager.OnMoneyChanged += _ => RefreshButton();

		_button = GetComponent<Button>();
	}

	void Start()
	{
		_upgradeSoundSource = GetComponent<AudioSource>();
	}

	public void Configure(IUpgradable upgradable)
	{
		Upgradable = upgradable;
		_button.onClick.AddListener(Upgrade);
	}

	private void Upgrade()
	{
		if (!CanUpdate()) return;

		_upgradeSoundSource.Play();
		_gamemanager.Money -= Upgradable.UpgradeCost;
		Upgradable.Upgrade();
		RefreshButton();
	} 

	private void RefreshButton()
	{
		var canUpgrade = CanUpdate();

		_button.enabled = canUpgrade;
		_button.GetComponent<Image>().color = canUpgrade ? Color.white : Color.gray;

		var textComponent = _button.GetComponentInChildren<Text>();
		textComponent.text = $"{_text} ({Upgradable.CurrentLevel.ToString()})";

		if (!IsMaximumLevel())
			textComponent.text += $"\n{Upgradable.UpgradeCost}$";
	}

	private bool CanUpdate() => !IsMaximumLevel() && IsMoneyEnough();

	private bool IsMoneyEnough() => Upgradable.UpgradeCost <= _gamemanager.Money;

	private bool IsMaximumLevel() => Upgradable.CurrentLevel >= Upgradable.MaxLevel;
}
