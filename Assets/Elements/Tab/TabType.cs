using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TabType  {

    

        [Serializable]
        public class Component
        {
            public Data data;

            public Style style;

            public Geometry geometry;


            public ButtonType.Component tabButton;

            public Structure structure;




            // = new POGlobals();
            //  [HideInInspector]
            //  public List<DataSubType.element.Button.Component> tabButtons = new List<DataSubType.element.Button.Component>();
        }

        [Serializable]
        public class Data
        {
            public string name;
            public bool active = true;
            public bool isRoot;
            public bool dropdown=false;

            public int selectedChild=0; // only tab mode
            public SubType.TabType type = SubType.TabType.Panel;

        ///   public int index;

           [HideInInspector]
            public BranchValuesComponent branchValues;
        }

        [Serializable]
        public class Geometry
        {
            public int depth;

            public SubType.THPSR tabHeader = new SubType.THPSR();
            public SubType.PSR tabContainer = new SubType.PSR(); 
            public SubType.SR tabBody = new SubType.SR(); 
            public SubType.Rect tabContent = new SubType.Rect();
            public SubType.Rect tabHeaderContent = new SubType.Rect();

            [HideInInspector]
            public Vector2 bodyScrollViewVector = Vector2.zero;
            [HideInInspector]
            public Vector2 headerScrollViewVector = Vector2.zero;


        }

        [Serializable]
        public class Structure
        {
//[HideInInspector]
            public TabComponent tabParent;


            // public   POTabs[] childrenTabs;
            //[HideInInspector]
    //        [HideInInspector]
            public List<TabComponent> childrenTabs = new List<TabComponent>();
  //          [HideInInspector]
      //      public List<ButtonComponent> childrenButtons = new List<ButtonComponent>();
  //          [HideInInspector]
      //      public List<TextareaComponent> childrenTextAreas = new List<TextareaComponent>();

         //   public List<Component> children = new List<Component>();
             public SubType.ComponentList children = new SubType.ComponentList();
    }

        [Serializable]
        public class Style
        {
            public bool headerSkinFollowBranch = false;
            public GUISkin headerSkin;
            public GUISkin headerContentSkin; //debug onlu


            public bool containerSkinFollowBranch = false;
            public GUISkin containerSkin;

            public bool bodySkinFollowBranch = false;
            public GUISkin bodySkin;

            public GUISkin contentSkin; //debug onlu

        }

        //Get Generic
        public static GenericType.Component Generic(TabType.Component tab)
        {
        if (tab == null) { return null; }
            GenericType.Component gen = new GenericType.Component();
            GenericType.Data data = new GenericType.Data();
            GenericType.Geometry geometry = new GenericType.Geometry();
            GenericType.Structure structure = new GenericType.Structure();
            GenericType.Style style = new GenericType.Style();

            gen.data = data;
            gen.geometry = geometry;
            gen.structure = structure;
            gen.style = style;

           //generic type 
            gen.type = "Tab";

        //Data
           gen.data.name = tab.data.name;
            gen.data.active = tab.data.active;
            gen.data.type = tab.data.type;
            //gen.data.globalParams = tab.data.globalParams;

            // Full Geometries
            
            gen.geometry.tabHeader = tab.geometry.tabHeader;
            gen.geometry.tabHeaderContent = tab.geometry.tabHeaderContent;
            gen.geometry.tabContainer = tab.geometry.tabContainer;
            gen.geometry.tabBody = tab.geometry.tabBody;
            gen.geometry.tabContent = tab.geometry.tabContent;

        // Generic geometry 
        gen.geometry.depth = tab.geometry.depth;
        gen.geometry.header.rect = tab.geometry.tabHeader.rect;
        gen.geometry.headerContent.rect = tab.geometry.tabHeaderContent.rect;
        gen.geometry.container.rect = tab.geometry.tabContainer.rect;
        gen.geometry.body.rect = tab.geometry.tabBody.rect;
        gen.geometry.content.rect = tab.geometry.tabContent.rect;



        // Structure
            gen.structure.tabParent = tab.structure.tabParent;
            gen.structure.childrenTabs = tab.structure.childrenTabs;
        //    gen.structure.childrenButtons = tab.structure.childrenButtons;
       //     gen.structure.childrenTextAreas = tab.structure.childrenTextAreas;
            gen.structure.children = tab.structure.children;

            // Style
            gen.style.headerSkinFollowBranch = tab.style.headerSkinFollowBranch;
            gen.style.headerSkin = tab.style.headerSkin;
            gen.style.headerContentSkin = tab.style.headerContentSkin;
            gen.style.containerSkinFollowBranch = tab.style.containerSkinFollowBranch;
            gen.style.containerSkin = tab.style.containerSkin;
            gen.style.bodySkinFollowBranch = tab.style.bodySkinFollowBranch;
            gen.style.bodySkin = tab.style.bodySkin;
            gen.style.contentSkin = tab.style.contentSkin;  //debug only



        


            return gen;

        }

     
}
