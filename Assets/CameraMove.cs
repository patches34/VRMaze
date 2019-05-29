using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    float panStepAmount, zoomStepAmount;

    // Update is called once per frame
    void Update()
    {
        #region Camera Pan
        Vector3 delta = Vector3.zero;
        delta.x = panStepAmount * Input.GetAxis("Horizontal") * Time.deltaTime;
        delta.y = panStepAmount * Input.GetAxis("Vertical") * Time.deltaTime;

        transform.Translate(delta);
        #endregion

        #region Camera Zoom
        Camera.main.orthographicSize -= Input.mouseScrollDelta.y * zoomStepAmount * Time.deltaTime;
        #endregion
    }

    public void Recenter()
    {
        transform.position = LevelManager.Instance.GetLevelCenter();
    }
}
