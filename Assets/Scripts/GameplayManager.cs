using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public int playerAmount;
    public GameObject playerPrefab;
    List<GameObject> playersCreated = new List<GameObject>();
    public GameObject cardPrefab;
    List<GameObject> cardsCreated = new List<GameObject>();
    public Transform table;
    public int seedForRandom = 992;
    //The first player is coded as 1 in this table while every card has a fixed position 
    //e.g. cardOwnerTable[0] = 2 means Heart 9 is in the hand of player 2 but cardOwnerTable[0] = 3 means Heart 9 is in the hand of player 3... another example cardOwnerTable[23] = 2 means Pikes Ass is in the hand of Player 2
    //When a card is taken as a point from the player the negative number is coded
    public int[] cardOwnerTable = new int[24];
    
    void Start()
    {
        //distribute cards fair
        for(int i=0;i<cardOwnerTable.Length;i++){
            if(i%3==0)
                cardOwnerTable[i]=0;
            else if(i%3==1)
                cardOwnerTable[i]=1;
            else if(i%3==2)
                cardOwnerTable[i]=2;
        }
        //shuffle
        for(int i = 0;i<100;i++){
            Random.InitState(seedForRandom+i);
            int genRand= Random.Range(0,24);
            Debug.Log(genRand);
            int genRand2= Random.Range(0,24);
            Debug.Log(genRand2);
            int Temp = cardOwnerTable[genRand];
            cardOwnerTable[genRand] = cardOwnerTable[genRand2];
            cardOwnerTable[genRand2] = Temp;
        }
        //create visual representations 
        for(int i = 0;i<24;i++){
            GameObject instance = Spawn(cardPrefab,1);
            cardsCreated.Add(instance);
        }
        for(int i = 0;i<playerAmount;i++){
            GameObject instance = Spawn(playerPrefab,0);
            playersCreated.Add(instance);
        }
        //game logic
        //StartCoroutine(MoveCoroutine());
    }

    //visual 
    void Update()
    {}

    GameObject Spawn(GameObject obj,int position)
    {
        GameObject instance = (GameObject)Instantiate(obj, transform.position, Quaternion.identity);

        switch(position)
        {
        case 0:
            instance.transform.parent = this.transform.parent;
            break;
        case 1:
            instance.transform.parent = table;
            break;
        default:
            instance.transform.parent = this.transform;
            break;
        }
        return instance;
	}
}