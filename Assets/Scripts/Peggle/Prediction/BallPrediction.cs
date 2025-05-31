using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Peggle.Peggle.Prediction
{
	public static class BallPrediction
	{
		private static Scene? _predictionScene;
		private static Dictionary<GameObject, Collider2D> _collider2Ds = new Dictionary<GameObject, Collider2D>();
		private static Transform _registeredCollidersRoot;
		private static void ConfigurePhysicsSceneLazy()
		{
			// if (_predictionScene == null)
			// {
			// 	_predictionScene = SceneManager.CreateScene("Physics Prediction Scene");
			// }
			//
			// if (!_registeredCollidersRoot)
			// {
			// 	_registeredCollidersRoot = new GameObject("Prediction Colliders").transform;
			// 	SceneManager.MoveGameObjectToScene(_registeredCollidersRoot.gameObject, _predictionScene.Value);
			// }
		}
		
		public static void RegisterCircleColliderForPrediction(GameObject gameObject, CircleCollider2D collider)
		{
			// if (gameObject.scene != _predictionScene)
			// {
			// 	ConfigurePhysicsSceneLazy();
			// 	UnregisterColliderForPrediction(gameObject);
			// 	var newGo = new GameObject(gameObject.name + "(Physics Clone");
			// 	var newCol = newGo.AddComponent<Collider2D>();
			// 	
			// 	newCol.sharedMaterial = collider.sharedMaterial;
			// 	newCol.isTrigger = collider.isTrigger;
			// 	newCol.tag = collider.tag;
			// 	newCol.offset = collider.offset;
			// 	newCol.callbackLayers = collider.callbackLayers;
			// 	newCol.density = collider.density;
			// 	newCol.excludeLayers = collider.excludeLayers;
			// 	newCol.includeLayers = collider.includeLayers;
			// 	newCol.layerOverridePriority = collider.layerOverridePriority;
			// 	
			// 	_collider2Ds.Add(gameObject, newCol);
			// }
			// else
			// {
			// 	Debug.LogError("Cannot register collider for prediction, this is a prediction object!");
			// }
		}
		
		public static void UnregisterColliderForPrediction(GameObject gameObject)
		{
			// if (_collider2Ds.ContainsKey(gameObject))
			// {
			// 	GameObject.Destroy(_collider2Ds[gameObject]);
			// 	_collider2Ds.Remove(gameObject);
			// }
		}
		
		public static void SetEnabled(GameObject gameObject, bool enabled)
		{
			// if (_collider2Ds.ContainsKey(gameObject))
			// {
			// 	_collider2Ds[gameObject].enabled = enabled;
			// }
		}
	}
}