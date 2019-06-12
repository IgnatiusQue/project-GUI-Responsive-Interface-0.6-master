using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextareaComponent : MonoBehaviour {
    private bool editModeWarningFlag;
    public bool editMode=false;
    public string debug = "";
 
    public TextareaType.Component textarea = new TextareaType.Component();
     
   
    public void Init()
    {

        if (!textarea.style.bodySkin) { textarea.style.bodySkin = Resources.Load("GUI/DefaultSkins/Textarea/TextareaBody") as GUISkin; }
        if (!textarea.style.contentSkin) { textarea.style.contentSkin = Resources.Load("GUI/DefaultSkins/Textarea/TextareaContent") as GUISkin; }
        


      textarea.structure.tabParent = gameObject.transform.parent.GetComponent<TabComponent>();

        debug = textarea.structure.tabParent.name;


    
        SetGeometry();




        // textarea.geometry.scroll.DefaultSizes.x = textarea.style.containerSkin.verticalScrollbar.fixedWidth;
        //      textarea.geometry.scroll.DefaultSizes.y = textarea.style.containerSkin.horizontalScrollbar.fixedHeight;

       
    }

    private void SetGeometry() {


        textarea.geometry.depth = textarea.structure.tabParent.tab.geometry.depth - 1;


        Size ContentSize = TextareaGeometry.getContentSize(this);
        textarea.geometry.textareaContent.rect = new Rect(0, 0, ContentSize.w, ContentSize.h);

        GenericGeometry.ofElement Body = new GenericGeometry.ofElement(this); 
        textarea.geometry.textareaBody.rect = Body.rect;// new Rect(bodyPosition.x, bodyPosition.y, bodySize.w, bodySize.h);




        // Body = new GenericGeometry.ofElement(this);
        //  textarea.geometry.textareaBody.rect = Body.rect;// new Rect(bodyPosition.x, bodyPosition.y, bodySize.w, bodySize.h);
        //    textarea.geometry.textareaBody.rect.height = textarea.geometry.textareaContent.rect.height;

        //  textarea.geometry.textareaBody.rect = new GenericGeometry.ofElement(this).rect;


        textarea.style.bodySkin = GenericGeometry.getSkin(textarea.style.bodySkin, textarea.style.bodySkinFollowBranch, textarea.structure.tabParent.tab.data.branchValues.textarea.style.bodySkin, textarea.structure.tabParent.tab.data.branchValues.textarea.style.bodySkinFollowBranch);

        textarea.geometry.scroll.elementScrollModeStyles = GenericGeometry.getScrollStyles(textarea.style.bodySkin, textarea.geometry.scroll, textarea.structure.tabParent.tab.data.branchValues.textarea.geometry.scroll);
        textarea.style.contentSkin = GenericGeometry.getSkin(textarea.style.contentSkin, textarea.style.contentSkinFollowBranch, textarea.structure.tabParent.tab.data.branchValues.textarea.style.contentSkin, textarea.structure.tabParent.tab.data.branchValues.textarea.style.contentSkinFollowBranch);




        textarea.structure.tabParent.UpdateBodyContentSize();
    }

    public	void Draw () {
        if (!textarea.data.active) { return; }
        if (textarea.structure.tabParent == null) { print("Parent panel null"); return; }
     //   if (editMode && (textarea.geometry.textareaBody.position.XStack || textarea.geometry.textareaBody.position.YStack)) { print("Cannot set editMode ON if position of element contains Xstack or Y stack"); editMode = false; }




        if (editMode) {
            SetGeometry();
            
        }




        GUI.depth = textarea.geometry.depth;

        GUI.skin = textarea.style.contentSkin;
        GUI.BeginGroup(textarea.geometry.textareaBody.rect);
         
        textarea.geometry.bodyScrollViewVector = GUI.BeginScrollView(new Rect(0, 0, textarea.geometry.textareaBody.rect.width, textarea.geometry.textareaBody.rect.height), textarea.geometry.bodyScrollViewVector, new Rect(0, 0, textarea.geometry.textareaContent.rect.width, textarea.geometry.textareaContent.rect.height),false,false, textarea.geometry.scroll.elementScrollModeStyles[0], textarea.geometry.scroll.elementScrollModeStyles[1]);
          GUI.Box(new Rect(0, 0, textarea.geometry.textareaBody.rect.width, textarea.geometry.textareaBody.rect.height), "");

      //  textarea.geometry.objTextarea = GUI.TextArea(new Rect(0, 0, textarea.geometry.textareaContent.rect.width, textarea.geometry.textareaContent.rect.height), textarea.data.GUIContent.text, textarea.style.contentSkin.textArea);
        textarea.geometry.objTextarea = GUI.TextArea(new Rect(0, 0, textarea.geometry.textareaContent.rect.width, textarea.geometry.textareaContent.rect.height), textarea.data.GUIContent.text, textarea.style.contentSkin.textArea);



        // Mandatory update always 
        //Content size
      
        //Hide Show Scrollbars
        //textarea.style.containerSkin = GenericGeometry.getScrollMode(textarea.style.containerSkin, textarea.geometry.scroll, textarea.structure.tabParent.tab.data.globalParams.textArea.geometry.scroll,editMode);


        GUI.EndScrollView();
        GUI.EndGroup();


     

       
    }

     
}


