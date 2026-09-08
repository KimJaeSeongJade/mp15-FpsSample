using UnityEngine;

public interface IDamageable
{
    public GameObject GameObject { get; }

    public void TakeDamage(int damage);

    public void Knockback(Vector3 knockback)
    {
        GameObject.GetComponent<Rigidbody>()?.AddForce(knockback, ForceMode.Impulse);
    }
}
