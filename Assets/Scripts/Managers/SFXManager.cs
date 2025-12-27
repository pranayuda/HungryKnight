using UnityEngine;

// Manages sound effects in the game
public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip captureClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayCaptureSound()
    {
        audioSource.PlayOneShot(captureClip);
    }
}
