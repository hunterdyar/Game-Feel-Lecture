using System.Collections.Generic;
using UnityEngine;

namespace Peggle
{
	public static class Utility
	{
		public static void Shuffle<T>(this IList<T> list)
		{
			//fisher-yates
			for (int i = 0; i < list.Count; i++)
			{
				int r = UnityEngine.Random.Range(i, list.Count);
				(list[i], list[r]) = (list[r], list[i]);
			}
		}

		public static void MatchProperties(this CircleCollider2D circle, CircleCollider2D other)
		{
			circle.sharedMaterial = other.sharedMaterial;
			circle.isTrigger = other.isTrigger;
			circle.tag = other.tag;
			circle.offset = other.offset;
			circle.callbackLayers = other.callbackLayers;
			circle.excludeLayers = other.excludeLayers;
			circle.includeLayers = other.includeLayers;
			circle.layerOverridePriority = other.layerOverridePriority;
			circle.radius = other.radius;
		}

		public static void MatchProperties(this BoxCollider2D box, BoxCollider2D other)
		{
			box.sharedMaterial = other.sharedMaterial;
			box.isTrigger = other.isTrigger;
			box.tag = other.tag;
			box.offset = other.offset;
			box.callbackLayers = other.callbackLayers;
			box.excludeLayers = other.excludeLayers;
			box.includeLayers = other.includeLayers;
			box.layerOverridePriority = other.layerOverridePriority;
			box.size = other.size;
			box.edgeRadius = other.edgeRadius;
		}

		public static void MatchProperties(this Rigidbody2D rb, Rigidbody2D other, bool position = true)
		{
			rb.sharedMaterial = other.sharedMaterial;
			rb.tag = other.tag;
			rb.excludeLayers = other.excludeLayers;
			rb.includeLayers = other.includeLayers;
			rb.mass = other.mass;
			rb.constraints = other.constraints;
			rb.collisionDetectionMode = other.collisionDetectionMode;
			rb.centerOfMass = other.centerOfMass;
			rb.angularDamping = other.angularDamping;
			rb.freezeRotation = other.freezeRotation;
			rb.gravityScale = other.gravityScale;
			rb.interpolation = other.interpolation;
			rb.bodyType = other.bodyType;
			rb.simulated = other.simulated;
			rb.inertia = other.inertia;
			
			if (position)
			{
				rb.position = other.position;
				rb.angularVelocity = other.angularVelocity;
				rb.linearVelocity = other.linearVelocity;
				rb.rotation = other.rotation;
			}
		}
	}
}