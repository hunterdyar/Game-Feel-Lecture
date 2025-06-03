using System;
using PresentationSystem;
using UnityEngine;

namespace Diagrams
{
	public class InteractionLoopSequencer : MonoBehaviour
	{
		public AnimatedDotAnimator _playerToSystem;
		public AnimatedDotAnimator _systemFeedback;
		public AnimatedDotAnimator _systemToPlayer;
		public AnimatedDotAnimator _playerRection;
		private int _index;

		private void OnEnable()
		{
			PresentationSystem.Presentation.OnChangeSlide += OnChangeSlide;
		}

		private void OnDisable()
		{
			PresentationSystem.Presentation.OnChangeSlide -= OnChangeSlide;
		}

		//Consume slide change event to handle substates.
		private SlideChangeEvent OnChangeSlide(SlideChangeEvent e)
		{
			_index += e.ChangeDirection;
			
			if(_index + e.ChangeDirection < 0 || _index + e.ChangeDirection > 4)
			{
				return e;
			}
			else
			{
				e.Consume();
				SetAnimated(_index);
				return e;
			}
		}

		private void Start()
		{
			SetAnimated(-1);
		}

		[ContextMenu("Next")]
		public void Next()
		{
			SetAnimated((_index+1)%4);
		}

		public void SetAnimated(int index)
		{
			_index = index;
			bool _pts = index == 0;
			bool _sf = index == 1;
			bool _stp = index == 2;
			bool _pr = index == 3;
			_playerToSystem?.SetAnimated(_pts);
			_systemFeedback?.SetAnimated(_sf);
			_systemToPlayer?.SetAnimated(_stp);
			_playerRection?.SetAnimated(_pr);
		}
	}
}