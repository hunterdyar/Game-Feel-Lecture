using System;
using System.Collections;
using BTween;
using UnityEngine;

namespace Peggle
{
	[RequireComponent(typeof(Peg))]
	public class PegAnimation : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _pegRenderer;
		[SerializeField] private SpriteRenderer _clearedOverlayRenderer;
		[SerializeField] private SpriteRenderer _pegImpactOverlayRenderer;
		
		private Tween _impactTween;
		
		private Peg _peg;
		void Awake()
		{
			_peg = GetComponent<Peg>();
			
			//configure tween.
			_impactTween = _pegImpactOverlayRenderer.transform.BScaleFromTo(Vector3.one, Vector3.one * 2, 0.2f, Ease.EaseOutCirc, false)
				.Add(new FloatTween((c)=>
					{
						if (_pegImpactOverlayRenderer != null)
						{
							_pegImpactOverlayRenderer.color = new Color(_pegImpactOverlayRenderer.color.r,
								_pegImpactOverlayRenderer.color.g, _pegImpactOverlayRenderer.color.b, c);
						}

						return c;
					},
					1,0,Ease.EaseOutCirc));
			_impactTween.OnStart(()=> _pegImpactOverlayRenderer.enabled = true);
			_impactTween.OnComplete(() => _pegImpactOverlayRenderer.enabled = false);
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
			if (PeggleManager.Settings.bonkPegAnimationOnHit)
			{
				_impactTween.Start();
			}
		}

		private void OnPegStateChanged(PegState state)
		{
			switch (state)
			{
				case PegState.ActiveToBeHit:
					_pegRenderer.enabled = true;
					_clearedOverlayRenderer.enabled = false;
					_pegImpactOverlayRenderer.enabled = true;
					break;
				case PegState.LitUp:
					_pegRenderer.enabled = true;
					_clearedOverlayRenderer.enabled = PeggleManager.Settings.pegsShowHitState;
					_pegImpactOverlayRenderer.enabled = true;
					break;
				case PegState.Cleared:
					_clearedOverlayRenderer.enabled = false;
					_pegRenderer.enabled = false;
					_pegImpactOverlayRenderer.enabled = false;
					break;
				case PegState.ClearedByStuck:
					_pegRenderer.enabled = false;
					_clearedOverlayRenderer.enabled = false;
					_pegImpactOverlayRenderer.enabled = false;
					break;
			}
		}
		
		private void OnPegTypeChanged(PegType pegType)
		{
			switch (pegType)
			{
				case PegType.Basic:
					_pegRenderer.color = PeggleManager.Settings.basicPegColor;
					_pegImpactOverlayRenderer.color = PeggleManager.Settings.basicPegColor;
					break;
				case PegType.Required:
					_pegRenderer.color = PeggleManager.Settings.requiredPegColor;
					_pegImpactOverlayRenderer.color = PeggleManager.Settings.requiredPegColor;
					break;
				case PegType.SuperDuper:
					_pegRenderer.color = PeggleManager.Settings.specialPegColor;
					_pegImpactOverlayRenderer.color = PeggleManager.Settings.specialPegColor;
					break;
			}
		}
	}
}