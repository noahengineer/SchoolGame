using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField]
    private Transform gunStartPos;
    [SerializeField] 
    private Transform gunZoomPos;
    [SerializeField]
    private float timeToZoom = 1f;
    [SerializeField]
    private AnimationCurve zoomCurve;
    [SerializeField]
    private GameObject redical;

    private float timeElapsed = 0;

    internal bool isAming;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            //timeElapsed = 0;
            timeElapsed += Time.deltaTime;
            float percentageComplete = timeElapsed / timeToZoom;

            transform.position = Vector3.Lerp(transform.position, gunZoomPos.position, zoomCurve.Evaluate(percentageComplete));

            redical.SetActive(false);
            isAming = true;
        }
        else
        {
            timeElapsed = 0;
            timeElapsed += Time.deltaTime;
            float percentageComplete = timeElapsed / timeToZoom;

            transform.position = Vector3.Lerp(transform.position, gunStartPos.position, zoomCurve.Evaluate(percentageComplete));

            redical.SetActive(true);
            isAming = false;
        }
    }
}
