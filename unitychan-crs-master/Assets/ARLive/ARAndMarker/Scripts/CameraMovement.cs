using UnityEngine;
using UnityEngine.UI;
public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Text status;

    /// <summary>
    /// 調整値
    /// </summary>
    private readonly Quaternion BASE_ROTATION = Quaternion.Euler(0, 180, 0);

    private Quaternion baseRotation;

    /// <summary>
    ///  カメラのTransform
    /// </summary>
    [SerializeField] private Transform camera;

    /// <summary>
    /// ジャイロ基準値
    /// </summary>
    private Quaternion gyroini;

    /// <summary>
    /// ジャイロ値
    /// </summary>
    private Quaternion gyro;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        // ジャイロセンサーをサポートしていない場合、コンポーネントを破棄する
        if (!SystemInfo.supportsGyroscope)
        {
            Destroy(this);
            return;
        }

        // ジャイロセンサを有効
        Input.gyro.enabled = true;

        // 加速度センサーをサポートしていない場合、コンポーネントを破棄する
        if (!SystemInfo.supportsAccelerometer)
        {
            Destroy(this);
            return;
        }

        baseRotation = transform.rotation;

        gyroini = Input.gyro.attitude;
        gyro = gyroini;
        gyroini = BASE_ROTATION * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
    }

    /// <summary>
    /// メインループ
    /// </summary>
    private void Update()
    {
        // ジャイロセンサからカメラの角度を設定する
        gyro = Input.gyro.attitude;
        //gyro = BASE_ROTATION * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));

        camera.localRotation = BASE_ROTATION;// * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
        // ジャイロ基準値とジャイロ値から差分の角度を設定する
        //camera.localRotation = Quaternion.Inverse(gyroini) * gyro;

        // 加速度センサからカメラの移動量を設定する
        //Vector3 acceleration = Input.acceleration;
        //camera.Translate(new Vector3(acceleration.x, 0, acceleration.z));

        ShowStatus();
    }

    private void ShowStatus()
    {
        status.text = "Accel:" + Input.acceleration + "\n";
        status.text += "Base rotation:" + BASE_ROTATION + "\n";
        status.text += "Rotation:" + camera.localRotation + "\n";

        Quaternion outGyro = Input.gyro.attitude;
        status.text += "Gyro:" + outGyro + "\n";
        status.text += "Processing:" + baseRotation * (new Quaternion(-outGyro.x, -outGyro.y, outGyro.z, outGyro.w)) + "\n";
        status.text += "Attach Rotation:" + Quaternion.Inverse(gyroini) * outGyro;
    }
}
