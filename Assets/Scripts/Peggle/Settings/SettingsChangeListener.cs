using UnityEngine;

namespace Peggle
{
	public abstract class SettingsChangeListener : MonoBehaviour
	{
		protected virtual void OnEnable()
		{
			PeggleManager.OnSettingsChanged += OnSettingsChanged;
		}

		protected virtual void OnDisable()
		{
			PeggleManager.OnSettingsChanged -= OnSettingsChanged;
		}

		protected abstract void OnSettingsChanged(PeggleSettings settings);
	}
}