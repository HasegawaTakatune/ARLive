using UnityEngine;

public class ARCamera : MonoBehaviour
{
    [SerializeField] private GameObject stageDirector;

    [SerializeField] private Transform trans;

    private CameraMovement cameraMovement;

    private bool created = false;

    private void Start()
    {
        cameraMovement = gameObject.GetComponent<CameraMovement>();
    }

    public void OnTrackingFound()
    {
        if (!created)
        {
            created = true;            

            trans.position = new Vector3(0, 1, 8);
            trans.rotation = Quaternion.Euler(0, 0, 0);

            StartLiveStage();

            //Destroy(this);
        }
    }

    private void StartLiveStage()
    {
        stageDirector.SetActive(true);
        cameraMovement.enabled = true;
    }

    public void OnTrackingLost() { }
}
