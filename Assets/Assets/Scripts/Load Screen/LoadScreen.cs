using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadScreen : MonoBehaviour
{
    Animator animator;
    [SerializeField] Image imagePlaceHolder;

    [Tooltip("Add the images you want to display as the background here")]
    [SerializeField] List<Sprite> SlideShowImages; 


    float nextTimeToChange;
    [SerializeField] float ImageSwitchRate;

    int currentImage=0;

    [Header("TIPS THAT'LL BE DISPLAYED ON THE LOAD SCREEN"),TextArea(5,15)]
	public string[] Tips;
	int currentTip;
	[SerializeField] TMP_Text tipText;


    internal enum SwitchType
    {
        Sequential,
        Random
    }
    [SerializeField] SwitchType switchType;
    void OnEnable()
    {
        animator=GetComponent<Animator>();
        ChangeImage();
    }

    // Update is called once per frame
    void Update()
    {
        if(switchType==SwitchType.Random)
        {
            currentImage=Random.Range(0,SlideShowImages.Count);
            currentTip=Random.Range(0,Tips.Length);
        }
        if(SlideShowImages.Count>1)
        {
            if(Time.time>=nextTimeToChange)
            {
                nextTimeToChange=Time.time+ImageSwitchRate;
                animator.SetTrigger("ChangeImage");
            }
        }
    }
    public void ChangeImage()
    {
        imagePlaceHolder.sprite=SlideShowImages[currentImage];
        tipText.text=Tips[currentTip];
        if(switchType==SwitchType.Sequential)
        {
            if(currentImage==SlideShowImages.Count-1)
            {
                currentImage=0;
            }
            else
            {
                currentImage++;
            }

            if(currentTip==Tips.Length-1)
            {
                currentTip=0;
            }
            else
            {
                currentTip++;
            }
        }
    }
}
