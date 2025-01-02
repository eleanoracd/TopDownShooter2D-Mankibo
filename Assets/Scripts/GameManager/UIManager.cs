using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("TextMeshPro")]
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI waveTimerText;

    [Header("Bullet Icons")]
    [SerializeField] private Image basicBulletIcon;
    [SerializeField] private Image laserBulletIcon;
    [SerializeField] private Image explosiveBulletIcon;
    [SerializeField] private Image poisonBulletIcon;

    [Header("Bullet Colors")]
    [SerializeField] private Color selectedColor = Color.white;
    [SerializeField] private Color unselectedColor = new Color(1, 1, 1, 0.5f); // Semi-transparent white

    private PlayerLogic playerLogic;
    private SpawnManager spawnManager;
    private BulletManager bulletManager;

    private void Start()
    {
        playerLogic = FindObjectOfType<PlayerLogic>();
        spawnManager = FindObjectOfType<SpawnManager>();
        bulletManager = FindObjectOfType<BulletManager>();

        if (playerLogic == null || spawnManager == null || bulletManager == null)
        {
            Debug.LogError("PlayerLogic, SpawnManager, or BulletManager not found in the scene!");
        }

        InitializeBulletIcons();
    }

    private void Update()
    {
        UpdatePlayerHealth();
        UpdateWaveTimer();
        UpdateBulletSelection();
    }

    private void UpdatePlayerHealth()
    {
        if (playerLogic != null)
        {
            float currentHealth = playerLogic.GetCurrentHealth();
            playerHealthText.text = $"Health: {currentHealth:0}";
        }
    }

    private void UpdateWaveTimer()
    {
        if (spawnManager != null)
        {
            float timeRemaining = Mathf.Max(0, spawnManager.GetWaveTimeRemaining());
            waveTimerText.text = $"Wave Ends In: {timeRemaining:0.0}s";
        }
    }

    private void InitializeBulletIcons()
    {
        if (bulletManager == null)
        {
            Debug.LogError("BulletManager not found!");
            return;
        }

        SetBulletIcon(basicBulletIcon, BulletType.Basic);
        SetBulletIcon(laserBulletIcon, BulletType.Laser);
        SetBulletIcon(explosiveBulletIcon, BulletType.Explosive);
        SetBulletIcon(poisonBulletIcon, BulletType.Poison);
    }

    private void SetBulletIcon(Image icon, BulletType bulletType)
    {
        if (bulletManager == null || icon == null) return;

        Sprite bulletSprite = bulletManager.GetBulletSprite(bulletType);
        if (bulletSprite != null)
        {
            icon.sprite = bulletSprite;
        }
        else
        {
            Debug.LogWarning($"No sprite found for {bulletType} bullet! Icon will remain blank.");
            icon.sprite = null; // Optional: Assign a placeholder sprite if desired
        }
    }

    private void UpdateBulletSelection()
    {
        if (bulletManager == null) return;

        BulletType currentBulletType = bulletManager.GetCurrentBulletType();

        basicBulletIcon.color = currentBulletType == BulletType.Basic ? selectedColor : unselectedColor;
        laserBulletIcon.color = currentBulletType == BulletType.Laser ? selectedColor : unselectedColor;
        explosiveBulletIcon.color = currentBulletType == BulletType.Explosive ? selectedColor : unselectedColor;
        poisonBulletIcon.color = currentBulletType == BulletType.Poison ? selectedColor : unselectedColor;
    }
}