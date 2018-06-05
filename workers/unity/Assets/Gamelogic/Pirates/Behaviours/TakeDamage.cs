using Assets.Gamelogic.Core;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;
using Improbable.Ship;

namespace Assets.Gamelogic.Pirates.Behaviours
{
    // Add this MonoBehaviour on UnityWorker (server-side) workers only
    [WorkerType(WorkerPlatform.UnityWorker)]
    public class TakeDamage : MonoBehaviour
    {
		// this worker must have ...
		[Require]
		// ... write access on this entity's health component
		private Health.Writer HealthWriter;

		// note: the "other" game object is the cannonball
        private void OnTriggerEnter(Collider other)
        {
			if (HealthWriter == null) 
			{
				return;
			}

			if (HealthWriter.Data.currentHealth <= 0) 
			{
				return;
			}

            if (other != null && other.gameObject.tag == SimulationSettings.CannonballTag)
            {
				// for now, just do some logging
				// Debug.LogWarning("Collision detected with " + gameObject.EntityId());
				int newHealth = HealthWriter.Data.currentHealth - 250;
				HealthWriter.Send (new Health.Update ().SetCurrentHealth (newHealth));
            }
        }
    }
}