using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    public AudioSource backgroundSong;
    public AudioClip[] songs;
    public AudioClip battleSong;

    private bool isBattle = false;
    private int currentSongIndex = 0;

    void Awake()
    {
        // Garante que só exista um AudioController na cena
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // opcional: mantém ao trocar de cena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentSongIndex = Random.Range(0, songs.Length);
        PlaySong(currentSongIndex);
    }

    void Update()
    {
        if (!backgroundSong.isPlaying && !isBattle)
        {
            PlayNextSong();
        }
    }

    public void SetBattleSong()
    {
        isBattle = true;
        backgroundSong.clip = battleSong;
        backgroundSong.loop = true;
        backgroundSong.Play();
    }

    public void RemoveBattleSong()
    {
        isBattle = false;
        PlaySong(currentSongIndex);
    }

    private void PlaySong(int index)
    {
        if (songs.Length == 0) return;

        currentSongIndex = index % songs.Length;
        backgroundSong.clip = songs[currentSongIndex];
        backgroundSong.loop = false;
        backgroundSong.Play();
    }

    private void PlayNextSong()
    {
        currentSongIndex = (currentSongIndex + 1) % songs.Length;
        PlaySong(currentSongIndex);
    }
}
