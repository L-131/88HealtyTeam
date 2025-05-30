using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    public GameObject panel;
    public Coroutine tutorialCoroutine;
    private List<TutorialType> alreadyShownTutorials = new List<TutorialType>(); // �̹� ǥ���� Ʃ�丮�� ����Ʈ

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTutorial(TutorialType type, string text, float duration)
    {
        if (alreadyShownTutorials.Contains(type))
            return;

        alreadyShownTutorials.Add(type);

        if (tutorialCoroutine != null)
        {
            StopCoroutine(tutorialCoroutine);
        }

        tutorialText.text = text;
        gameObject.SetActive(true);

        tutorialCoroutine = StartCoroutine(DisableTutorialUI(duration));
    }

    private IEnumerator DisableTutorialUI(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        gameObject.SetActive(false);
        tutorialCoroutine = null;
    }
}
