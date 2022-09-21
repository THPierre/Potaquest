using Lean.Common;
using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConstraints : LeanConstrainLocalPosition
{
    [Space(20)]
    [SerializeField]
    private LeanPinchCamera cameraPinch;
    [Space(20)]
    [SerializeField]
    private int minXminZoom;
    [SerializeField]
    private int maxXminZoom;
    [SerializeField]
    private int minYminZoom;
    [SerializeField]
    private int maxYminZoom;
    [Space(20)]
    [SerializeField]
    private int maxXmaxZoom;
    [SerializeField]
    private int minXmaxZoom;
    [SerializeField]
    private int maxYmaxZoom;
    [SerializeField]
    private int minYmaxZoom;
    protected override void LateUpdate()
    {
        float cameraZoom = (cameraPinch.Zoom - cameraPinch.ClampMin) / (cameraPinch.ClampMax - cameraPinch.ClampMin);
        XMax = Mathf.Lerp(maxXmaxZoom, maxXminZoom, cameraZoom);
        XMin = Mathf.Lerp(minXmaxZoom, minXminZoom, cameraZoom);
        YMax = Mathf.Lerp(maxYmaxZoom, maxYminZoom, cameraZoom);
        YMin = Mathf.Lerp(minYmaxZoom, minYminZoom, cameraZoom);
        base.LateUpdate();
    }
}
