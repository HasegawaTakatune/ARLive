using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamera : MonoBehaviour
{

    private int width = 1920;
    private int height = 1080;

    [SerializeField] private RawImage display;

    private WebCamTexture webCamTexture = null;

    private IEnumerator Start()
    {
        // カメラデバイスの存在確認
        if (WebCamTexture.devices.Length == 0)
        {
            Destroy(this);
            yield break;
        }

        // カメラデバイスの使用許可を求める
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

        // カメラデバイスの使用許可が下りなかった場合
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Destroy(this);
            yield break;
        }

        WebCamDevice webCamDevice = WebCamTexture.devices[0];
        webCamTexture = new WebCamTexture(webCamDevice.name, width, height);
    }

    void Update()
    {

    }
}
