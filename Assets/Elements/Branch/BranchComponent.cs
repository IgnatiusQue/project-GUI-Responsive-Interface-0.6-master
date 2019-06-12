using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tab.childrenTabs= Structure.Get.ChildrenTabs(gameObject,transform);




public class BranchComponent : MonoBehaviour {
    //Root 

    BranchValuesComponent GlobalParameters;// = new POGlobals(); 
    public List<TabComponent> childrenTabs = new List<TabComponent>();
    public List<Component> children = new List<Component>();


    void Start()
    {
         childrenTabs = Structure.Tab.getChildren(gameObject, transform);
       children=Structure.Common.getChildren(gameObject, transform);
        GlobalParameters = gameObject.transform.GetComponent<BranchValuesComponent>();




        // PODrawer.Tabs.Init(true,childrenTabs);
        Drawer.Common.Init(children,true);

    }
    void OnGUI () {
      

       
      Drawer.Common.Draw(children);
      //PODrawer.Tabs.Draw(childrenTabs);


    }
	
	// Update is called once per frame

}
