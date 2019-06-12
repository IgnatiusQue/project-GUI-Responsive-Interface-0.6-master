using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  
    public class GenericType
    {

        [Serializable]
        public class Component
        {
           public string type;
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
            public SubType.TabType type = SubType.TabType.Panel;
          
         
            public GUIContent GUIcontent = new GUIContent();
         public int index;
        }

        [Serializable]
        public class Geometry
        {
            public int depth;

         // Generic geometries 
            public SubType.Rect header = new SubType.Rect();
            public SubType.Rect headerContent = new SubType.Rect();
            public SubType.Rect container = new SubType.Rect();
            public SubType.Rect body = new SubType.Rect();
            public SubType.Rect content = new SubType.Rect();   

            public Vector2 bodyScrollViewVector = Vector2.zero; 
            public Vector2 headerScrollViewVector = Vector2.zero;

        //Tab
        public SubType.THPSR tabHeader = new SubType.THPSR();
        public SubType.PSR tabContainer = new SubType.PSR();
        public SubType.SR tabBody = new SubType.SR();
        public SubType.Rect tabContent = new SubType.Rect();
        public SubType.Rect tabHeaderContent = new SubType.Rect();


        //Textarea 
        public SubType.PSR textareaBody = new SubType.PSR();
        public SubType.Rect textareaContent = new SubType.Rect();
        public SubType.Scroll scroll; // Textarea uses it 
           public string objTextarea; // Textarea uses it 

        //Button
        public SubType.PSR buttonBody = new SubType.PSR();


    }

        [Serializable]
        public class Structure
        {
            [HideInInspector]
            public TabComponent tabParent;
            [HideInInspector]
            public List<TabComponent> childrenTabs = new List<TabComponent>();
            [HideInInspector]
            public List<ButtonComponent> childrenButtons = new List<ButtonComponent>();
            [HideInInspector]
            public List<TextareaComponent> childrenTextAreas = new List<TextareaComponent>();

           // public List<Component> children= new List<Component>();
         public  SubType.ComponentList children =  new SubType.ComponentList();
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

            public bool contentSkinFollowBranch = false;
            public GUISkin contentSkin; //debug onlu

        }

    }


    public class SubType {

        [Serializable]
        public enum TabType { Tab, Panel, Paginator, Root }

        [Serializable]
        public class DropDown
        {
            public bool deployFollowBranch = false;
            public enum Deploy { Left, Right, Up, Down }
            public Deploy deploy = Deploy.Down;
            public enum Expand { Left, Right, Top, Bottom }
            public Expand expand = Expand.Left;
        }

        [Serializable]
        public class Rect // Position Size Rect
        {
            public UnityEngine.Rect rect = new UnityEngine.Rect();
        }


    [Serializable]
    public class ComponentList // Position Size Rect
    {
        // public UnityEngine.Component component = new UnityEngine.Component();
        public List<Component> Components = new List<Component>();
    }
    [Serializable]
        public class PSR // Position Size Rect
        {
            public PositionGeneric position;
            public SizeGeneric size;
            

            public UnityEngine.Rect rect;

        }

        [Serializable]
        public class THPSR // TabHeaderPosition Size Rect
        {
            public enum Position { Left, Right, Top, Bottom }
            public Position position = Position.Top;
            public Scroll scroll;
            public SizeGeneric Size;
            public UnityEngine.Rect rect;

        }
        [Serializable]
        public class SR //   Scroll Rect
        {

            public Scroll scroll;

            public UnityEngine.Rect rect;

        }

        [Serializable]
        public class SizeGeneric
        {

            public bool wFollowBranch = false;
            public bool relWidth = true;
            public float width = 0.5f;
        public bool wFitChildren = false;

        public bool hFollowBranch = false;
            public bool relHeight = true;
            public float height = 0.5f; 
        public bool hFitChildren = false;


        public LimitsGeneric hLimits; 
        public LimitsGeneric wLimits; 




    }

        [Serializable]
        public class PositionGeneric
        {
  
          public bool RelX = true;
            public float x = 0.5f;
            public bool RelY = true;
            public float y = 0.5f;

            public bool XStack = false;
            public bool YStack = false;

            public bool XFill = false;
            public bool YFill = false;

          public bool xMirror = false;
          public bool yMirror = false;

        public bool xFollowBranch = false;
        public bool yFollowBranch = false;

        public enum Pivot { Center, TopLeft }
        public Pivot pivot = Pivot.Center;
         public bool pivotFollowBranch = false;

       /* public enum stickto {None, Center_Center, Top_Left, Top_Center, Top_Right }
        public stickto stickTo = stickto.None;
        public bool stickToFollowBranch = false;*/

        public enum xstick { None, Left, Center, Right }
        public xstick xStick = xstick.None;

        public enum ystick { None, Top, Center, Bottom }
        public ystick yStick = ystick.None;
    }


    [Serializable]
    public class LimitsGeneric
    {
        public float min = 0;
        public float max = 0; 
        public bool hideBeforeMin = false;
        public bool hideAfterMax = false;


    }
        [Serializable]
        public class Scroll
        {

            public bool ScrollFollowBranch = false;



            public enum scrollMode { Horizontal, Vertical, Both, None }
            public scrollMode Mode = scrollMode.Vertical;

            public enum scrollEdge { Inner, Normal, Outer }
            public scrollEdge Edge = scrollEdge.Normal;

            public GUIStyle[] elementScrollModeStyles;

        }





    }

 
  public   class Size  {
    public float w;
    public float h;

    //Normal
    public Size(float Width,float Height) {
        w = Width;
        h = Height;
    }
    //From Rect
    public Size(Rect Rect)
    {
        w = Rect.width;
        h = Rect.height;
    }

    public Size()
    {
        w =0;
        h =0;
    }
} 

 
