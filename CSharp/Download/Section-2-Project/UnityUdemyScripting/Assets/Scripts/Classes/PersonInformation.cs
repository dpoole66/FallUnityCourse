using UnityEngine;
using System.Collections;

public enum DatingStatus {
	Single, 
	ItsComplicated,
	Dating
}

[System.Serializable]
public class PersonInformation {
	
	public int id;
	private int age;
	public string name, address, city, country;
	public DatingStatus datingStatus;
	public static string species = "Homo Sapiens Sapiens";
	public static string planet = "Earth";

	public int Age {
		// inside an encapsulator you can have either a get statement or a set statement, or maybe both
		get { return age; }
		set {
			if (value >= 0) {
				age = value;
			} else {
				age = 0;
			}
		}
	}

	public int GetAge () {
		return age;
	}

	public void SetAge (int valueForAge) {
		if (valueForAge >= 0) {
			age = valueForAge;
		} else {
			age = 0;
		}
	}

	public PersonInformation (string Name, int Id, int Age, string Address, string City, string Country, DatingStatus LegalStatus) {
		name = Name;
		id = Id;
		age = Age;
		address = Address;
		city = City;
		country = Country;
		datingStatus = LegalStatus;
	}

	public PersonInformation (string name, int id, int age){
		this.name = name;
		this.id = id;
		this.age = age;
		this.address = city = country = "N/A";
	}

	public void Propose (PersonInformation anotherPerson) {

		if (anotherPerson.datingStatus == DatingStatus.Single && datingStatus == DatingStatus.Single) {
			anotherPerson.datingStatus = datingStatus = DatingStatus.Dating;
		}
	}

	public static void Propose (PersonInformation a, PersonInformation b) {
		if (a.datingStatus == DatingStatus.Single && b.datingStatus == DatingStatus.Single) {
			a.datingStatus = b.datingStatus = DatingStatus.Dating;
		}
	}

}
