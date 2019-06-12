using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure
{

    public class Common
    {
        public static List<Component> getChildren(GameObject gameObject, Transform transform)
        {

            {
                List<Component> res = new List<Component>();
                Component[] temp = gameObject.transform.GetComponentsInChildren<Component>();
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i].GetType() == typeof(Transform)) { continue; }
                    if (temp[i].transform.parent != transform) { continue; }
                    res.Add(temp[i]);
                }
                return res;
            }

        }


    }
        public class Tab
        {
            public static List<TabComponent> getChildren(GameObject gameObject, Transform transform)
            {

                {
                    List<TabComponent> res = new List<TabComponent>();
                    TabComponent[] temp = gameObject.transform.GetComponentsInChildren<TabComponent>();
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i].transform.parent != transform) { continue; }
                        res.Add(temp[i]);
                    }
                    return res;
                }

            }


        }
        public class Button
        {
            public static List<global::ButtonComponent> getChildren(GameObject gameObject, Transform transform)
            {

                {
                    List<global::ButtonComponent> res = new List<ButtonComponent>();
                    ButtonComponent[] temp = gameObject.transform.GetComponentsInChildren<ButtonComponent>();
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i].transform.parent != transform) { continue; }
                        res.Add(temp[i]);
                    }
                    return res;
                }

            }
        }

       public class TextArea
        {
            public static List<TextareaComponent> getChildren(GameObject gameObject, Transform transform)
            {

                {
                    List<TextareaComponent> res = new List<TextareaComponent>();
                    TextareaComponent[] temp = gameObject.transform.GetComponentsInChildren<TextareaComponent>();
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i].transform.parent != transform) { continue; }
                        res.Add(temp[i]);
                    }
                    return res;
                }

            }
        }

    }
        /*
        public static bool isRoot(POTabs tabParent, GameObject gameObject, Transform transform)
        {

            tabParent = gameObject.transform.parent.GetComponentInParent<POTabs>();

            if (tabParent != null)
            {
                if (tabParent.transform == transform.parent) { return false; } //else {  debug = "TnoP"; } 

            }


            return true;
        }*/


    
 
