using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class SimonSaysManager : MonoBehaviour
{
    #region Simon Says
    [Header("Simon Says")]

    [SerializeField] int neededWins = 3;
    int wins = 0;

    [SerializeField] int simonSequenceTotal = 5;
    [SerializeField] string[] color = { "Red", "Green", "Blue", "Yellow" };

    public List<string> activeSimonSequence;
    [SerializeField] List<string> simonSequence;


    [SerializeField] List<SimonSaysElement> simonElements;

    public bool isShowing = false;

    AudioSource audioSource;
    [SerializeField] AudioClip failSound;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        foreach (SimonSaysElement element in FindObjectsOfType<SimonSaysElement>())
        {
            simonElements.Add(element);
        }
        RandomizeSimonSays();
        audioSource = GetComponent<AudioSource>();

        ShowSimonSequence();
    }

    /// <summary>
    /// Re-Randomizes the list simonSequence
    /// </summary>
    void RandomizeSimonSays()
    {
        simonSequence.Clear();
        for (int i = 0; i < simonSequenceTotal + wins; i++)
        {
            int randomNumber = UnityEngine.Random.Range(0, color.Length);
            simonSequence.Add(color[randomNumber]);
        }
    }
    
    public void SimonSaysAdd(string colorType)
    {
        if (activeSimonSequence.Count < simonSequence.Count)
        {
            activeSimonSequence.Add(colorType);
        }

        // Checks if both lists are the same
        if (activeSimonSequence.SequenceEqual<string>(simonSequence) == true)
        {
            // Checks if the needed wins is lower than the needed amount
            if (wins < neededWins)
            {
                wins++;
            }

            // Checks if needed amount of wins is reached
            // If it has not been reached it resets the list and starts the next round
            // If it has been reached it ends the game
            if (wins != neededWins)
            {
                activeSimonSequence.Clear();
                RandomizeSimonSays();
                ShowSimonSequence();
            }
            else
            {

            }
        }
        // If the active list is the same size as the random list but not the same it resets it and restarts the same stage
        else if (activeSimonSequence.Count == simonSequence.Count)
        {
            activeSimonSequence.Clear();
            audioSource.clip = failSound;
            audioSource.Play();

            RandomizeSimonSays();
            ShowSimonSequence();
        }
    }

    public void ShowSimonSequence()
    {
        if (isShowing == false)
        {
            isShowing = true;
            activeSimonSequence.Clear();
            StartCoroutine(SimonDelay());
        }
    }

    IEnumerator SimonDelay()
    {
        foreach (string sequence in simonSequence)
        {
            foreach (SimonSaysElement element in simonElements)
            {
                if (element.colorType == sequence)
                {
                    yield return new WaitForSeconds(1f);
                    element.PlaySound();
                }
            }
        }
        isShowing = false;
    }
}
