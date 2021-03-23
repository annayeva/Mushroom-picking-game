using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHoverHighlight : MonoBehaviour {
    private MeshRenderer rend;
    private SkinnedMeshRenderer skinRend;
    public float maxOutlineWidth;
    private bool SkinRendIsTrue;
    public float visualKind;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.lockState = CursorLockMode.Confined;

        if (gameObject.GetComponent<MeshRenderer>() != null)
        {
            rend = GetComponent<MeshRenderer>();
        }

        else if (gameObject.GetComponent<SkinnedMeshRenderer>() != null)
        {
            skinRend = GetComponent<SkinnedMeshRenderer>();
            SkinRendIsTrue = true;
        }
    }



    public void OnMouseDown()
    {
        if (gameObject.tag == "GoodMushroom") //check the tag of clicked object
        {
            //GM.instance.AddPoint(); // adding points for a picked mushroom
            gameObject.SetActive(false); //hide picked mushroom
            GM.instance.VisualKindCheck(visualKind);
            
        }
        else if (gameObject.tag == "BadMushroom")
        {
            GM.instance.OnBadMushroomPick();
        }
    }

    void OnMouseEnter()
    {
        if (GM.instance.timeIsOut == false) // don't show outline if game is finished
        { 
        ShowOutline();
        }
    }

    void OnMouseExit()
    {
        HideOutline();
    }

    public void ShowOutline()
    {
        if (SkinRendIsTrue) // in case the object uses skin mesh renderer
        {
          skinRend.material.SetFloat("_Outline", maxOutlineWidth);
          skinRend.material.SetColor("_OutlineColor", Color.blue);
        }
        else  // in case the object uses mesh renderer
        {
        rend.material.SetFloat("_Outline", maxOutlineWidth);
        rend.material.SetColor("_OutlineColor", Color.blue);
        }
    }

    public void HideOutline()
    {
        if (SkinRendIsTrue)
        {
            skinRend.material.SetFloat("_Outline", 0f);
        }
        else
        {
            rend.material.SetFloat("_Outline", 0f);
        }           
    }
}

