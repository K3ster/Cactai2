using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnChanceSettingsUI : MonoBehaviour
{
    public Slider normalSlider;
    public Slider iceSlider;
    public Slider spikeSlider;
    public Slider enemySlider;
    public Slider nextSpawnX; // Dodatkowy np. na monetê itp.
    public Slider Move_Speed; // Dodatkowy np. na monetê itp.
    public Slider Jump_Power; // Dodatkowy np. na monetê itp.

    public Button saveButton;

    public CameraFollow spawnManager;
    public PlayerController playerController;
    public GameObject settingsPanel;

    private void Start()
    {
        LoadSettings();
        AddListeners();
    }

    void AddListeners()
    {
        normalSlider.onValueChanged.AddListener(delegate { UpdatePreview(); });
        iceSlider.onValueChanged.AddListener(delegate { UpdatePreview(); });
        spikeSlider.onValueChanged.AddListener(delegate { UpdatePreview(); });
        enemySlider.onValueChanged.AddListener(delegate { UpdatePreview(); });
        nextSpawnX.onValueChanged.AddListener(delegate { UpdatePreview(); });
        Move_Speed.onValueChanged.AddListener (delegate { UpdatePreview(); });
        Jump_Power.onValueChanged.AddListener(delegate { UpdatePreview(); });

        saveButton.onClick.AddListener(SaveSettings);
    }

    void LoadSettings()
    {
        normalSlider.value = PlayerPrefs.GetFloat("normalChance", 0.25f);
        iceSlider.value = PlayerPrefs.GetFloat("iceChance", 0.25f);
        spikeSlider.value = PlayerPrefs.GetFloat("spikeChance", 0.25f);
        enemySlider.value = PlayerPrefs.GetFloat("enemyChance", 0.25f);
        nextSpawnX.value = PlayerPrefs.GetFloat("nextSpawnX", 0.25f);
        Move_Speed.value = PlayerPrefs.GetFloat("Move_Speed", 0.25f);
        Jump_Power.value = PlayerPrefs.GetFloat("Jump_Power", 0.25f);

        UpdatePreview(); // Ustaw te¿ wartoœci w CameraFollow
    }

    void SaveSettings()
    {
        PlayerPrefs.SetFloat("normalChance", normalSlider.value);
        PlayerPrefs.SetFloat("iceChance", iceSlider.value);
        PlayerPrefs.SetFloat("spikeChance", spikeSlider.value);
        PlayerPrefs.SetFloat("enemyChance", enemySlider.value);
        PlayerPrefs.SetFloat("nextSpawnX", nextSpawnX.value);
        PlayerPrefs.SetFloat("Jump_Power", Jump_Power.value);
        PlayerPrefs.SetFloat("Move_Speed", Move_Speed.value);

        PlayerPrefs.Save();

        UpdatePreview();
        Debug.Log("Ustawienia zapisane!");
        settingsPanel.SetActive(false);

        
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdatePreview()
    {
        float total = TotalAssigned();

        if (total > 0f)
        {
            spawnManager.normalChance = normalSlider.value / total;
            spawnManager.iceChance = iceSlider.value / total;
            spawnManager.spikeChance = spikeSlider.value / total;
            spawnManager.enemyChance = enemySlider.value / total;
            spawnManager.nextSpawnX = nextSpawnX.value / total;
            playerController.moveSpeed = Move_Speed.value / total;
            playerController.jumpPower = Jump_Power.value / total;
        }
    }

    float TotalAssigned()
    {
        return normalSlider.value + iceSlider.value + spikeSlider.value + enemySlider.value + nextSpawnX.value;
    }


    public void ShowGameOver()
    {
        settingsPanel.SetActive(true);

        // Zatrzymaj grê
        Time.timeScale = 0f;
    }
    }
