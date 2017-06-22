using UnityEngine;
using System.Collections;

using DG.Tweening;

public enum TWEEN
{
    iTween = 0,
    LTween,
    DTween
}

public class iTweenMoveTest : MonoBehaviour
{
    public Transform moveTarget;
    public TWEEN tween;

    void Update()
    {
        //move
        if (Input.GetKeyDown(KeyCode.S))
        {
            switch (tween)
            {
                case TWEEN.iTween:
                    {
                        Hashtable hash = new Hashtable();
                        hash.Add("position", moveTarget);
                        hash.Add("orienttopath", true);
                        hash.Add("looktime", 2.0f);
                        hash.Add("time", 5.0f);
                        hash.Add("easetype", iTween.EaseType.easeInOutExpo);
                        iTween.MoveTo(gameObject, hash);
                    }
                    break;

                case TWEEN.LTween:
                    {
                        LTDescr description = LeanTween.move(gameObject, moveTarget, 5.0f);
                        description.setEase(LeanTweenType.easeInOutExpo);

                        Quaternion origin = transform.rotation;
                        transform.LookAt(moveTarget);
                        Vector3 euler = transform.eulerAngles;
                        transform.rotation = origin;

                        LeanTween.rotate(gameObject, euler, 2.0f).setEase(LeanTweenType.easeOutCirc);
                    }
                    break;

                case TWEEN.DTween:
                    {
                        transform.DOMove(moveTarget.position, 5.0f).SetEase(Ease.InOutExpo);
                        transform.DOLookAt(moveTarget.position, 2.0f).SetEase(Ease.OutCirc);
                    }
                    break;

                default: { break; }
            }
        }

        //scale
        if (Input.GetKeyDown(KeyCode.D))
        {
            switch (tween)
            {
                case TWEEN.iTween:
                    {
                        Hashtable hash = new Hashtable();
                        hash.Add("x", 3);
                        hash.Add("y", 3);
                        hash.Add("z", 3);
                        hash.Add("speed", 1.5f);
                        hash.Add("easetype", iTween.EaseType.easeOutElastic);
                        iTween.ScaleTo(gameObject, hash);
                    }
                    break;

                case TWEEN.LTween:
                    {
                        Vector3 targetScale = new Vector3(3.0f, 3.0f, 3.0f);
                        float distance = Mathf.Abs(Vector3.Distance(transform.localScale, targetScale));
                        float time = distance / 3.0f;

                        LeanTween.scale(gameObject, targetScale, time).setEase(LeanTweenType.easeOutElastic).setDelay(1.5f);
                    }
                    break;

                case TWEEN.DTween:
                    {
                        Vector3 targetScale = new Vector3(3.0f, 3.0f, 3.0f);
                        float distance = Mathf.Abs(Vector3.Distance(transform.localScale, targetScale));
                        float time = distance / 3.0f;

                        transform.DOScale(targetScale, time).SetEase(Ease.OutElastic).SetDelay(1.5f);
                    }
                    break;

                default: { break; }
            }
        }

        //rotate
        if (Input.GetKeyDown(KeyCode.F))
        {
            switch (tween)
            {
                case TWEEN.iTween:
                    {
                        Hashtable hash = new Hashtable();
                        hash.Add("rotation", new Vector3(0.0f, 3600.0f, 0.0f));
                        hash.Add("time", 3.0f);
                        hash.Add("easetype", iTween.EaseType.easeOutExpo);

                        iTween.RotateTo(gameObject, hash);
                    }
                    break;

                case TWEEN.LTween:
                    {
                        Vector3 targetRotate = new Vector3(0.0f, 3600.0f, 0.0f);
                        float distance = Mathf.Abs(Vector3.Distance(transform.localScale, targetRotate));
                        float time = distance / 10.0f;

                        LeanTween.rotate(gameObject, targetRotate, time).setEase(LeanTweenType.easeOutExpo);
                    }
                    break;

                case TWEEN.DTween:
                    {
                        Vector3 targetRotate = new Vector3(0.0f, 3600.0f, 0.0f);
                        float distance = Mathf.Abs(Vector3.Distance(transform.localScale, targetRotate));
                        float time = distance / 10.0f;

                        transform.DORotate(targetRotate, time).SetEase(Ease.OutExpo);
                    }
                    break;

                default: { break; }
            }
        }
    }
}
