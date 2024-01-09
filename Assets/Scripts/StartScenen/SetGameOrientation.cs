using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameOrientation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_EDITOR
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
#endif

    }

}
