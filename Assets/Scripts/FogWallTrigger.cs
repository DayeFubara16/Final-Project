using UnityEngine;

public class FogWallTrigger : MonoBehaviour
{
    [Header("References")]
    public GameObject fogWallObject;
    public Transform teleportPoint;

    [Header("Settings")]
    public KeyCode interactKey = KeyCode.E;
    public string playerTag = "Player";

    private bool playerInRange = false;
    private GameObject currentPlayer;

    void Start()
    {
        if (fogWallObject != null)
        {
            fogWallObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && currentPlayer != null && Input.GetKeyDown(interactKey))
        {
            TeleportPlayer();
            ActivateFog();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = true;
            currentPlayer = other.gameObject;
            Debug.Log("Press E to enter the dungeon.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = false;
            currentPlayer = null;
        }
    }

    private void TeleportPlayer()
    {
        CharacterController controller = currentPlayer.GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.enabled = false;
            currentPlayer.transform.position = teleportPoint.position;
            currentPlayer.transform.rotation = teleportPoint.rotation;
            controller.enabled = true;
        }
        else
        {
            currentPlayer.transform.position = teleportPoint.position;
            currentPlayer.transform.rotation = teleportPoint.rotation;
        }
    }

    private void ActivateFog()
    {
        if (fogWallObject != null)
        {
            fogWallObject.SetActive(true);
        }
    }
}