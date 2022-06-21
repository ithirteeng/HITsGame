using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using MouseButton = UnityEngine.UIElements.MouseButton;
using Random = UnityEngine.Random;
using Slider = UnityEngine.UI.Slider;


public class MinigameScript : MonoBehaviour
{
    public Slider slider;
    public GameObject[] BugModel;
    public GameObject[] borders;
    public GameObject BugDeadModel;
    public GameObject[] deadLines;

    
    private List<Bug> _list = new List<Bug>();
    private float _timeToNextBug;
    private float _deltaTimeMoving = 0f;
    private float _deltaTimeRotation = 0f;
    private List<GameObject> deadBugs = new List<GameObject>();
    private class Bug
    {
        public GameObject view;
        public float speed;
        public int currentBugModel;
        public int buff = 0;

        private float direction = -90;
        private bool isLastTurnOnRight = false;

        public void init(GameObject view, float speed, int currentBugModel, float direction)
        {
            this.direction = direction;
            this.view = view;
            this.speed = speed;
            this.currentBugModel = currentBugModel;
        }

        public void move()
        {
            view.transform.position = new Vector3(
                (float)(view.transform.position.x + speed * Math.Cos(toRad(direction))),
                (float)(view.transform.position.y + speed * Math.Sin(toRad(direction))),
                view.transform.position.z);
        }

        public void rotateBug()
        {
            direction += Random.Range(-45, 45);
            view.transform.rotation = Quaternion.Euler(0f, 0f, direction -90f);
        }

        float toRad(float deg)
        {
            return deg * 2f * (float)Math.PI / 360f;
        }

        public void changeModel(GameObject model)
        {
            model.transform.position = view.transform.position;
            model.transform.rotation = view.transform.rotation;
            Destroy(view);
            view = model;
            currentBugModel += 1;
        }
        
    }
    
    private void Start()
    {
        _timeToNextBug = 0.7f;
    }

    void Update()
    {
        _timeToNextBug -= Time.deltaTime;
        _deltaTimeMoving += Time.deltaTime;
        _deltaTimeRotation += Time.deltaTime;
        
        if (_timeToNextBug < 0)
        {
            Bug newBug = new Bug();
            int a = Random.Range(0, 4);
            newBug.init(Instantiate(BugModel[0]), 0.1f, 0, -a*90 - 90 );
            _list.Add(newBug);
            _timeToNextBug = Random.Range(0.5f, 1f);
            var pos = borders[a].transform.position;
            if (a % 2 == 0) pos.x += Random.Range(0f, 7f);
            else pos.y += Random.Range(0f, 4f);
            newBug.view.transform.position = pos;
            newBug.view.transform.rotation = Quaternion.Euler(0, 0, -90*a - 180);
        }
        
        if (slider.value >= 100)
        {
            closeMinigame();
        }
        
        if (_deltaTimeMoving > 0.05f)
        {
            _deltaTimeMoving = 0;
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].move();
                _list[i].changeModel(Instantiate(BugModel[(_list[i].currentBugModel + 1) % BugModel.Length]));
            }
        }

        if (_deltaTimeRotation > 0.5)
        {
            _deltaTimeRotation = 0;
            for (int i = 0; i < _list.Count; i++)
            {
                if (Random.Range(0f, 1f) <= 0.8)
                {
                    if (_list[i].buff > 1) _list[i].rotateBug();
                    else _list[i].buff++;
                }
            }

            for (int i = _list.Count - 1; i >= 0; i--)
            {
                float x = _list[i].view.transform.position.x;
                float y = _list[i].view.transform.position.y;

                if (!(deadLines[3].transform.position.x < x && x < deadLines[1].transform.position.x &&
                    deadLines[2].transform.position.y < y && y < deadLines[0].transform.position.y))
                {
                    Destroy(_list[i].view);
                    _list.Remove(_list[i]);
                }
            }

        }
        
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            for (int i = _list.Count - 1; i >= 0; i--)
            {

                if (hit.transform.position == _list[i].view.transform.position)
                {
                    var bugDead = Instantiate(BugDeadModel);
                    bugDead.transform.position = _list[i].view.transform.position;
                    bugDead.transform.rotation = _list[i].view.transform.rotation;
                    Destroy(_list[i].view);
                    _list.Remove(_list[i]);
                    slider.value += 5f;
                    deadBugs.Add(bugDead);
                }
            }
        }
    }
    void closeMinigame()
    {
        for (int i = 0; i < _list.Count; i++)
        {
            Destroy(_list[i].view);
        }

        for (int i = 0; i < deadBugs.Count; i++)
        {
            Destroy(deadBugs[i]);
        }
        SceneManager.UnloadSceneAsync("MinigameScene");
        PlayerAppearance.player.SetActive(true);
        PlayerAppearance.camera.enabled = true;
    }
    
}