using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeBeforeEndTransition = 3f;
    [SerializeField] private float levelTextTime = 1f;
    [SerializeField] private ObjectFade objectFade;
    [SerializeField] private int nextSceneId = 0;
    [SerializeField] private int levelId = 0;
    [SerializeField] private float musicVolume = 0.5f;
    private FadeAudioSource audioFade;
    public static GameManager Instance;
    private Fader fader;
    private int enemyCount = 1;
    private bool gameEnded = false;
    private bool gameStarted = false;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        audioFade = FindObjectOfType<FadeAudioSource>();
        fader = FindObjectOfType<Fader>();
        StartCoroutine(fader.FadeIn(1));
        StartCoroutine(StartGame());
        StartCoroutine(audioFade.StartFade(1, musicVolume));
    }

    private IEnumerator StartGame()
    {
        yield return objectFade.FadeIn();
        yield return new WaitForSeconds(levelTextTime);
        yield return objectFade.FadeOut();
        enemyCount = FindObjectOfType<EnemySpawner>().StartSpawn();
        gameStarted = true;
    }

    public void EnemyDead()
    {
        enemyCount -= 1;
    }

    private void Update()
    {
        if (gameStarted && !gameEnded && enemyCount == 0)
        {
            StartCoroutine(EndGame());
            gameEnded = true;
            int score = ScoreManager.Instance.GetScore();
            if (PlayerPrefs.HasKey(levelId.ToString() + "score"))
            {
                if (score > PlayerPrefs.GetInt(levelId.ToString() + "score"))
                    PlayerPrefs.SetInt(levelId.ToString() + "score", score);
            }
            else
                PlayerPrefs.SetInt(levelId.ToString() + "score", score);
        }
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(timeBeforeEndTransition);
        StartCoroutine(audioFade.StartFade(1, 0));
        StartCoroutine(fader.TransitionToScene(nextSceneId));
    }

    public void PlayerDead()
    {
        if (gameEnded)
          return;
        gameEnded = true;
        StartCoroutine(audioFade.StartFade(1, 0));
        StartCoroutine(fader.TransitionToScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public int GetEnemyRemaining()
    {
        return enemyCount;
    }
}
