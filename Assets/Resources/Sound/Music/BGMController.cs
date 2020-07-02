
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public enum Music
    {
       Cave = 0,
       Castle = 1,
       Explore = 2,
       Ocean = 3,
       Town = 4,
    }

    public static BGMController Instance { get; set; }
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlaySound(Music music)
    {
        int value = (int)music;
        audioSource.clip = audioClips[value];
        audioSource.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            audioSource.Play();
        }
    }
}
