using UnityEngine;
using System.Collections;
using System;

public class Sentence : IEquatable<Sentence> {
	public enum Verb{
		Unknown,
		Murder,
		StayingIn,
		Love,
		Arrow,

		Shocked,
		Greeting,
		Goodbye,

		Bag,
		Pool,
		Beer,
		Burger,
		Toilet,
		Sleep,
		TV,
		investigate,
		police,
	};

	public GameObject noun1;
	public GameObject noun2;
	public Verb verb;

	public Sentence(GameObject noun1, Verb verb, GameObject noun2){
		this.noun1 = noun1;
		this.verb = verb;
		this.noun2 = noun2;
	}

	public bool Equals(Sentence other){
		if(verb != other.verb){
			return false;
		}
		else if(!isSymmetrical()){
			if(noun1 == other.noun1 && noun2 == other.noun2){
				return true;
			}
			else{
				return false;
			}
		}
		else{
			if((noun1 == other.noun1 && noun2 == other.noun2) ||
			   (noun1 == other.noun2 && noun2 == other.noun1)){
				return true;
			}
			else{
				return false;
			}
		}
	}

	public bool isQuestion(){
		if(verb == Verb.Unknown){
			return true;
		}

		if(hasSubjects()){
			if(noun1 == null || noun2 == null){
				return true;
			}
		}

		return false;
	}

	public bool isSymmetrical(){
		if(verb == Verb.Murder){
			return false;
		}
		else{
			return true;
		}
	}

	public bool hasSubjects(){
		if(noun1 != null && noun2 != null){
			return true;
		}
		else{
			return false;
		}
	}
}
