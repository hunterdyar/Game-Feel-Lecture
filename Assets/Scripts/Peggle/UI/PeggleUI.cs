using BTween;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Pool;


namespace Peggle.Peggle.UI
{
	public class PeggleUI : MonoBehaviour
	{
		private UIDocument _doc;
		private Label _totalScoreLabel;
		private LagNumberUpdater _totalScoreUpdater;
		private Label _shotScoreLabel;
		private LagNumberUpdater _shotScoreUpdater;
		private Label _ballsLeftLabel;

		private Transform _pool;
		public WorldSpaceUIDocument labelUIPrefab;
		private IObjectPool<WorldSpaceUIDocument> uiDocumentPool;

		WorldSpaceUIDocument Create() => Instantiate(labelUIPrefab, _pool, true);
		void OnTake(WorldSpaceUIDocument uiDocument)
		{
			uiDocument.transform.localScale = labelUIPrefab.transform.localScale;
			uiDocument.gameObject.SetActive(true);
		}

		void OnRelease(WorldSpaceUIDocument uiDocument) => uiDocument.gameObject.SetActive(false);
		void OnDestroyPool(WorldSpaceUIDocument uiDocument) => Destroy(uiDocument.gameObject);
		private void Awake()
		{
			_pool = new GameObject("Pool").transform;
			_pool.gameObject.name = "UI Pool";
			
			_doc = GetComponent<UIDocument>();
			_totalScoreLabel = _doc.rootVisualElement.Q<Label>("TotalScore");
			_shotScoreLabel = _doc.rootVisualElement.Q<Label>("ShotScore");
			_ballsLeftLabel = _doc.rootVisualElement.Q<Label>("BallsLeft");
			uiDocumentPool = new ObjectPool<WorldSpaceUIDocument>(Create, OnTake, OnRelease, OnDestroyPool);
			
			_totalScoreUpdater = new LagNumberUpdater();
			_totalScoreUpdater.OnCurrentChanged += i =>
			{
				_totalScoreLabel.text = i.ToString();
			} ;
			_totalScoreLabel.text = "0";

			_shotScoreUpdater = new LagNumberUpdater();
			_shotScoreUpdater.OnCurrentChanged += i =>
			{
				_shotScoreLabel.text = i.ToString();
			};
			_shotScoreLabel.text = "0";
		}
		

		private void OnEnable()
		{
			PeggleManager.OnShotScoreChanged += OnShotScoreChanged;
			PeggleManager.OnTotalScoreChanged += OnTotalScoreChanged;
			PeggleManager.OnScoreEarned += OnScoreEarned;
			PeggleManager.OnRemainingBallCountChanged += OnRemainingBallCountChanged;

			SetVisible(PeggleManager.Settings.showTextUI);
		}

		private void SetVisible(bool visible)
		{
			_doc.rootVisualElement.visible = visible;
		}


		private void OnDisable()
		{
			PeggleManager.OnShotScoreChanged -= OnShotScoreChanged;
			PeggleManager.OnTotalScoreChanged -= OnTotalScoreChanged;
			PeggleManager.OnScoreEarned -= OnScoreEarned;
			PeggleManager.OnRemainingBallCountChanged += OnRemainingBallCountChanged;
		}

		private void OnRemainingBallCountChanged(int c)
		{
			_ballsLeftLabel.text = c.ToString();
		}

		private void OnShotScoreChanged(int score)
		{
			if (PeggleManager.Settings.AnimateScoreChanges)
			{
				_shotScoreUpdater.SetDesired(score);
			}
			else
			{
				_shotScoreUpdater.SetCurrent(score);
			}
		}
		private void OnTotalScoreChanged(int score)
		{
			if (PeggleManager.Settings.AnimateScoreChanges)
			{
				_totalScoreUpdater.SetDesired(score);
			}
			else
			{
				_totalScoreUpdater.SetCurrent(score);
			}
		}
		private void Update()
		{
			_shotScoreUpdater.Tick(Time.deltaTime);
			_totalScoreUpdater.Tick(Time.deltaTime);
		}

		private void OnScoreEarned(Vector3 worldPos, int points)
		{
			if (PeggleManager.Settings.AnimateScoreDings)
			{
				var spawnPos = worldPos + Vector3.up * 0.5f;
				WorldSpaceUIDocument instance = uiDocumentPool.Get();
				instance.transform.SetPositionAndRotation(spawnPos, Quaternion.identity);
				instance.SetLabelText(points.ToString());

				//create tween.
				instance.transform.BMoveFromTo(spawnPos, spawnPos + Vector3.up * 0.5f, 0.5f, Ease.EaseOutCirc)
					.Then(instance.transform.BScaleFromTo(instance.transform.localScale, Vector3.zero, 0.1f,
						Ease.EaseInCirc, false))
					.OnComplete(() => { uiDocumentPool.Release(instance); });
			}
		}
	}
}