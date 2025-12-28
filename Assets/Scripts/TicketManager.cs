using UnityEngine;
using TMPro;
using System;
public class TicketManager : MonoBehaviour
{
    [SerializeField] GameObject ticketDesc;
    [SerializeField] GameObject ticketTitle;
    [SerializeField] String[] ticketDescriptions;

    void Start()
    {
        ticketDesc.GetComponent<TextMeshProUGUI>().text = "Select a ticket to view its details.";
        ticketTitle.GetComponent<TextMeshProUGUI>().text = "";
    }
    public void onSelectTicket(int ticketID)
    {
        if(ticketID == 1)
        {
            ticketDesc.GetComponent<TextMeshProUGUI>().text = ticketDescriptions[0];
            ticketTitle.GetComponent<TextMeshProUGUI>().text = "Ticket 1";

        }
        else if(ticketID == 2)
        {
            ticketDesc.GetComponent<TextMeshProUGUI>().text = ticketDescriptions[1];
            ticketTitle.GetComponent<TextMeshProUGUI>().text = "Ticket 2";
        }
        else if(ticketID == 3)
        {
            ticketDesc.GetComponent<TextMeshProUGUI>().text = ticketDescriptions[2];
            ticketTitle.GetComponent<TextMeshProUGUI>().text = "Ticket 3";
        }
    }
}
