using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class CameraShaker : MonoBehaviour
{

    private float ShakeY = 0f;
    private float ShakeX = 0f;
    private float ShakeYSpeed = 0.5f;
    private float ShakeXSpeed = 0.5f;

    public void setShake(float someY, float someX)
    {
        ShakeY = someY;
        ShakeX = someX;
    }

    void Update()
    {
        Vector2 _newPosition = new Vector2(ShakeX, ShakeY);
        if (ShakeY < 0)
        {
            ShakeY *= ShakeYSpeed;
        }
        if (ShakeX < 0)
        {
            ShakeX *= ShakeXSpeed;
        }
        ShakeY = -ShakeY;
        ShakeX = -ShakeX;
        transform.Translate(_newPosition, Space.Self);
    }

}