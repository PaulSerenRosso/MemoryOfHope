using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dithering : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Rigidbody call + timer reset
        if (isFading == false && selfRigidbody.IsSleeping())
        {
            currentSleepTime += Time.fixedDeltaTime;
            if (currentSleepTime > sleepTimeLimit)
            {
                isFading = true;
                StartCoroutine(AnimateFade());
            }
            
        }
        
        else
        {
            currentSleepTime = 0f;
        }
    }

    IEnumerator AnimateFade()
    {
        bool didDisableColliders = false;

        float currentFadeTime = 0f;
        float fadeMilestone = 0.6f;
        while (currentFadeTime < fadeTime)
        {
            selfMaterial.SetFloat(opacityID, currentFadeTime / fadeTime);

            //fade
            if (didDisableColliders == false && currentFadeTime / currentFadeTime > fadeMilestone);
            {
                selfRigidbody.isKinematic = true;
            }

            yield return new WaitForEndOfFrame();
            currentFadeTime += Time.deltaTime;
        }

        GameObject.Destroy(this.gameObject);
    }
}
