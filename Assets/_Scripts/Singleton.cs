using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Instance { get; private set; }

	public bool dontDestroyOnLoad;

	protected void Awake()
	{
		if (Instance == null)
		{
			Instance = this as T;
		}
		else
		{
			Destroy(gameObject);
		}
		
		if (dontDestroyOnLoad && transform.parent == null) DontDestroyOnLoad(gameObject);
	}
}
