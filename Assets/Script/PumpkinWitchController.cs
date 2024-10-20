using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WitchOrWhich.Configs;

public class PumpkinWitchController : MonoBehaviour
{
    [field: SerializeField] public bool canMove {private get; set;}= false;
    [field: SerializeField] private GameObject killEffect;
    private GameConfig gameConfig;
    private System.Random random = new System.Random();
    private float tempTime = 0;
    private float time;
    private Vector3 initialPosition;
    private Vector3 finalPosition;
    public void Init(GameConfig gameConfig){
        this.gameConfig = gameConfig;
        SetPosition();
    }

    void SetPosition(){
        tempTime = 0;
        initialPosition = transform.position;
        finalPosition = new Vector3(random.Next(-gameConfig.xRange, gameConfig.xRange),
                 random.Next(-gameConfig.yRange,gameConfig.yRange));
        time = Vector3.Distance(initialPosition, finalPosition) / 4;
    }
    // Update is called once per frame
    void Update()
    {
        if(!canMove) return;
        tempTime += Time.deltaTime;
        if(tempTime < time){
            transform.position = Vector3.Lerp(initialPosition, finalPosition, tempTime/time);
        }else{
            SetPosition();
        }
    }

    public void Duplicate(){
        GameObject.Instantiate(this.gameObject);
    }

    public void Kill(){
        Instantiate(killEffect, transform.position, transform.rotation);
        this.gameObject.SetActive(false);
    }
}
