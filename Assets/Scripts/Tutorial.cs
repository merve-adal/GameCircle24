using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<Transform> vehiclesInOrder = new();
    private Transform tutorialObjectsTransform;
    private Animator tutorialAnimator;
    Vehicle vehicle; //to know if vehicle arrives finish line
    GameManager gameManager;
    private float delayAtStart = 0.2f; //tutorial starts after delay

    private void Awake()
    {
        tutorialObjectsTransform = transform.GetChild(0);
        tutorialAnimator = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<GameManager>();
    }
    void Start()
    {
        // playTutorial();
        vehicle = vehiclesInOrder[0].GetComponent<Vehicle>();
        StartCoroutine(WaitAndPlayTutorial());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (vehicle.IsOnFinishLevel)
        {
            if (vehiclesInOrder.Count > 1)
            {
                vehiclesInOrder.RemoveAt(0);
                playTutorial();
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
        else if(tutorialAnimator.GetBool("TutorialAnimationFinished"))
        {
            AnimationFinished();
        }
    }

    private void playTutorial()
    {
        if (vehiclesInOrder.Count != 0 && vehiclesInOrder[0].gameObject.activeInHierarchy)
        {
            Vector3 vehiclePosition = Camera.main.WorldToViewportPoint(vehiclesInOrder[0].position);

            float posX = (vehiclePosition.x - 0.5f) * this.GetComponent<RectTransform>().rect.width;
            float posY = (vehiclePosition.y - 0.5f) * this.GetComponent<RectTransform>().rect.height;

            tutorialObjectsTransform.localPosition = new Vector3(posX, posY, 0);
            vehicle = vehiclesInOrder[0].GetComponent<Vehicle>();
            gameManager.IsPlayable = false;
            tutorialObjectsTransform.gameObject.SetActive(true);
            tutorialAnimator.SetTrigger("StartTutorial");
        }
    }

    public void AnimationFinished() //access from state machine behaviour
    {
        tutorialObjectsTransform.gameObject.SetActive(false);
        gameManager.IsPlayable = true;
        tutorialAnimator.SetBool("TutorialAnimationFinished", false);
    }

    private IEnumerator WaitAndPlayTutorial()
    {
        yield return new WaitForSeconds(delayAtStart);
        playTutorial();
    }

}
