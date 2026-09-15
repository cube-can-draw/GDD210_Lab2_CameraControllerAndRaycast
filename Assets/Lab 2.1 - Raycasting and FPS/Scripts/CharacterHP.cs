using UnityEngine;

public class CharacterHP : MonoBehaviour
{
	public int Health;

	public void TakeDamage(int damage)
	{
		Health -= damage;
		if (Health <= 0)
		{
			Destroy(gameObject);
		}
	}
}
