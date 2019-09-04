using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelect : MonoBehaviour
{
    public void OnClickARButton()
    {
        SceneManager.LoadScene("AR");
    }

    public void OnClickARAndMarker()
    {
        SceneManager.LoadScene("ARAndMarker");
    }
}
