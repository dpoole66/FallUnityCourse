using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public Text boo;
    private enum States { intro, room, mirror_0, mirror_room, sheets_0, sheets_1,
                        lock_0, lock_1, corridor_0, corridor_1, corridor_2, corridor_3,
                        floor, stairs_0, stairs_1, stairs_2, closet_door, closet_inside,
                        sheet_room, courtyard, lock_2, bowl_0 };
    private States myState;

    // Use this for initialization
    void Start() {
        myState = States.room;
    }

    // Update is called once per frame
    void Update() {
        print(myState);
        if (myState == States.room) {
            state_room();
        }

        else if (myState == States.sheets_0) {
            state_sheets_0();
        }

        else if (myState == States.sheets_1) {
            state_sheets_1();
        }

        else if (myState == States.lock_0) {
            state_lock_0();
        }

        else if (myState == States.lock_1) {
            state_lock_1();
        }

        else if (myState == States.lock_2){
            state_lock_2();
        }

        else if (myState == States.mirror_0) {
            state_mirror_0();
        }

        else if (myState == States.mirror_room)
        {
            state_mirror_room();
        }

        else if (myState == States.courtyard) {
            state_courtyard();
        }

        else if (myState == States.intro) {
            state_intro();
        }

        else if (myState == States.corridor_0) {
            state_corridor_0();
        }

        else if (myState == States.corridor_1) {
            state_corridor_1();
        }

        else if (myState == States.corridor_2) {
            state_corridor_2();
        }

        else if (myState == States.corridor_3) {
            state_corridor_3();
        }

        else if (myState == States.stairs_0) {
            state_stairs_0();
        }

        else if (myState == States.stairs_1) {
            state_stairs_1();
        }

        else if (myState == States.stairs_2) {
            state_stairs_2();
        }

        else if (myState == States.closet_inside)   {
            state_closet_inside();
        }

        else if (myState == States.closet_door) {
            state_closet_door();
        }

        else if (myState == States.floor) {
            state_floor();
        }

        else if (myState == States.sheet_room)  {
            state_sheet_room();
        }

        else if (myState == States.bowl_0){
            state_bowl_0();
        }
    }

    void KeyKrazy() {

        boo.text = "Oh Boy! We're going nuts now!";
        boo.color = Color.red;
    }

    // Define the states: Enums

    // Intro
    void state_intro() {

        boo.text =   "Personally, I would not care \n\n" +
                     "for immortality in the least. \n\n" +

                     "There is nothing better than \n\n" +
                     "oblivion, since in oblivion there \n\n" +
                     "is no wish unfulfilled. We had it \n\n" +
                     "before we were born yet did not \n\n" +
                     "complain. Shall we whine because \n\n" +
                     "we know it will return? \n\n\n\n" +

                     "It is Elysium enough for me, " +
                     "at any rate.. \n\n\n\n" +

                     "Press \"Space\" to continue.";

        if (Input.GetKeyDown(KeyCode.M))
        {
            myState = States.mirror_0;
        }

        boo.color = Color.red;

    }


    // Room
    void state_room() {

        boo.text =    "You are awake again. \n\n" +

                      "You sit upright on the bed. " +
                      "The room is dimly lit and very cold. " +
                      "There is a mirror on the far wall. \n\n" +

                      "In it you see yourself smeared on the glass. " +
                      "The disgusting clown makeup and dirty underwear " +
                      "appear more grotesque than normal in the early light. \n\n" +

                      "On the bed beside you is a pile of filthy sheets., \n\n" +

                      "To your left is a locked door. \n\n\n\n" +

                      "Press \"M\" for Mirror, \n\n" +
                      "Press \"L\" for Lock and \n\n" +
                      "Press \"S\" for Sheet. \n\n";

        if (Input.GetKeyDown(KeyCode.M)) {
            myState = States.mirror_0;
        }

        else if (Input.GetKeyDown(KeyCode.S)) {
            myState = States.sheets_0;
        }

        else if (Input.GetKeyDown(KeyCode.L)) {
            myState = States.lock_0;

        }

        boo.color = Color.grey;

    }


    // Sheets
    void state_sheets_0() {

        boo.text = "Your filthy bedsheet with eyeholes? \n\n" +
                   "You should consider wearing something \n\n" +
                   "else today of all days.\n\n\n\n" +

                   "Press \"R\" to continue exploring the room.";

        if (Input.GetKeyDown(KeyCode.R)) {
            myState = States.room;
        }

    }

    // Sheets 1
    void state_sheets_1()
    {

        boo.text = "The bedsheet won't disguise you. \n\n" +
                   "You need to find something better \n\n" +
                   "if you want to Escape. \n\n\n\n" +
                   "Press \"R\" to Return to the room.";

        if (Input.GetKeyDown(KeyCode.R))
        {
            myState = States.sheet_room;
        }

        boo.color = Color.grey;

    }


    // Lock 0
    void state_lock_0() {

        boo.text = "This lock needs a Key. \n\n\n\n" +
                   "Press \"R\" to Return to corridor.";

        if (Input.GetKeyDown(KeyCode.R)) {
            myState = States.room;

        }

        boo.color = Color.grey;

    }

    // Lock 1
    void state_lock_1() {

        boo.text = "There are no sounds from the Corridor. \n\n" +
                   "However, the morning shift change will \n\n" +
                   "mean the Guard will be up hear soon. \n\n\n\n" +

                   "Use the Key to get out of this room? \n\n\n\n" +

                   "Press \"K\" to use the Key." +
                   "Press \"L\" to return to your room remain a Loser?";

        if (Input.GetKeyDown(KeyCode.K)) {
            myState = States.corridor_0;

        }

        else if (Input.GetKeyDown(KeyCode.L))   {
            myState = States.sheet_room;
        }

        boo.color = Color.grey;

    }

    // Lock 2
    void state_lock_2()
    {

        boo.text = "There are no sounds from the Corridor. \n\n" +
                   "However, the morning shift change will \n\n" +
                   "mean the Guard will be up hear soon. \n\n" +
                   "If you weren't a Loser, you would get the Key. \n\n" +

                   "Press \"L\" to return to your room remain a Loser?";

        if (Input.GetKeyDown(KeyCode.L))
        {
            myState = States.sheet_room;
        }

        boo.color = Color.grey;

    }


    // Mirror
    void state_mirror_0() {

        boo.text = "Why are you wearing that bedsheet? \n\n" +
                   "However, this mirror reveals that the Key \n\n" +
                   "is on the rope around your neck. \n\n\n\n" +
                   "Press \"R\" to continue exploring the room. \n\n" +
                   "Press \"K\" to take the Key. ";

        if (Input.GetKeyDown(KeyCode.K)) {
            myState = States.mirror_room;

        }

        else if (Input.GetKeyDown(KeyCode.R)) {
            myState = States.room;

        }

        boo.color = Color.grey;
    }


    // Mirror_Room
    void state_mirror_room()    {

        boo.text = "You have the Key. It can be used to open the door. \n\n\n\n" +

                   "The only way out of the building is \n\n" +
                   "downstairs through the Courtyard and \n\n" +
                   "the exit is Guarded. \n\n\n\n" +

                   "Press \"K\" to go to the door and listen for the Guard? \n\n" +
                   "Press \"R\" to put down the Key and coninue sulking.";

        if (Input.GetKeyDown(KeyCode.K)) {
            myState = States.lock_1;

        }

        else if (Input.GetKeyDown(KeyCode.R)) {
            myState = States.sheet_room;

        }

        boo.color = Color.grey;

    }

    // Sheet Room
    void state_sheet_room() {

        boo.text =    "You sit back down on your filthy bed. " +
                      "You just stare into the Mirror until it's daylight. \n\n" +

                      "On the bed beside you is a Bowl of spagettios. \n\n" +

                      "To your left is a locked Door. \n\n\n\n" +

                      "Press \"M\" for Mirror, \n\n" +
                      "Press \"D\" for Door and \n\n" +
                      "Press \"B\" for Bowl. \n\n";

        if (Input.GetKeyDown(KeyCode.D))
        {
            myState = States.lock_2;

        }

        else if (Input.GetKeyDown(KeyCode.M))
        {
            myState = States.mirror_0;

        }

        else if (Input.GetKeyDown(KeyCode.B))
        {
            myState = States.bowl_0;
        }

        boo.color = Color.grey;

    }



    // Bowl 0
    void state_bowl_0()
    {

        boo.text = "A bowl of cold speghettios. \n\n\n\n" +

                   "You made the bowl from a human skullcap. \n\n\n\n" +

                   "Press \"E\" to Eat. \n\n" +
                   "Press \"R\" to put down the bowl and coninue sulking.";

        if (Input.GetKeyDown(KeyCode.E))
        {
            myState = States.intro;

        }

        else if (Input.GetKeyDown(KeyCode.R))
        {
            myState = States.sheet_room;

        }

        boo.color = Color.grey;

    }
    /// 
    /// 
    /// <summary>
    /// First Corridor States
    /// </summary>
    /// 
    // Corridor_0
    void state_corridor_0() {

        boo.text = "You have opened the door and you are now in a long dark corridor. \n\n\n\n" +

                   "You are not going to get past the Guard \n\n " +
                   "wearing that bedsheet. \n\n" +
                   "You must search for a better disguise. \n\n\n\n" +

                   "At the end of the corridor is a flight of Stairs . \n\n\n\n" +

                   "To the right, there is a Door with a sign \"Janitor\". \n\n\n\n" +

                   "There is an Object on the floor midway down the hall. \n\n\n\n" +

                   "Press \"C\" to inspect the Janitor's Closet. \n\n" +
                   "Press \"F\" to inspect the Floor. \n\n" +
                   "Press \"S\" to inspect the Stairs. \n\n";

        if (Input.GetKeyDown(KeyCode.S)){
            myState = States.stairs_0;
        }

        else if (Input.GetKeyDown(KeyCode.C)){
            myState = States.closet_door;
        }

        else if (Input.GetKeyDown(KeyCode.F)){
            myState = States.floor;
        }
        

        boo.color = Color.grey;
    }
  

    // Stairs 0
    void state_stairs_0() {

        boo.text = "You probably shouldn't try to use the stairs. \n\n" +
                   "They will lead you down to the courtyard but it's guarded. \n\n\n\n" +

                   "Press \"R\" to return to the corridor.";

        if (Input.GetKeyDown(KeyCode.R)) {
            myState = States.corridor_0;
        }

        boo.color = Color.grey;
    }

    // Closet Door
    void state_closet_door() {

            boo.text = "This door is locked. \n\n" +
                       "There must be a uniform in the closet you can use. \n\n" +

                       "Look around the corridor for something to pick it with. \n\n\n\n" +
                       "Press \"R\" to return to the corridor.";

            if (Input.GetKeyDown(KeyCode.R))    {
                myState = States.corridor_0;
            }

        boo.color = Color.grey;
    }

    // Floor
    void state_floor()  {

        boo.text = "There is a Hairpin on the floor. \n\n" +
                   "It can be used to pick locks. \n\n\n\n" +

                   "Press \"H\" to pick up the Hairpin. \n\n" +
                   "Press \"R\" to Return to the corridor.";

            if (Input.GetKeyDown(KeyCode.H))    {
                myState = States.corridor_1;
            }

            else if (Input.GetKeyDown(KeyCode.R))   {
                myState = States.corridor_0;
            }

        boo.color = Color.grey;
    }

    /// 
    /// 
    /// <summary>
    /// Second Corridor States
    /// </summary>
    /// 
    // Corridor 1
    void state_corridor_1() {

        boo.text = "The Janitor\'s Closet can be opened now. \n\n" +
                   "Use the hairpin to pick the lock. \n\n" +
                   "There are noises coming from the floor below. \n\n\n\n" +
                   "Press \"L\" to pick the lock on the Janitor\'s Closet. \n\n" +
                   "Press \"S\" to inspect the stairs to the Courtyard below.";

        if (Input.GetKeyDown(KeyCode.S))    {
            myState = States.stairs_1;
        }

        else if (Input.GetKeyDown(KeyCode.L))   {
            myState = States.closet_inside;
        }

        boo.color = Color.grey;
    }

    // Stairs 1
    void state_stairs_1()   {

        boo.text = "You hear noises. It's not safe to stand here wearing this bedsheet. \n\n" +
                   "Find a disguise, or Hide in the Janitor\'s Closet. \n\n" +
                   "Press \"R\" to return to the corridor.";

        if (Input.GetKeyDown(KeyCode.R))    {
            myState = States.corridor_1;
        }

        boo.color = Color.grey;

    }

    // Closet Inside
    void state_closet_inside()  {

        boo.text = "You found a Janitor\'s Uniform \n\n" +
                   "Use the hairpin to pick the lock. \n\n" +
                   "Press \"D\" to Dress in the Janitor\'s uniform. \n\n" +
                   "Press \"R\" to Return.";

        if (Input.GetKeyDown(KeyCode.R))    {
            myState = States.corridor_2;
        }

        else if (Input.GetKeyDown(KeyCode.D))   {
            myState = States.corridor_3;
        }

        boo.color = Color.grey;
    }

    ///
    /// 
    /// <summary>
    /// Third Corridor State
    /// Adding tension with comments at stairs. Nowerew to go but back to the 
    /// closet and ethier hide or change and escape
    /// </summary>
    /// 

    // Corridor 2
    void state_corridor_2()
    {

        boo.text = "The noises from the stairs are much louder... \n\n" +
                   "UA guard is coming up the stairs to make his rounds! \n\n\n\n" +
                   "You look like a Janitor so you should act like one or Hide! \n\n\n\n" +
                   "Press \"B\" to go Back to the Janitor\'s Closet. \n\n" +
                   "Press \"S\" to investigate the Stairs.";

        if (Input.GetKeyDown(KeyCode.B))    {
            myState = States.closet_inside;
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            myState = States.stairs_2;
        }

        boo.color = Color.grey;
    }

    // Stairs 2
    void state_stairs_2()
    {

        boo.text = "A Guard coumes throught the doorway to the stairs. \n\n" +
                   "He looks directly at you. \n\n\n\n" +
                   "\"Janice dear. Are you wandering again? Matlock isn\'t on until 4 O\"'clock. \n\n" +
                   "Let\'s get you back to you\'r room for you\'r nap.\" \n\n\n\n" +
                   "Press \"L\" to return to your \"Lithium Stupor\".";

        if (Input.GetKeyDown(KeyCode.L))
        {
            myState = States.room;
        }

    }


    /// <summary>
    /// The Final Corridor States
    /// </summary>
    /// 


    // Corridor 3
    void state_corridor_3()
    {

        boo.text = "The noises from the stairs are much louder... \n\n\n\n" +
                   "A guard is coming up the stairs to make his rounds! \n\n" +
                   "You look like a Janitor so you should act like one or Hide! \n\n\n\n" +
                   "Press \"B\" to go Back to the Janitor's Closet. \n\n" +
                   "Press \"J\" to act like a Janitor and calmly walk to the courtyard.";

        if (Input.GetKeyDown(KeyCode.B))
        {
            myState = States.closet_inside;
        }

        else if (Input.GetKeyDown(KeyCode.J))
        {
            myState = States.stairs_2;
        }
    }

    // Courtyard
    void state_courtyard()
    {

        boo.text = "You calmly walk toward the stairs carrying a putrid mop in one hand.  \n\n\n\n" +
                   "A Guard comes through the doorway to the stairs and looks directly at you. \n\n\n\n" +
                   "\"Hey! Get the Hell away from me with that stinking shitmop! Goddamn you fuckin\' guys! \n\n" +
                   " I don\'t get paid enough! I\'m gonna go take a shit myself right now. \n\n" +
                   "Give you something to do whit yer mop later.\" \n\n" +
                   "Press \"E\" to go walk into the Courtyard and away.";

        if (Input.GetKeyDown(KeyCode.H))
        {
            myState = States.intro;
        }
    }





}







