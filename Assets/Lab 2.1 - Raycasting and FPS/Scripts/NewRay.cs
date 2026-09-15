using UnityEngine;

public class NewRay : MonoBehaviour
{
	private void Update()
	{
		if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
		{
			CharacterHP character = hit.transform.GetComponent<CharacterHP>();
			if (character != null)
			{
				if(hit.distance <= 5)
				{
					Debug.DrawLine(transform.position, hit.point, Color.red);
					Debug.DrawLine(hit.point, hit.point + hit.normal, Color.magenta);
				}
			}
		}
	}
}