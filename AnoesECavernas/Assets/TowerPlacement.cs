using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacement : MonoBehaviour
{
    public GameObject Tower;
    private bool caminho, MouseUI;
    private int coinsaux;
    public int coins;
    private bool ocupado = false;
    [SerializeField] private SpriteRenderer sr;
    Cursor cursor;
    [SerializeField] GameObject rangePrefab;

    private void Start()
    {
        GameObject range = Instantiate(rangePrefab, transform.position, Quaternion.identity, gameObject.transform);
        range.transform.localScale = 2 * range.transform.localScale * Tower.GetComponent<Barrack>().atkRange;
    }
    // Update is called once per frame
    void Update()
    {
        IsOverlappingWithTag("Tower");
        // Update the current player's coins
        coinsaux = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerResources>().coins;

        // Check if the mouse is over a UI element
        if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.CompareTag("Botão"))
        {
            MouseUI = true;
        }
        else
        {
            MouseUI = false;
        }

        // Check if the cursor is over a valid path (assuming you have a Cursor script managing this)
        caminho = GetComponent<Cursor>().caminhoBool;

        // Check for mouse click, path validity, UI interaction, coin availability, and occupation status
        if (Input.GetMouseButtonDown(0) && !caminho && !MouseUI && coins <= coinsaux && !ocupado)
        {
            // Check if the current collider overlaps with any "TOWER" colliders
            if (IsOverlappingWithTag("Tower"))
            {
                Debug.Log("Cannot place tower here, position is occupied by another tower.");
                return;
            }

            // Instantiate the tower at the current position
            GameObject tower = Instantiate(Tower, transform.position, transform.rotation, null);

            // Deduct the cost of the tower from the player's resources
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerResources>().AddResource(-tower.GetComponent<Barrack>().price);

            // Deactivate the current game object (if necessary)
            transform.gameObject.SetActive(false);
        }
    }

    private bool IsOverlappingWithTag(string tag)
    {
        // Create a contact filter for the collider's layer mask
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useTriggers = false;

        // List to store overlapping colliders
        List<Collider2D> results = new List<Collider2D>();

        // Check for overlapping colliders
        int overlapCount = GetComponent<Collider2D>().OverlapCollider(contactFilter, results);

        // Iterate through the overlapping colliders and check their tags
        foreach (Collider2D col in results)
        {
            if (col.CompareTag(tag) || cursor.caminhoBool)
            {
                sr.color = Color.red;
                return true; // Overlapping with a collider having the specified tag
            }
        }
        sr.color = Color.white;
        return false; // No overlapping colliders with the specified tag found
    }
}
