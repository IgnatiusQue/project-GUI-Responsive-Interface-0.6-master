using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonType
{

    [Serializable]
    public class Component
    {


        public Data data;

        public Style style = new Style();
        public Geometry geometry = new Geometry();
        public SubType.DropDown dropdown = new SubType.DropDown();
        public Structure structure;


    }

    [Serializable]
    public class Data
    {
        public bool active = true;
        public string name;
        public GUIContent GUIcontent = new GUIContent();
    }

    [Serializable]
    public class Geometry
    {
        public int depth;


        public SubType.PSR buttonBody = new SubType.PSR();

    }

    [Serializable]
    public class Structure
    {
      //  [HideInInspector]
        public TabComponent tabParent;
      //  [HideInInspector]
        public List<TabComponent> childrenTabs = new List<TabComponent>();

     //   public SubType.ComponentList children = new SubType.ComponentList();
    }

    [Serializable]
    public class Style
    {
        public bool bodySkinFollowBranch = false;
        public GUISkin bodySkin;

        public bool selectedBodySkinFollowBranch = false;
        public GUISkin bodySkinSelected;

    }


    //Get Generic
    public static GenericType.Component Generic(ButtonType.Component button)
    {
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
        gen.type = "Button";

        //Data
        gen.data.name = button.data.name;
        gen.data.active = button.data.active;
        gen.data.GUIcontent = button.data.GUIcontent;
        //GUIContent

        // Geometry Generic
        gen.geometry.depth = button.geometry.depth; 
        gen.geometry.body.rect = button.geometry.buttonBody.rect;

        //Geometry xpecific
        gen.geometry.buttonBody = button.geometry.buttonBody;
       // gen.geometry.buttonBody.rect = button.geometry.buttonBody.rect;
       // gen.geometry.buttonBody.size = button.geometry.buttonBody.size;

      

    // Structure
         gen.structure.tabParent = button.structure.tabParent;
        gen.structure.childrenTabs = button.structure.childrenTabs;
       // gen.structure.children = button.structure.children;

        // Style

        gen.style.bodySkinFollowBranch = button.style.bodySkinFollowBranch;
        gen.style.bodySkin = button.style.bodySkin;





        return gen;

    }
}

