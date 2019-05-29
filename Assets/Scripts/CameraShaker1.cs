using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class CameraShaker1 : MonoBehaviour
{
    public float ShakeAmount = 0.1f;
    public float DecreaseFactor = 0.3f;

    private new Camera camera;
    private Vector3 cameraPos;
    private float shake = 0.0f;


    void Awake()
    {
        this.camera = (Camera) this.GetComponent<Camera>();
        if (this.camera == null)
        {
            // Print an error.
            Debug.Log("CameraShake: Unable to find 'Camera' component attached to GameObject.");
        }
    }
    void Update()
    {
        // Check if the screen should be shaking.
        if (this.shake > 0.0f)
        {
            // Shake the camera.
            this.camera.transform.localPosition = Random.insideUnitSphere * this.ShakeAmount * this.shake;

            // Reduce the amount of shaking for next tick.
            this.shake -= Time.deltaTime * this.DecreaseFactor;

            // Check to see if we've stopped shaking.
            if (this.shake <= 0.0f)
            {
                // Clamp the shake amount back to zero, and reset the camera position to our cached value.
                this.shake = 0.0f;
                this.camera.transform.localPosition = this.cameraPos;
            }
        }

    }
    public void Shake(float amount)
    {
        // Check if we're already shaking.
        if (this.shake <= 0.0f)
        {
            // If we aren't, cache the camera position.
            this.cameraPos = this.camera.transform.position;
            Debug.Log("shake");
        }
        // Set the 'shake' value.
        this.shake = amount;
    }
}