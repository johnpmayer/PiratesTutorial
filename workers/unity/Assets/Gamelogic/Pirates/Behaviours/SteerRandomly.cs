using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Ship;

namespace Assets.Gamelogic.Pirates.Behaviours
{
	// this only runs server-side
	[WorkerType(WorkerPlatform.UnityWorker)]
	public class SteerRandomly : MonoBehaviour {

		// only enabled if the worker is allowed to have this component...
		[Require]
		// ...specifically, write access to the entity's ship controls component
		private ShipControls.Writer ShipControlsWriter;

		private void RandomizeSteering()
		{
			ShipControlsWriter.Send (new ShipControls.Update ()
				.SetTargetSpeed (Random.value)
				.SetTargetSteering ((Random.value * 30.0f) - 15.0f));
		}

		private void OnEnable()
		{
			InvokeRepeating ("RandomizeSteering", 0, 5.0f);
		}

		private void OnDisable()
		{
			CancelInvoke ("RandomizeSteering");
		}

	}
}

