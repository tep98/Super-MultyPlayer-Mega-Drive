using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Damageble : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private Text text;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Animator anim;

    private void Start()
    {
        text.text = _health.ToString() + "HP";
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        anim.SetTrigger("Damage");
        text.text = _health.ToString() + "HP";

        if (_health <= 0)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0;
            Invoke("Die", 1f);
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
