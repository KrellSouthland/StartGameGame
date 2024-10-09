using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private float maxHp;// = 100;

    private void Start()
    {
        GameObject player = FindObjectOfType<PlayerMovement>().gameObject;
        playerHealth = player.GetComponent<Health>();
        currenthealthBar.fillAmount = 0;
        maxHp = playerHealth._maxHp;
        //totalhealthBar.fillAmount = playerHealth.currentHealth / maxHp;
    }
    private void Update()
    {
        currenthealthBar.fillAmount = Mathf.Lerp(currenthealthBar.fillAmount, playerHealth.currentHealth / maxHp, 0.07f);
    }
}