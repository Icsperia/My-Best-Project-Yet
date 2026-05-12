using UnityEngine;

public class WebCamInScena : MonoBehaviour
{
    private WebCamTexture _webCamTexture;
    private Renderer _renderer;

    void Start()
    {

        _renderer = GetComponent<Renderer>();

        if (WebCamTexture.devices.Length > 0)
        {
            
            _webCamTexture = new WebCamTexture(WebCamTexture.devices[0].name);

            _renderer.material.mainTexture = _webCamTexture;

    
            _webCamTexture.Play();
        }
        else
        {
            Debug.LogError("Nu am găsit nicio cameră!");
        }
    }
}