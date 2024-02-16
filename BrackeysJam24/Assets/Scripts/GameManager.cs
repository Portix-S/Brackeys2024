using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Material> materials;
    public List<GameObject> doors;
    [SerializeField] private List<GameObject> doorsToOpen;
    [SerializeField] private GameObject _selectedDoor;

    private GameObject _winningDoor;
    private GameObject _loosingDoor;
    private GameObject _remaingDoor;
    
    private bool _canInteract;
    private bool _openingDoor;
    private bool _firstChoice = true;
    private bool _secondChoice = false;
    private bool _onPrologue = true;
    private bool _firstTime = true;

    public static GameManager Instance;
    private Inputs inputAction;

    [SerializeField] private List<DoorBehaviour> prologueDoorBehaviours;
    [SerializeField] private List<DoorBehaviour> badDoorBehaviours;
    [SerializeField] private List<DoorBehaviour> neutralDoorBehaviours;
    [SerializeField] private List<DoorBehaviour> goodDoorBehaviours;

    public Inputs InputActions => inputAction;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        inputAction = new Inputs();
        inputAction.Player.Interact.performed += Interact;
        inputAction.Player.Enable();
        // GenerateWinningDoor();
    }
    
    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
        if (_canInteract && !_openingDoor)
        {
            if (_onPrologue && _firstChoice)
            {
                GenerateWinningDoor();
            }
            SelectDoor();
        }
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }

    public void SetCurrentDoor(GameObject door)
    {
        _selectedDoor = door;
        _canInteract = true;
    }
    
    public void LoseCurrentDoor(GameObject door)
    {
        if (door != _selectedDoor) return;
        _selectedDoor = null;
        _canInteract = false;
    }
    
    private void GenerateWinningDoor()
    {
        // Add enum later?
        if (_onPrologue)
        {
            doors.Remove(_selectedDoor);
            if (_firstTime)
            {
               FirstTimeGen();
            }
            else
            {
                SecondTimeGen();
            }
        }
        else
        {
            GameplaySelection();
        }
        
        
        // Just for testing
        _winningDoor.GetComponentInChildren<SpriteRenderer>().material = materials[0];
        _loosingDoor.GetComponentInChildren<SpriteRenderer>().material = materials[1];
        _remaingDoor.GetComponentInChildren<SpriteRenderer>().material = materials[2];

        Debug.Log("Winning door: " + _winningDoor);
        Debug.Log("Loosing door: " + _loosingDoor);
    }

    
    // Makes player always enter the game
    private void FirstTimeGen()
    {
        Debug.Log("First");
        _remaingDoor = _selectedDoor;
        // doors.Remove(_remaingDoor);
        doorsToOpen = new List<GameObject>();
        int winningIndex = Random.Range(0, doors.Count);
        _winningDoor = doors[winningIndex];
        doorsToOpen.Add(_winningDoor);
        doors.Remove(_winningDoor);
        int loosingIndex = Random.Range(0, doors.Count);
        _loosingDoor = doors[loosingIndex];
        doorsToOpen.Add(_loosingDoor);
        doors.Remove(_loosingDoor);
        doorsToOpen.Add(_remaingDoor);
        _winningDoor.GetComponent<Door>().SetDoorBehaviour(prologueDoorBehaviours[0]);
        _loosingDoor.GetComponent<Door>().SetDoorBehaviour(prologueDoorBehaviours[1]);
        _remaingDoor.GetComponent<Door>().SetDoorBehaviour(prologueDoorBehaviours[2]);
    }

    // Player can actually change the door, but can only win or loose, game will not restart
    private void SecondTimeGen()
    {
        Debug.Log("Second");
        int rand = Random.Range(0, 2);
        doorsToOpen = new List<GameObject>();
        if (rand == 0)
        {
            _winningDoor = _selectedDoor;
            doorsToOpen.Add(_winningDoor);
            doors.Remove(_winningDoor);
            int loosingIndex = Random.Range(0, doors.Count);
            _loosingDoor = doors[loosingIndex];
            doorsToOpen.Add(_loosingDoor);
            doors.Remove(_loosingDoor);
        }
        else
        {
            _loosingDoor = _selectedDoor;
            doorsToOpen.Add(_loosingDoor);
            doors.Remove(_loosingDoor);
            int winningIndex = Random.Range(0, doors.Count);
            _winningDoor = doors[winningIndex];
            doorsToOpen.Add(_winningDoor);
            doors.Remove(_winningDoor);
        }
        _remaingDoor = doors[0];
        doorsToOpen.Add(_remaingDoor);
        _winningDoor.GetComponent<Door>().SetDoorBehaviour(prologueDoorBehaviours[0]);
        _loosingDoor.GetComponent<Door>().SetDoorBehaviour(prologueDoorBehaviours[1]);
        _remaingDoor.GetComponent<Door>().SetDoorBehaviour(prologueDoorBehaviours[2]);
        
    }

    // Player has Free will
    private void GameplaySelection()
    {
        doorsToOpen = new List<GameObject>();
        
        int winningIndex = Random.Range(0, doors.Count);
        _winningDoor = doors[winningIndex];
        doorsToOpen.Add(_winningDoor);
        doors.Remove(_winningDoor);
        
        int loosingIndex = Random.Range(0, doors.Count);
        _loosingDoor = doors[loosingIndex];
        doorsToOpen.Add(_loosingDoor);
        doors.Remove(_loosingDoor);
        
        _remaingDoor = doors[0];
        doorsToOpen.Add(_remaingDoor);
        doors.Remove(_remaingDoor);
        
        int randomIndex = Random.Range(0, goodDoorBehaviours.Count);
        _winningDoor.GetComponent<Door>().SetDoorBehaviour(goodDoorBehaviours[randomIndex]);
        randomIndex = Random.Range(0, badDoorBehaviours.Count);
        _loosingDoor.GetComponent<Door>().SetDoorBehaviour(badDoorBehaviours[randomIndex]);
        randomIndex = Random.Range(0, neutralDoorBehaviours.Count);
        _remaingDoor.GetComponent<Door>().SetDoorBehaviour(neutralDoorBehaviours[randomIndex]);
        
    }
    
    private void SelectDoor()
    {
        // Enum can make this easier
        if (!_canInteract) return;
        if (_firstChoice)
        {
            _canInteract = false;
            _firstChoice = false;
            if(_onPrologue)
            {
                // Makes player always enter the game or win/lose situation
                if (_selectedDoor == _winningDoor || _selectedDoor == _remaingDoor)
                {
                    Debug.Log("not loosing door");
                    if(_firstTime)
                        StartCoroutine(OpenDoor(_loosingDoor));
                    else
                        StartCoroutine(OpenDoor(_remaingDoor));
                }
                else
                {
                    Debug.Log("loosing door");
                    StartCoroutine(OpenDoor(_remaingDoor));
                }
            }
            else // Open selected door
            {
                StartCoroutine(OpenDoor(_selectedDoor));
            }
        }
        else // Makes Player always win or loose 
        {
            _secondChoice = true;
            _canInteract = false;
            if (_onPrologue && _firstTime)
            {
                _firstTime = false;
                if (_selectedDoor == _winningDoor)
                {
                    // Play "Hmm, acho q n"
                }
                else
                {
                    // Play "putz, que triste"
                }
                StartCoroutine(OpenDoor(_remaingDoor, true));
                return;
            }
            StartCoroutine(OpenDoor(_selectedDoor,true));
        }
        //Open Door
    }
    
    // Open a door, play animation, remove from list
    private IEnumerator OpenDoor(GameObject door, bool activeDoor = false)
    {
        _openingDoor = true;
        door.GetComponent<Door>().OpenDoor(activeDoor);
        doorsToOpen.Remove(door);
        yield return new WaitForSeconds(1);
        if(!_secondChoice)
            _openingDoor = false;
        _canInteract = true;
        
        // 
        if (!_onPrologue && doorsToOpen.Count > 0)
        {
            doorsToOpen[0].GetComponent<Door>().OpenDoor(false);
            doorsToOpen[1].GetComponent<Door>().OpenDoor(false);
        }
        if(_secondChoice && _onPrologue)
        {
            _onPrologue = false;
            _secondChoice = false;
        }
        // door.GetComponent<Animator>().SetTrigger("Open");
        // Get Door Component and play open animation / sound / give "reward"
    }

    public void SetNewDoors(List<GameObject> newDoors, bool prologueTrigger)
    {
        doors.Clear();
        doors = newDoors;
        _openingDoor = false;
        if (prologueTrigger)
        {
            _onPrologue = true;
            _firstChoice = true;
            _secondChoice = false;
        }
        else
        {
            GenerateWinningDoor();
        }

    }

}
