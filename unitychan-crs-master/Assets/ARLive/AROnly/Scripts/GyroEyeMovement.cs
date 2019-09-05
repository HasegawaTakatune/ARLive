using UnityEngine;
using UnityEngine.UI;

public class GyroEyeMovement : MonoBehaviour
{
    /// <summary>
    /// 調整値
    /// </summary>
    private readonly Quaternion BASE_ROTATION = Quaternion.Euler(90, 0, 0);

    /// <summary>
    ///  カメラのTransform
    /// </summary>
    [SerializeField] private Transform camera;

    /// <summary>
    /// ジャイロ値
    /// </summary>
    private Quaternion gyro;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        // サポートしていない場合、コンポーネントを破棄する
        if (!SystemInfo.supportsGyroscope)
        {
            Destroy(this);
            return;
        }

        // ジャイロセンサを有効
        Input.gyro.enabled = true;
    }

    /// <summary>
    /// メインループ
    /// </summary>
    void Update()
    {
        // ジャイロセンサからカメラの角度を設定する
        gyro = Input.gyro.attitude;
        camera.localRotation = BASE_ROTATION * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
    }
}
