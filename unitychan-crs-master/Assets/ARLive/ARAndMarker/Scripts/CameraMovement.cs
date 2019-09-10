using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    /// <summary>
    /// カメラを子に持つオブジェクト
    /// </summary>
    [SerializeField] private Transform parent;

    /// <summary>
    /// カメラオブジェクト
    /// </summary>
    private Transform child;

    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField] private float speed = 0.1f;

    /// <summary>
    /// アナログパッドを取得
    /// </summary>
    private Controller controller;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        controller = FindObjectOfType<Controller>();
        child = transform;
    }

    /// <summary>
    /// メインループ
    /// </summary>
    private void Update()
    {
        // コントローラーで移動していたら、カメラ移動を開始する
        if (controller.Moving())
        {
            // 方向
            float angle = controller.GetAngle();
            // 移動量
            float value = (child.localEulerAngles.y + angle) * Mathf.PI / 180;
            // 移動
            // 移動
            parent.position += new Vector3(
                -Mathf.Cos(value),
                0,
                Mathf.Sin(value)) * speed;
        }
    }
}
