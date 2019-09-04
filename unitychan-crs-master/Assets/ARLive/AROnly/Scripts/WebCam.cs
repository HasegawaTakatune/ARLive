using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WebCam : MonoBehaviour
{
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

    ///// <summary>
    ///// Skyboxに設定するマテリアル
    ///// </summary>
    //[SerializeField] private Material material;

    /// <summary>
    /// Webカメラテクスチャ
    /// </summary>
    private WebCamTexture webCamTexture;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <returns></returns>
    private IEnumerator Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        // カメラデバイスの存在確認
        if (devices.Length == 0)
        {
            RenderSettings.skybox = null;
            Destroy(this);
            yield break;
        }

        // カメラデバイスの使用許可を求める
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

        // カメラデバイスの使用許可が下りなかった場合
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            RenderSettings.skybox = null;
            Destroy(this);
            yield break;
        }

        webCamTexture = new WebCamTexture(Width, Height, FPS);

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webCamTexture;
        webCamTexture.Play();
             
    }
}
