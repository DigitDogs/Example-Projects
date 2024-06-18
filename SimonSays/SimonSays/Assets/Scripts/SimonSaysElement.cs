using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SimonSaysElement : MonoBehaviour
{
    [Header("SimonSays Element")]
    public string colorType;

    [SerializeField] AudioClip clip;
    [SerializeField] AudioSource audioSource;

    SimonSaysManager simonSaysManager;

    [SerializeField] Image image;
    Color ogColor;

    // Start is called before the first frame update
    void Start()
    {
        simonSaysManager = FindObjectOfType<SimonSaysManager>();
        image = GetComponent<Image>();
        ogColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SendColor()
    {
        if(simonSaysManager.isShowing == false){
            PlaySound();
            simonSaysManager.SimonSaysAdd(colorType);
        }
    }
    public void PlaySound()
    {
        StartCoroutine(ResetColor());
        if (audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    IEnumerator ResetColor()
    {
        image.color = Color.white;
        yield return new WaitForSeconds(.8f);
        image.color = ogColor;
    }
}
