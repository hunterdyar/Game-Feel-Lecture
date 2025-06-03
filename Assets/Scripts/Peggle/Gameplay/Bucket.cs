using Peggle;
using UnityEngine;


public class Bucket : MonoBehaviour
{
	[SerializeField] private PeggleManager _manager;
	[SerializeField] private float _width;
	[SerializeField] private Transform _leftPosition;
	[SerializeField] private Transform _rightPosition;
	private int moveDir = 1;
	private float LeftEdge => _leftPosition.position.x + _width / 2;
	private float RightEdge => _rightPosition.position.x - _width / 2;

	void Start()
	{
		if (PeggleManager.Settings.randomizeBucketStartPosition)
		{
			float randPos = Random.Range(LeftEdge, RightEdge);
			transform.position = new Vector3(randPos, transform.position.y, transform.position.z);
		}
	}

	void Update()
	{
		if (transform.position.x < LeftEdge)
		{
			moveDir = 1;
		}else if (transform.position.x > RightEdge)
		{
			moveDir = -1;
		}
		
		transform.Translate(Vector3.right * (moveDir * PeggleManager.Settings.bucketMoveSpeed * Time.deltaTime), Space.World);
	}
}
