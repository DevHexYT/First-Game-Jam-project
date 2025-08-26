using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public Texture2D map;

	public ColorToPrefab[] colorMappings;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		GenerateLevel();
	}

	void GenerateLevel() {
		for (int x = 0; x < map.width; x++) {
			for (int y = 0; y < map.height; y++) {
				GenerateTile(x,y);
			}
		}
	}

	void GenerateTile(int x, int y) {
		Color pixelColor = map.GetPixel(x, y);

		if(pixelColor.a == 0) {
			return;	
		}

		foreach(ColorToPrefab colorMapping in colorMappings) {
			if (colorMapping.color.Equals(pixelColor)) {
				Vector2 pos = new Vector2(x * 1.5f, y * 1.5f);
				GameObject newPrefab = Instantiate(colorMapping.prefab, pos, Quaternion.identity, transform);
				newPrefab.name = colorMapping.prefab.name;
			}
		}
	}

}
