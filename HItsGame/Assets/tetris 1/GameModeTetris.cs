using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameModeTetris : MonoBehaviour
{

    public GameObject[] blocksModel;
    public GameObject spawner;
    public GameObject bottomLeft;
    public GameObject bottomRight;
    public float movingDeltaTime;
    public GameObject[] shadowModel;
    public GameObject ERotation;
    public GameObject QRotation;
    public TextMeshProUGUI scoreText;
    public static bool isSeparateGame;
    public GameObject button;
    public GameObject exitButton;

    private class Pair
    {
        public readonly int X;
        public int Y;

        public Pair(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    private bool[][][,] _typesOfBlocks;
    private const int CountOfTypes = 7;

    private int scoreByRemoveLine = 100;

    private bool isStopped;
    private Pair _currentType;
    private float _movingTimer;
    private GameObject[][] _fallingBlock;
    private GameObject[][] _shadowBlock;
    private GameObject[][] QBlock;
    private GameObject[][] EBlock;
    private const float Scale = 0.5f;
    private readonly Pair _sizeOfBlocks = new Pair(4, 4);
    private readonly Pair _sizeOfPlane = new Pair(10, 20);
    private readonly List<GameObject[]> _plane = new List<GameObject[]>();
    private int score = 0;

    private void Start()
    {
        isStopped = false;
        if (!isSeparateGame)
        {
            exitButton.SetActive(false);
            button.SetActive(false);
        }
        initTypes();
        for (int i = 0; i < _sizeOfPlane.Y; i++)
        {
            _plane.Add(generateNewLine());
        }

        _shadowBlock = new GameObject[_sizeOfBlocks.X][];
        _fallingBlock = new GameObject[_sizeOfBlocks.X][];
        QBlock = new GameObject[_sizeOfBlocks.X][];
        EBlock = new GameObject[_sizeOfBlocks.X][];
        for (int i = 0; i < _sizeOfBlocks.X; i++)
        {
            _shadowBlock[i] = new GameObject[_sizeOfBlocks.Y];
            _fallingBlock[i] = new GameObject[_sizeOfBlocks.Y];
            QBlock[i] = new GameObject[_sizeOfBlocks.Y];
            EBlock[i] = new GameObject[_sizeOfBlocks.Y];
        }

        spawn();
        makeShadow(_fallingBlock);
        _movingTimer = movingDeltaTime;

        button.GetComponent<Button>().onClick.AddListener(delegate
        {
            flush();
            Start();
        }); 
        exitButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            closeScene();
        });
    }

    private void Update()
    {
        if (!isStopped)
        {
            _movingTimer -= Time.deltaTime;
            if (_movingTimer < 0)
            {
                _movingTimer = movingDeltaTime;
                if (CanMove(new Vector2(0f, -Scale), _fallingBlock))
                {
                    move(new Vector2(0f, -Scale), _fallingBlock);
                }
                else
                {
                    putBlock();
                    updatePlate();
                    spawn();
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (CanMove(new Vector2(-Scale, 0f), _fallingBlock))
                {
                    move(new Vector2(-Scale, 0f), _fallingBlock);
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (CanMove(new Vector2(Scale, 0f), _fallingBlock))
                {
                    move(new Vector2(Scale, 0f), _fallingBlock);
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (CanMove(new Vector2(0f, -Scale), _fallingBlock))
                {
                    move(new Vector2(0f, -Scale), _fallingBlock);
                }
                else
                {
                    putBlock();
                    updatePlate();
                    spawn();
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (canRotate(KeyCode.Q))
                {
                    Rotate(KeyCode.Q);
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (canRotate(KeyCode.E))
                {
                    Rotate(KeyCode.E);
                }
            }

            makeShadow(_fallingBlock);
            updateRotation();
            scoreText.text = score.ToString();
            updateSpeed();
        }

        if (score >= 1000 && !isSeparateGame)
        {
            closeScene();
            isStopped = true;
        }
    }

    void flush()
    {
        for (int i = 0; i < _sizeOfBlocks.X; i++)
        {
            for (int j = 0; j < _sizeOfBlocks.Y; j++)
            {
                if (_fallingBlock[i][j] != null) Destroy(_fallingBlock[i][j]);
                if (_shadowBlock[i][j] != null) Destroy(_shadowBlock[i][j]);
                if (QBlock[i][j] != null) Destroy(QBlock[i][j]);
                if (EBlock[i][j] != null) Destroy(EBlock[i][j]);
            }
        }

        for (int i = 0; i < _sizeOfPlane.Y; i++)
        {
            for (int j = 0; j < _sizeOfPlane.X; j++)
            {
                if (_plane[i][j] != null) Destroy(_plane[i][j]);
            }
        }
    }

    void closeScene()
    {
        flush();
        SceneManager.UnloadSceneAsync("TetrisScene");
        PlayerAppearance.player.SetActive(true);
        PlayerAppearance.camera.enabled = true;
    }

    private void updateSpeed()
    {
        movingDeltaTime = 1f / (float)Math.Log(score + 100, 8);
    }

    private void updateRotation()
    {
        for (int i = 0; i < _sizeOfBlocks.X; i++)
        {
            for (int j = 0; j < _sizeOfBlocks.Y; j++)
            {
                if (EBlock[i][j] != null) Destroy(EBlock[i][j]);
                if (QBlock[i][j] != null) Destroy(QBlock[i][j]);
                if (_typesOfBlocks[_currentType.X][(_currentType.Y + 4) % 4][i, j])
                {
                    EBlock[i][j] = Instantiate(blocksModel[0]);
                    EBlock[i][j].transform.SetParent(bottomLeft.transform);
                    EBlock[i][j].transform.position = new Vector3(ERotation.transform.position.x + i * Scale,
                        ERotation.transform.position.y + j * Scale);
                    EBlock[i][j].transform.localScale = new Vector3(EBlock[i][j].transform.localScale.x * Scale,
                        EBlock[i][j].transform.localScale.y * Scale);
                }

                if (_typesOfBlocks[_currentType.X][(_currentType.Y + 2) % 4][i, j])
                {
                    QBlock[i][j] = Instantiate(blocksModel[0]);
                    QBlock[i][j].transform.SetParent(bottomLeft.transform);

                    QBlock[i][j].transform.position = new Vector3(QRotation.transform.position.x + i * Scale,
                        QRotation.transform.position.y + j * Scale);
                    QBlock[i][j].transform.localScale = new Vector3(QBlock[i][j].transform.localScale.x * Scale,
                        QBlock[i][j].transform.localScale.y * Scale);
                }
            }
        }
    }

    private bool CanMove(Vector2 delta, GameObject[][] block)
    {
        for (int i = 0; i < _sizeOfBlocks.Y; i++)
        {
            for (int j = 0; j < _sizeOfBlocks.X; j++)
            {
                if (block[i][j] == null) continue;
                float x = (block[i][j].transform.position.x - bottomLeft.transform.position.x + delta.x) / Scale;
                float y = (block[i][j].transform.position.y - bottomLeft.transform.position.y + delta.y) / Scale;
                if (!(0 <= x && x < _sizeOfPlane.X && 0 <= y)) return false;
                if (y >= _sizeOfPlane.Y) continue;
                if (_plane[(int)y][(int)x]) return false;
            }
        }

        return true;
    }

    private void Rotate(KeyCode keyCode)
    {
        float x = bottomLeft.transform.position.x - Scale, y = x;
        for (int i = 0; i < _sizeOfBlocks.X; i++)
        {
            for (int j = 0; j < _sizeOfBlocks.Y; j++)
            {
                if (_fallingBlock[j][i] == null) continue;
                if (x == bottomLeft.transform.position.x - Scale)
                {
                    x = _fallingBlock[j][i].transform.position.x - i * Scale;
                    y = _fallingBlock[j][i].transform.position.y + j * Scale;
                }

                Destroy(_fallingBlock[j][i]);
            }
        }

        int offset = 1;
        if (keyCode == KeyCode.Q) offset = -1;
        _currentType.Y += offset;
        if (_currentType.Y < 0) _currentType.Y += 4;
        _currentType.Y %= 4;
        for (int i = 0; i < _sizeOfBlocks.X; i++)
        {
            for (int j = 0; j < _sizeOfBlocks.Y; j++)
            {
                if (!_typesOfBlocks[_currentType.X][_currentType.Y][j, i]) continue;
                _fallingBlock[j][i] = Instantiate(blocksModel[0]);
                _fallingBlock[j][i].transform.SetParent(bottomLeft.transform);

                _fallingBlock[j][i].transform.position = new Vector3(x + i * Scale, y - j * Scale);
                _fallingBlock[j][i].transform.localScale = new Vector3(
                    _fallingBlock[j][i].transform.localScale.x * Scale,
                    _fallingBlock[j][i].transform.localScale.y * Scale,
                    _fallingBlock[j][i].transform.localScale.z * Scale);
            }
        }
    }

    private void makeShadow(GameObject[][] block)
    {
        for (int i = 0; i < _sizeOfBlocks.X; i++)
        {
            for (int j = 0; j < _sizeOfBlocks.Y; j++)
            {
                if (_shadowBlock[i][j] != null) Destroy(_shadowBlock[i][j]);
                if (block[i][j] != null)
                {
                    _shadowBlock[i][j] = Instantiate(shadowModel[0]);
                    _shadowBlock[i][j].transform.SetParent(bottomLeft.transform);

                    _shadowBlock[i][j].transform.position = new Vector3(
                        block[i][j].transform.position.x,
                        block[i][j].transform.position.y);
                    _shadowBlock[i][j].transform.localScale = new Vector3(
                        block[i][j].transform.localScale.x,
                        _fallingBlock[i][j].transform.localScale.y);
                }
            }
        }

        while (CanMove(new Vector2(0f, -Scale), _shadowBlock))
        {
            move(new Vector2(0f, -Scale), _shadowBlock);
        }
    }

    private bool canRotate(KeyCode keyCode)
    {
        float x = bottomLeft.transform.position.x - Scale, y = x;
        for (int i = 0; i < _sizeOfBlocks.X; i++)
        {
            for (int j = 0; j < _sizeOfBlocks.Y; j++)
            {
                if (_fallingBlock[j][i] == null) continue;
                if (x == bottomLeft.transform.position.x - Scale)
                {
                    x = _fallingBlock[j][i].transform.position.x - i * Scale;
                    y = _fallingBlock[j][i].transform.position.y + j * Scale;
                }
            }
        }

        int offset = 1;
        if (keyCode == KeyCode.Q) offset = -1;
        for (int i = 0; i < _sizeOfBlocks.X; i++)
        {
            for (int j = 0; j < _sizeOfBlocks.Y; j++)
            {
                if (!_typesOfBlocks[_currentType.X][(_currentType.Y + offset + 4) % 4][i, j]) continue;
                if (_typesOfBlocks[_currentType.X][(_currentType.Y + offset + 4) % 4][i, j] ==
                    _typesOfBlocks[_currentType.X][_currentType.Y][i, j]) continue;
                var pos = new Vector3(x + j * Scale, y - i * Scale);
                if (!(bottomLeft.transform.position.x <= pos.x && pos.x <= bottomRight.transform.position.x &&
                      bottomLeft.transform.position.y <= pos.y)) return false;
                if (pos.y >= bottomLeft.transform.position.y + _sizeOfPlane.Y * Scale) continue;
                if (_plane[(int)((pos.y - bottomLeft.transform.position.y) / Scale)]
                    [(int)((pos.x - bottomLeft.transform.position.x) / Scale)]) return false;
            }
        }

        return true;
    }

    private void putBlock()
    {
        for (int i = 0; i < _sizeOfBlocks.Y; i++)
        {
            for (int j = 0; j < _sizeOfBlocks.X; j++)
            {
                if (_fallingBlock[i][j] == null) continue;
                float x = (_fallingBlock[i][j].transform.position.x - bottomLeft.transform.position.x) / Scale;
                float y = (_fallingBlock[i][j].transform.position.y - bottomLeft.transform.position.y) / Scale;

                if (y >= _sizeOfPlane.Y)
                {
                    isStopped = true;
                    if (!isSeparateGame) closeScene();
                    
                }
                else
                {
                    _plane[(int)y][(int)x] = _fallingBlock[i][j];
                }
            }
        }
    }

    private void move(Vector2 delta, GameObject[][] block)
    {
        for (int i = 0; i < _sizeOfBlocks.Y; i++)
        {
            for (int j = 0; j < _sizeOfBlocks.X; j++)
            {
                if (block[i][j] == null) continue;
                var pos = block[i][j].transform.position;
                block[i][j].transform.position = new Vector3(pos.x + delta.x, pos.y + delta.y, pos.z);
            }
        }
    }

    private void spawn()
    {
        int index = Random.Range(0, 7);
        // index = 0;
        _currentType = new Pair(index, 0);

        for (int i = 0; i < _sizeOfBlocks.X; i++)
        {
            for (int j = 0; j < _sizeOfBlocks.Y; j++)
            {
                if (_typesOfBlocks[_currentType.X][_currentType.Y][i, j])
                {
                    _fallingBlock[i][j] = Instantiate(blocksModel[0]);
                    _fallingBlock[i][j].transform.SetParent(bottomLeft.transform);
                }
                else _fallingBlock[i][j] = null;
                
            }
        }

        var offset = Random.Range(-(_sizeOfPlane.X / 2 - 2), (_sizeOfPlane.X / 2 - 1));
        for (int i = 0; i < _sizeOfBlocks.Y; i++)
        {
            for (int j = 0; j < _sizeOfBlocks.X; j++)
            {
                if (_fallingBlock[i][j] == null) continue;
                _fallingBlock[i][j].transform.localScale = new Vector3(
                    _fallingBlock[i][j].transform.localScale.x * Scale,
                    _fallingBlock[i][j].transform.localScale.y * Scale,
                    _fallingBlock[i][j].transform.localScale.z * Scale);
                _fallingBlock[i][j].transform.position = new Vector3(
                    spawner.transform.position.x - (1.5f - j - offset) * Scale,
                    spawner.transform.position.y - i * Scale + Scale / 2.0f,
                    0f);
            }
        }
    }

    private void updatePlate()
    {
        int count = 0;
        for (int i = _sizeOfPlane.Y - 1; i >= 0; i--)
        {
            if (isLineFull(i))
            {
                removeLine(i);
                count++;
            }
        }

        score += count * count * scoreByRemoveLine;
    }

    private bool isLineFull(int lineIndex)
    {
        for (int j = 0; j < _sizeOfPlane.X; j++)
        {
            if (!_plane[lineIndex][j])
            {
                return false;
            }
        }

        return true;
    }

    private void removeLine(int lineIndex)
    {
        for (int i = 0; i < _sizeOfPlane.X; i++)
        {
            if (_plane[lineIndex][i] == null) continue;
            Destroy(_plane[lineIndex][i]);
        }

        updatePosition(lineIndex);
        _plane.Remove(_plane[lineIndex]);
        _plane.Add(generateNewLine());
    }

    private void updatePosition(int deletedLine)
    {
        for (int i = deletedLine; i < _sizeOfPlane.Y; i++)
        {
            for (int j = 0; j < _sizeOfPlane.X; j++)
            {
                if (_plane[i][j] == null) continue;
                _plane[i][j].transform.position = new Vector3(_plane[i][j].transform.position.x,
                    _plane[i][j].transform.position.y - Scale);
            }
        }
    }

    private GameObject[] generateNewLine()
    {
        GameObject[] line = new GameObject[_sizeOfPlane.X];
        for (int i = 0; i < _sizeOfPlane.X; i++)
        {
            line[i] = null;
        }

        return line;
    }

    private void initTypes()
    {
        _typesOfBlocks = new bool[CountOfTypes][][,];
        for (int i = 0; i < CountOfTypes; i++)
        {
            _typesOfBlocks[i] = new bool[4][,];
            for (int j = 0; j < 4; j++)
            {
                _typesOfBlocks[i][j] = new bool[_sizeOfBlocks.X, _sizeOfBlocks.Y];
            }
        }

        _typesOfBlocks[0][0] = new bool[4, 4]
        {
            { false, false, false, false },
            { false, false, false, false },
            { true, true, true, true },
            { false, false, false, false }
        };
        _typesOfBlocks[0][1] = new bool[4, 4]
        {
            { false, false, true, false },
            { false, false, true, false },
            { false, false, true, false },
            { false, false, true, false }
        };
        _typesOfBlocks[0][2] = _typesOfBlocks[0][0];
        _typesOfBlocks[0][3] = _typesOfBlocks[0][1];

        _typesOfBlocks[1][0] = new bool[4, 4]
        {
            { false, false, false, false },
            { false, true, false, false },
            { false, true, true, true },
            { false, false, false, false }
        };
        _typesOfBlocks[1][1] = new bool[4, 4]
        {
            { false, false, true, false },
            { false, false, true, false },
            { false, true, true, false },
            { false, false, false, false }
        };
        _typesOfBlocks[1][2] = new bool[4, 4]
        {
            { false, false, false, false },
            { false, true, true, true },
            { false, false, false, true },
            { false, false, false, false }
        };
        _typesOfBlocks[1][3] = new bool[4, 4]
        {
            { false, false, true, true },
            { false, false, true, false },
            { false, false, true, false },
            { false, false, false, false }
        };

        _typesOfBlocks[2][0] = new bool[4, 4]
        {
            { false, false, false, false },
            { false, false, false, true },
            { false, true, true, true },
            { false, false, false, false }
        };
        _typesOfBlocks[2][1] = new bool[4, 4]
        {
            { false, false, true, false },
            { false, false, true, false },
            { false, false, true, true },
            { false, false, false, false }
        };
        _typesOfBlocks[2][2] = new bool[4, 4]
        {
            { false, false, false, false },
            { false, true, true, true },
            { false, true, false, false },
            { false, false, false, false }
        };
        _typesOfBlocks[2][3] = new bool[4, 4]
        {
            { false, true, true, false },
            { false, false, true, false },
            { false, false, true, false },
            { false, false, false, false }
        };

        _typesOfBlocks[3][0] = new bool[4, 4]
        {
            { false, false, false, false },
            { false, true, true, false },
            { false, true, true, false },
            { false, false, false, false }
        };
        _typesOfBlocks[3][1] = _typesOfBlocks[3][0];
        _typesOfBlocks[3][2] = _typesOfBlocks[3][0];
        _typesOfBlocks[3][3] = _typesOfBlocks[3][0];

        _typesOfBlocks[4][0] = new bool[4, 4]
        {
            { false, false, false, false },
            { false, false, true, true },
            { false, true, true, false },
            { false, false, false, false }
        };
        _typesOfBlocks[4][1] = new bool[4, 4]
        {
            { false, true, false, false },
            { false, true, true, false },
            { false, false, true, false },
            { false, false, false, false }
        };
        _typesOfBlocks[4][2] = _typesOfBlocks[4][0];
        _typesOfBlocks[4][3] = _typesOfBlocks[4][1];

        _typesOfBlocks[5][0] = new bool[4, 4]
        {
            { false, false, false, false },
            { false, true, true, false },
            { false, false, true, true },
            { false, false, false, false }
        };
        _typesOfBlocks[5][1] = new bool[4, 4]
        {
            { false, false, false, true },
            { false, false, true, true },
            { false, false, true, false },
            { false, false, false, false }
        };
        _typesOfBlocks[5][2] = _typesOfBlocks[5][0];
        _typesOfBlocks[5][3] = _typesOfBlocks[5][1];

        _typesOfBlocks[6][0] = new bool[4, 4]
        {
            { false, false, false, false },
            { false, false, true, false },
            { false, true, true, true },
            { false, false, false, false }
        };
        _typesOfBlocks[6][1] = new bool[4, 4]
        {
            { false, false, true, false },
            { false, true, true, false },
            { false, false, true, false },
            { false, false, false, false }
        };
        _typesOfBlocks[6][2] = new bool[4, 4]
        {
            { false, false, false, false },
            { false, true, true, true },
            { false, false, true, false },
            { false, false, false, false }
        };
        _typesOfBlocks[6][3] = new bool[4, 4]
        {
            { false, false, true, false },
            { false, false, true, true },
            { false, false, true, false },
            { false, false, false, false }
        };
    }
}
