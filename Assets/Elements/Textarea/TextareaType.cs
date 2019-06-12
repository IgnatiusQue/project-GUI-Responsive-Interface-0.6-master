using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextareaType
{

    [Serializable]
    public class Component
    {
        public Data data;
        public Geometry geometry;
        public Style style;
        public Structure structure;


    }

    [Serializable]
    public class Data
    {
        public bool active = true;
        public string name;
        public GUIContent GUIContent = new GUIContent();
        // public string Text;
    }

    [Serializable]
    public class Geometry
    {
        public int depth;


        public SubType.Scroll scroll;
        public SubType.PSR textareaBody = new SubType.PSR(); 
        public SubType.Rect textareaContent = new SubType.Rect();
        [HideInInspector]
        public Vector2 bodyScrollViewVector = Vector2.zero;
        [HideInInspector]
        public string objTextarea;
       


    }

    [Serializable]
    public class Structure
    {
        public TabComponent tabParent;
    }

    [Serializable]
    public class Style
    {
        public bool bodySkinFollowBranch = false;
        public GUISkin bodySkin;
        public bool contentSkinFollowBranch = false;
        public GUISkin contentSkin;

    }


    public static GenericType.Component Generic(TextareaType.Component textarea)
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
        gen.type = "Textarea";
        //Data
        gen.data.name = textarea.data.name;
        gen.data.active = textarea.data.active;
        gen.data.GUIcontent = textarea.data.GUIContent;


        // Geometry rects
        gen.geometry.depth = textarea.geometry.depth;

        //Generic
        gen.geometry.body.rect = textarea.geometry.textareaBody.rect;
        gen.geometry.content.rect = textarea.geometry.textareaContent.rect;


        //Xpecific
        gen.geometry.textareaBody = textarea.geometry.textareaBody;
        gen.geometry.textareaContent = textarea.geometry.textareaContent;




        // Structure
        gen.structure.tabParent = textarea.structure.tabParent;


        // Style

        gen.style.bodySkinFollowBranch = textarea.style.bodySkinFollowBranch;
         gen.style.bodySkin = textarea.style.bodySkin;
        gen.style.contentSkinFollowBranch = textarea.style.contentSkinFollowBranch;

        gen.style.contentSkin = textarea.style.contentSkin;  //




        return gen;

    }

}

