using UnityEngine;
using Improbable.Entity.Component;
using Improbable.Ship;
using Improbable.Unity;
using Improbable.Unity.Visualizer;

namespace Assets.Gamelogic.Pirates.Behaviours
{
	[WorkerType(WorkerPlatform.UnityWorker)]
	public class TrackScore : MonoBehaviour {

		[Require] private Score.Writer ScoreWriter;

		private AwardResponse OnAwardPoints(AwardPoints request, ICommandCallerInfo callerInfo)
		{
			int newScore = ScoreWriter.Data.numberOfPoints + (int)request.amount;
			ScoreWriter.Send (new Score.Update ().SetNumberOfPoints (newScore));
			return new AwardResponse (request.amount);
		}

		void OnEnable()
		{
			ScoreWriter.CommandReceiver.OnAwardPoints.RegisterResponse (OnAwardPoints);
		}

		private void OnDisable()
		{
			ScoreWriter.CommandReceiver.OnAwardPoints.DeregisterResponse ();
		}

	}

}