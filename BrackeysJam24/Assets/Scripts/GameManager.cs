using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    public static GameManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // GenerateWinningDoor();
    }

    // Update is called once per frame
    void Update()
    {
        if (_canInteract && Input.GetKeyDown(KeyCode.E) && !_openingDoor)
        {
            if (_onPrologue && _firstChoice)
            {
                GenerateWinningDoor();
            }
            SelectDoor();
        }
    }

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
        if (_onPrologue)
        {
            _remaingDoor = _selectedDoor;
            doors.Remove(_remaingDoor);
        }
        // Randomly select a door to be the winning door
        doorsToOpen = new List<GameObject>();
        int winningIndex = Random.Range(0, doors.Count);
        doorsToOpen.Add(doors[winningIndex]);
        doors.Remove(doors[winningIndex]);
        int loosingIndex = Random.Range(0, doors.Count);
        doorsToOpen.Add(doors[loosingIndex]);
        doors.Remove(doors[loosingIndex]);
        if (!_onPrologue)
        {
            doorsToOpen.Add(doors[0]);
            doors.Remove(doors[0]);
            _remaingDoor = doorsToOpen[2];
        }
        else
        {
            doorsToOpen.Add(_remaingDoor);
        }
        _winningDoor = doorsToOpen[0];
        _loosingDoor = doorsToOpen[1];
        Debug.Log("Winning door: " + _winningDoor);
        Debug.Log("Loosing door: " + _loosingDoor);
    }

    private void SelectDoor()
    {
        if (!_canInteract) return;
        if (_firstChoice)
        {
            _canInteract = false;
            _firstChoice = false;
            if (_selectedDoor == _winningDoor || _selectedDoor == _remaingDoor)
            {
                Debug.Log("not loosing door");
                StartCoroutine(OpenDoor(_loosingDoor));
            }
            else
            {
                Debug.Log("loosing door");
                StartCoroutine(OpenDoor(_remaingDoor));
            }
        }
        else
        {
            _secondChoice = true;
            _canInteract = false;
            if (_onPrologue)
            {
                if (_selectedDoor == _winningDoor)
                {
                    // Play "Hmm, acho q n"
                }
                else
                {
                    // Play "putz, que triste"
                }
                StartCoroutine(OpenDoor(_remaingDoor));
                return;
            }
            StartCoroutine(OpenDoor(_selectedDoor));
        }
        //Open Door
    }
    
    private IEnumerator OpenDoor(GameObject door)
    {
        _openingDoor = true;
        door.GetComponent<Animator>().SetTrigger("OpenDoor");
        doorsToOpen.Remove(door);
        yield return new WaitForSeconds(1);
        if(!_secondChoice)
            _openingDoor = false;
        _canInteract = true;
        if (_secondChoice && !_onPrologue)
        {
            _secondChoice = false;
            StartCoroutine(OpenDoor(doorsToOpen[0]));
        }
        if(_secondChoice && _onPrologue)
        {
            _onPrologue = false;
            _secondChoice = false;
        }
        // door.GetComponent<Animator>().SetTrigger("Open");
        // Get Door Component and play open animation / sound / give "reward"
    }

}
