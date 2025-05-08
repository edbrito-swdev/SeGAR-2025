using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{


    public GameObject Step1;
    public GameObject Step2;
    public GameObject Step3;
    public GameObject Step4;
    public GameObject Step5;
    public GameObject Step6;

    public int activeStep = 1;

    public Button nextButton;
    public Button previousButton;

    private void Start()
    {
        nextButton.onClick.AddListener(NextStep);
        previousButton.onClick.AddListener(PreviousStep);
    }

    public void NextStep()
    {
        switch (activeStep)
        {
            case 1: Step1.SetActive(false); Step2.SetActive(true); break;
            case 2: Step2.SetActive(false); Step3.SetActive(true); break;
            case 3: Step3.SetActive(false); Step4.SetActive(true); break;
            case 4: Step4.SetActive(false); Step5.SetActive(true); break;
            case 5: Step5.SetActive(false); Step6.SetActive(true); break;
        }
        if(activeStep < 6)
        {
            activeStep++;
        }
    }

    public void PreviousStep()
    {
        switch (activeStep)
        {
            case 2: Step2.SetActive(false); Step1.SetActive(true); break;
            case 3: Step3.SetActive(false); Step2.SetActive(true); break;
            case 4: Step4.SetActive(false); Step3.SetActive(true); break;
            case 5: Step5.SetActive(false); Step4.SetActive(true); break;
            case 6: Step6.SetActive(false); Step5.SetActive(true); break;
        }
        if (activeStep > 1)
        {
            activeStep--;
        }
    }
}