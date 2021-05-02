using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController inst;

    [Header("Object References")]
    [Tooltip("Game Over Panel")]
    [SerializeField] GameObject restartPanel;
    [Tooltip("Stats Panel")]
    [SerializeField] GameObject statsPanel;
    [Tooltip("Place holder for the health")]
    [SerializeField] TMP_Text playerHealth;
    [Tooltip("Place holder for the max health")]
    [SerializeField] TMP_Text playerMaxHealth;

    Vector3 initialPosition = new Vector3(-30f, 7.2f, -6.7f);

    // Start is called before the first frame update
    void Start()
    {

        if (inst != null && inst != this)
            Destroy(this.gameObject);
        else
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchStatsPanel();
        }
    }
    
    public void PauseGame()
    {

    }

    public void SwitchStatsPanel()
    {
        statsPanel.SetActive(!statsPanel.activeInHierarchy);
    }

    public void GameOver()
    {
        restartPanel.SetActive(true);
    }

    public void Restart()
    {
        restartPanel.SetActive(false);
        PlayerBehaviour.inst.ResetPlayerHealth();
        PlayerBehaviour.inst.SetStartPoint(initialPosition);
        SceneManager.LoadScene(1);
    }

    public void SetPlayerHP(int health, int maxHealth)
    {
        playerHealth.text = health.ToString();
        playerMaxHealth.text = maxHealth.ToString();
    }

}
