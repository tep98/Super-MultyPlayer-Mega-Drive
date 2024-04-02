using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;
using Mirror.Examples.Basic;

public class Damageble : NetworkBehaviour
{
    public event System.Action<float> OnPlayerHealthChanged;
    public event System.Action<bool> OnPlayerLoseChanged;
    public event System.Action<string> OnPlayerAnimChanged;

    [Header("Health")]
    [SerializeField] private float _health;

    [Header("Player UI")]
    [SerializeField] private GameObject playerUIPrefab;
    GameObject playerUIObject;
    PlayerUI playerUI = null;

    #region SyncVars
    [Header("SyncVars")]
    //Hooks для вызова методов изменений
    [SyncVar(hook = nameof(PlayerHealthChanged))]
    public float playerHealth = 50;

    [SyncVar(hook = nameof(PlayerLoseChanged))]
    public bool status = false;

    [SyncVar(hook = nameof(PlayerAnimChanged))]
    public string triggerName = "Damage";

    //Методы для вызова в Hook, которые находятся выше
    void PlayerHealthChanged(float _, float newPlayerHealth)
    {
        OnPlayerHealthChanged?.Invoke(newPlayerHealth);
    }
    void PlayerLoseChanged(bool _, bool newPlayerLose)
    {
        OnPlayerLoseChanged?.Invoke(newPlayerLose);
    }
    void PlayerAnimChanged(string _, string newPlayerAnim)
    {
        OnPlayerAnimChanged?.Invoke(newPlayerAnim);
    }
    #endregion


    #region Client

    public override void OnStartClient()
    {
        // Instantiate the player UI as child of the Players Panel
        playerUIObject = Instantiate(playerUIPrefab, CanvasUI.GetPlayersPanel());
        playerUIObject.SetActive(true);
        playerUI = playerUIObject.GetComponent<PlayerUI>();

        playerHealth = 50;
        syncDirection = SyncDirection.ClientToServer;

        // wire up all events to handlers in PlayerUI
        OnPlayerHealthChanged = playerUI.HealthTextChange;
        OnPlayerLoseChanged = playerUI.SetActiveLose;
        OnPlayerAnimChanged = playerUI.AnimSetTrigger;

        // Invoke all event handlers with the initial data from spawn payload
        OnPlayerHealthChanged.Invoke(playerHealth);
    }

    public override void OnStartLocalPlayer()
    {
        // Activate the main panel
        CanvasUI.SetActive(true);
        playerUIObject.SetActive(false);
    }


    private void UIChanger()
    {
        if (!isLocalPlayer)
        {
            playerUIObject.SetActive(false);
        }
        else
        {
            playerUIObject.SetActive(true);
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        playerHealth -= damage;
        OnPlayerAnimChanged.Invoke("Damage");
        OnPlayerHealthChanged.Invoke(playerHealth);

        if (_health <= 0)
        {
            OnPlayerHealthChanged.Invoke(0);
            OnPlayerLoseChanged.Invoke(true);
            Invoke("RpcReloadScene", 1f);
        }
    }

/*    public override void OnStopLocalPlayer()
    {
        // Disable the main panel for local player
        CanvasUI.SetActive(false);
    }*/
/*    public override void OnStopClient()
    {
        // disconnect event handlers
        OnPlayerHealthChanged = null;
        OnPlayerLoseChanged = null;
        OnPlayerAnimChanged = null;

        // Remove this player's UI object
        Destroy(playerUIObject);
    }*/
    #endregion

    [ClientRpc]
    private void RpcReloadScene()
    {
        NetworkManager.singleton.ServerChangeScene(SceneManager.GetActiveScene().name);
    }
}
