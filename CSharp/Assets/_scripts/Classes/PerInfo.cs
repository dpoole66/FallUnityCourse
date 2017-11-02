using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defining states outside of the class below

    public enum ActionStatus {
        Idle,
        MovingTo,
        AttackStance,
        Attack,
        Block,
        Retreat
}

    public enum BattleStatus {
        InRange,
        NotInRange,
}

    public enum AlertStatus {
        Relaxed,
        Alerted,
        Investigating,
        TargetFixed,
        Alarm_1,
        Alarm_2,
        Alarm_3,
}



[System.Serializable]
public class PerInfo {

    public ActionStatus actionStatus;
    public BattleStatus battleStatus;
    public AlertStatus alertStatus;


    public int id, hitPoints, armor, stamina;
    public string playerName, weaponOne, weaponTwo;
    public Vector3 playerLoc;
    private int jak;

    // Encapsulator must be higher cast than the var to be encapusulated (example jak is private so the encapsulator must be public).  It ether "Gets" or "Sets" so observe the following syntax:
    public int Jak {
        get { return jak; }
        set {
            if (value >= 0) {
                jak = value;
            } else {
                jak = 0;
            }
        } 
    }

    public int GetJak() {
        return jak;
    }

    public void SetJak(int valueJak) {
        if (valueJak >= 0) {
            jak = valueJak;
        } else {
            jak = 0;
        }
    }


    public static string character = "Player Character";

    public PerInfo(string Name, int Id, Vector3 PlayerLoc, int HitPoints, 
        int Armor, int Stamina, string WeaponOne, string WeaponTwo,
        ActionStatus Astatus, BattleStatus Bstatus, AlertStatus Estatus, int Jak) {

        playerName = Name;
        id = Id;
        playerLoc = PlayerLoc;
        hitPoints = HitPoints;
        armor = Armor;
        stamina = Stamina;
        weaponOne = WeaponOne;
        weaponTwo = WeaponTwo;
        actionStatus = Astatus;
        battleStatus = Bstatus;
        alertStatus = Estatus;
        jak = Jak;
        
    }


    public void Attack(PerInfo enemyPerson) {
        if (enemyPerson.alertStatus == AlertStatus.Relaxed && enemyPerson.battleStatus == BattleStatus.InRange) {
            enemyPerson.actionStatus = actionStatus = ActionStatus.Attack;
        }

        Debug.Log("You Attacked!");
    } 

    public static void Block(PerInfo defender, PerInfo attacker) {
        if (defender.actionStatus == ActionStatus.AttackStance && attacker.actionStatus == ActionStatus.Attack) {
            defender.actionStatus = ActionStatus.Block;
        }

    }

}