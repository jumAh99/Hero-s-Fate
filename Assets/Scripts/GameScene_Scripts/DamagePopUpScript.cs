using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class DamagePopUpScript : MonoBehaviour
{
    private TextMeshPro textMesh; 

    private void Awake(){
        textMesh = transform.GetComponent<TextMeshPro>(); 
    }

    public void Setuo(int damage){
        textMesh.SetText(damage.ToString());
    }
}
