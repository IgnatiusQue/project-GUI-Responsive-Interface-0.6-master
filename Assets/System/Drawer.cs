using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public   class  Drawer {

    public class Common {

        public static void Init(List<Component> children,bool isRoot) {
      
            for (int i = 0; i < children.Count; i++)
            {
                TabComponent tab = children[i].GetComponent<TabComponent>();
                TextareaComponent textarea = children[i].GetComponent<TextareaComponent>();
                ButtonComponent button = children[i].GetComponent<ButtonComponent>();

                if (tab != null) { tab.tab.data.isRoot = isRoot; tab.Init(); }
                if (textarea != null) { textarea.Init(); }
                if (button != null) { button.Init(); }

            }
        }

        public static void Draw(List<Component> children)
        {

            for (int i = 0; i < children.Count; i++)
            {
                TabComponent tab = children[i].GetComponent<TabComponent>();
                TextareaComponent textarea = children[i].GetComponent<TextareaComponent>();
                ButtonComponent button = children[i].GetComponent<ButtonComponent>();

                if (tab != null) { tab.Draw(); }
                if (textarea != null) { textarea.Draw(); }
                if (button != null) { button.Draw(); }

            }
        }

    }



  

}
