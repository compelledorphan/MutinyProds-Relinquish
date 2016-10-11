using UnityEngine;
using System.Collections;

public class EnterInitials : MonoBehaviour 
{
    int SlotSelected = 1;

    public char InitialOne = 'A';
    public char InitialTwo = 'A';
    public char InitialThree = 'A';

    string FinalInitials = "AAA";

    bool AxisInUse = false;
    bool Increase = false;
    bool Decrease = false;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            SlotSelected++;
            Mathf.Clamp(SlotSelected, 1, 3);
            
            AxisInUse = true;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            SlotSelected--;
            Mathf.Clamp(SlotSelected, 1, 3);

            AxisInUse = true;
        }
        else if (Input.GetAxisRaw("Vertical") == 1)
        {
            Increase = true;

            AxisInUse = true;
        }
        else if (Input.GetAxisRaw("Vertical") == -1)
        {
            Decrease = true;

            AxisInUse = true;
        }
        else
        {
            // Do nothing
        }

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            AxisInUse = false;
        }

        switch (SlotSelected)
        {
            case 1:
                {
                    if(Increase == true)
                    {
                        InitialOne++;
                        if(InitialOne == '[')
                        {

                        }
                    }
                    break;
                }
            case 2:
                {
                    break;
                }
            case 3:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }

        FinalInitials = InitialOne.ToString() + InitialTwo.ToString() + InitialThree.ToString();
	}
}
