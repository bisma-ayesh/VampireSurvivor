using UnityEngine;

public class UpgradeState : GameState
{
    private Player _player;
    private GameObject _upgradePanel; 
    private UpgradeButton _panel;
    private SpawnManager _spawnManager;
    private SpawnUpgradeData _spawnUpgradeData;
    public GameObject upgradePanelPrefab;

    public UpgradeState(GameStateManager gameStateManager, Player player, SpawnManager spawnManager, SpawnUpgradeData spawnUpgradeData) : base(gameStateManager)
    {
        this._player = player;
        this._spawnManager = spawnManager;
        this._spawnUpgradeData = spawnUpgradeData;
    }

    public override void EnterState()
    {
        Time.timeScale = 0; 
        _upgradePanel = Object.Instantiate(upgradePanelPrefab); 
        _panel = _upgradePanel.GetComponent<UpgradeButton>();

        
        HealthUpgrade healthUpgrade = Resources.Load<HealthUpgrade>("HealthUpgrade");
        SpeedUpgrade speedUpgrade = Resources.Load<SpeedUpgrade>("SpeedUpgrade");
        _spawnUpgradeData = Resources.Load<SpawnUpgradeData>("SpawnUpgradeData"); 

        
        _panel.healthUpgrade = healthUpgrade; 
        _panel.speedUpgrade = speedUpgrade; 
        _panel.Setup(OnHealthUpgradeSelected, OnSpeedUpgradeSelected, GameStateManager); 
    }

    public override void UpdateState()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnHealthUpgradeSelected(_panel.healthUpgrade);
            UpdateSpawnData();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            OnSpeedUpgradeSelected(_panel.speedUpgrade);
            UpdateSpawnData();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
          
            ExitToPlayingState();
        }
    }

    private void OnHealthUpgradeSelected(HealthUpgrade healthUpgrade)
    {
        healthUpgrade.ApplyUpgrade(_player);
        ExitToPlayingState(); 
    }

    private void OnSpeedUpgradeSelected(SpeedUpgrade speedUpgrade)
    {
        speedUpgrade.ApplyUpgrade(_player);
        ExitToPlayingState();
    }

    private void UpdateSpawnData()
    {
       
        if (_spawnUpgradeData != null)
        {
            _spawnUpgradeData.UpdateSpawndData(_spawnManager);
        }
    }

    public override void ExitState()
    {
        
        if (_upgradePanel != null)
        {
            Object.Destroy(_upgradePanel);
        }
    }

    private void ExitToPlayingState()
    {
        ExitState(); 
        Time.timeScale = 1;
        GameStateManager.ChangeState(GameStateManager.playingState);
    }
}
