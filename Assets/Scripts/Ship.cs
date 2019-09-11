using System;
using UnityEngine;

[RequireComponent(typeof(Collision2D))]
public class Ship : MonoBehaviour
{
	public event Action OnShipDestroyed;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.gameObject.CompareTag("Asteroid")) return;
			if (OnShipDestroyed != null)
				OnShipDestroyed.Invoke();
	}
}
