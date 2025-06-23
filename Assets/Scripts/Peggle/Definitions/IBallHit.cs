using UnityEngine;

namespace Peggle
{
	public interface IBallHit
	{
		public void Hit(Ball ball, Collision2D collision);
		public void Exit(Ball ball, Collision2D collision);
		public void Stay(Ball ball, Collision2D collision);
	}
}