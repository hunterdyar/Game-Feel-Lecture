using System;
using UnityEngine;

namespace Peggle
{
	public class PhysicMatSettingsUpdater : SettingsChangeListener
	{
		[SerializeField] private PhysicsMaterial2D BallPhysMat;
		[SerializeField] private PhysicsMaterial2D PegPhysMat;
		[SerializeField] private PhysicsMaterial2D WallPhysMat;
		[SerializeField] private PhysicsMaterial2D BucketPhysMat;
		
		protected override void OnSettingsChanged(PeggleSettings peggleSettings)
		{
			BallPhysMat.bounciness = peggleSettings.ballBounciness;
			PegPhysMat.bounciness = peggleSettings.pegBounciness;
			WallPhysMat.bounciness = peggleSettings.wallBounciness;
			BucketPhysMat.bounciness = peggleSettings.bucketBounciness;
		}
	}
}