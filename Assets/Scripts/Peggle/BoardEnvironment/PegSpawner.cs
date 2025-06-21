using System;
using System.Collections.Generic;
using UnityEngine;

namespace Peggle
{
	public class PegSpawner : MonoBehaviour
	{
		[SerializeField] private PeggleManager _manager;
		private Peg PegPrefab => PeggleManager.Settings.PegPrefab;
		private IShape _shape;

		private List<Peg> _pegs = new List<Peg>();

		void Awake()
		{
			_shape = GetComponent<IShape>();
		}

		private void OnEnable()
		{
			PeggleManager.PrepareGame += Spawn;
		}

		private void OnDisable()
		{
			PeggleManager.PrepareGame -= Spawn;
		}

		public void Spawn()
		{
			//PrepareGame can be called at any time. 
			foreach (Peg peg in _pegs)
			{
				Destroy(peg.gameObject);
			}
			_pegs.Clear();

			foreach (Vector3 shapePoint in _shape.Points)
			{
				var p = Instantiate(PegPrefab, shapePoint, Quaternion.identity, transform);
				_pegs.Add(p);
			}
		}

		public void OnDrawGizmos()
		{
			if (_shape == null)
			{
				_shape = GetComponent<IShape>();
			}

			if (_shape != null)
			{
				foreach (var p in _shape.Points)
				{
					Gizmos.color = Color.yellow;
					Gizmos.DrawWireSphere(p, 0.2f);
				}
			}
		}

		private void OnValidate()
		{
			if (_shape == null)
			{
				_shape = GetComponent<IShape>();
			}
		}
	}
}