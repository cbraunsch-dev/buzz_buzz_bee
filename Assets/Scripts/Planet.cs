using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public GameObject angryBeePrefab;

    public string FlowerColorCurrentlyBeingPollinated
    {
        get
        {
            return currentColorToPollinate;
        }
    }

    public GameMode CurrentGameMode
    {
        get
        {
            return this.gameMode;
        }
    }

    private List<GameObject> allFlowers = new List<GameObject>();
    private List<GameObject> redFlowers = new List<GameObject>();
    private List<GameObject> greenFlowers = new List<GameObject>();
    private List<GameObject> blueFlowers = new List<GameObject>();
    private List<GameObject> yellowFlowers = new List<GameObject>();
    private int numberOfNewFlowersThatShouldGrow = 3;
    private int indexOfNextFlowerToGrow = 0;

    private int numberOfPollinatedRedFlowers = 0;
    private int numberOfPollinatedGreenFlowers = 0;
    private int numberOfPollinatedBlueFlowers = 0;
    private int numberOfPollinatedYellowFlowers = 0;

    private string currentColorToPollinate = Tags.RedFlower;

    private Hud hud;
    private bool hudUpdatedWithInitialValues = false;

    private int nrToPollinateBeforeSpawnNewBee = 5;
    private int spawnBeeFrequencyFactor = 1;    // The amount by which nrToPollinateBeforeSpawnNewBee changes when a flower gets pollinated
    private int nrPollinatedSinceLastBeeSpawned = 0;

    private GameMode gameMode = GameMode.TimeTrial;

    // Stuff for time-trial
    private float totalPenaltyInSeconds = 0f;

    // Start is called before the first frame update
    void Start()
    {
        hud = GameObject.FindGameObjectWithTag(Tags.HUD).GetComponent<Hud>();

        // This order here is important when determining which set of flowers to pollinate next (see FlowerWasPollinated())
        redFlowers = new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.RedFlower));
        redFlowers.ForEach(flower =>
        {
            allFlowers.Add(flower);
        });
        greenFlowers = new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.GreenFlower));
        greenFlowers.ForEach(flower =>
        {
            allFlowers.Add(flower);
        });
        blueFlowers = new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.BlueFlower));
        blueFlowers.ForEach(flower =>
        {
            allFlowers.Add(flower);
        });
        yellowFlowers = new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.YellowFlower));
        yellowFlowers.ForEach(flower =>
        {
            allFlowers.Add(flower);
        });

        GrowNextBatchOfFlowers(numberOfNewFlowersThatShouldGrow);
        numberOfNewFlowersThatShouldGrow++;

        // Spawn a bee to kick things off in an interesting fashion
        SpawnAngryBee();
    }

    private void GrowNextBatchOfFlowers(int numberOfFlowersToGrow)
    {
        int numberOfFlowersThatJustGrew = 0;
        while (numberOfFlowersThatJustGrew < numberOfFlowersToGrow)
        {
            if (indexOfNextFlowerToGrow < allFlowers.Count)
            {
                var nextFlowerToGrow = allFlowers[indexOfNextFlowerToGrow];
                nextFlowerToGrow.GetComponent<Animator>().SetTrigger(Triggers.StartGrowing); // It is kinda dangerous to trigger the state machine from the outside but I currently can't think of a better way to do this
                indexOfNextFlowerToGrow++;
                numberOfFlowersThatJustGrew++;
            }
            else
            {
                break;
            }
        }
    }

    internal void AddPenaltyTime()
    {
        totalPenaltyInSeconds += 3f;    //Add a penalty of 3 seconds
        float minutes = (int)(totalPenaltyInSeconds / 60f);
        float seconds = (int)(totalPenaltyInSeconds % 60f);

        // No idea why we have to re-obtain the HUd here and why we can't use the global variable
        var theHud = GameObject.FindGameObjectWithTag(Tags.HUD).GetComponent<Hud>();
        theHud.UpdatePenalty(minutes, seconds);
    }

    // Update is called once per frame
    void Update()
    {
        if(!hudUpdatedWithInitialValues)
        {
            hudUpdatedWithInitialValues = true;
            hud.UpdatePercentPollinated(0);
            hud.UpdateTime(0, 0);
            hud.UpdateColorToPollinate(currentColorToPollinate);
            if(gameMode == GameMode.TimeTrial)
            {
                hud.PrepareUIForTimeTrial();
            }
            else if(gameMode == GameMode.Survival)
            {
                hud.PrepareUIForSurvival();
            }
        }

        // Keep track of time and show it in UI
        float minutes = (int)(Time.time / 60f);
        float seconds = (int)(Time.time % 60f);
        hud.UpdateTime(minutes, seconds);
    }

    private void PlayerShouldPollinateNewColor(string color)
    {
        // Update UI
        Debug.Log("Start pollinating new color: " + currentColorToPollinate);
    }

    public void FlowerWasPollinated(string tagOfFlower)
    {
        if (tagOfFlower == Tags.RedFlower)
        {
            numberOfPollinatedRedFlowers++;
            GrowMoreFlowers();
        }
        else if (tagOfFlower == Tags.GreenFlower)
        {
            numberOfPollinatedGreenFlowers++;
            GrowMoreFlowers();
        }
        else if (tagOfFlower == Tags.BlueFlower)
        {
            numberOfPollinatedBlueFlowers++;
            GrowMoreFlowers();
        }
        else if (tagOfFlower == Tags.YellowFlower)
        {
            numberOfPollinatedYellowFlowers++;
            GrowMoreFlowers();
        }

        // NB: The order in which to pollinate the flowers has to match up with the way the flowers are added
        // to the main list of flowers in the Start() method. This is pretty fragile and gross but time-constraints
        // make this the easier choice than trying to figure out some algorithm. Gotta submit this to the game jam.
        if (currentColorToPollinate == Tags.RedFlower && numberOfPollinatedRedFlowers == redFlowers.Count)
        {
            // Time to pollinate next set of flowers
            currentColorToPollinate = Tags.GreenFlower;
            PlayerShouldPollinateNewColor(currentColorToPollinate);
            hud.UpdateColorToPollinate(currentColorToPollinate);
        }
        else if (currentColorToPollinate == Tags.GreenFlower && numberOfPollinatedGreenFlowers == greenFlowers.Count)
        {
            // Time to pollinate next set of flowers
            currentColorToPollinate = Tags.BlueFlower;
            PlayerShouldPollinateNewColor(currentColorToPollinate);
            hud.UpdateColorToPollinate(currentColorToPollinate);
        }
        else if (currentColorToPollinate == Tags.BlueFlower && numberOfPollinatedBlueFlowers == blueFlowers.Count)
        {
            // Time to pollinate next set of flowers
            currentColorToPollinate = Tags.YellowFlower;
            PlayerShouldPollinateNewColor(currentColorToPollinate);
            hud.UpdateColorToPollinate(currentColorToPollinate);
        }

        float totalNrOfPollinatedFlowers = numberOfPollinatedRedFlowers + numberOfPollinatedGreenFlowers + numberOfPollinatedBlueFlowers + numberOfPollinatedYellowFlowers;
        float percentagePollinated = totalNrOfPollinatedFlowers / allFlowers.Count * 100;
        hud.UpdatePercentPollinated((int)percentagePollinated);

        // See if it's time to spawn a new angry bee
        SpawnNewAngryBeeIfNecessary();
    }

    private void SpawnNewAngryBeeIfNecessary()
    {
        nrPollinatedSinceLastBeeSpawned++;
        if (nrPollinatedSinceLastBeeSpawned >= nrToPollinateBeforeSpawnNewBee)
        {
            // Each time it's time to spawn a new bee, we increase the frequency by which angry bees are spawned
            SpawnAngryBee();
            nrPollinatedSinceLastBeeSpawned = 0;
            nrToPollinateBeforeSpawnNewBee -= spawnBeeFrequencyFactor;
            if (nrToPollinateBeforeSpawnNewBee < 1)
            {
                nrToPollinateBeforeSpawnNewBee = 1;
            }
        }
    }

    private void GrowMoreFlowers()
    {
        GrowNextBatchOfFlowers(numberOfNewFlowersThatShouldGrow);

        // Increase the number of flowers that should grow faster and faster the more have already grown.
        // Let's say numberOfNewFlowersThatShouldGrow starts off at 1. This is what it would look like if we incremented it like this a few times. We'll call
        // the value n to keep it shorter:
        // - n += 1 + (1 / 2) -> n = 2
        // - n += 1 + (2 / 2) -> n = 4
        // - n += 1 + (4 / 2) -> n = 7
        numberOfNewFlowersThatShouldGrow += 1 + (numberOfNewFlowersThatShouldGrow / 2);
    }

    public void SpawnAngryBee()
    {
        var angryBee = Instantiate(angryBeePrefab);
        angryBee.GetComponent<AngryBee>().targetBee = GameObject.FindGameObjectWithTag(Tags.Player);
    }

    public float Radius
    {
        get
        {
            return GetComponent<SphereCollider>().radius * transform.localScale.x;
        }
    }

    public List<GameObject> Flowers
    {
        get
        {
            return this.allFlowers;
        }
    }

    public enum GameMode
    {
        TimeTrial,
        Survival
    }
}
