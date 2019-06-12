using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabComponent  :MonoBehaviour
{
    public string debug;
    public bool editMode;
    public TabType.Component tab;//= new TabType.Component();
    GUISkin finalSkin;// = new GUISkin();


  
    public void Init() {
       
       

        //Get skins
        if (!tab.style.containerSkin) { tab.style.containerSkin = Resources.Load("GUI/DefaultSkins/Tab/TabContainer") as GUISkin; } 
        if (!tab.style.headerSkin) { tab.style.headerSkin = Resources.Load("GUI/DefaultSkins/Tab/TabHeader") as GUISkin; }
        if (!tab.style.headerContentSkin) { tab.style.headerContentSkin = Resources.Load("GUI/DefaultSkins/Tab/TabHeaderContent") as GUISkin; }
        if (!tab.style.bodySkin) { tab.style.bodySkin = Resources.Load("GUI/DefaultSkins/Tab/TabBody") as GUISkin; }
        if (!tab.style.contentSkin) { tab.style.contentSkin = Resources.Load("GUI/DefaultSkins/Tab/TabContent") as GUISkin; }
 
        if (!tab.tabButton.style.bodySkin) { tab.tabButton.style.bodySkin = Resources.Load("GUI/DefaultSkins/Tab/TabButton") as GUISkin; }
        if (!tab.tabButton.style.bodySkinSelected) { tab.tabButton.style.bodySkinSelected = Resources.Load("GUI/DefaultSkins/Tab/TabButtonSelected") as GUISkin; }




        //Get global params
        tab.data.branchValues = gameObject.transform.GetComponentInParent<BranchComponent>().GetComponentInParent<BranchValuesComponent>();


        //Get Structure

        //Get parent
         if (!tab.data.isRoot ) {
            if (tab.structure.tabParent == null)
            {
                tab.structure.tabParent = gameObject.transform.parent.GetComponent<TabComponent>();
            }
           // else { print("Button has set the parent"); }
        }
       // debug = tab.structure.tabParent.name;
        //Get children
        tab.structure.childrenTabs = Structure.Tab.getChildren(gameObject, transform);

 
        tab.structure.children.Components = Structure.Common.getChildren(gameObject, transform);

       
        SetGeometry();

        Drawer.Common.Init(tab.structure.children.Components, false);

        //Set geometry again after loading children in case its size must fit on them
        if (tab.geometry.tabContainer.size.hFitChildren || tab.geometry.tabContainer.size.wFitChildren) { SetGeometry(); }




        if (!tab.data.isRoot) { tab.structure.tabParent.UpdateBodyContentSize(); }




        //Select a tab
        if (tab.data.type == SubType.TabType.Tab)     {   SelectTab(0);   }

         
 
       

    }


    private void SelectTab(int index)
    {
       


        if (tab.structure.childrenTabs.Count == 0) { return; }
            ClearTabs();
        //Activate three only edit mode
        if (editMode) { if (gameObject.transform.childCount != 0) { gameObject.transform.GetChild(index).gameObject.SetActive(true); } }


        //show panel
        tab.structure.childrenTabs[index].tab.data.active = true;

        //Set  selected child
        tab.data.selectedChild = index;

    }

    private void SetGeometry() {

        //Content First
     //   UpdateBodyContentSize();
   

      // Container
        GenericGeometry.ofElement Container = new GenericGeometry.ofElement(this);


        if (tab.data.dropdown)
        {
            //Position set by button
            tab.geometry.tabContainer.rect.width = Container.rect.width;
            tab.geometry.tabContainer.rect.height = Container.rect.height;
        }
        else {
            tab.geometry.tabContainer.rect = Container.rect;
        }
     
      tab.style.containerSkin = GenericGeometry.getSkin(tab.style.containerSkin, tab.style.containerSkinFollowBranch, tab.data.branchValues.tab.style.containerSkin, tab.data.branchValues.tab.style.containerSkin);

         

        // Header
        if (tab.data.type==SubType.TabType.Tab) {

            
            tab.geometry.tabHeader.rect = new TabGeometry.Header(tab).rect;

            SetTabButtonsGeometry();



            //Recalculate header if it fits children 

            //Get Branch geometries or component geometries
           
            //SubType.SizeGeneric headerSizeValues = new SubType.SizeGeneric() ;

            bool wFitChildren=false;
            bool hFitChildren=false;

            if ((tab.geometry.tabHeader.Size.wFollowBranch || tab.data.branchValues.tab.geometry.tabHeader.Size.wFollowBranch)) { wFitChildren = tab.data.branchValues.tab.geometry.tabHeader.Size.wFitChildren; } else { wFitChildren = tab.geometry.tabHeader.Size.wFitChildren; }
            if ((tab.geometry.tabHeader.Size.hFollowBranch || tab.data.branchValues.tab.geometry.tabHeader.Size.hFollowBranch)) { hFitChildren = tab.data.branchValues.tab.geometry.tabHeader.Size.hFitChildren; } else { hFitChildren = tab.geometry.tabHeader.Size.hFitChildren; }


            //Resuze header
            if (wFitChildren) {   tab.geometry.tabHeader.rect.width = new TabGeometry.Header(tab).rect.width;   }
              if (hFitChildren) {    tab.geometry.tabHeader.rect.height = new TabGeometry.Header(tab).rect.height;  }


           tab.style.headerSkin = GenericGeometry.getSkin(tab.style.headerSkin, tab.style.headerSkinFollowBranch, tab.data.branchValues.tab.style.headerSkin, tab.data.branchValues.tab.style.headerSkin);
          tab.geometry.tabHeader.scroll.elementScrollModeStyles = GenericGeometry.getScrollStyles(tab.style.headerSkin, tab.geometry.tabHeader.scroll, tab.data.branchValues.tab.geometry.tabHeader.scroll);
             
        }


         



        // Body
        TabGeometry.Body Body = new TabGeometry.Body(tab);



      tab.geometry.tabBody.rect =Body.rect;// new Rect(Body.position, new Vector2(Body.size.w, Body.size.h));


      tab.style.bodySkin = GenericGeometry.getSkin(tab.style.bodySkin, tab.style.bodySkinFollowBranch, tab.data.branchValues.tab.style.bodySkin, tab.data.branchValues.tab.style.bodySkin);
      tab.geometry.tabBody.scroll.elementScrollModeStyles = GenericGeometry.getScrollStyles(tab.style.bodySkin, tab.geometry.tabBody.scroll, tab.data.branchValues.tab.geometry.tabBody.scroll);



        if (!tab.data.isRoot) { tab.structure.tabParent.UpdateBodyContentSize(); }

    }

  private void SetTabButtonsGeometry() {


      //Create Buttons 
     // tab.tabButtons.Clear();

          //Set buttons style 
          for (int i = 0; i < tab.structure.childrenTabs.Count; i++)
          {
           ///////   if (tab.structure.childrenTabs[i].transform.parent != transform) { continue; }


              tab.structure.childrenTabs[i].tab.tabButton.style.bodySkin = GenericGeometry.getSkin( tab.structure.childrenTabs[i].tab.tabButton.style.bodySkin, 
                                                                                     tab.structure.childrenTabs[i].tab.tabButton.style.bodySkinFollowBranch,
                                                                                     tab.data.branchValues.tabButton.style.bodySkin,
                                                                                     tab.data.branchValues.tabButton.style.bodySkinFollowBranch  ); 

          }



 
        
   Size headerSize = new Size(tab.geometry.tabHeader.rect.width, tab.geometry.tabHeader.rect.height);

           tab.geometry.tabHeaderContent.rect = Rect.zero;

      

      if (tab.geometry.tabHeader.position == SubType.THPSR.Position.Top)
      {
          float offset = 0;
          for (int i = 0; i < tab.structure.childrenTabs.Count; i++)
          {

               TabGeometry.TabButton tabButton =new TabGeometry.TabButton(new Vector2(offset, 0), headerSize, tab.structure.childrenTabs[i].tab.tabButton, tab.data.branchValues.tabButton);


             // tab.structure.childrenTabs[i].tab.tabButton.geometry.buttonBody.rect = tabButton.rect;  //   geo.ConvertHeaderButtonSize(new Vector2(m.geometryData.Header.Rect.width, m.geometryData.Header.Rect.height), m.tabButtons[i], new Vector2(0,0))
               tab.structure.childrenTabs[i].tab.tabButton.geometry.buttonBody.rect = tabButton.rect;  //   geo.ConvertHeaderButtonSize(new Vector2(m.geometryData.Header.Rect.width, m.geometryData.Header.Rect.height), m.tabButtons[i], new Vector2(0,0))
                offset += tabButton.size.w;
                tab.geometry.tabHeaderContent.rect = AddToRect(tab.geometry.tabHeaderContent.rect, tab.structure.childrenTabs[i].tab.tabButton.geometry.buttonBody.rect);

            }
        }
      else if (tab.geometry.tabHeader.position == SubType.THPSR.Position.Bottom)
      {
          float offset = 0;
          for (int i = 0; i < tab.structure.childrenTabs.Count; i++)
          {
              TabGeometry.TabButton tabButton = new TabGeometry.TabButton(new Vector2(offset, 0), headerSize, tab.structure.childrenTabs[i].tab.tabButton, tab.data.branchValues.tabButton);

              //      Vector2 buttonSize = ButtonGeometry.getContainerSize(headerSize, tab.structure.childrenTabs[i].tab.tabButton, tab.data.branchValues.tabButton);
              //  Vector2 buttonPosition = new Vector2(offset, tab.geometry.tabContainer.rect.height - tab.geometry.tabHeader.rect.height);
              //    Vector2 buttonPosition = new Vector2(offset, 0);
              tab.structure.childrenTabs[i].tab.tabButton.geometry.buttonBody.rect = tabButton.rect;// new Rect(buttonPosition, buttonSize);  //   geo.ConvertHeaderButtonSize(new Vector2(m.geometryData.Header.Rect.width, m.geometryData.Header.Rect.height), m.tabButtons[i], new Vector2(0,0))
              offset += tabButton.size.w;
                tab.geometry.tabHeaderContent.rect = AddToRect(tab.geometry.tabHeaderContent.rect, tab.structure.childrenTabs[i].tab.tabButton.geometry.buttonBody.rect);


            }
        }
      else if (tab.geometry.tabHeader.position == SubType.THPSR.Position.Left)
      {
          float offset = 0;
          for (int i = 0; i < tab.structure.childrenTabs.Count; i++)
          {
              TabGeometry.TabButton tabButton = new TabGeometry.TabButton(new Vector2( 0, offset), headerSize, tab.structure.childrenTabs[i].tab.tabButton, tab.data.branchValues.tabButton);

             tab.structure.childrenTabs[i].tab.tabButton.geometry.buttonBody.rect = tabButton.rect;
              offset += tabButton.size.h;
                tab.geometry.tabHeaderContent.rect = AddToRect(tab.geometry.tabHeaderContent.rect, tab.structure.childrenTabs[i].tab.tabButton.geometry.buttonBody.rect);

            }
        }
      else if (tab.geometry.tabHeader.position == SubType.THPSR.Position.Right)
      {
          float offset = 0;
          for (int i = 0; i < tab.structure.childrenTabs.Count; i++)
          {
              TabGeometry.TabButton tabButton = new TabGeometry.TabButton(new Vector2(0, offset), headerSize, tab.structure.childrenTabs[i].tab.tabButton, tab.data.branchValues.tabButton);

              tab.structure.childrenTabs[i].tab.tabButton.geometry.buttonBody.rect =tabButton.rect;// new Rect(buttonPosition, buttonSize);  //   geo.ConvertHeaderButtonSize(new Vector2(m.geometryData.Header.Rect.width, m.geometryData.Header.Rect.height), m.tabButtons[i], new Vector2(0,0))
              offset += tabButton.size.h;
                tab.geometry.tabHeaderContent.rect = AddToRect(tab.geometry.tabHeaderContent.rect, tab.structure.childrenTabs[i].tab.tabButton.geometry.buttonBody.rect);

            }
        }



  }


  public  void Draw()
  {
      if (!tab.data.active) { return; }
      if (!tab.data.isRoot) { if (tab.structure.tabParent == null) { print("Debug:Geometry not set"); return;  } }
    //  if (editMode && (tab.geometry.tabContainer.position.XStack || tab.geometry.tabContainer.position.YStack)) { print("Cannot set editMode ON if position of element contains Xstack or Y stack"); editMode = false; }

     // if (!editMode && !flag) { SetGeometry(); if (tab.data.type == DataSubType.Component.Tabs.SubType.Tab) { SetTabButtonsGeometry(); if (!tab.data.isRoot) { tab.structure.tabParent.UpdateBodyContentSize(); } } flag = true; }
      if (editMode)
      { 
          SetGeometry(); 
          //Tab buttons Geometry
      //   if (tab.data.type == SubType.TabType.Tab) { SetTabButtonsGeometry(); }
      }




      //Paint
      GUI.BeginGroup(tab.geometry.tabContainer.rect);

      GUI.Box(new Rect(0, 0, tab.geometry.tabContainer.rect.width, tab.geometry.tabContainer.rect.height), "", tab.style.containerSkin.box);
      //GUI.skin = tab.style.headerSkin;


      if (tab.data.type == SubType.TabType.Tab)
      {
          GUI.Box(tab.geometry.tabHeader.rect, "", tab.style.headerSkin.box);


          // tab.geometry.headerScrollViewVector = GUI.BeginScrollView(new Rect(0, 0, tab.geometry.tabHeader.rect.width, tab.geometry.tabHeader.rect.height), tab.geometry.headerScrollViewVector, new Rect(0, 0, tab.geometry.tabHeaderContent.rect.width, tab.geometry.tabHeaderContent.rect.height),false,false, tab.geometry.tabHeader.scroll.elementScrollModeStyles[0], tab.geometry.tabHeader.scroll.elementScrollModeStyles[1]);
          tab.geometry.headerScrollViewVector = GUI.BeginScrollView(tab.geometry.tabHeader.rect, tab.geometry.headerScrollViewVector, new Rect(0, 0, tab.geometry.tabHeaderContent.rect.width, tab.geometry.tabHeaderContent.rect.height), false, false, tab.geometry.tabHeader.scroll.elementScrollModeStyles[0], tab.geometry.tabHeader.scroll.elementScrollModeStyles[1]);



    //      tab.geometry.tabHeaderContent.rect = Rect.zero;
        
            for (int i = 0; i < tab.structure.childrenTabs.Count; i++)
          {

if (tab.data.selectedChild==i) { finalSkin = tab.structure.childrenTabs[i].tab.tabButton.style.bodySkinSelected; }else   { finalSkin = tab.structure.childrenTabs[i].tab.tabButton.style.bodySkin; } 

              if (GUI.Button(tab.structure.childrenTabs[i].tab.tabButton.geometry.buttonBody.rect, tab.structure.childrenTabs[i].tab.tabButton.data.GUIcontent, finalSkin.button))
              {

                    SelectTab(i);
                 //   ClearTabs();

//                  tab.structure.childrenTabs[i].tab.data.active = true;
  //                if (editMode) { gameObject.transform.GetChild(i).gameObject.SetActive(true); }

              }
         //     tab.geometry.tabHeaderContent.rect = AddToRect(tab.geometry.tabHeaderContent.rect, tab.structure.childrenTabs[i].tab.tabButton.geometry.buttonBody.rect);
          }

          GUI.EndScrollView();
      }

      //Paint Tabbuttons ed
     // GUI.skin = tab.style.bodySkin;
      GUI.Box(tab.geometry.tabBody.rect, "");
    //  GUI.Box(tab.geometry.body.rect, "", tab.style.bodySkin.box);

          GUI.BeginGroup(tab.geometry.tabBody.rect);

      tab.geometry.bodyScrollViewVector = GUI.BeginScrollView(new Rect(0, 0, tab.geometry.tabBody.rect.width, tab.geometry.tabBody.rect.height), tab.geometry.bodyScrollViewVector, new Rect(0, 0, tab.geometry.tabContent.rect.width, tab.geometry.tabContent.rect.height),false,false, tab.geometry.tabBody.scroll.elementScrollModeStyles[0], tab.geometry.tabBody.scroll.elementScrollModeStyles[1]);
      GUI.Box(new Rect(0, 0, tab.geometry.tabContent.rect.width, tab.geometry.tabContent.rect.height), "", tab.style.contentSkin.box);

      //Content here


      /*
      PODrawer.Tabs.Draw(tab.structure.childrenTabs); 
      PODrawer.Button.Draw(tab.structure.childrenButtons);
      PODrawer.TextArea.Draw(tab.structure.childrenTextAreas);
      */


        //List<Component> list = new List<Component>();
        //   list = Structure.Common.getChildren(gameObject, transform);

        /*  List<Component> c = tab.structure.children.Components;
          for (int i = 0; i < c.Count; i++)
          {
              TabComponent tab = c[i].GetComponent<TabComponent>();
              TextareaComponent textarea = c[i].GetComponent<TextareaComponent>();
              ButtonComponent button = c[i].GetComponent<ButtonComponent>();

              if (tab != null) { tab.Draw(); }
              if (textarea != null) { textarea.Draw(); }
              if (button != null) { button.Draw(); }

          }*/

        Drawer.Common.Draw(tab.structure.children.Components);

        GUI.EndScrollView();
            GUI.EndGroup();
        GUI.EndGroup();

   
    }





    public void ClearTabs() {
        for (int i = 0; i < tab.structure.childrenTabs.Count; i++)
        {
            tab.structure.childrenTabs[i].tab.data.active = false;
            if (editMode) { gameObject.transform.GetChild(i).gameObject.SetActive(false); }
        }
        }


    public void UpdateBodyContentSize()
    {
        tab.geometry.tabContent.rect.width = 0;
        tab.geometry.tabContent.rect.height = 0;
     
        /*
     
        for (int i= 0;i< tab.structure.childrenTabs.Count;i++) { tab.geometry.tabContent.rect = AddToRect(tab.geometry.tabContent.rect, tab.structure.childrenTabs[i].tab.geometry.tabContainer.rect); }
        for (int i= 0;i< tab.structure.childrenTextAreas.Count;i++) { tab.geometry.tabContent.rect = AddToRect(tab.geometry.tabContent.rect, tab.structure.childrenTextAreas[i].textarea.geometry.textareaBody.rect); }
        for (int i= 0;i< tab.structure.childrenButtons.Count;i++) { tab.geometry.tabContent.rect = AddToRect(tab.geometry.tabContent.rect, tab.structure.childrenButtons[i].button.geometry.buttonBody.rect); }
        */
        for (int i= 0;i<tab.structure.children.Components.Count;i++) {

            TabComponent childTab = tab.structure.children.Components[i].GetComponent<TabComponent>();
            ButtonComponent childButton = tab.structure.children.Components[i].GetComponent<ButtonComponent>();
            TextareaComponent childTextarea = tab.structure.children.Components[i].GetComponent<TextareaComponent>();

            if (childTab!=null) {  tab.geometry.tabContent.rect = AddToRect(tab.geometry.tabContent.rect, childTab.tab.geometry.tabContainer.rect); } else 
            if (childButton != null) { tab.geometry.tabContent.rect = AddToRect(tab.geometry.tabContent.rect, childButton.button.geometry.buttonBody.rect); } else 
            if (childTextarea != null) { tab.geometry.tabContent.rect = AddToRect(tab.geometry.tabContent.rect, childTextarea.textarea.geometry.textareaBody.rect); }  


        }

    }
    /*
        public void addToBodyContent(Rect elementRect)
    {
        
            float ElementsMaxY= elementRect.height + elementRect.y;
           if (ElementsMaxY > tab.geometry.content.rect.height)
            {
            tab.geometry.content.rect.height = ElementsMaxY; 
             }




        float ElementsMaxX = elementRect.width + elementRect.x;
 
        if (ElementsMaxX > tab.geometry.content.rect.width)
            {
            tab.geometry.content.rect.width = ElementsMaxX;
            //   geometryData.Content.Rect.width= CurrentMaxX;
        }



    }
    */
    public Rect AddToRect(Rect rect,Rect rectToadd)
    {

        float ElementsMaxY = rectToadd.height + rectToadd.y;
        if (ElementsMaxY >  rect.height)
        {
           rect.height = ElementsMaxY;
        }




        float ElementsMaxX = rectToadd.width + rectToadd.x;

        if (ElementsMaxX > rect.width)
        {
            rect.width = ElementsMaxX;
           
        }

        return rect;

    }




  public  void printf(string str) {
        print(str);
    }
}


