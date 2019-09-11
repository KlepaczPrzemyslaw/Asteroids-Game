using UnityEngine;

public class ShipMovement : MonoBehaviour
{
	private Camera _camera = null;

	[SerializeField]
	private float _speed = 10f;
	
	[SerializeField]
	private Vector2 _movementArea = new Vector2(2.2f, 4.4f);

	void Start()
	{
		_camera = FindObjectOfType<Camera>();
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(Vector3.zero, _movementArea * 2f);
	}

	void Update()
	{
		var targetPosition = (Vector2) _camera.ScreenToWorldPoint(Input.mousePosition);

		targetPosition.x = Mathf.Clamp(
			targetPosition.x, 
			- _movementArea.x, 
			_movementArea.x); 
		targetPosition.y = Mathf.Clamp(
			targetPosition.y, 
			- _movementArea.y, 
			_movementArea.y); 

		transform.position = Vector3.Lerp(
			transform.position,
			targetPosition,
			_speed * Time.deltaTime);
	}
}
