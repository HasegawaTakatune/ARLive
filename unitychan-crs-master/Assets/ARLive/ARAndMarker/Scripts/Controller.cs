using UnityEngine;

public class Controller : MonoBehaviour
{
    /// <summary>
    /// アナログパッドのスタート位置
    /// </summary>
    [SerializeField] private RectTransform StartController;

    /// <summary>
    /// アナログパッドの移動位置
    /// </summary>
    [SerializeField] private RectTransform MoveController;

    /// <summary>
    /// アナログパッドのスタート座標を格納
    /// </summary>
    private Vector3 startPosition;

    /// <summary>
    /// アナログパッドの移動範囲
    /// </summary>
    [SerializeField] private float range = 5;

    /// <summary>
    /// アナログパッドの傾き格納
    /// </summary>
    private float angle;

    /// <summary>
    /// アナログパッドが動いているか判定
    /// </summary>
    private bool move = false;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        StartController.gameObject.SetActive(false);
        MoveController.gameObject.SetActive(false);
    }

    /// <summary>
    /// メインループ
    /// </summary>
    void Update()
    {
#if UNITY_EDITOR
        MouseControll();
#elif UNITY_ANDROID
        TouchControll();
#endif
    }

    /// <summary>
    /// マウスでのアナログパッド操作
    /// エディター用
    /// </summary>
    private void MouseControll()
    {
        // タッチ開始操作
        if (Input.GetMouseButtonDown(0))
            BeginControll(Input.mousePosition);

        // タッチ移動操作
        if (Input.GetMouseButton(0))
            MoveControll(Input.mousePosition);

        // タッチ終了操作
        if (Input.GetMouseButtonUp(0))
            EndControll();
    }

    /// <summary>
    /// タッチでのアナログパッド操作
    /// Android用
    /// </summary>
    private void TouchControll()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // タッチ開始操作
            if (touch.phase == TouchPhase.Began)
                BeginControll(touch.position);

            // タッチ移動操作
            if (touch.phase == TouchPhase.Moved)
                MoveControll(touch.position);

            // タッチ終了操作
            if (touch.phase == TouchPhase.Ended)
                EndControll();
        }
    }

    /// <summary>
    /// タッチ開始操作
    /// </summary>
    /// <param name="pos">初期座標</param>
    private void BeginControll(Vector3 pos)
    {
        move = true;

        // アナログパッドを表示
        StartController.gameObject.SetActive(true);
        MoveController.gameObject.SetActive(true);

        // アナログパッドの初期位置設定
        StartController.position = pos;
        MoveController.position = pos;

        startPosition = pos;
    }

    /// <summary>
    /// タッチ終了操作
    /// </summary>
    private void EndControll()
    {
        move = false;

        // アナログパッドを非表示にする
        StartController.gameObject.SetActive(false);
        MoveController.gameObject.SetActive(false);
    }

    /// <summary>
    /// タッチ移動操作
    /// </summary>
    /// <param name="pos">移動した座標</param>
    private void MoveControll(Vector3 pos)
    {
        // タッチ移動した分、アナログパッドを移動させる
        MoveController.position = GetRange(pos);

        // アナログパッドの移動方向を設定（角度）
        angle = GetAim(MoveController.position, startPosition);
    }

    /// <summary>
    /// アナログパッドが指定範囲内で動くようにする
    /// </summary>
    /// <param name="pos">アナログパッドの移動位置</param>
    /// <returns>範囲内に収めた際の移動位置</returns>
    private Vector3 GetRange(Vector3 pos)
    {
        return new Vector3(
            Mathf.Clamp(pos.x, startPosition.x - range, startPosition.x + range),
            Mathf.Clamp(pos.y, startPosition.y - range, startPosition.y + range),
            Mathf.Clamp(pos.z, startPosition.z - range, startPosition.z + range));
    }

    /// <summary>
    /// 2転換の角度を取得
    /// </summary>
    /// <param name="p1">座標１</param>
    /// <param name="p2">座標２</param>
    /// <returns>２点間の角度</returns>
    private float GetAim(Vector2 p1, Vector2 p2)
    {
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        float rad = Mathf.Atan2(dx, dy);
        float degree = rad * Mathf.Rad2Deg;
        if (degree < 0) degree += 360;
        return degree;
    }

    /// <summary>
    /// アナログパッドの角度を取得
    /// </summary>
    /// <returns>アナログパッドの角度</returns>
    public float GetAngle() { return angle; }

    /// <summary>
    /// アナログパッドが移動しているかの判定
    /// </summary>
    /// <returns>true:移動  false:停止</returns>
    public bool Moving() { return move; }
}
