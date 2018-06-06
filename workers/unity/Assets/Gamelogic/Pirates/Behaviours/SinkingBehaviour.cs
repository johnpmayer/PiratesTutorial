using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;
using Improbable.Ship;

namespace Assets.Gamelogic.Pirates.Behaviours
{
    // Add this MonoBehaviour on client workers only
    [WorkerType(WorkerPlatform.UnityClient)]
    public class SinkingBehaviour : MonoBehaviour
    {
		// only runs on worker which has ...
		[Require]
		// ... read access on entity's health component
		private Health.Reader HealthReader;

		private bool alreadySunk = false; // don't animate twice

		private void OnCurrentHealthUpdated(int currentHealth)
		{
			if (!alreadySunk && currentHealth <= 0)
			{
				VisualiseSinking ();
				alreadySunk = true;
			}
		}

		private void InitializeSinkingAnimation()
		{
			// in case some new client checks out this entity already dead, don't visualize 
			if (HealthReader.Data.currentHealth <= 0)
			{
				// this code jumps to the end of the animation
				foreach (AnimationState state in SinkingAnimation) 
				{
					state.normalizedTime = 1;
				}
				VisualiseSinking ();
				alreadySunk = true;
			}
		}

        [SerializeField]
        private Animation SinkingAnimation;

        private void OnEnable()
        {
			alreadySunk = false; // prefab pooling - this instance could have been re-used
			InitializeSinkingAnimation();
			HealthReader.CurrentHealthUpdated.Add (OnCurrentHealthUpdated);
        }

        private void OnDisable()
        {
			HealthReader.CurrentHealthUpdated.Remove (OnCurrentHealthUpdated);
        }

        private void VisualiseSinking()
        {
            SinkingAnimation.Play();
        }
    }
}
