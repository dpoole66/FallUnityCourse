using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Implement : MonoBehaviour {
    // Implementation

    public PerInfo p0;
    public PerInfo p1;
    public Vector3 PlayerLoc;

	// Use this for initialization
	void Start () {
        p0 = new PerInfo("Whoopty", 007, PlayerLoc, 100, 50, 100, "Rifle", "Handgun", ActionStatus.Idle, BattleStatus.InRange, AlertStatus.Relaxed, 9);
        p1 = new PerInfo("Nubler", 001, PlayerLoc, 200, 150, 80, "Hammer", "Knife", ActionStatus.MovingTo, BattleStatus.InRange, AlertStatus.TargetFixed, 60);
        PrintPerInfo(p0);
        PrintPerInfo(p1);
        p1.Attack(p0);
        Debug.Log("Attack");
        p0.SetJak(77);
        PrintPerInfo(p0);
        PerInfo.Block(p0, p1);
        Debug.Log("Block");


    }

    void PrintPerInfo(PerInfo person) {
        Debug.Log(person.playerName + ", "
            + person.id + ", "
            + person.playerLoc + ","
            + person.hitPoints + ", "
            + person.armor + ", "
            + person.stamina + ", "
            + person.weaponOne + ", "
            + person.weaponTwo + ","
            + person.actionStatus + ","
            + person.battleStatus + ","
            + person.alertStatus + ","
            + person.Jak);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ShowStats(){
        if(Input.GetKeyDown(KeyCode.Space))  {

            
        }
    } 
        
}
