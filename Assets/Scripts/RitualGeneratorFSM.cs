using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RitualGeneratorFSM
{
	#region Control Constants
	// Tweak these values to control the generation of rituals
	const int summonRitualCount = 5;
	const int summonRitualMinInteractions = 2;
	const int summonRitualMaxInteractions = 3;
	const int dudRitualCount = 0;
	const int dudRitualMinInteractions = 2;
	const int dudRitualMaxInteractions = 6;
	const int objectCount = 6;
	const int objectInteractionCount = 4;
	#endregion

	#region Current status variables
	// When a ritual is completed, one of these objects is created and placed in the completedRitualMessages list
	public class RitualResult
	{
		public bool isDud;
		public int ritualNumber; // 0..summonRitualCount-1 || 0..dudRitualCount-1
	}

    // Contains completed rituals
    public int lastCompletedRitual = -1;
	public List<RitualResult> completedRitualMessages = new List<RitualResult>();

	public class CurrentRitualProgress
	{
		public bool isDud = true;
		public int ritualNumber; // 0..summonRitualCount-1 || 0..dudRitualCount-1
		public int currentSteps;
		public int requiredSteps;
	}

	// current progress towards ritual
	public CurrentRitualProgress currentRitualProgress = new CurrentRitualProgress();
	#endregion

	#region Private variables

	private class Ritual
	{
		public bool isDud;
		public int ritualNumber; // 0..summonRitualCount-1 || 0..dudRitualCount-1
		public List<int> objects = new List<int>();
		public List<int> interactions = new List<int>();
	}

	private List<Ritual> rituals = new List<Ritual>();

	private int currentInternalRitualIndex = -1;
	#endregion

	#region Public Interface
	public void InitializeRituals(int randomSeed)
	{
        completedRitualMessages.Clear();
        lastCompletedRitual = -1;

        currentRitualProgress.isDud = true;
        currentRitualProgress.ritualNumber = -1;
        currentRitualProgress.currentSteps = -1;
        currentRitualProgress.currentSteps = -1;

		System.Random rng = new System.Random(randomSeed);

		for (int i = 0; i < summonRitualCount + dudRitualCount; ++i)
		{
			Ritual currentRitual = new Ritual();
			int ritualLength = 0;
			// Is this a summon ritual, or a dud ritual?
			if (i < summonRitualCount)
			{
				currentRitual.isDud = false;
				currentRitual.ritualNumber = i;
				// Generate a length of ritual;
				ritualLength = rng.Next(summonRitualMinInteractions, summonRitualMaxInteractions);
			}
			else
			{
				currentRitual.isDud = true;
				currentRitual.ritualNumber = i - summonRitualCount;
				// Generate a length of ritual;
				ritualLength = rng.Next(dudRitualMinInteractions, dudRitualMaxInteractions);
			}
			for (int ritualInteractionIndex = 0; ritualInteractionIndex < ritualLength; ++ritualInteractionIndex)
			{
				// generate a random object interaction, but
				// Make sure this doesn't start another ritual
				int objectToInteractWith = 0;
				int objectInteraction = 0;

				bool interactionConflicts = false;
				do
				{
                    interactionConflicts = false;

					objectToInteractWith = rng.Next(0, objectCount);
					objectInteraction = rng.Next(0, objectInteractionCount);				
					
					for (int j = 0; j < rituals.Count; ++j)
					{
						if (rituals[j].objects[0] == objectToInteractWith && rituals[j].interactions[0] == objectInteraction)
						{
							interactionConflicts = true;
							break;
						}
					}
				}
				while (interactionConflicts);

				currentRitual.objects.Add(objectToInteractWith);
				currentRitual.interactions.Add(objectInteraction);
			}
            rituals.Add(currentRitual);
		}
	}

	/// <summary>
	/// Call this when an object is interacted to see what happens with the ritual
	/// </summary>
	/// <returns><c>true</c>, if a ritual is in progress or completed after the interaction, <c>false</c> otherwise.</returns>
	/// <param name="objectNumber">object number 0..objectCount-1</param>
	/// <param name="interactionNumber">interaction number 0..interactionCount-1</param>
	public bool InteractWithObject(int objectNumber, int interactionNumber)
	{
		// Check to see if this interaction is the next step in the current ritual
		if (currentInternalRitualIndex >= 0)
		{
			var currentRitual = rituals[currentInternalRitualIndex];
			if (objectNumber == currentRitual.objects[currentRitualProgress.currentSteps]
			    && interactionNumber == currentRitual.interactions[currentRitualProgress.currentSteps])
			{
				++currentRitualProgress.currentSteps;
				if (currentRitualProgress.currentSteps >= currentRitual.objects.Count)
				{
					// Ritual complete!
					RitualResult result = new RitualResult();
					result.isDud = currentRitual.isDud;
					result.ritualNumber = currentRitual.ritualNumber;
					completedRitualMessages.Add(result);

                    lastCompletedRitual = currentRitual.ritualNumber;

                    // start working on another
                    currentInternalRitualIndex = -1;
				}
				// We made progress, so return true
				return true;
			}
			else
			{
                return false;
				// Not the current ritual, cancel it
                // dont' cancel rituals anymore,  you gotta finish it
				//currentInternalRitualIndex = -1;
			}
		}

		// Find a ritual that starts with this interaction
		for (int i = 0; i < rituals.Count; ++i)
		{
			var currentRitual = rituals[i];
			if (objectNumber == currentRitual.objects[0]
			    && interactionNumber == currentRitual.interactions[0])
			{
				// it's this ritual!
				currentInternalRitualIndex = i;

				currentRitualProgress.isDud = currentRitual.isDud;
				currentRitualProgress.ritualNumber = currentRitual.ritualNumber;
				currentRitualProgress.currentSteps = 1;
				currentRitualProgress.requiredSteps = currentRitual.objects.Count;

				return true;
			}
		}                        

		currentRitualProgress.isDud = true;
		currentRitualProgress.ritualNumber = -1;
		currentRitualProgress.currentSteps = -1;
		currentRitualProgress.requiredSteps = -1;

		return false;
	}
    #endregion

    #region Debugging assists

    #endregion
}
