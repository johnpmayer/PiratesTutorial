using UnityEngine;
using Improbable.Ship;
using Improbable.Unity.Visualizer;

namespace Assets.Gamelogic.Pirates.Cannons
{
    // This MonoBehaviour will be enabled on both client and server-side workers
    public class CannonFirer : MonoBehaviour
    {
        private Cannon cannon;

		// only runs on workers that can ...
		[Require]
		// ... read entity's ship controls component
		private ShipControls.Reader ShipControlsReader;

        private void Start()
        {
            // Cache entity's cannon gameobject
            cannon = gameObject.GetComponent<Cannon>();
        }

        public void AttemptToFireCannons(Vector3 direction)
        {
            if (cannon != null)
            {
                cannon.Fire(direction);
            }
        }

		private void OnFireLeft(FireLeft fireLeft)
		{
			AttemptToFireCannons (-transform.right);
		}

		private void OnFireRight(FireRight fireRight)
		{
			AttemptToFireCannons (transform.right);
		}

		private void OnEnable()
		{
			ShipControlsReader.FireLeftTriggered.Add (OnFireLeft);
			ShipControlsReader.FireRightTriggered.Add (OnFireRight);
		}

		private void OnDisable()
		{
			ShipControlsReader.FireLeftTriggered.Remove (OnFireLeft);
			ShipControlsReader.FireRightTriggered.Remove (OnFireRight);
		}
    }
}