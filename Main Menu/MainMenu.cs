using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Believe.Games.Studios
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Tab Management")]
        [SerializeField] private List<GameObject> TabButtons;
        [SerializeField] private   List<GameObject> TabPanel;
        [SerializeField] private Color SelectedTabColor;
        [SerializeField] private Color DeSelectedTabColor;
        private int currentTab = 0;
        public void OpenLink(string url)
        {
            Application.OpenURL(url);
        }
        private void OnEnable()
        {
            OpenTab(0);
        }
        public void OpenTab(int tabIndex)
        {
            currentTab = tabIndex;
            SelectTab();
        }
        void SelectTab()
        {
            for(int i=0; i<TabPanel.Count && i<TabButtons.Count;i++)
            {
                if(currentTab==i)
                {
                    TabPanel[i].SetActive(true);
                    TabButtons[i].GetComponent<Image>().color = SelectedTabColor;
                }
                else
                {
                    TabPanel[i].SetActive(false);
                    TabButtons[i].GetComponent<Image>().color = DeSelectedTabColor;
                }
            }
        }
    }
}
