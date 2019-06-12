using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericGeometry {


    public static GUISkin getSkin(GUISkin elementSkin, bool elementSkinFollowGlobal, GUISkin globalSkin, bool globalSkinFollowBranch)
    {
        if (elementSkinFollowGlobal || globalSkinFollowBranch)
        {
            return globalSkin;
        }
        else
        {

            return elementSkin;
        }

    }

    public static Rect getRectWithScrollEdges(GUISkin elementSkin, Rect rect, SubType.Scroll componentValue, SubType.Scroll globalParam)
    {


        if (componentValue.ScrollFollowBranch || globalParam.ScrollFollowBranch)
        {
            componentValue.Edge = globalParam.Edge;
            componentValue.Mode = globalParam.Mode;
        }


        switch (componentValue.Edge)
        {
            case SubType.Scroll.scrollEdge.Inner:
                switch (componentValue.Mode)
                {
                    case SubType.Scroll.scrollMode.Vertical:
                        rect.width = rect.width - elementSkin.verticalScrollbar.fixedWidth - 1;
                        break;

                    case SubType.Scroll.scrollMode.Horizontal:
                        rect.height = rect.height - elementSkin.horizontalScrollbar.fixedHeight - 1;
                        break;
                    case SubType.Scroll.scrollMode.Both:
                        rect.width = rect.width - elementSkin.verticalScrollbar.fixedWidth - 1;
                        rect.height = rect.height - elementSkin.horizontalScrollbar.fixedHeight - 1;
                        break;

                }

                break;
            case SubType.Scroll.scrollEdge.Normal:
                rect.width = rect.width;
                rect.height = rect.height;
                break;
            case SubType.Scroll.scrollEdge.Outer:
                switch (componentValue.Mode)
                {
                    case SubType.Scroll.scrollMode.Vertical:
                        rect.width = rect.width + elementSkin.verticalScrollbar.fixedWidth;
                        break;

                    case SubType.Scroll.scrollMode.Horizontal:
                        rect.height = rect.height + elementSkin.horizontalScrollbar.fixedHeight;
                        break;
                    case SubType.Scroll.scrollMode.Both:
                        rect.width = rect.width + elementSkin.verticalScrollbar.fixedWidth;
                        rect.height = rect.height + elementSkin.horizontalScrollbar.fixedHeight;
                        break;

                }
                break;
        }

        return rect;
    }


    public static GUIStyle[] getScrollStyles(GUISkin ScrollSKin, SubType.Scroll componentValue, SubType.Scroll globalParam)
    {
        GUIStyle[] res = { null, null };
        if (componentValue.ScrollFollowBranch || globalParam.ScrollFollowBranch)
        { componentValue.Mode = globalParam.Mode; }
        res[0] = new GUIStyle(ScrollSKin.horizontalScrollbar);
        res[1] = new GUIStyle(ScrollSKin.verticalScrollbar);

        switch (componentValue.Mode)
        {
            case SubType.Scroll.scrollMode.Vertical:
                res[0].fixedHeight = 0;
                res[1].fixedWidth = ScrollSKin.verticalScrollbar.fixedWidth;
                break;
            case SubType.Scroll.scrollMode.Horizontal:
                res[0].fixedHeight = ScrollSKin.horizontalScrollbar.fixedHeight;
                res[1].fixedWidth = 0;
                break;
            case SubType.Scroll.scrollMode.Both:
                res[0].fixedHeight = ScrollSKin.horizontalScrollbar.fixedHeight;
                res[1].fixedWidth = ScrollSKin.verticalScrollbar.fixedWidth;
                break;
            case SubType.Scroll.scrollMode.None:
                res[0].fixedHeight = 0;
                res[1].fixedWidth = 0;
                break;
        }

        return res;
    }




    public class ofElement{

     public   Size size;
        public Vector2 position;
        public Rect rect;
        public ofElement(Component _ele) {
            size = GetSize(_ele);
            position = GetPosition(_ele,size);
            rect =  new Rect(position, new Vector2(size.w, size.h));
        }


        public static Size GetSize(Component _ele) { return new GenericSize(_ele).size; }

        public static Vector2 GetPosition(Component _ele,Size eSize) { return new GenericPosition(_ele, eSize).position; }

    }


    private class GenericValues
    {
        public TabType.Component parent = new TabType.Component(); 
        public GenericType.Component ele= new GenericType.Component();
        public SubType.PositionGeneric componentPosition = new SubType.PositionGeneric();
        public SubType.PositionGeneric branchPosition = new SubType.PositionGeneric();

        public SubType.SizeGeneric componentSize = new SubType.SizeGeneric();
        public SubType.SizeGeneric branchSize = new SubType.SizeGeneric();



        // Get parent

       

public   GenericValues(Component _ele) {

            //Get parent
            TabComponent _parent = _ele.transform.parent.GetComponent<TabComponent>();
            if (_parent != null)
            {
                parent = _parent.tab;
            }
            else
            {
                parent.geometry = new TabType.Geometry();
                parent.geometry.tabContainer.rect.width = Screen.width;
                parent.geometry.tabContainer.rect.height = Screen.height;
                parent.geometry.tabBody.rect.width = Screen.width;
                parent.geometry.tabBody.rect.height = Screen.height;
                parent.data = new TabType.Data();
                parent.data.type = SubType.TabType.Panel;

            }

            //Get values by component
            BranchValuesComponent _branchValues = _ele.GetComponentInParent<BranchValuesComponent>(); 
            if (_ele.GetType() == typeof(TabComponent))
            {
                ele = TabType.Generic(_ele.GetComponents<TabComponent>()[0].tab);
                componentPosition = ele.geometry.tabContainer.position;
                branchPosition = _branchValues.tab.geometry.tabContainer.position;
                componentSize = ele.geometry.tabContainer.size;
                branchSize = _branchValues.tab.geometry.tabContainer.size;
               
            }
            else if (_ele.GetType() == typeof(ButtonComponent))
            {
                ele = ButtonType.Generic(_ele.GetComponents<ButtonComponent>()[0].button);
                componentPosition = ele.geometry.buttonBody.position;
                branchPosition = _branchValues.button.geometry.buttonBody.position;
                componentSize = ele.geometry.buttonBody.size;
                branchSize = _branchValues.button.geometry.buttonBody.size;
            }
            else if (_ele.GetType() == typeof(TextareaComponent))
            {
                ele = TextareaType.Generic(_ele.GetComponents<TextareaComponent>()[0].textarea);
                componentPosition = ele.geometry.textareaBody.position;
                branchPosition = _branchValues.textarea.geometry.textareaBody.position;
                componentSize = ele.geometry.textareaBody.size;
                branchSize = _branchValues.textarea.geometry.textareaBody.size;
            }

        }
    }



    public class GenericSize {

       public Size size =new Size();

        TabType.Component parent = new TabType.Component();
        GenericType.Component ele = new GenericType.Component();
        SubType.SizeGeneric componentValues = new SubType.SizeGeneric();
        SubType.SizeGeneric branchValues = new SubType.SizeGeneric();
        Component _ele = new Component();
        public GenericSize(Component _e) {
             
            GenericValues val = new GenericValues(_e);
            _ele = _e;
            componentValues = val.componentSize;
            branchValues = val.branchSize;
            parent = val.parent;
            ele = val.ele;
           
            SetWidth(); 
            SetHeight();
            SetLimits();

        } 

        private void SetWidth() {
            SubType.SizeGeneric values = componentValues;
            
            //branch
            if (values.wFollowBranch || branchValues.wFollowBranch) { values = branchValues; }

            //wfit
            if (values.wFitChildren )
            {

            if (ele.type == "Tab") { 
                if (ele.data.type == SubType.TabType.Tab && (ele.geometry.tabHeader.position == SubType.THPSR.Position.Left || ele.geometry.tabHeader.position == SubType.THPSR.Position.Right))
               {
                    size.w = ele.geometry.tabContent.rect.width + ele.geometry.tabHeader.rect.width; return;
               }
                else
                {
                    size.w = ele.geometry.tabContent.rect.width; return;
               } 
                }
                 
            //    if (ele.type == "Textarea") { size.w = ele.style.bodySkin.textArea.CalcSize(ele.data.GUIcontent).x; return; }
                if (ele.type == "Button") { size.w = ele.style.bodySkin.button.CalcSize(ele.data.GUIcontent).x; return; }

            }
           
            //Relative
            if (values.relWidth)  {   size.w = values.width * parent.geometry.tabBody.rect.width; return;  }
          
            //Literal
            size.w = values.width; return;



        }

        private void SetHeight() {
            SubType.SizeGeneric values = componentValues;
            //branch
            if (componentValues.hFollowBranch || branchValues.hFollowBranch) { values = branchValues; }


            //hfit 
            if (values.hFitChildren)
            {

                if (ele.type == "Tab")
                {
                    if (ele.data.type == SubType.TabType.Tab && (ele.geometry.tabHeader.position == SubType.THPSR.Position.Top || ele.geometry.tabHeader.position == SubType.THPSR.Position.Bottom))
                    {
                        size.h = ele.geometry.tabContent.rect.height + ele.geometry.tabHeader.rect.height; return;
                    }
                    { size.h = ele.geometry.content.rect.height; return; }

                }

                if (ele.type == "Textarea") {
                 
                    size.h = ele.geometry.textareaContent.rect.height; return;
                   // size.h = TextareaGeometry.getContentSize3(_ele).h; return;
                 //   size.h = TextareaGeometry.getContentSize2(ele).h; return;
                }
            //    if (ele.type == "Textarea") { size.h = ele.style.contentSkin.textArea.CalcSize(ele.data.GUIcontent).y; return; }
              //  if (ele.type == "Textarea") { size.h = ele.style.contentSkin.textArea.CalcHeight(ele.data.GUIcontent, size.w); }
                /*    if (ele.type == "Textarea") {
                     Size ContentSize = TextareaGeometry.getContentSize(ele);
                     ele.geometry.textareaContent.rect = new Rect(0, 0, ContentSize.w, ContentSize.h);
                     size.h =; return;
                 }
                if (ele.type == "Textarea")
                 {
                  Size ContentSize = TextareaGeometry.getContentSize(ele);
                   ele.geometry.textareaContent.rect = new Rect(0, 0, ContentSize.w, ContentSize.h);
                  //   size.h = ContentSize.h;
                     size.h = ele.style.contentSkin.textArea.CalcHeight(ele.data.GUIcontent,size.w);
                     return;

                 } */
                if (ele.type == "Button") { size.h = ele.style.bodySkin.button.CalcSize(ele.data.GUIcontent).y; return; }
                 
            }


            //Relative
            if (values.relHeight) {    size.h = values.height * parent.geometry.tabBody.rect.height;return; }

            //Literal
            size.h = values.height; return;


        }

        private void SetLimits()
        {
            SubType.SizeGeneric values = componentValues;
            //Xbranch
            if (values.wFollowBranch || branchValues.wFollowBranch) { values = branchValues; }
            //Xmax
            if (values.wLimits.max > 0)
            {   if (!values.wLimits.hideAfterMax && (size.w > values.wLimits.max)) { size.w = values.wLimits.max; }
                if (values.wLimits.hideAfterMax && (size.w > values.wLimits.max)) { size.w = 0; } 
            }
            //Xmin
            if (values.wLimits.min > 0)
            {
                if (!values.wLimits.hideBeforeMin && (size.w < values.wLimits.min)) { size.w = values.wLimits.min; }
                if (values.wLimits.hideBeforeMin && (size.w < values.wLimits.min)) { size.w = 0; }
            }
            //YBranch
            if (values.hFollowBranch || branchValues.hFollowBranch) { values = branchValues; }
            //Ymax
            if (values.hLimits.max > 0)
            {
                if (!values.hLimits.hideAfterMax && (size.h > values.hLimits.max)) { size.h = values.hLimits.max; }
                if (values.hLimits.hideAfterMax && (size.h > values.hLimits.max)) { size.h = 0; }
            }
            //Ymin
            if (values.hLimits.min > 0)
            {
                if (!values.hLimits.hideBeforeMin && (size.h < values.hLimits.min)) { size.h = values.hLimits.min; }
                if (values.hLimits.hideBeforeMin && (size.h < values.hLimits.min)) { size.h = 0; }
            }

        }
      

    }



    private class GenericPosition
    {

        public Vector2 position = new Vector2();

        TabType.Component parent = new TabType.Component();
        Component _ele = new Component();
        GenericType.Component ele = new GenericType.Component();
        SubType.PositionGeneric componentValues = new SubType.PositionGeneric();
        SubType.PositionGeneric branchValues = new SubType.PositionGeneric();


        int index ;
        int brotherCount;

        public GenericPosition(Component _e, Size eSize)
        {
              _ele = _e;
            GenericValues val = new GenericValues(_ele);
            componentValues = val.componentPosition;
            branchValues = val.branchPosition;
            parent = val.parent;
            ele = val.ele;

            index = _ele.transform.GetSiblingIndex();
            brotherCount = _ele.transform.parent.childCount;

            SetX(eSize);
            SetY(eSize);
            SetPivot(eSize);
            setXStick(eSize);


             }

        private void SetX(Size eSize)
        {


            //Values from component or branch
           SubType.PositionGeneric values = componentValues; 
             if (values.xFollowBranch || branchValues.xFollowBranch) { values = branchValues; }
            // if Yfill X set in SetY
            if (values.YFill) { return; }


            //XStick
            if (values.xStick != SubType.PositionGeneric.xstick.None) {
                setXStick(eSize);
                if (values.xMirror) { position.x = parent.geometry.tabBody.rect.width - eSize.w - position.x; }
                return;
            }


            //XFill
            if (values.XFill) { // all children must have same size
                 
             

                int pW = (int)parent.geometry.tabBody.rect.width;
                //int eW = (int)eSize.w; 
                int eW = (int)eSize.w; 
                int elementCountByRow = pW / eW; 
                position.x = eSize.w * ((index ) % elementCountByRow);
                position.y = eSize.h * ((index  ) / elementCountByRow);
                if (values.xMirror) { position.x = parent.geometry.tabBody.rect.width - eSize.w - position.x; }
                if (values.yMirror) { position.y = parent.geometry.tabBody.rect.height - eSize.h - position.y; } 
                return; 
                }
            
            //Xstack
            if (values.XStack)
            {

                int index = _ele.transform.GetSiblingIndex();
                 int brotherCount = _ele.transform.parent.childCount; 
                int i = 0;
                if (index > 0) { i = index - 1; }

                TabComponent brotherTab = _ele.transform.parent.GetChild(i).GetComponent<TabComponent>();
                ButtonComponent brotherButton = _ele.transform.parent.GetChild(i).GetComponent<ButtonComponent>();
                TextareaComponent brotherTextarea = _ele.transform.parent.GetChild(i).GetComponent<TextareaComponent>();

                Rect container = new Rect(0,0,0,0);

                if (brotherTab != null) { container = brotherTab.tab.geometry.tabContainer.rect; }
                if (brotherButton != null) { container = brotherButton.button.geometry.buttonBody.rect; }
                if (brotherTextarea != null) { container = brotherTextarea.textarea.geometry.textareaBody.rect; }
                 
                if (values.xMirror)   {  position.x = container.x - eSize.w;return;}
                if (!values.xMirror) { position.x = container.width + container.x; return; }

               

            }
          
            //x rel
            if (values.RelX) {
                position.x = values.x * parent.geometry.tabContainer.rect.width;
                if (values.xMirror) { position.x = parent.geometry.tabBody.rect.width - eSize.w- position.x; }
                return;
            }

            //x literal

            position.x = values.x;
            if (values.xMirror) { position.x = parent.geometry.tabBody.rect.width - eSize.w - position.x; }
            return;







        }
        private void SetY(Size eSize)

        {  //Y

              SubType.PositionGeneric values = componentValues;
            //SubType.PositionGeneric values;

            //branch
           if (componentValues.yFollowBranch || branchValues.yFollowBranch) { componentValues = branchValues; }

            if (values.XFill)  { return; } //  Y is set by setX


            //YStick
            if (values.yStick != SubType.PositionGeneric.ystick.None)
            {
                setYStick(eSize);
                if (values.yMirror) { position.y = eSize.h - position.y; }
                return;
            }

            //YFill
            if (values.YFill)
            { // all children must have same size



                int pH = (int)parent.geometry.tabBody.rect.height;
                int eH = (int)eSize.h;
                int elementCountByCol = pH / eH;
                position.y = eSize.h * ((index) % elementCountByCol);
                position.x = eSize.w * ((index) / elementCountByCol);
                if (values.yMirror) { position.y = parent.geometry.tabBody.rect.height - eSize.h - position.y; }
                if (values.xMirror) { position.x = parent.geometry.tabBody.rect.width - eSize.w - position.x; }
                return;
            }



            //Ystack
            if (values.YStack)  {
                int index = _ele.transform.GetSiblingIndex();
                int brotherCount = _ele.transform.parent.childCount;
                int i = 0;
                if (index > 0) { i = index - 1; }

                TabComponent brotherTab = _ele.transform.parent.GetChild(i).GetComponent<TabComponent>();
                ButtonComponent brotherButton = _ele.transform.parent.GetChild(i).GetComponent<ButtonComponent>();
                TextareaComponent brotherTextarea = _ele.transform.parent.GetChild(i).GetComponent<TextareaComponent>();

                Rect container = new Rect(0, 0, 0, 0);

                if (brotherTab != null) { container = brotherTab.tab.geometry.tabContainer.rect; }
                if (brotherButton != null) { container = brotherButton.button.geometry.buttonBody.rect; }
                if (brotherTextarea != null) { container = brotherTextarea.textarea.geometry.textareaBody.rect; }

                if (values.yMirror) { position.y = container.y - eSize.h; return; }
                if (!values.yMirror) { position.y = container.height + container.y; return; }

                    /*
                    position.y = parent.geometry.tabContent.rect.height;
                    if (values.yMirror) { position.y = parent.geometry.tabBody.rect.height - eSize.h - position.y; }
                    return;

                    */
                }

                //Yrel
                if (values.RelY) {
                position.y = values.y * parent.geometry.tabContainer.rect.height;
            if (values.yMirror) { position.y = parent.geometry.tabBody.rect.height - eSize.h - position.y; }
                return;
            }

           //YLIteral
            position.y = values.y;
            if (values.yMirror) { position.y = parent.geometry.tabBody.rect.height - eSize.h - position.y; }
            return;

            
        }

        private void SetPivot(Size eSize)
        {
            SubType.PositionGeneric values = componentValues;
            //branch
            if (values.pivotFollowBranch || branchValues.pivotFollowBranch) { values = branchValues; }


            switch (values.pivot)
            {
                case SubType.PositionGeneric.Pivot.Center:

                    position.x = position.x + (-eSize.w / 2);
                    position.y = position.y + (-eSize.h / 2);
                    break;

                case SubType.PositionGeneric.Pivot.TopLeft:
                    //Default Position
                    break;
            }
        }

        private void setXStick(Size eSize)
        {
        
            switch (componentValues.xStick)
            {
             //   case SubType.PositionGeneric.xstick.None:return;break;


                case SubType.PositionGeneric.xstick.Left:
                    position.x = 0;
                    break;
                case SubType.PositionGeneric.xstick.Right:
                    position.x = parent.geometry.tabBody.rect.width - eSize.w;
                    break;
                case SubType.PositionGeneric.xstick.Center:
                    position.x = (parent.geometry.tabBody.rect.width / 2) - (eSize.w / 2);
                    break;

                

               
            }
        }

        private void setYStick(Size eSize)
        {

            switch (componentValues.yStick)
            {
                //   case SubType.PositionGeneric.xstick.None:return;break;


                case SubType.PositionGeneric.ystick.Top:
                    position.y = 0;
                    break;
                case SubType.PositionGeneric.ystick.Bottom:
                    position.y = parent.geometry.tabBody.rect.height - eSize.h;
                    break;
                case SubType.PositionGeneric.ystick.Center:
                    position.y = (parent.geometry.tabBody.rect.height / 2) - (eSize.h / 2);
                    break;




            }
        }

    } 
    }
