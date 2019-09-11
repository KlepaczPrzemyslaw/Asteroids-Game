using UnityEngine;

[RequireComponent(typeof(Collision2D))]
public class AsteroidDestroyer : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Asteroid"))
		{
			Destroy(collision.gameObject);
		}
	}
}
