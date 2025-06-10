using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Peggle
{
	public static class BallPrediction
	{
		private static Scene? _predictionScene;
		private static PhysicsScene2D _physics;
		private static Dictionary<GameObject, Collider2D> _collider2Ds = new Dictionary<GameObject, Collider2D>();
		private static Transform _registeredCollidersRoot;
		private static Rigidbody2D _predictionBall;
		private static bool _isDirty;
		
		public static Vector3[] PredictionPositions => _predictionPositions;
		private static Vector3[] _predictionPositions;
		private static void ConfigurePhysicsSceneLazy()
		{
			if (_predictionScene == null)
			{
				_predictionScene = SceneManager.CreateScene("Physics Prediction Scene", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
				_physics = _predictionScene.Value.GetPhysicsScene2D();
				
			}
			
			if (!_registeredCollidersRoot)
			{
				_registeredCollidersRoot = new GameObject("Prediction Colliders").transform;
				SceneManager.MoveGameObjectToScene(_registeredCollidersRoot.gameObject, _predictionScene.Value);
			}
		}

		public static void MarkDirty()
		{
			_isDirty = true;
		}
		public static void ConfigurePredictionBall(Ball ball)
		{
			if (ball == null)
			{
				return;
			}
			
			if (_predictionBall != null)
			{
				Object.Destroy(_predictionBall.gameObject);
			}

			ConfigurePhysicsSceneLazy();
			
			

			var newGo = new GameObject("Prediction Ball");
			newGo.transform.SetParent(_registeredCollidersRoot.gameObject.transform);
			_predictionBall = newGo.AddComponent<Rigidbody2D>();
			var circleCollider2D = newGo.AddComponent<CircleCollider2D>();
			var circ = ball.GetComponent<CircleCollider2D>();
			circleCollider2D.MatchProperties(circ);
			_predictionBall.MatchProperties(ball.GetComponent<Rigidbody2D>());
		}

		public static void RegisterCircleColliderForPrediction(GameObject gameObject, CircleCollider2D collider)
		{
			if (gameObject.scene != _predictionScene)
			{
				ConfigurePhysicsSceneLazy();
				UnregisterColliderForPrediction(gameObject);
				var newGo = new GameObject(gameObject.name + "(Physics Clone)");
				var newCol = newGo.AddComponent<CircleCollider2D>();
				newCol.MatchProperties(collider);

				var match = newGo.AddComponent<MatchTransform>();
				match.SetTarget(gameObject.transform);
				newGo.transform.SetParent(_registeredCollidersRoot, false);
					
				_collider2Ds.Add(gameObject, newCol);
			}
			else
			{
				Debug.LogError("Cannot register collider for prediction, this is a prediction object!");
			}
		}
		
		public static void UnregisterColliderForPrediction(GameObject gameObject)
		{
			if (_collider2Ds.ContainsKey(gameObject))
			{
				Object.Destroy(_collider2Ds[gameObject]);
				_collider2Ds.Remove(gameObject);
				MarkDirty();
			}
		}
		
		public static void SetEnabled(GameObject gameObject, bool enabled)
		{
			if (_collider2Ds.TryGetValue(gameObject, out var collider2D))
			{
				if (collider2D)
				{
					collider2D.gameObject.SetActive(enabled);
					MarkDirty();
				}
				else
				{
					_collider2Ds.Remove(gameObject);
					MarkDirty();
				}
			}
		}


		public static void PredictLaunch(Ball settingsBallPrefab, Vector3 launchPointPosition,
			Quaternion launchPointRotation, float launchSpeed, Vector3 noise) {

			if (!_isDirty || !PeggleManager.Settings.LauncherPredictionEnabled)
			{
				return;
			}
			if (_predictionBall == null)
			{
				ConfigurePredictionBall(settingsBallPrefab);
			}

			if (_predictionBall == null)
			{
				//not ready yet, race condition, abort.
				return;
			}

			_predictionBall.transform.position = launchPointPosition;
			_predictionBall.transform.rotation = launchPointRotation;
			_predictionBall.linearVelocity = Vector3.zero;
			_predictionBall.angularVelocity = 0f;
			_predictionBall.AddForce(noise-_predictionBall.transform.transform.up * launchSpeed, ForceMode2D.Impulse);
			
			if(_predictionPositions == null || _predictionPositions.Length != PeggleManager.Settings.LauncherTrailTickCount)
			{
				_predictionPositions = new Vector3[PeggleManager.Settings.LauncherTrailTickCount];
			}
			
			// todo: check if the settings have saved since last prediction.
			for(int i = 0; i < PeggleManager.Settings.LauncherTrailTickCount; i++)
			{
				_physics.Simulate(Time.fixedDeltaTime);
				_predictionPositions[i] = _predictionBall.transform.position;
			}

			_isDirty = false;
		}

		public static void RegisterBoxColliderForPrediction(GameObject gameObject, BoxCollider2D collider)
		{
			if (gameObject.scene != _predictionScene)
			{
				ConfigurePhysicsSceneLazy();
				UnregisterColliderForPrediction(gameObject);
				var newGo = new GameObject(gameObject.name + "(Physics Clone)");
				var newCol = newGo.AddComponent<BoxCollider2D>();
				newCol.MatchProperties(collider);

				var match = newGo.AddComponent<MatchTransform>();
				match.SetTarget(gameObject.transform);
				newGo.transform.SetParent(_registeredCollidersRoot, false);

				_collider2Ds.Add(gameObject, newCol);
			}
			else
			{
				Debug.LogError("Cannot register collider for prediction, this is a prediction object!");
			}
		}
	}
}