using UnityEngine;
using System.Collections;

public class Mole : MonoBehaviour
{

    Animator animator;  

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    bool touchPossible = false;

    public void OpenInit()
    {
        touchPossible = true;
        Debug.Log("open init");
    }

    public void IdleEnd()
    {
        touchPossible = false;
        Debug.Log("Close End");
    }

    public void OnMouseDown()
    {
        if(touchPossible)
        {
            touchPossible = false;
            animator.SetTrigger("isTouch");
        }
    }

    IEnumerator CloseEvent()
    {
        float randomTime = Random.Range(1.0f, 3.0f);
        yield return new WaitForSeconds(randomTime);
        animator.SetTrigger("open");
    }
}
