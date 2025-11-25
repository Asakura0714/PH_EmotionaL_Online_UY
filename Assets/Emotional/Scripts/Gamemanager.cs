using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    private GameMainLogicCount _mainLogic;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainLogic = new GameMainLogicCount(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
