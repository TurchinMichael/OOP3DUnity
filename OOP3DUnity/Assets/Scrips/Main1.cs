using Helper;

public class Main1 : Singleton<Main1>
{
	protected Main1() { }

	[System.Serializable]
	public struct SceneDate
	{
		public SceneField MainMenu;
		public SceneField Game;
	}
	
	public SceneDate Scenes;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	public void InitGame()
	{
		// Add Controllers
	}
}

