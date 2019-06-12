 
using UnityEngine;

public class TabGeometry
{






    public class Header
    {
        public Vector2 position;
        public Size size;
        public Rect rect;
        public Header(TabType.Component tab)
        {
            size = getHeaderSize(tab);
            position = getHeaderPosition(tab, size);

            rect = new Rect(position, new Vector2(size.w, size.h));

       

            rect = GenericGeometry.getRectWithScrollEdges(tab.style.headerSkin, rect, tab.geometry.tabHeader.scroll, tab.data.branchValues.tab.geometry.tabHeader.scroll);

        }


        public static Vector2 getHeaderPosition(TabType.Component tab, Size tabHeaderSize)
        {

            Size tabContainerSize; if (tab.data.isRoot) { tabContainerSize = new Size(Screen.width, Screen.height); } else { tabContainerSize = new Size(tab.structure.tabParent.tab.geometry.tabContainer.rect); }

            //Horizontal Display
            if (tab.geometry.tabHeader.position == SubType.THPSR.Position.Top)
            {
                return new Vector2(0, 0);
            }
            else if (tab.geometry.tabHeader.position == SubType.THPSR.Position.Bottom)
            {
                return new Vector2(0, tabContainerSize.h - tabHeaderSize.h);
            }
            else if (tab.geometry.tabHeader.position == SubType.THPSR.Position.Left)
            {
                return new Vector2(0, 0);
            }
            else if (tab.geometry.tabHeader.position == SubType.THPSR.Position.Right)
            {
                return new Vector2(tabContainerSize.w - tabHeaderSize.w, 0);
            }

            return new Vector2(0, 0);
        }

        public static Size getHeaderSize(TabType.Component tab)
        {
            if (tab.data.type == SubType.TabType.Panel) { return new Size(0, 0); }

            SubType.SizeGeneric branchValues = tab.data.branchValues.tab.geometry.tabHeader.Size;
            SubType.SizeGeneric values = tab.geometry.tabHeader.Size; 
            //branch
            if (values.wFollowBranch || branchValues.wFollowBranch) { values = branchValues; }


            //TabGeometry.Body Body = new .width,
            Size tabBodySize =new Size(new Body(tab).rect);
            //if (tab.data.isRoot) { tabBodySize = new Size(Screen.width, Screen.height); } else { tabBodySize = new Size(tab.structure.tabParent.tab.geometry.tabContainer.rect); }

            Size headerContentSize = new Size(tab.geometry.tabHeaderContent.rect);

            Size size = new Size(0, 0);



            if (values.wFitChildren) { size.w = headerContentSize.w; }
            else
            {

                if (values.relWidth) { size.w = values.width * tabBodySize.w; } else { size.w = values.width; }
            }



            //Height
        values = tab.geometry.tabHeader.Size;
            if (values.hFollowBranch || branchValues.hFollowBranch) { values = branchValues; }



            if (values.hFitChildren) { size.h = headerContentSize.h; }
            else
            {
                if (values.relHeight) { size.h = values.height * tabBodySize.h; } else { size.h = values.height; }
            }



            return size;
        }


    }
    
    public class Body
    {
        public Vector2 position;
        public Size size;
        public Rect rect;
        public Body(TabType.Component tab)
        {
            if (tab.geometry.tabHeader.position == SubType.THPSR.Position.Top)
            {
                position = new Vector2(0, tab.geometry.tabHeader.rect.height);
                size = new Size(tab.geometry.tabContainer.rect.width, tab.geometry.tabContainer.rect.height - tab.geometry.tabHeader.rect.height);
            }
            else if (tab.geometry.tabHeader.position == SubType.THPSR.Position.Bottom)
            {
                position = new Vector2(0, 0);
                size = new Size(tab.geometry.tabContainer.rect.width, tab.geometry.tabContainer.rect.height - tab.geometry.tabHeader.rect.height);

            }
            else if (tab.geometry.tabHeader.position == SubType.THPSR.Position.Left)
            {
                position = new Vector2(tab.geometry.tabHeader.rect.width, 0);
                size = new Size(tab.geometry.tabContainer.rect.width - tab.geometry.tabHeader.rect.width, tab.geometry.tabContainer.rect.height);


            }
            else if (tab.geometry.tabHeader.position == SubType.THPSR.Position.Right)
            {
                position = new Vector2(0, 0);
                size = new Size(tab.geometry.tabContainer.rect.width - tab.geometry.tabHeader.rect.width, tab.geometry.tabContainer.rect.height);
            }

            rect = new Rect(position, new Vector2(size.w, size.h));
            rect = GenericGeometry.getRectWithScrollEdges(tab.style.bodySkin, rect, tab.geometry.tabBody.scroll, tab.data.branchValues.tab.geometry.tabBody.scroll);

        }
    }
    
    public class TabButton {

        TabType.Component parent = new TabType.Component();
  //      public GenericType.Component ele = new GenericType.Component();
        public SubType.SizeGeneric componentValues = new SubType.SizeGeneric();
        public SubType.SizeGeneric branchValues = new SubType.SizeGeneric();
        public Size size = new Size(0, 0);
        public Vector2 position = new Vector2(0, 0);
        public Rect rect;
        ButtonType.Component button = new ButtonType.Component();
        public TabButton(Vector2 _position, Size headerSize, ButtonType.Component _button, ButtonType.Component branch)
        {
             parent.geometry = new TabType.Geometry();
            parent.geometry.tabBody.rect.width = headerSize.w;
            parent.geometry.tabBody.rect.height = headerSize.h;
            componentValues = _button.geometry.buttonBody.size;

            branchValues = branch.geometry.buttonBody.size;

            button =_button;
            position = _position;
            SetWidth();
            SetHeight();

            rect = new Rect(position, new Vector2(size.w, size.h));

        }


        private void SetWidth()
        {
            SubType.SizeGeneric values = componentValues;

            
            //branch
            if (values.wFollowBranch || branchValues.wFollowBranch) { values = branchValues; }

            //wfit
            if (values.wFitChildren) {size.w = button.style.bodySkin.button.CalcSize(button.data.GUIcontent).x; return; }
            
            //Relative
            if (values.relWidth) { size.w = values.width * parent.geometry.tabBody.rect.width; return; }

            //Literal
            size.w = values.width; return;



        }

        private void SetHeight()
        {
            SubType.SizeGeneric values = componentValues;
            //branch
            if (componentValues.hFollowBranch || branchValues.hFollowBranch) { values = branchValues; }


            //hfit 
            if (values.hFitChildren)  {  size.h = button.style.bodySkin.button.CalcSize(button.data.GUIcontent).y; return;   }


            //Relative
            if (values.relHeight) { size.h = values.height * parent.geometry.tabBody.rect.height; return; }

            //Literal
            size.h = values.height; return;


        }

        /*
        public static Size getSize(Vector2 headerSize, ButtonType.Component b, ButtonType.Component branchValues)
        {

            Size size = new Size(0, 0);



            // h size 
            if (branchValues.geometry.buttonBody.size.hFollowBranch || b.geometry.buttonBody.size.hFollowBranch)
            {  //Get w from global
                if (branchValues.geometry.buttonBody.size.relHeight) { size.h = branchValues.geometry.buttonBody.size.height * headerSize.y; } else { size.h = branchValues.geometry.buttonBody.size.height; }
            }
            else
            {  //Get h from component 
                if (b.geometry.buttonBody.size.relHeight) { size.h = b.geometry.buttonBody.size.height * headerSize.y; } else { size.h = b.geometry.buttonBody.size.height; }
            }






            // Fit H to content
            //    if (globalParams.geometry.buttonBody.size.hFitChildrenFollowBranch || b.geometry.buttonBody.size.hFitChildrenFollowBranch)
            //  {
            //    if (globalParams.geometry.buttonBody.size.hFitChildren)
            //    {
            //    size.h = b.style.bodySkin.button.CalcSize(b.data.GUIcontent).y;
            //  }

            //   }
            //  else
            //   {
            if (b.geometry.buttonBody.size.hFitChildren)
            {
                //  if (b.geometryData.Container.Size.HFitChildren) { b.geometryData.Container.Rect.height = b.styleData.ButtonSkin.button.CalcSize(b.Content).y; }
                size.h = b.style.bodySkin.button.CalcSize(b.data.GUIcontent).y;


            }

            // }

            return size;
        }

        */
    }

 

    
}
