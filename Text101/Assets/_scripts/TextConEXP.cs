using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextConEXP : MonoBehaviour
{

    public Text boo;
    private enum States { intro, room, mirror_0, mirror_room, sheets_0, sheets_1, sheets_2, lock_0, lock_1, key_room, freedom };
    private States myState;

    // Use this for initialization
    void Start()
    {
        myState = States.room;
    }

    // Update is called once per frame
    void Update()
    {
        print(myState);
        if (myState == States.room)
        {
            state_room();
        }

        else if (myState == States.sheets_0)
        {
            state_sheets_0();
        }

        else if (myState == States.sheets_1)
        {
            state_sheets_1();
        }

        else if (myState == States.lock_0)
        {
            state_lock_0();
        }

        else if (myState == States.key_room)
        {
            state_key_room();
        }

        else if (myState == States.mirror_0)
        {
            state_mirror_0();
        }

        else if (myState == States.mirror_room)
        {
            state_mirror_room();
        }

        else if (myState == States.freedom)
        {
            state_freedom();
        }

        else if (myState == States.intro)
        {
            state_intro();
        }

    }

    void KeyKrazy()
    {

        boo.text = "Oh Boy! We're going nuts now!";
        boo.color = Color.red;
    }

    // Define the states: Enums

    // Intro
    void state_intro()
    {

        boo.text = "Personally, I would not care " +
                 "for immortality in the least. \n\n" +
                 "There is nothing better than " +
                 "oblivion, since in oblivion there " +
                 "is no wish unfulfilled. We had it " +
                 "before we were born yet did not " +
                 "complain. Shall we whine because " +
                 "we know it will return? \n\n" +
                 "It is Elysium enough for me, " +
                 "at any rate.. \n\n" +
                 "Press \"Space\" to continue."; ;

        boo.color = Color.grey;

    }


    // Room
    void state_room()
    {

        boo.text = "You are awake again. \n\n" +

                      "You sit upright on the bed. " +
                      "The room is dimly lit and very cold. " +
                      "There is a mirror on the far wall. \n\n" +

                      "In it you see yourself smeared on the glass. " +
                      "The disgusting clown makeup and piss-sodden " +
                      "suit appear more grotesque than normal in the low light. \n\n" +

                      "\"we know it will return?\" \n\n" +

                      "On the bed beside you is a pile of filthy sheets., \n\n" +

                      "To your left is a locked door. \n\n" +

                      "Press \"M\" for Mirror, \"L\" for Lock and \"S\" for Sheet. \n\n";

        if (Input.GetKeyDown(KeyCode.M))
        {
            myState = States.mirror_0;
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            myState = States.sheets_0;
        }

        else if (Input.GetKeyDown(KeyCode.L))
        {
            myState = States.lock_0;

        }

        boo.color = Color.grey;

    }



    // Sheets 0
    void state_sheets_0()
    {

        boo.text = "If you put on the sheets you will become Invisible. " +
                   "Press \"U\" to dress up in your ghost suit." +
                   "Press \"R\" to continue exploring the room.";

        if (Input.GetKeyDown(KeyCode.R))
        {
            myState = States.mirror_room;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            myState = States.sheets_1;
        }

        boo.color = Color.grey;

    }


    // Sheets 1
    void state_sheets_1()
    {

        boo.text = "put on the sheet? " +
                   "Press \"U\" Put on the Sheet. \"R\" to Return to your misery";

        if (Input.GetKeyDown(KeyCode.R))
        {
            myState = States.mirror_room;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            myState = States.sheets_2;
        }

        boo.color = Color.grey;
    }


        // Sheets 2
        void state_sheets_2()
    {

        boo.text = "You look fabulous. You may now Return to the door, unlock it and run amok. " +
                   "Press \"U\" to Exit or Press \"R\" to Return to your misery" ;

        if (Input.GetKeyDown(KeyCode.R))
        {
            myState = States.mirror_room;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            myState = States.key_room;
        }

        boo.color = Color.grey;

    }


    // Lock
    void state_lock_0()
    {

        boo.text = "This lock needs a key. " +
                   "Press \"R\" to look for the key.";

        if (Input.GetKeyDown(KeyCode.R))
        {
            myState = States.room;

        }

        boo.color = Color.grey;

    }

    // Key Room
    void state_key_room()
    {

        boo.text = "You are one handsome Devil. Now wander the halls for eternity! " +
                   "Press \"U\" to Exit the room.";

        if (Input.GetKeyDown(KeyCode.U))
        {
            myState = States.freedom;

        }

        boo.color = Color.grey;

    }


    // Mirror
    void state_mirror_0()
    {

        boo.text = "You look horrible. However, this mirror reveals that the Key " +
                   "is on a cord around your neck. \n\n" +
                   "Press \"R\" to continue exploring the room." +
                   "Press \"T\" to Take the Key. ";

        if (Input.GetKeyDown(KeyCode.T))
        {
            myState = States.mirror_room;

        }

        else if (Input.GetKeyDown(KeyCode.R))
        {
            myState = States.room;

        }

        boo.color = Color.grey;
    }


    // Mirror_Room
    void state_mirror_room()
    {

        boo.text = "You have looked into the mirror and found the Key. It can be used to open the door. \n\n" +
                   "You will be seen by the living if you leave this room without your protective sheet. \n\n" +
                   "Press \"U\" to go back to the bed and get your sheet? \n\n" +
                   "Press \"R\" to put down the sheet and coninue sulking.";

        if (Input.GetKeyDown(KeyCode.U))
        {
            myState = States.sheets_1;

        }

        else if (Input.GetKeyDown(KeyCode.R))
        {
            myState = States.room;

        }

        boo.color = Color.grey;

    }
    // Freedom
    void state_freedom()
    {

        boo.text = "You have open the door and now you may control the universe. " +
                   "Remember, you are invisible to the living if you are wearing your sheet." +
                   "Press \"S\" to hear a beadtime story.";

        if (Input.GetKeyDown(KeyCode.S))
        {
            myState = States.intro;
        }

        boo.color = Color.red;
    }


}







