using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaraUiController : MonoBehaviour
{
    public bool trigger;
    public TMP_Text flavourText;
    public Transform facePos;
    public GameObject[] faces;
    GameObject face;
    public int faceID, textID;
    int textLength;
    string displayedString;
    bool canLoop;
    public float characterDelay;

    // Start is called before the first frame update
    void Start()
    {
        face = Instantiate(faces[0], facePos.position, Quaternion.identity); //spawns default face on start
        face.transform.parent = facePos;
        canLoop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger)
        {
            trigger = false;
            UIupdate();
        }

        if(canLoop && (textLength < 50))
        {
            canLoop = false;
            StartCoroutine(LengthenText());
        }
    }

    void UIupdate()
    {
        Destroy(face);

        textLength = 0;

        face = Instantiate(faces[faceID], facePos.position, Quaternion.identity); //spawns face of id passed by other script
        face.transform.parent = facePos;

        if(textID == 0) //in an actual system, there'd be randomised integers and ids within the if textid statements corresponding to more strings for variety
        {
            displayedString = "This is some demonstration positive text!";
        }
        else if(textID == 1)
        {
            displayedString = "This is some demonstration negative text!";
        }
        else if(textID == 2)
        {
            displayedString = "This is some demonstration concerned text!";
        }
        else if(textID == 3)
        {
            displayedString = "This is some demonstration congrats text!";
        }


        flavourText.maxVisibleCharacters = 0;
        flavourText.text = displayedString;

    }

    public IEnumerator LengthenText()
    {
        yield return new WaitForSeconds(characterDelay);
        textLength += 1;
        flavourText.maxVisibleCharacters = textLength;
        canLoop = true;
    }
}
