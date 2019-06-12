using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGeoType : MonoBehaviour {

    public Component Parent;
    public Component Children;
    TabComponent a;
    ButtonComponent b;


    // Use this for initialization
    void Start()
    {

        a = gameObject.transform.GetComponentInChildren<TabComponent>();
        b = gameObject.transform.GetComponentInChildren<ButtonComponent>();

        if (a != null)
        {
            // print(a.tab.data.name);
            //TypeGeneric.Component ge = GetGen(a.tab);
            GenericType.Component ge = TabType.Generic(a.tab);

            print(ge.data.name+"_____");
        }
        else {
            print("No Tabs Found");
        }



        if (b != null) { print(b.button.data.name); }

       

        
        //parent 
    }

    

    
}
