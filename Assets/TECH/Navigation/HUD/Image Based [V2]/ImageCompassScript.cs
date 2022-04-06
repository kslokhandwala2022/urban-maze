using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCompassScript : MonoBehaviour
{

    public RawImage CompassImage;

    public Transform rotationReference;
    public Text text;

    protected float CompassBarCurrentDegrees;

    // Update is called once per frame
    void Update()
    {

        // set compass bar texture coordinates
        CompassImage.uvRect = new Rect((rotationReference.eulerAngles.y / 360f) - .5f, 0f, 1f, 1f);

        // calculate 0-360 degrees value
        //Vector3 playerForward = Vector3.ProjectOnPlane(rotationReference.forward, Vector3.up);
        //float angle = Vector3.SignedAngle(playerForward, new Vector3(0, 0, -1), Vector3.up);

        Vector3 perpDirection = Vector3.Cross(Vector3.forward, rotationReference.forward);
        float angle = Vector3.Angle(new Vector3(rotationReference.forward.x, 0f, rotationReference.forward.z), Vector3.forward);
        
        CompassBarCurrentDegrees = (perpDirection.y >= 0f) ? angle : 360f - angle;

        // Update the degrees text
        text.text = ((int)CompassBarCurrentDegrees).ToString();

    }
}
