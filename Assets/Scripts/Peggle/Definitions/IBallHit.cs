using UnityEngine;

namespace Peggle
{
	public interface IBallHit
	{
		public void Hit(Ball ball, Collision2D collision);
	}
}