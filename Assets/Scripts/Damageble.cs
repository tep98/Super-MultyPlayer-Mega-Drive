using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;

public class Damageble : NetworkBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private Text text;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject _canvas;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            _canvas.SetActive(false);

        }
        else
        {
            _canvas.SetActive(true);
            text.text = _health.ToString() + "HP";
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        anim.SetTrigger("Damage");
        text.text = _health.ToString() + "HP";

        if (_health <= 0)
        {
            text.text = 0 + "HP";
            losePanel.SetActive(true);
            Invoke("RpcReloadScene", 1f);
        }
    }

    [ClientRpc]
    private void RpcReloadScene()
    {
        NetworkManager.singleton.ServerChangeScene(SceneManager.GetActiveScene().name);
    }
}
