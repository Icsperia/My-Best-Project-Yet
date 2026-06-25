using UnityEngine;

public class WebCamInScena : MonoBehaviour
{
    private WebCamTexture webCamTexture;
    private Renderer renderer;

    void Start()
    {

        renderer = GetComponent<Renderer>();

        if (WebCamTexture.devices.Length > 0)
        {
            
            webCamTexture = new WebCamTexture(WebCamTexture.devices[0].name);

            renderer.material.mainTexture = webCamTexture;

    
            webCamTexture.Play();
        }
        else
        {
            Debug.LogError("No camera found!");
        }
    }
}