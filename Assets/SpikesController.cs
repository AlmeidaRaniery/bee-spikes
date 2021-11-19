using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : MonoBehaviour
{
    public int initialDifficulty; // The ammount of spikes to be raised whenever the player scores a point.
    public int maxDifficulty; // The maximum ammount of spikes to be raised during the game.
    public int[] scoreThresholds; // Difficulty will increase by 1 once you go past these scores (in order)

    private List<Transform> spikes;
    private List<Transform> innactiveSpikes;
    private int currentDifficulty;

    private Vector3 startPos;
    private Vector3 startScale;
    private List<int> thresholds;
    // Start is called before the first frame update

    // Gets all the transforms of the spikes children to this gameObject's transform,
    // then creates and populates a list of all the spikes and one of all of the spikes that
    // haven't been activated.
    void Start()
    {
        spikes = new List<Transform>();
        innactiveSpikes = new List<Transform>();
        thresholds = new List<int>(scoreThresholds); // Creates a list from the array of threshHolds, for ease of manipulation.
        startPos = transform.position; // Assigns the initial position, so that the transform can be reset at the start of the Gameplay state.
        startScale = transform.localScale; // Assigns the initial scale, so that the transform can be reset at the start of the Gameplay state.
        Random.InitState((int) System.DateTime.Now.Ticks); //Sets the seed to the Random class to be the current time in Ticks.

        foreach (Transform child in transform)
        {
            if (child != transform)
            {
                spikes.Add(child);
                innactiveSpikes.Add(child);
            }
        }
    }

    //Called when gameplay begins. Resets active spikes and difficulty data in order to start again.
    public void StartSpikes()
    {
       currentDifficulty = initialDifficulty;
       transform.position = startPos;
       transform.localScale = startScale;
       thresholds.Clear();
       thresholds.AddRange(scoreThresholds);

       UnsetSpikes();
       RandomizeSpikes(initialDifficulty, innactiveSpikes);
    }

    // Callback for the player's onWallHit event, moves the spikes to the opposite direction and
    // receives the current score in order to compare with the difficulty thresholds
    public void OnWallHit(int score)
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        transform.position = -transform.position;
        
        UnsetSpikes();
        RandomizeSpikes(currentDifficulty, innactiveSpikes);

        if (thresholds.Count > 0 && score >= thresholds[0])
            IncreaseDifficulty();
    }

    //Resets all spikes to their initial state.
    public void UnsetSpikes()
    {
        foreach (Transform spike in spikes)
        {
            if (spike.gameObject.activeInHierarchy)
            {
              spike.gameObject.SetActive(false);
              innactiveSpikes.Add(spike);
            }
        }
    }
    
    // Takes the amount of spikes to raise and a list of all the spikes tha haven't been raised yet,
    // then loops through spikeCount, getting a random element form the spikePool list each time and
    // activating it, then removes them from the list. 
    void RandomizeSpikes(int spikeCount, List<Transform> spikePool)
    {
        for (int i = 0; i < spikeCount; i++)
        {
            int randomIndex = Random.Range(0, spikePool.Count);
            spikePool[randomIndex].gameObject.SetActive(true);
            spikePool.RemoveAt(randomIndex);
        }
    }

    void IncreaseDifficulty()
    {
        if (currentDifficulty < maxDifficulty)
            currentDifficulty++;
        
        if (thresholds.Count > 0)
            thresholds.RemoveAt(0);
    }

}
