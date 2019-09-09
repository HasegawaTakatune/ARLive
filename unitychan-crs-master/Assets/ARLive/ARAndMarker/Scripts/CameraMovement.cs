using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform parent;
    private Transform child;

    [SerializeField] private float speed = 0.1f;

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
        if (controller.Moving())
        {
            float angle = controller.GetAngle();
            float value = (child.localEulerAngles.y + angle) * Mathf.PI / 180;
            parent.position += new Vector3(
                -Mathf.Cos(value),
                0,
                Mathf.Sin(value)) * speed;

            //Debug.Log(child.localEulerAngles.y);
        }
    }
}
