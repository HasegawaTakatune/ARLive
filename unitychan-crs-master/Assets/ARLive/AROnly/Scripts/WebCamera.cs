using System.Collections;
using UnityEngine;

public class WebCamera : MonoBehaviour
{

    [SerializeField] private Material outMaterial;

    private WebCamDevice[] webCamDevices = null;

    private int selectCamera = 0;

    /// <summary>
    /// 幅
    /// </summary>
    private int Width = 1920;

    /// <summary>
    /// 高さ
    /// </summary>
    private int Height = 1080;

    /// <summary>
    /// フレームレート
    /// </summary>
    private int FPS = 30;

    /// <summary>
    /// Webカメラテクスチャ
    /// </summary>
    private WebCamTexture webCamTexture;

    /// <summary>
    /// 初期化
    /// </summary>
    private IEnumerator Start()
    {
        int count = 0;
        while (true)
        {
            try
            {
                if (count >= 100) break;
                count++;
                webCamDevices = WebCamTexture.devices;
                if (webCamDevices.Length > 0) break;
            }
            catch { }
            yield return new WaitForSeconds(Time.deltaTime);
        }

        if (webCamDevices != null && webCamDevices.Length > 0)
        {
            webCamTexture = new WebCamTexture(webCamDevices[selectCamera].name, Width, Height, FPS);

            outMaterial.mainTexture = webCamTexture;
            webCamTexture.Play();
        }
    }
}
