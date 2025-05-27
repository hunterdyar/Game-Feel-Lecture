using System;
using UnityEngine;

namespace Peggle
{
	[RequireComponent(typeof(Peg))]
	public class PegAnimation : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _pegRenderer;
		[SerializeField] private SpriteRenderer _clearedOverlayRenderer;
		private Peg _peg;
		void Awake()
		{
			_peg = GetComponent<Peg>();
		}

		private void OnEnable()
		{
			_peg.OnPegTypeChanged += OnPegTypeChanged;
			_peg.OnThisPegHit += OnThisPegHit;
			_peg.OnPegStateChanged += OnPegStateChanged;
		}

		private void OnDisable()
		{
			_peg.OnPegTypeChanged -= OnPegTypeChanged;
			_peg.OnThisPegHit -= OnThisPegHit;
			_peg.OnPegStateChanged -= OnPegStateChanged;
		}
		
		private void OnThisPegHit(int arg1, Collision2D arg2)
		{
			
		}

		private void OnPegStateChanged(PegState state)
		{
			switch (state)
			{
				
				case PegState.ActiveToBeHit:
					_pegRenderer.enabled = true;
					_clearedOverlayRenderer.enabled = false;
					break;
				case PegState.LitUp:
					_pegRenderer.enabled = true;
					_clearedOverlayRenderer.enabled = true;
					break;
				case PegState.Cleared:
					_clearedOverlayRenderer.enabled = false;
					_pegRenderer.enabled = false;
					break;
				case PegState.ClearedByStuck:
					_pegRenderer.enabled = false;
					_clearedOverlayRenderer.enabled = false;
					break;
			}
		}
		
		private void OnPegTypeChanged(PegType pegType)
		{
			switch (pegType)
			{
				case PegType.Basic:
					_pegRenderer.color = _peg.Manager.Settings.basicPegColor;
					break;
				case PegType.Required:
					_pegRenderer.color = _peg.Manager.Settings.requiredPegColor;
					break;
				case PegType.SuperDuper:
					_pegRenderer.color = _peg.Manager.Settings.specialPegColor;
					break;
			}
		}
	}
}