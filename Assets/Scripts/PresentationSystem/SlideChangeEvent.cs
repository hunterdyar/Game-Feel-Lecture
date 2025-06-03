using PresentationSystem.Viewer.SlideViewers;

namespace PresentationSystem
{
	public struct SlideChangeEvent
	{
		public bool Consumed { get; private set; }
		public int ChangeDirection;
		public SlideChangeEvent(int dir, SlideBase slide)
		{
			ChangeDirection = dir;
			Consumed = false;
		}

		public void Consume()
		{
			Consumed = true;
		}
	}
}