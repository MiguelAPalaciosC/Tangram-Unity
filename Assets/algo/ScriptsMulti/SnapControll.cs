using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class SnapControll : MonoBehaviour
{
    public List<Transform> snapPoints;
    public List<Draggable> draggableObjects;
    public List<Vector3> auxDraggables;
    public GameObject pieza;
    public TextMeshProUGUI text;
    public TextMeshProUGUI puntuacion;
    public TextMeshProUGUI timer;
    //public Button resetButton;
    public float snapRange = 0.5f;

    // IA Variables
    private int Depth = 5;

    private GridGenerate<Draggable> grid;
    private ArrayList goodPositions;
    private int score;
    private float tiempoRestante;
    private bool isActive;
    private GameEvents geSC;


    private void Awake()
    {
        grid = new GridGenerate<Draggable>(4, 4, 100, pieza, snapPoints);
        goodPositions = new ArrayList();
        auxDraggables = draggableObjects.Select((x) => x.transform.localPosition).ToList();
        geSC = new GameEvents();
    }
    private void Start()
    {
        isActive = true;
        tiempoRestante = 30;
        score = 20;
        geSC.OnPieceUp += OnPieceUp;
        foreach (Draggable draggable in draggableObjects)
        {
            //OnDragEnded(draggable.dragb);
            draggable.dragEndedCallback = OnDragEnded;
        }
    }

    private void Update()
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            resetDraggables();
        }
        gameOver();
        
    }

    private void resetDraggables()
    {
        isActive = true;
        tiempoRestante = 30;
        score = 20;
        for (int i = 0; i < draggableObjects.Count; i++)
        {
            draggableObjects[i].transform.localPosition = auxDraggables[i];
            grid.resetDict();
        }
        puntuacion.text = "" + score;
    }
    private void gameOver()
    {
        if (isActive)
        {
            tiempoRestante -= Time.deltaTime;
            timer.SetText("" + Mathf.Round(tiempoRestante) + " sg");
            if (tiempoRestante < 0 || score < 1)
            {
                isActive = false;
                PhotonNetwork.Disconnect();
                SceneManager.LoadScene("GameOverMultiplayer");
            }
        }

    }
    public void actualizarScore(int punto)
    {
        score = score - punto;
        Debug.Log(score);
        puntuacion.text = "" + score;
    }
    private void OnDragEnded(Draggable draggable)
    {
        float closestDistance = 0;
        Transform closestSnapPoint = null;
        foreach (Transform snapPoint in snapPoints)
        {
            float currentDistance = Vector3.Distance(draggable.transform.localPosition, snapPoint.localPosition);
            if (closestSnapPoint == null || currentDistance < closestDistance)
            {
                closestSnapPoint = snapPoint;
                closestDistance = currentDistance;
            }
        }

        if (closestSnapPoint != null && closestDistance <= snapRange)
        {
            Vector2 fixedSnapPoint = new Vector2(closestSnapPoint.localPosition.x, closestSnapPoint.localPosition.y);
            draggable.transform.localPosition = fixedSnapPoint;
            grid.setValue(fixedSnapPoint, draggable);
        }
        if (!Win())
        {
            Debug.Log("");
            Draggable auxPiece = getOkPiece();
            //Debug.Log(auxPiece);
            var goodPosToAuxPiece = getSamePieces(auxPiece);
            if (goodPositions.Count >= 1)
            {
                foreach (Vector2 dra in goodPosToAuxPiece)
                {
                    auxPiece.transform.localPosition = dra;
                    grid.setValue(dra, auxPiece);
                }
            }
            Debug.Log("--->> "+grid.getPieces().Keys.Count);
        }
        else
        {
            Debug.Log("Finish!!!!!.....");
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("WinMultiplayer");
            //Toca colocar la escena gano



            //var Keys = grid.getPieces().Keys.Select(x => x.name).ToList();
            //var Values = grid.getPieces().Values.ToList();
            //Dictionary<string, Vector2> auxDict = new Dictionary<string, Vector2>();
            //for (int i = 0; i < Keys.Count; i++)
            //{
            //    auxDict[Keys[i]] = Values[i];
            //}
            //if (!goodPositions.Contains(auxDict.ToString()))
            //{
            //    goodPositions.Add(auxDict);
            //}

            //foreach(Dictionary<Draggable, Vector2> ps in goodPositions)
            //{
            //    ConsoleOutput(ps);
            //}
        }
        //MiniMaxAI();

    }

    private ArrayList getSamePieces(Draggable wantedPiece)
    {
        ArrayList samePieces = new ArrayList();
        foreach (Dictionary<string, Vector2> dic in goodPositions)
        {
            samePieces.Add(dic[wantedPiece.name]);
        }
        return samePieces;
    }

    private Draggable getRandomPiece(int i)
    {
        return draggableObjects[i];
    }

    private Draggable getOkPiece()
    {
        Draggable d = default(Draggable);
        for (int i = 0; i < 15; i++)
        {
            d = getRandomPiece(i);
            if (!grid.getPieces().ContainsKey(d)) break;
        }
        return d;
    }

    public void BtnVolverM()
    {
        SceneManager.LoadScene("Loading");
    }

    private bool winningMove()
    {
        bool isOverSomething = false;
        Dictionary<Draggable, Vector2> piezas = grid.getPieces();
        foreach (var pieza in piezas.Keys)
        {
            if (pieza != null)
            {
                if (pieza.collides())
                {
                    isOverSomething = true;
                }

            }
        }
        return !isOverSomething;
    }

    private bool finalMove()
    {
        int counter = 0;
        Dictionary<Draggable, Vector2> piezas = grid.getPieces();
        foreach (var pieza in piezas.Keys)
        {
            if (pieza != null)
            {
                counter++;
            }
        }
        //Debug.LogFormat("counter {0}", counter);
        //Debug.LogFormat("count {0}", draggableObjects.Count);

        return counter == draggableObjects.Count;
    }
    private bool Win()
    {
        //Debug.Log("winningMove" + winningMove());
        //Debug.Log("finalMove" + finalMove());
        return finalMove() && winningMove();
    }
    void ConsoleOutput(Dictionary<Draggable, Vector2> Keys)
    {
        foreach (KeyValuePair<Draggable, Vector2> kvp in Keys)
        {
            Debug.Log("Key = {0} + Value = {1}" + kvp.Key + kvp.Value);
        }
    }


    private void OnPieceUp(Draggable draggable)
    {
        grid.deletePiece(draggable);
    }
}
