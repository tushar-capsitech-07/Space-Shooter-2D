using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance;

    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOver;

    public gameState currentState = gameState.notRunning;
    public bool isRunning => currentState == gameState.running;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        showStart();
        currentState = gameState.notRunning;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == gameState.pause)
                resume();
            else
                paus();
        }
    }

    public void paus()
    {
        Time.timeScale = 0f;
        currentState = gameState.pause;
        showPause();
    }

    public void resume()
    {
        Time.timeScale = 1f;
        currentState = gameState.running;
        showGame();
    }

    public void play()
    {
          Time.timeScale = 0f;
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

    public void showStart()
    {

        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void showPause()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void showGame()
    {
        currentState = gameState.notRunning;

        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
    }
    public void showGameOver()
    {
        gameOver.SetActive(true);
        gamePanel.SetActive(false);
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Application quitting...");
    }

    public enum gameState
    {
        notRunning,
        running,
        pause
    }
}
