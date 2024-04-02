using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerUI : NetworkBehaviour
{
    [Header("UI")]
    [SerializeField] private Text text;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Animator anim;

    public void AnimSetTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public void HealthTextChange(float health)
    {
        text.text = health.ToString() + " HP";
    }

    public void SetActiveLose(bool status)
    {
        losePanel.SetActive(status);
    }
}