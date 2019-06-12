using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextareaGeometry {

    public static Size getContentSize(TextareaComponent _textarea)
    {
 
       
        TextareaType.Component textarea = _textarea.textarea;
        Size size = new Size(0, 0);
        Size bodySize = new GenericGeometry.GenericSize(_textarea).size;


         
        size.w =bodySize.w;
        size.h = textarea.style.bodySkin.textArea.CalcHeight(textarea.data.GUIContent, size.w);
        //size.h = b.textArea.CalcHeight(textarea.data.GUIContent, size.w);
     
        
        // size.w = textarea.style.contentSkin.textArea.CalcSize(textarea.data.GUIContent).x;
       // size.h = new GenericGeometry.GenericSize(_textarea).size.h;


        return size;
    }
     

    
    }
