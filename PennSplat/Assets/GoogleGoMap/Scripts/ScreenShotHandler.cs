using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

public class ScreenShotHandler : MonoBehaviour{
	public Texture2D text;
	public int score;
	public Text scoreText;


	// Use this for initialization
	void Start () {
		score = 0;
		text = null;
		scoreText.text = "0";
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

		for (int i = 0; i < pix.Length; i++) {
			if (pix[i].r > 0.99 && pix[i].g < 0.01 && pix[i].b < 0.01) {
				score = score + 1;
			}
		}

		scoreText.text = score.ToString ();
	}

	public void readTexture(string path) {
		if (text == null) {

			text = (Texture2D)Resources.Load (path);

			calResult ();
		}
	}
}
