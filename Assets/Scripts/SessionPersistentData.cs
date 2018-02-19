public static class SessionPersistentData {
	//Data in this class persists between scenes.

	private static string lastScene;

	public static string LastScene {
		get {
			return lastScene;
		}
		set {
			lastScene = value;
		}
	}
}
