using UnityEngine;
using UnityEngine.UI;

public class WebCamTextures : MonoBehaviour
{
    private RawImage _rawImage;
    private WebCamTexture _webCamTexture;

    void Start()
    {
            
        _rawImage = GetComponent<RawImage>();


        if (WebCamTexture.devices.Length > 0)
        {

            _webCamTexture = new WebCamTexture(WebCamTexture.devices[0].name);
            

            _rawImage.texture = _webCamTexture;

            _webCamTexture.Play();
        }
        else
        {
            Debug.LogError("Nu a fost găsită nicio cameră video!");
        }
    }
}