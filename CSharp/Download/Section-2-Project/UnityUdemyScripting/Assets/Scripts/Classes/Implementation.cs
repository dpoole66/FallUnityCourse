using UnityEngine;
using System.Collections;

public class Implementation : MonoBehaviour {
	
	public PersonInformation p2;

	// Use this for initialization
	void Start () {
		PersonInformation p = new PersonInformation("Ben Thompson", 0000341, 34, "Sunshine Street, 304", "Vancouver", "Canada", DatingStatus.Single);
		p2 = new PersonInformation("Maria Flores", 0000190, 45, "Flowers Street, 102", "Mexico City", "Mexico", DatingStatus.Single);

		PrintPersonInformation (p);
		PrintPersonInformation (p2);

		p.SetAge (-40);
		PrintPersonInformation (p);
	}

	void PrintPersonInformation (PersonInformation person){
		print (person.name + ", " + person.datingStatus + ", " + person.id + ", " + person.GetAge() + ", " + person.address + ", " + person.city + " - " + person.country);
	}

	// Update is called once per frame
	void Update () {

	}
}
