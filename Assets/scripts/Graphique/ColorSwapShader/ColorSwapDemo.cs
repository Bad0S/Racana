using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwapDemo : MonoBehaviour {
	Texture2D mColorSwapTex;
	Color[] mSpriteColors;

	SpriteRenderer mSpriteRenderer;

	float mHitEffectTimer = 0.0f;
	const float cHitEffectTime = 0.1f;

	public enum SwapIndex
	{
		Eye = 255,
		OrangeOutline = 233,
		OrangeOutlineSec = 213,
		OrangeOutlineTert = 187,
		SkinPrimary = 128,
		SkinLightened = 152,
		SkinDarkened = 99,
		SkinDarkenedSec = 76,
		SkinDarkenedTert = 54,

	}

	void Awake()
	{
		mSpriteRenderer = GetComponent<SpriteRenderer>();
		InitColorSwapTex();
		SwapDemoColors();
	}

	public void RandomSwapSkin()
	{
		int skin = Random.Range (0, 5);
		SwapColor(SwapIndex.Eye, ColorFromInt(0x784a00));
		SwapColor(SwapIndex.OrangeOutline, ColorFromInt(0x4c2d00));
		SwapColor(SwapIndex.OrangeOutlineSec, ColorFromInt(0xc4ce00));
		SwapColor(SwapIndex.OrangeOutlineTert, ColorFromInt(0x784a00));
		SwapColor(SwapIndex.SkinPrimary, ColorFromInt(0x594f00));
		SwapColor(SwapIndex.SkinLightened, ColorFromInt(0xffbd99));
		SwapColor(SwapIndex.SkinDarkened, ColorFromInt(0x7a3600));
		SwapColor(SwapIndex.SkinDarkenedSec, ColorFromInt(0xb9000b));
		SwapColor(SwapIndex.SkinDarkenedTert, ColorFromInt(0x51000b));

		mColorSwapTex.Apply();
		if (skin == 1)
		{

		}
		else if (skin == 1)
		{

			mColorSwapTex.Apply();
		}
		else if (skin == 1)
		{

			mColorSwapTex.Apply();
		}
		else if (skin == 1)
		{

		}
		else
		{

			mColorSwapTex.Apply();
		}
	}

	public void SwapDemoColors()
	{
		SwapColor(SwapIndex.Eye, ColorFromInt(0x784a00));
		SwapColor(SwapIndex.OrangeOutline, ColorFromInt(0x4c2d00));
		SwapColor(SwapIndex.OrangeOutlineSec, ColorFromInt(0xc4ce00));
		SwapColor(SwapIndex.OrangeOutlineTert, ColorFromInt(0x784a00));
		SwapColor(SwapIndex.SkinPrimary, ColorFromInt(0x594f00));
		SwapColor(SwapIndex.SkinLightened, ColorFromInt(0xffbd99));
		SwapColor(SwapIndex.SkinDarkened, ColorFromInt(0x7a3600));
		SwapColor(SwapIndex.SkinDarkenedSec, ColorFromInt(0xb9000b));
		SwapColor(SwapIndex.SkinDarkenedTert, ColorFromInt(0x51000b));

		mColorSwapTex.Apply();
		if (name.Contains("1"))
		{

		}
		else if (name.Contains("2"))
		{

			mColorSwapTex.Apply();
		}
		else if (name.Contains("3"))
		{

			mColorSwapTex.Apply();
		}
		else if (name.Contains("4"))
		{

		}
		else
		{

			mColorSwapTex.Apply();
		}
	}

	public void StartHitEffect()
	{
		mHitEffectTimer = cHitEffectTime;
		SwapAllSpritesColorsTemporarily(Color.white);
	}

	public static Color ColorFromInt(int c, float alpha = 1.0f)
	{
		int r = (c >> 16) & 0x000000FF;
		int g = (c >> 8) & 0x000000FF;
		int b = c & 0x000000FF;

		Color ret = ColorFromIntRGB(r, g, b);
		ret.a = alpha;

		return ret;
	}

	public static Color ColorFromIntRGB(int r, int g, int b)
	{
		return new Color((float)r / 255.0f, (float)g / 255.0f, (float)b / 255.0f, 1.0f);
	}

	public void InitColorSwapTex()
	{
		Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
		colorSwapTex.filterMode = FilterMode.Point;

		for (int i = 0; i < colorSwapTex.width; ++i)
			colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

		colorSwapTex.Apply();

		mSpriteRenderer.material.SetTexture("_SwapTex", colorSwapTex);

		mSpriteColors = new Color[colorSwapTex.width];
		mColorSwapTex = colorSwapTex;
	}

	public void SwapColor(SwapIndex index, Color color)
	{
		mSpriteColors[(int)index] = color;
		mColorSwapTex.SetPixel((int)index, 0, color);
	}


	public void SwapColors(List<SwapIndex> indexes, List<Color> colors)
	{
		for (int i = 0; i < indexes.Count; ++i)
		{
			mSpriteColors[(int)indexes[i]] = colors[i];
			mColorSwapTex.SetPixel((int)indexes[i], 0, colors[i]);
		}
		mColorSwapTex.Apply();
	}

	public void ClearColor(SwapIndex index)
	{
		Color c = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		mSpriteColors[(int)index] = c;
		mColorSwapTex.SetPixel((int)index, 0, c);
	}

	public void SwapAllSpritesColorsTemporarily(Color color)
	{
		for (int i = 0; i < mColorSwapTex.width; ++i)
			mColorSwapTex.SetPixel(i, 0, color);
		mColorSwapTex.Apply();
	}

	public void ResetAllSpritesColors()
	{
		for (int i = 0; i < mColorSwapTex.width; ++i)
			mColorSwapTex.SetPixel(i, 0, mSpriteColors[i]);
		mColorSwapTex.Apply();
	}

	public void ClearAllSpritesColors()
	{
		for (int i = 0; i < mColorSwapTex.width; ++i)
		{
			mColorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));
			mSpriteColors[i] = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		}
		mColorSwapTex.Apply();
	}

	void OnSetSkin0Color(Color color)
	{
		mColorSwapTex.Apply();
	}
		
	void Start(){

		mColorSwapTex.Apply();
	}

	public void Update()
	{
		if (mHitEffectTimer > 0.0f)
		{
			mHitEffectTimer -= Time.deltaTime;
			if (mHitEffectTimer <= 0.0f)
				ResetAllSpritesColors();
		}
	}
}
