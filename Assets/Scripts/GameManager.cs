using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public event Action<int> OnMoneyChanged;

	private int _money = 0;
	public int Money
	{
		get { return _money; }
		set
		{
			_money = Mathf.Max(0, value);

			if (OnMoneyChanged != null)
				OnMoneyChanged.Invoke(_money);
		}
	}

	void Awake()
	{
		FindObjectOfType<Ship>().OnShipDestroyed +=
			() => OnGameEnded();
	}

	void Start()
	{
		Money = 0;
	}

	private void OnGameEnded()
	{
		var points = FindObjectOfType<AsteroidWaveController>().CurrentWaveNumber;
		points = points * points + Money;
		GameState.SetCurrentResult(points);

		SceneManager.LoadScene("GameOver");
	}
}
