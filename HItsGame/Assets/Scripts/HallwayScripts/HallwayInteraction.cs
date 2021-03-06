
using TMPro;
using UnityEngine;

public class HallwayInteraction : MonoBehaviour
{
    public GameObject messageBox;
    public TextMeshProUGUI text;
    public string room;
    public PortalTrigger IsPortalTriggered;

    private void Start()
    {
        messageBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        messageBox.SetActive(true);
        if (room == "kitchen")
        {
            if (!IsPortalTriggered.isPortalTriggered)
            {
                text.text = "Чем тут пахнет?";
            }
            else
            {
                text.text = "Там портал, туда нам надо";
            }
        }
        else
        {
            text.text = "Нам нужна кухня, а не " + room + " комната";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        messageBox.SetActive(false);
    }
}