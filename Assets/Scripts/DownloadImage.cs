using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DownloadImage : MonoBehaviour
{
    
    void Start()
    {
        var url = "https://cdn.pixabay.com/photo/2017/09/07/10/44/logo-2724541_1280.png";
        StartCoroutine(ImageDownloader(url, (sprite =>
        {
            gameObject.GetComponent<Image>().sprite = sprite;
        })));
        
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator ImageDownloader(string imageUrl, Action<Sprite> onImageDownloaded = null)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D webTexture = DownloadHandlerTexture.GetContent(www);

            Sprite webSprite = SpriteFromTexture2D(webTexture);
            onImageDownloaded?.Invoke(webSprite);
        }
        else
        {
            Debug.Log(www.result);
        }
    }
    
    Sprite SpriteFromTexture2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f),
            100.0f);
    }
}
