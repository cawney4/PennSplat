using UnityEngine;
using System.Collections;
//using UnityEditor;
using UnityEngine.UI;

public class ScreenShotHandler : MonoBehaviour{
	public Texture2D text;
	public int score;
	public Text scoreText;
    public GameObject player;

	// Use this for initialization
	void Start () {
		score = 0;
		text = null;
		scoreText.text = "0";
        player = (GameObject)FindObjectOfType(typeof(CharacterController));
	}

	public void calResult() {
		RenderTexture tmp = RenderTexture.GetTemporary (text.width,
			                    text.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
		Graphics.Blit (text, tmp);
		RenderTexture previous = RenderTexture.active;
		RenderTexture.active = tmp;
		Texture2D tmpTexture = new Texture2D (text.width, text.height);
		tmpTexture.ReadPixels (new Rect (0, 0, tmp.width, tmp.height), 0, 0);
		tmpTexture.Apply ();
		RenderTexture.active = previous;
		RenderTexture.ReleaseTemporary (tmp);


		int x = tmpTexture.width;
		int y = tmpTexture.height;

		int size = x * y;

		Color[] pix = new Color[size];
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				pix [i * y + j] = tmpTexture.GetPixel (i, j);
			}
		}
        Color color = player.GetComponent<Renderer>().material.color;
		for (int i = 0; i < pix.Length; i++) {
			if (Mathf.Approximately(pix[i].r, color.r) && Mathf.Approximately(pix[i].g, color.g) && Mathf.Approximately(pix[i].b, color.b)) {
				score = score + 1;
			}
		}

        scoreText.color = color;
        score = score / size * 100;
		scoreText.text = score.ToString ();
	}

	public void readTexture(string path) {
		if (text == null) {

			text = (Texture2D)Resources.Load (path);

			calResult ();
		}
	}
}
