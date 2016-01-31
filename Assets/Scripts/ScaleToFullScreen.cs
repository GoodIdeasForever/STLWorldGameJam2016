using UnityEngine;
using System.Collections;

public class ScaleToFullScreen : MonoBehaviour {

    public Vector3 posTarget;
    public Vector2 widthHeightTarger;
    public Vector3 rotationTarget;
    public float scaleDuration;
    public float rotationDuration;
    RectTransform thisTrans;

    Vector3 posStart;
    Vector2 widthHeightStart;
    Vector3 rotationStart;

    void Awake()
    {
        thisTrans = GetComponent<RectTransform>();
        posStart = thisTrans.transform.localPosition;
        widthHeightStart = thisTrans.sizeDelta;
        rotationStart = thisTrans.transform.localEulerAngles;
    }

	// Use this for initialization
	void Start () {
        //StartCoroutine(ScaleOut());
	}

    public void BeginScaleOut()
    {
        StartCoroutine(ScaleOut());
    }


    public void BeginScaleIn()
    {
        StartCoroutine(ScaleIn());
    }

    IEnumerator ScaleOut()
    {
        float delta = 0f;
        while (delta < scaleDuration)
        {
            thisTrans.transform.localPosition = new Vector3(posStart.x + (posTarget.x - posStart.x) * Mathf.Clamp(Mathf.Pow(delta / scaleDuration, 4f), 0f, 1f),
                                                            posStart.y + (posTarget.y - posStart.y) * Mathf.Clamp(Mathf.Pow(delta / scaleDuration, 4f), 0f, 1f),
                                                            posStart.z + (posTarget.z - posStart.z) * Mathf.Clamp(Mathf.Pow(delta / scaleDuration, 4f), 0f, 1f));
            thisTrans.sizeDelta = new Vector2(widthHeightStart.x + (widthHeightTarger.x - widthHeightStart.x) * Mathf.Clamp(Mathf.Pow(delta / scaleDuration, 4f), 0f, 1f),
                                              widthHeightStart.y + (widthHeightTarger.y - widthHeightStart.y) * Mathf.Clamp(Mathf.Pow(delta / scaleDuration, 4f), 0f, 1f));

            thisTrans.transform.localEulerAngles = new Vector3(rotationStart.x + (rotationTarget.x - rotationStart.x) * Mathf.Clamp(Mathf.Pow(delta / rotationDuration, 4f), 0f, 1f),
                                                            rotationStart.y + (rotationTarget.y - rotationStart.y) * Mathf.Clamp(Mathf.Pow(delta / rotationDuration, 4f), 0f, 1f),
                                                            rotationStart.z + (rotationTarget.z - rotationStart.z) * Mathf.Clamp(Mathf.Pow(delta / rotationDuration, 4f), 0f, 1f));

            yield return null;
            delta += Time.deltaTime;
        }

        thisTrans.transform.localPosition = new Vector3(posTarget.x, posTarget.y, posTarget.z);
        thisTrans.sizeDelta = new Vector2(widthHeightTarger.x, widthHeightTarger.y);
        thisTrans.transform.localEulerAngles = new Vector3(rotationTarget.x, rotationTarget.y, rotationTarget.z);
        delta = 0f;
        while (delta < scaleDuration)
        {
            yield return null;
            delta += Time.deltaTime;
        }
        //StartCoroutine(ScaleIn());
    }

    IEnumerator ScaleIn()
    {
        float delta = 0f;
        while (delta < scaleDuration)
        {
            thisTrans.transform.localPosition = new Vector3(posTarget.x + (posStart.x - posTarget.x) * Mathf.Clamp(Mathf.Pow(delta / scaleDuration, 4f), 0f, 1f),
                                                            posTarget.y + (posStart.y - posTarget.y) * Mathf.Clamp(Mathf.Pow(delta / scaleDuration, 4f), 0f, 1f),
                                                            posTarget.z + (posStart.z - posTarget.z) * Mathf.Clamp(Mathf.Pow(delta / scaleDuration, 4f), 0f, 1f));
            thisTrans.sizeDelta = new Vector2(widthHeightTarger.x + (widthHeightStart.x - widthHeightTarger.x) * Mathf.Clamp(Mathf.Pow(delta / scaleDuration, 4f), 0f, 1f),
                                              widthHeightTarger.y + (widthHeightStart.y - widthHeightTarger.y) * Mathf.Clamp(Mathf.Pow(delta / scaleDuration, 4f), 0f, 1f));

            thisTrans.transform.localEulerAngles = new Vector3(rotationTarget.x + (rotationStart.x - rotationTarget.x) * Mathf.Clamp(Mathf.Pow(delta / rotationDuration, 4f), 0f, 1f),
                                                            rotationTarget.y + (rotationStart.y - rotationTarget.y) * Mathf.Clamp(Mathf.Pow(delta / rotationDuration, 4f), 0f, 1f),
                                                            rotationTarget.z + (rotationStart.z - rotationTarget.z) * Mathf.Clamp(Mathf.Pow(delta / rotationDuration, 4f), 0f, 1f));

            yield return null;
            delta += Time.deltaTime;
        }

        thisTrans.transform.localPosition = new Vector3(posStart.x, posStart.y, posStart.z);
        thisTrans.sizeDelta = new Vector2(widthHeightStart.x, widthHeightStart.y);
        thisTrans.transform.localEulerAngles = new Vector3(rotationStart.x, rotationStart.y, rotationStart.z);

        delta = 0f;
        while (delta < scaleDuration)
        {
            yield return null;
            delta += Time.deltaTime;
        }
        UIManager.Instance.acceptingInput = true;
    }
}
