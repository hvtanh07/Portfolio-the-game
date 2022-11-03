using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeAmt = 2f; // the degrees to shake the camera
    public float shakePeriodTime = 0.2f; // The period of each shake
    public float dropOffTime = 1.6f; // How long it takes the shaking to settle down to nothing
    public void Shake(float duration)
    {

        LeanTween.cancel(gameObject);

        LTDescr shakeTween = LeanTween.rotateAroundLocal(gameObject, Vector3.right, shakeAmt, shakePeriodTime)
        .setEase(LeanTweenType.easeShake) // this is a special ease that is good for shaking
        .setLoopClamp()
        .setRepeat(-1);

        // Slow the camera shake down to zero
        LeanTween.value(gameObject, shakeAmt, 0f, duration).setOnUpdate(
            (float val) =>
            {
                shakeTween.setTo(Vector3.right * val);
            }
        ).setEase(LeanTweenType.easeOutQuad);

    }
}
