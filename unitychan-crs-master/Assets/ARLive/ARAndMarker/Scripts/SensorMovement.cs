using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorMovement : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private Transform body;

    /// <summary>
    /// 調整値
    /// </summary>
    private readonly Quaternion BASE_ROTATION = Quaternion.Euler(90, 0, 0);    

    /// <summary>
    /// ジャイロ値
    /// </summary>
    private Quaternion gyro;

    /// <summary>
    /// 加速度値
    /// </summary>
    private Vector3 accelerometer;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        // ジャイロセンサーをサポートしていない場合、コンポーネントを破棄する
        if (!SystemInfo.supportsGyroscope)
        {
            Destroy(this);
            return;
        }

        // ジャイロセンサーを有効化
        Input.gyro.enabled = true;

        // 加速度センサーをサポートしていない場合、コンポーネントを破棄する
        if (!SystemInfo.supportsAccelerometer)
        {
            Destroy(this);
            return;
        }
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
