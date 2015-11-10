using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KnowledgeBase{
	private List<Sentence> bank = new List<Sentence>();

	private EventManager eventManager;

	public KnowledgeBase(EventManager eventManager){
		this.eventManager = eventManager;
	}


	public bool addSentence(Sentence sentence){
		if(!bank.Contains(sentence) && !(sentence.noun1 == null && sentence.noun2 == null)){
			bank.Add(sentence);
			return true;
		}
		else{
			return false;
		}
	}

	public void removeSentence(Sentence sentence){
		bank.Remove(sentence);
	}

	public bool Murdered(GameObject self, GameObject victim){
		foreach(Sentence s in bank){
			if(s.noun1 == self && s.verb == Sentence.Verb.Murder && s.noun2 == victim){
				return true;
			}
		}

		return false;
	}

	//TODO EXPAND TO ADD LIES AND STUFF
	public Sentence GetResponse(Sentence sentence){
		foreach(Sentence s in bank){
			if(!sentence.Equals(s)){
				if(GetPartialMatch(sentence, s)){
					if(s.verb != Sentence.Verb.Murder || s.noun1 != eventManager.gameObject){
						return s;
					}
				}
			}
		}

		if(!bank.Contains(sentence)){
			return new Sentence(null, Sentence.Verb.Shocked, null);
		}
		else{
			return new Sentence(null, Sentence.Verb.Unknown, null);
		}

		//return new Sentence(null, Sentence.Verb.Unknown, null);
	}



	public bool ContainsSentence(Sentence sentence){
		return bank.Contains(sentence);
	}

	public Sentence GetRandomQuestion(){
		List<Sentence> questionList = new List<Sentence>();
		foreach(Sentence s in bank){
			if(s.isQuestion()){
				questionList.Add(s);
			}
		}
		
		int questionIndex = Random.Range(0, questionList.Count);
		if(questionList.Count > 0){
			return questionList[questionIndex];
		}
		else{
			return null;
		}
	}

	public Sentence RandomGossip(){
		List<Sentence> goss = new List<Sentence>();
		foreach(Sentence s in bank){
			if(s.verb == Sentence.Verb.Love || s.verb == Sentence.Verb.Murder){
				if(s.noun1 != eventManager.gameObject && s.noun2 != eventManager.gameObject){
					goss.Add(s);
				}
			}
		}

		if(goss.Count > 0){
			return goss[Random.Range(0, goss.Count)];
		}
		else{
			return null;
		}
	}
	
	//Does sentence 2 answer question 1?
	//TODO (worry about this later) Also need to worry about sentence level symmetry
	//Also need to worry about always returning the first result
	private bool GetPartialMatch(Sentence sentence1, Sentence sentence2){
		
		if(sentence2.isQuestion()){
			return false;
		}
		
		if(sentence1.noun1 != null){
			if(sentence1.noun1 != sentence2.noun1){
				return false;
			}
		}
		
		if(sentence1.noun2 != null){
			if(sentence1.noun2 != sentence2.noun2){
				return false;
			}
		}
		
		if(sentence1.verb != Sentence.Verb.Unknown){
			if(sentence1.verb != sentence2.verb){
				return false;
			}
		}
		
		return true;
	}

	public int Affairs(GameObject self, GameObject so){
		int affairs = 0;
		foreach(Sentence s in bank){
			if(s.verb == Sentence.Verb.Love && (s.noun1 == self || s.noun2 == self) && (s.noun1 != so && s.noun2 != so)){
				affairs++;
			}
		}

		return affairs;
	}

	public GameObject RandomAffair(GameObject self, GameObject so){
		List<GameObject> affairs = new List<GameObject>();
		foreach(Sentence s in bank){
			if(s.verb == Sentence.Verb.Love && (s.noun1 == self || s.noun2 == self) && (s.noun1 != so && s.noun2 != so)){
				if(s.noun1 == self){
					affairs.Add(s.noun2);
				}
				else{
					affairs.Add(s.noun1);
				}
			}
		}

		return affairs[Random.Range(0, affairs.Count)];
	}
}
