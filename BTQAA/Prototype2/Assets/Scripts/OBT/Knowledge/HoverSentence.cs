using UnityEngine;
using System.Collections;

public class HoverSentence : MonoBehaviour {
	
	private GameObject target; //What to hover above
    private GameObject log; //Where to display the information
	
	public Sprite smallBackground;
	public Sprite largeBackground;
	
	public Sprite question;
	public Sprite murdered;
	public Sprite shocked;
	public Sprite greeting;
	public Sprite goodbye;
	public Sprite stayingIn;
	public Sprite love;
	public Sprite arrow;
	public Sprite bag;
	public Sprite pool;
	public Sprite beer;
	public Sprite burger;
	public Sprite toilet;
	public Sprite sleep;
	public Sprite tv;
	public Sprite police;
	public Sprite investigate;
	
	public SpriteRenderer noun1;
	public SpriteRenderer verb;
	public SpriteRenderer noun2;
	public SpriteRenderer background;
	
	private float height = 0.5f;

	void Start(){
        
    }

    void Awake()
    {
        log = GameObject.Find("Log");
    }

	void Update () {
		Reposition ();
	}

	private void Reposition(){
		if(target != null){
			gameObject.transform.position = target.transform.position + new Vector3(0f, 1f, height);
			gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		}
	}
	
	public void SelfDestruct(){
		Object.Destroy(this.gameObject);
	}
	
	public void ConstructSentence(Sentence sentence, GameObject target, float time){
		
		this.target = target;
		gameObject.transform.parent = target.transform;

		HoverSentence[] hovers = target.transform.GetComponentsInChildren<HoverSentence>();
		height = (hovers.Length) * 0.5f;
		
		switch(sentence.verb)
		{
		case Sentence.Verb.Murder:
			verb.sprite = murdered;
            break;
		case Sentence.Verb.Shocked:
			verb.sprite = shocked;
			break;
		case Sentence.Verb.Greeting:
			verb.sprite = greeting;
			break;
		case Sentence.Verb.Goodbye:
			verb.sprite = goodbye;
			break;
		case Sentence.Verb.StayingIn:
			verb.sprite=stayingIn;
			break;
		case Sentence.Verb.Love:
			verb.sprite = love;
            log.GetComponent<log>().Happening("Love is in the air for " + target.GetComponent<AuthoredGuestManager>().actor_name + "!", true);
            break;
		case Sentence.Verb.Arrow:
			verb.sprite = arrow;
			break;
		case Sentence.Verb.Bag:
			verb.sprite = bag;
			break;
		case Sentence.Verb.Pool:
			verb.sprite = pool;
            log.GetComponent<log>().Happening(target.GetComponent<AuthoredGuestManager>().actor_name + " is playing pool.", false);
            break;
		case Sentence.Verb.Beer:
			verb.sprite = beer;
            log.GetComponent<log>().Happening(target.GetComponent<AuthoredGuestManager>().actor_name + " is drinking the day away.", false);
            break;
		case Sentence.Verb.Burger:
			verb.sprite = burger;
            log.GetComponent<log>().Happening(target.GetComponent<AuthoredGuestManager>().actor_name + " is having a meal.", false);
            break;
		case Sentence.Verb.Toilet:
			verb.sprite = toilet;
            log.GetComponent<log>().Happening(target.GetComponent<AuthoredGuestManager>().actor_name + " is visiting the restroom.", false);
			break;
		case Sentence.Verb.Sleep:
			verb.sprite = sleep;
            log.GetComponent<log>().Happening(target.GetComponent<AuthoredGuestManager>().actor_name + " is resting.", false);
            break;
		case Sentence.Verb.TV:
			verb.sprite = tv;
            log.GetComponent<log>().Happening(target.GetComponent<AuthoredGuestManager>().actor_name + " is watching TV.", false);
            break;
		case Sentence.Verb.police:
			verb.sprite = police;
			break;
		case Sentence.Verb.investigate:
			verb.sprite = investigate;
			break;
		default:
			verb.sprite = question;
			break;
		}
		
		if(sentence.noun1 == null  && sentence.noun2 == null){
			background.sprite = smallBackground;
		}
		else{
			background.sprite = largeBackground;
			if(sentence.noun1 == null){ noun1.sprite = question; }
			else{
				if(sentence.noun1.GetComponent<EventManager>() != null){
					noun1.sprite = sentence.noun1.GetComponent<EventManager>().liveSprite;
				}
				else{
					noun1.sprite = sentence.noun1.GetComponentInChildren<SpriteRenderer>().sprite;
				}
			}
			
			if(sentence.noun2 == null){ noun2.sprite = question; }
			else{ 
				if(sentence.noun2.GetComponent<EventManager>() != null){
					noun2.sprite = sentence.noun2.GetComponent<EventManager>().liveSprite;
				}
				else if(sentence.noun2.GetComponentInChildren<SpriteRenderer>() != null){
					noun2.sprite = sentence.noun2.GetComponentInChildren<SpriteRenderer>().sprite;
				}
				else{
					noun2.sprite = question;
				}
			}
		}
		
		
		Reposition();
		Invoke("SelfDestruct", time);
	}
}
