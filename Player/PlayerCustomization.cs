using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomization : MonoBehaviour
{
    Button Chee;
    int hairCurrentIndex = 0;
    [SerializeField] SkinnedMeshRenderer[] HairMeshes;
    int glassesCurrentIndex = 0;
    [SerializeField] SkinnedMeshRenderer[] glassesMeshes;
    int beardIndex = 0;
    [SerializeField] SkinnedMeshRenderer[] beardMeshes; 
    int shirtIndex = 0;
    [SerializeField] SkinnedMeshRenderer[] shirtMeshes;
    int pantIndex = 0;
    [SerializeField] SkinnedMeshRenderer[] pantMeshes;
    int shoeIndex = 0;
    [SerializeField] SkinnedMeshRenderer[] shoeMeshes;

    public void ChooseHair(int index)
    {
        index = Mathf.Clamp(index,-1, 1);
        if(index<0)
        {
            if(hairCurrentIndex<=0)
            {
                hairCurrentIndex = HairMeshes.Length-1;
            }
            else
            {
                hairCurrentIndex--;
            }
        }
        else if(index>0)
        {
            if (hairCurrentIndex>=HairMeshes.Length-1)
            {
                hairCurrentIndex = 0;
            }
            else
            {
                hairCurrentIndex++;
            }
        }
        SelectHair();
    }
    public void ChooseGlasses(int index)
    {
        index=Mathf.Clamp(index,-1, 1);
        if (index < 0)
        {
            if (glassesCurrentIndex <= 0)
            {
                glassesCurrentIndex = glassesMeshes.Length - 1;
            }
            else
            {
                glassesCurrentIndex--;
            }
        }
        else if (index > 0)
        {
            if (glassesCurrentIndex >= glassesMeshes.Length - 1)
            {
                glassesCurrentIndex = 0;
            }
            else
            {
                glassesCurrentIndex++;
            }
        }
        SelectGlasses();
    }
    public void ChooseBeard(int index)
    {
        index=Mathf.Clamp(index,-1,1);
        if (index < 0)
        {
            if (beardIndex <= 0)
            {
                beardIndex = beardMeshes.Length - 1;
            }
            else
            {
                beardIndex--;
            }
        }
        else if (index > 0)
        {
            if (beardIndex >= beardMeshes.Length - 1)
            {
                beardIndex = 0;
            }
            else
            {
                beardIndex++;
            }
        }
        SelectBeard();
    }
    public void ChooseShirt(int index)
    {
        index=Mathf.Clamp (index,-1,1);
        if (index < 0)
        {
            if (shirtIndex <= 0)
            {
                shirtIndex = shirtMeshes.Length - 1;
            }
            else
            {
                shirtIndex--;
            }
        }
        else if (index > 0)
        {
            if (shirtIndex >= shirtMeshes.Length - 1)
            {
                shirtIndex = 0;
            }
            else
            {
                shirtIndex++;
            }
            SelectShirt();
        }
    }
    public void ChoosePant(int index)
    {
        index=Mathf.Clamp(index,-1,1);
        if (index < 0)
        {
            if (pantIndex <= 0)
            {
                pantIndex = pantMeshes.Length - 1;
            }
            else
            {
                pantIndex--;
            }
        }
        else if (index > 0)
        {
            if (pantIndex >= pantMeshes.Length - 1)
            {
                pantIndex = 0;
            }
            else
            {
                pantIndex++;
            }
            SelectPant();
        }
    }
    public void ChooseShoe(int index)
    {
        index=Mathf.Clamp(index,-1,1);
        if (index < 0)
        {
            if (shirtIndex <= 0)
            {
                shoeIndex = shoeMeshes.Length - 1;
            }
            else
            {
                shoeIndex--;
            }
        }
        else if (index > 0)
        {
            if (shoeIndex >= shoeMeshes.Length - 1)
            {
                shoeIndex = 0;
            }
            else
            {
                shoeIndex++;
            }
            SelectShoe();
        }
    }
    private void OnEnable()
    {
        SelectHair();
        SelectGlasses();
        SelectBeard();
        SelectShirt();
        SelectPant();
        SelectShoe();
    }
    #region Select Item
    void SelectHair()
    {
        for(int i = 0; i < HairMeshes.Length; i++)
        {
            if(i==hairCurrentIndex)
            {
                HairMeshes[i].gameObject.SetActive(true);
            }
            else
            {
                HairMeshes[i].gameObject.SetActive(false);
            }
        }
    }
    void SelectGlasses()
    {
        for(int i = 0; i < glassesMeshes.Length; i++)
        {
            if(i==glassesCurrentIndex)
            {
                glassesMeshes[i].gameObject.SetActive(true);
            }
            else
            {
                glassesMeshes[i].gameObject.SetActive(false);
            }
        }
    }
    void SelectBeard()
    {
        for(int i = 0; i < beardMeshes.Length; i++)
        {
            if(i==beardIndex)
            {
                beardMeshes[i].gameObject.SetActive(true);
            }
            else
            {
                beardMeshes[i].gameObject.SetActive(false);
            }
        }
    }
    void SelectShirt()
    {
        for(int i = 0; i < shirtMeshes.Length; i++)
        {
            if(i==shirtIndex)
            {
                shirtMeshes[i].gameObject.SetActive(true);
            }
            else
            {
                shirtMeshes[i].gameObject.SetActive(false);
            }
        }
    }
    void SelectPant()
    {
        for(int i = 0; i < pantMeshes.Length; i++)
        {
            if(i==pantIndex)
            {
                pantMeshes[i].gameObject.SetActive(true);
            }
            else
            {
                pantMeshes[i].gameObject.SetActive(false);
            }
        }
    }
    void SelectShoe()
    {
        for(int i = 0; i < shoeMeshes.Length; i++)
        {
            if(i==shoeIndex)
            {
                shoeMeshes[i].gameObject.SetActive(true);
            }
            else
            {
                shoeMeshes[i].gameObject.SetActive(false);
            }
        }
    }
    #endregion
    public void Save()
    {
        PlayerPrefs.SetInt("Hair",hairCurrentIndex);
        PlayerPrefs.SetInt("Glasses",glassesCurrentIndex);
        PlayerPrefs.SetInt("Beard",beardIndex);
        PlayerPrefs.SetInt("Shirt",shirtIndex);
        PlayerPrefs.SetInt("Pant", pantIndex);
        PlayerPrefs.SetInt("Shoe", shoeIndex);
    }
}
