using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance;
    public static bool isRestarting = false;

    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject pausePanel;

    private GameState currentState = GameState.notRunning;
    public bool IsPaused => currentState == GameState.pause;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (isRestarting)
        {
            ShowGamePanel();
            currentState = GameState.running;
            isRestarting = false;
        }
        else
        {
            ShowStartPanel();
            currentState = GameState.notRunning;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.pause)
            {
                Resume();
            }
            else if (currentState == GameState.running)
            {
                Pause();
            }
        }
    }

    public void Play()
    {
        Time.timeScale = 1f;
        ShowGamePanel();
        currentState = GameState.running;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        currentState = GameState.pause;
        ShowPausePanel();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        currentState = GameState.running;
        ShowGamePanel();
    }

    public void RestartGame()
    {
        isRestarting = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void ShowStartPanel()
    {
        Time.timeScale = 0f;
        PanelHandle(false, false, true, false);
    }

    public void ShowPausePanel()
    {
        PanelHandle(false, false, false, true);
    }

    public void ShowGamePanel()
    {
        Time.timeScale = 1f;
        PanelHandle(false, true, false, false);
    }

    public void ShowGameOverPanel()
    {
        PanelHandle(true, false, false, false);
    }

    private void PanelHandle(bool isGameOver, bool isGamePanel, bool isStartPanel, bool isPausePanel)
    {
        gameOver.SetActive(isGameOver);
        gamePanel.SetActive(isGamePanel);
        startPanel.SetActive(isStartPanel);
        pausePanel.SetActive(isPausePanel);
    }

    public enum GameState
    {
        notRunning,
        running,
        pause
    }
}