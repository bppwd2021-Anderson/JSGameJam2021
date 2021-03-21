using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;
    [HideInInspector]
    public Vector3 minValues, maxValue;
    [Range(0, 1)]
    public float moveCameraTriggerValue;

    [HideInInspector]
    public bool setupComplete = false;
    public enum SetupState { None, Step1, Step2}
    [HideInInspector]
    public SetupState ss = SetupState.None;

    // Start is called before the first frame update
    void Start()
    {
        Follow();
    }

    private void FixedUpdate()
    {
        var playerViewPortPos = Camera.main.WorldToScreenPoint(target.transform.position);

        if(playerViewPortPos.x >= moveCameraTriggerValue)
        {
            Follow();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValues.x, maxValue.x),
            Mathf.Clamp(targetPosition.y, minValues.y, maxValue.y),
            Mathf.Clamp(targetPosition.z, minValues.z, maxValue.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.deltaTime);
        transform.position = smoothPosition;

        minValues.x = smoothPosition.x;
    }

    public void ResetValues()
    {
        setupComplete = false;
        minValues = Vector3.zero;
        maxValue = Vector3.zero;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CameraFollow))]

public class CameraFollowEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var script = (CameraFollow)target;
        GUILayout.Space(20);
        GUIStyle defaultStyle = new GUIStyle();
        defaultStyle.fontSize = 12;
        defaultStyle.alignment = TextAnchor.MiddleCenter;

        GUIStyle titleStyle = new GUIStyle();
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.fontSize = 15;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("-=- Camera Boundaries Settings -=-", titleStyle);

        if (script.setupComplete)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Minimum Values:", defaultStyle);
            GUILayout.Label("Maximum Values:", defaultStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"X = {script.minValues.x}", defaultStyle);
            GUILayout.Label($"X = {script.maxValue.x}", defaultStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Y = {script.minValues.y}", defaultStyle);
            GUILayout.Label($"Y = {script.maxValue.y}", defaultStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("View Minimum"))
            {
                Camera.main.transform.position = script.minValues;
            }
            if (GUILayout.Button("View Maximum"))
            {
                Camera.main.transform.position = script.maxValue;
            }
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Focus On Target"))
            {
                Vector3 targetPos = script.target.position + script.offset;
                targetPos.z = script.minValues.z;
                Camera.main.transform.position = targetPos;
            }

            if (GUILayout.Button("Reset Camera Values"))
            {
                script.ResetValues();
            }
        }

        else
        {
            if (script.ss == CameraFollow.SetupState.None)
            {
                if (GUILayout.Button("Start Setting Camera Values"))
                {
                    script.ss = CameraFollow.SetupState.Step1;
                }
            }

            else if (script.ss == CameraFollow.SetupState.Step1)
            {
                GUILayout.Label($"1- Select your main camera", defaultStyle);
                GUILayout.Label($"2- Move it to the bottom left bound limit of your level", defaultStyle);
                GUILayout.Label($"3- Click the set minimum values button", defaultStyle);

                if (GUILayout.Button("Set Minimum Values"))
                {
                    script.minValues = Camera.main.transform.position;
                    script.ss = CameraFollow.SetupState.Step2;
                }
            }

            else if (script.ss == CameraFollow.SetupState.Step2)
            {
                GUILayout.Label($"1- Select your main camera", defaultStyle);
                GUILayout.Label($"2- Move it to the top right bound limit of your level", defaultStyle);
                GUILayout.Label($"3- Click the set maximum values button", defaultStyle);

                if (GUILayout.Button("Set Maximum Values"))
                {
                    script.maxValue = Camera.main.transform.position;
                    script.ss = CameraFollow.SetupState.None;
                    script.setupComplete = true;

                    Vector3 targetPos = script.target.position + script.offset;
                    targetPos.z = script.minValues.z;
                    Camera.main.transform.position = targetPos;
                }
            }
        }
    }
}
#endif
