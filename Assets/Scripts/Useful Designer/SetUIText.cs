using UnityEngine;
using UnityEngine.UI;

public class SetUIText : MonoBehaviour
{

    public Text textObject;

    public void SetText(string newText)
    {
        textObject.text = newText.Replace("\\n","\n");
    }
}
