using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraLetterbox : MonoBehaviour
{
    public Vector2 targetAspect = new Vector2(16, 9);

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        float target = targetAspect.x / targetAspect.y;
        float window = (float)Screen.width / Screen.height;
        float scaleHeight = window / target;

        if (scaleHeight < 1f)
        {
            // Letterbox (black bars top & bottom)
            Rect rect = new Rect(0f, (1f - scaleHeight) / 2f, 1f, scaleHeight);
            cam.rect = rect;
        }
        else
        {
            // Pillarbox (black bars left & right)
            float scaleWidth = 1f / scaleHeight;
            Rect rect = new Rect((1f - scaleWidth) / 2f, 0f, scaleWidth, 1f);
            cam.rect = rect;
        }
    }
}
