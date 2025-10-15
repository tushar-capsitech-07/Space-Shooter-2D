using UnityEngine;

using UnityEngine.SceneManagement;
public class PanelManager : MonoBehaviour

{

    public static PanelManager Instance; // Single-tone
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOver;

    private GameState currentState = GameState.notRunning;
    public bool isRunning => currentState == GameState.pause;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ShowStartPanel();
        currentState = GameState.notRunning;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.pause)
                Resume();
            else
                Pause();
        }
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

    public void Play()
    {
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game called!");
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

#else
 
                            Application.Quit();
 
#endif

    }

    public void ShowStartPanel()
    {
        Time.timeScale = 0f;
        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOver.SetActive(false);
    }

    public void ShowPausePanel()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(false);
        pausePanel.SetActive(true);
        gameOver.SetActive(false);
    }

    public void ShowGamePanel()
    {
        Time.timeScale = 1f;
        currentState = GameState.running;
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
        gameOver.SetActive(false);
    }

    public void ShowGameOverPanel()
    {
        Debug.Log("hello");
        gameOver.SetActive(true);
        gamePanel.SetActive(false);
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Application quitting...");
    }

    public enum GameState
    {
        notRunning,
        running,
        pause
    }

}