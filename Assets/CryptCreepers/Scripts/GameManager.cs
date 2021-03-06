using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int score;

    public int Score{
        get => score;
        set {
            score = value;
            if(score % 1000 == 0){
                gameDifficult++;
            }}
    }

    public static GameManager instance;
    public bool gameIsRunning = true;
    public int gameDifficult = 1;
    public int time = 30;
    
    private void Awake() {
        if(instance == null){
            instance = this;
        }
        
    }
    private void Start() {
        gameIsRunning = true;
        StartCoroutine(CountdownRuntime());
    }

    IEnumerator CountdownRuntime(){
        while(time > 0){
            yield return new WaitForSeconds(1);
            time--;
        }
        Debug.Log("Game Over");
    }
    
}
