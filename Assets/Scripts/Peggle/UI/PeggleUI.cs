using System;
using BTween;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Pool;


namespace Peggle.Peggle.UI
{
	public class PeggleUI : MonoBehaviour
	{
		private UIDocument _doc;
		private Label _totalScoreLabel;
		private Label _shotScoreLabel;

		private Transform _pool;
		public WorldSpaceUIDocument labelUIPrefab;
		private IObjectPool<WorldSpaceUIDocument> uiDocumentPool;

		WorldSpaceUIDocument Create() => Instantiate(labelUIPrefab, _pool, true);
		void OnTake(WorldSpaceUIDocument uiDocument) => uiDocument.gameObject.SetActive(true);
		void OnRelease(WorldSpaceUIDocument uiDocument) => uiDocument.gameObject.SetActive(false);
		void OnDestroyPool(WorldSpaceUIDocument uiDocument) => Destroy(uiDocument.gameObject);
		private void Awake()
		{
			_pool = new GameObject("Pool").transform;
			_pool.gameObject.name = "UI Pool";
			
			_doc = GetComponent<UIDocument>();
			_totalScoreLabel = _doc.rootVisualElement.Q<Label>("TotalScore");
			_shotScoreLabel = _doc.rootVisualElement.Q<Label>("ShotScore");
			uiDocumentPool = new ObjectPool<WorldSpaceUIDocument>(Create, OnTake, OnRelease, OnDestroyPool);
		}

		private void OnEnable()
		{
			PeggleManager.OnShotScoreChanged += OnShotScoreChanged;
			PeggleManager.OnTotalScoreChanged += OnTotalScoreChanged;
			PeggleManager.OnScoreEarned += OnScoreEarned;
		}

		private void OnDisable()
		{
			PeggleManager.OnShotScoreChanged -= OnShotScoreChanged;
			PeggleManager.OnTotalScoreChanged -= OnTotalScoreChanged;
			PeggleManager.OnScoreEarned -= OnScoreEarned;
		}

		private void OnShotScoreChanged(int score)
		{
			_shotScoreLabel.text = score.ToString("D");
		}
		private void OnTotalScoreChanged(int score)
		{
			_totalScoreLabel.text = score.ToString("D");
		}

		private void OnScoreEarned(Vector3 worldPos, int points)
		{
			var spawnPos = worldPos + Vector3.up;
			WorldSpaceUIDocument instance = uiDocumentPool.Get();
			instance.transform.SetPositionAndRotation(spawnPos, Quaternion.identity);
			instance.SetLabelText(points.ToString());
			
			//create tween.
			instance.transform.BMoveFromTo(worldPos, worldPos + Vector3.up * 0.5f, 0.5f, Ease.EaseOutCirc)
				.OnComplete(() =>
				{
					uiDocumentPool.Release(instance);
				});

			//onfinish, release.
		}
	}
}