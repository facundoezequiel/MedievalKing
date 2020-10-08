using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {
    
    void Start()
    {
        Invoke ("Destroy", 0.5f);
    }

   public void Destroy() {
       GameObject.Destroy(this.gameObject);
   }
}
