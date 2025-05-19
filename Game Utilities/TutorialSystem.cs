using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Believe.Games.Studios 
{
    public class TutorialSystem : MonoBehaviour
    {
        [SerializeField] GameObject panel;
        [SerializeField] List<Transform> tutorialImages;
        int currentTip = 0;
        private void OnEnable()
        {
            panel.SetActive(false);
            foreach(Transform t in tutorialImages)
            {
                t.gameObject.SetActive(false);
            }
        }
        public void StartTutorial()
        {
            currentTip = -1;
            panel.SetActive(true);
            DisplayNextTutorial();
        }
        public void DisplayNextTutorial()
        {
            if(currentTip>=tutorialImages.Count-1)
            {
                foreach (Transform t in tutorialImages)
                {
                    panel.SetActive(false);
                    t.gameObject.SetActive(false);
                }
                return;
            }
            currentTip++;

            for (int i = 0; i < tutorialImages.Count; i++)
            {
                if(i==currentTip)
                {
                    tutorialImages[i].gameObject.SetActive(true);
                }
                else
                {
                    tutorialImages[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
