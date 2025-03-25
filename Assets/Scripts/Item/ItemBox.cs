using UnityEngine;

public class ItemBox : MonoBehaviour, IHealth
{
    [SerializeField] GameObject obj;
    int health = 1;

    private void OnEnable() => UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
    private void OnDisable() => UpdateManager.Instance.UnSubscribeUpdate(UpdateMethod);
    private void UpdateMethod()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
            Instantiate(obj);
        }
    }
    public void Heal(int amount)
    {
        return;
    }

    public void TakeDamage(int damage, int force, GameObject obj)
    {
        health -= damage;
    }
}