using UnityEngine;

namespace Peggle
{
	public class BallDeathZone : MonoBehaviour
	{
		[SerializeField] private PeggleManager _manager;
		public void ConsumeBall(Ball ball)
		{
			_manager.BallLeftPlay(ball);
			ball.gameObject.SetActive(false);
		}
	}
}