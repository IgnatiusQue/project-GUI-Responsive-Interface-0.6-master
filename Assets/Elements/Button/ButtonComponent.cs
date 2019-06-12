using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonComponent : MonoBehaviour {


  //  public bool contentAlreadyUpdatedFlag=false;


 //   private bool editModeWarningFlag;
    public bool editMode;
     

    public ButtonType.Component button = new ButtonType.Component();


      
    public void Init()
    {


        //Get skins
        if (!button.style.bodySkin) { button.style.bodySkin = Resources.Load("GUI/DefaultSkins/Button/ButtonBody") as GUISkin; }



        //Get structure

        // button.structure.tabParent = gameObject.transform.parent.GetComponentInParent<TabComponent>();
        button.structure.tabParent = gameObject.transform.parent.GetComponent<TabComponent>();
        button.structure.childrenTabs = Structure.Tab.getChildren(gameObject, transform);

      //  button.structure.children.Components = Structure.Common.getChildren(gameObject, transform);

      



        SetGeometry();
      

        //Init dropdown child if it is 
        if (button.structure.childrenTabs.Count > 1) { print("Only the first child of button will be read, please delete extra tab childs in button."); }

        if (button.structure.childrenTabs.Count > 0)  {
            // tabChild = button.structure.children.Components[0].GetComponent<TabComponent>();
            button.structure.childrenTabs[0].tab.structure.tabParent = button.structure.tabParent;
            button.structure.childrenTabs[0].tab.data.active = false;
            button.structure.childrenTabs[0].tab.data.dropdown = true;
            button.structure.childrenTabs[0].tab.geometry.depth = button.geometry.depth-1;
            // tabChild.tab.data.isRoot = true;


            button.structure.childrenTabs[0].Init();
              SetDropDownGeometry();
        }




        
        button.structure.tabParent.UpdateBodyContentSize();





    }



    private void SetDropDownGeometry() {
        //CHildren tab dropdown button
        if (button.structure.childrenTabs.Count > 0)
        {

            button.structure.childrenTabs[0].tab.geometry.tabContainer.position.pivot = SubType.PositionGeneric.Pivot.TopLeft;
            button.structure.childrenTabs[0].tab.geometry.tabContainer.position.RelY = false;
            button.structure.childrenTabs[0].tab.geometry.tabContainer.position.RelX = false;

            switch (button.dropdown.deploy)
            {
                case SubType.DropDown.Deploy.Down:
                    button.structure.childrenTabs[0].tab.geometry.tabContainer.rect.y = button.geometry.buttonBody.rect.y + button.geometry.buttonBody.rect.height;
                    break;

                case SubType.DropDown.Deploy.Up:
                    button.structure.childrenTabs[0].tab.geometry.tabContainer.rect.y = button.geometry.buttonBody.rect.y - (button.structure.childrenTabs[0].tab.geometry.tabContainer.rect.height); //+ button.geometry.container.rect.height
                    break;

                case SubType.DropDown.Deploy.Left:
                    button.structure.childrenTabs[0].tab.geometry.tabContainer.rect.x = button.geometry.buttonBody.rect.x - (button.structure.childrenTabs[0].tab.geometry.tabContainer.rect.width); //+ button.geometry.container.rect.height
                    break;

                case SubType.DropDown.Deploy.Right:
                    button.structure.childrenTabs[0].tab.geometry.tabContainer.rect.x = button.geometry.buttonBody.rect.x + button.geometry.buttonBody.rect.width; //+ button.geometry.container.rect.height
                    break;
            }

            switch (button.dropdown.expand)
            {
                case SubType.DropDown.Expand.Right:
                    button.structure.childrenTabs[0].tab.geometry.tabContainer.rect.x = button.geometry.buttonBody.rect.x;
                    break;
                case SubType.DropDown.Expand.Left:
                    button.structure.childrenTabs[0].tab.geometry.tabContainer.rect.x = button.geometry.buttonBody.rect.x - (button.structure.childrenTabs[0].tab.geometry.tabContainer.rect.width - button.geometry.buttonBody.rect.width);
                    break;

                case SubType.DropDown.Expand.Bottom:
                    button.structure.childrenTabs[0].tab.geometry.tabContainer.rect.y = button.geometry.buttonBody.rect.y;
                    break;

                case SubType.DropDown.Expand.Top:
                    button.structure.childrenTabs[0].tab.geometry.tabContainer.rect.y = button.geometry.buttonBody.rect.y - (button.structure.childrenTabs[0].tab.geometry.tabContainer.rect.height - button.geometry.buttonBody.rect.height);
                    break;

            }

        }

    }

    private void SetGeometry()
    {
        button.geometry.depth = button.structure.tabParent.tab.geometry.depth - 1;

 
        button.style.bodySkin = GenericGeometry.getSkin(button.style.bodySkin, button.style.bodySkinFollowBranch, button.structure.tabParent.tab.data.branchValues.tabButton.style.bodySkin, button.structure.tabParent.tab.data.branchValues.tabButton.style.bodySkinFollowBranch);
        GenericGeometry.ofElement Body = new GenericGeometry.ofElement(this);
   


        button.geometry.buttonBody.rect = Body.rect;
         

        
    }


    public void Draw(   ) {
      
        if (!button.data.active) { return; }
        if (button.structure.tabParent == null) { print("Parent panel of button is  null"); return; }
   //      if (editMode && (button.geometry.buttonBody.position.XStack || button.geometry.buttonBody.position.YStack) ) {  print("Cannot set editMode ON if position of element contains Xstack or Y stack"); editMode=false;   }


        if (button.structure.childrenTabs.Count > 0) { button.structure.childrenTabs[0].editMode = editMode; }
        if (editMode) { 
            SetGeometry();
            SetDropDownGeometry();
            button.structure.tabParent.UpdateBodyContentSize();
           
        }

        //Todo cannot use xstack and ystack in edit mode because updateContent Only Increases 
        //  if (editMode) { button.structure.tabParent.UpdateContentSize(button.geometry.container.rect);   }


        GUI.depth = button.geometry.depth;

       // GUI.BeginGroup(new Rect(xOffset, yOffset, button.geometry.container.rect.width, button.geometry.container.rect.height));
       
        if (GUI.Button(new Rect(button.geometry.buttonBody.rect), button.data.GUIcontent, button.style.bodySkin.button))
        {


          if(button.structure.childrenTabs.Count>0) button.structure.childrenTabs[0].tab.data.active = !button.structure.childrenTabs[0].tab.data.active;

        }
      //  GUI.EndGroup();




        if (button.structure.childrenTabs.Count > 0) {
            //  PODrawer.Tabs.Draw(button.structure.childrenTabs);

            button.structure.childrenTabs[0].Draw();

         //   Drawer.Common.Draw(button.structure.children.Components);
        
        }
     

    }


    public void printf(string str)
    {
        print(str);
    }

}


