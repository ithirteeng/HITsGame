using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameMode : MonoBehaviour
{

    public GameObject Player;
    public GameObject[] spawns;
    public GameObject eggModel;
    public TextMeshProUGUI counter;
    public TextMeshProUGUI timer;

    private float _currentPlayerX;
    private float _currentPlayerY;
    private int _lastInd;
    private float _deltaTime;
    private float _spawnDeltaTime = 1f;
    private float timerDeltaTime;
    private int secondTimer;
    
    
    void Start()
    {
        _currentPlayerX = Player.transform.position.x;
        _currentPlayerY = Player.transform.position.y;
        counter.text = "0";
        timer.text = "--:--";
        _deltaTime = 0f;
    }
    void Update()
    {
        UpdateTimer();
        updatePlayer();

        timerDeltaTime += Time.deltaTime;
        _deltaTime += Time.deltaTime;

        if (_deltaTime >= _spawnDeltaTime)
        {
            SpawnEgg();
            _spawnDeltaTime = Random.Range(0.7f, 1.3f);
            //_spawnDeltaTime = 0.7f;
            _deltaTime = 0f;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        counter.text = (int.Parse(counter.text) + 1).ToString();
        Destroy(col.gameObject);
    }

    void UpdateTimer()
    {
        if (int.Parse(counter.text) > 3 && timer.text == "--:--")
        {
            timer.text = "01:00";
            secondTimer = 60;
            timerDeltaTime = 0f;
        }
        else if(timerDeltaTime > 1f && timer.text != "--:--" && secondTimer > 0)
        {
            secondTimer -= 1;
            timer.text = "00:";
            if (secondTimer < 10) timer.text += "0";
            timer.text += secondTimer.ToString();
            timerDeltaTime = 0;
        }
    }
    void updatePlayer()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            _currentPlayerY = Math.Abs(_currentPlayerY) * 1;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            _currentPlayerY = Math.Abs(_currentPlayerY) * -1;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            _currentPlayerX = Math.Abs(_currentPlayerX) * -1;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            _currentPlayerX = Math.Abs(_currentPlayerX) * 1;

        Player.transform.position = new Vector3(_currentPlayerX, _currentPlayerY, Player.transform.position.z);
    }
    
    void SpawnEgg()
    {
        int ind = _lastInd;
        while (ind == _lastInd)
        {
            ind = Random.Range(0, spawns.Length);
        }
        _lastInd = ind;
        GameObject egg = Instantiate(eggModel);
        
        egg.transform.position = new Vector3(
            spawns[ind].transform.position.x, 
            spawns[ind].transform.position.y + Random.Range(0f, 0.5f),
            spawns[ind].transform.position.z);
    }
}
