using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyZonesHelper : MonoBehaviour
{
    struct ContactInfo
    {
        public Vector3 contactPoint;
        public Vector3 contactRay;
    };

    public CameraController cameraController;
    public GameObject supplyZoneMarker;
    public List<SupplyZone> supplyZones;

    private List<ContactInfo> contacts = new List<ContactInfo>();
    private List<GameObject> contactMarkers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(var zone in supplyZones)
        {
            GameObject marker = Instantiate(supplyZoneMarker);
            marker.SetActive(false);
            contactMarkers.Add(marker);
        }
    }

    bool testVisibility(Vector3 position, Rect rect)
    {
        bool xInside = position.x > rect.x && position.x < (rect.x + rect.width);
        bool yInside = position.y > rect.y && position.y < (rect.y + rect.height);
        return xInside && yInside;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        contacts.Clear();

        var cameraBounds = cameraController.getCameraBounds();
        var cameraPosition = Camera.main.transform.position;
        transform.position = cameraPosition;

        var collider = gameObject.GetComponent<BoxCollider2D>();
        var colliderSize = collider.size;
        colliderSize.x = cameraBounds.width;
        colliderSize.y = cameraBounds.height;
        collider.size = colliderSize;

        foreach (var zone in supplyZones)
        {
            Vector3 directionVector = new Vector3(zone.transform.position.x - transform.position.x, zone.transform.position.y - transform.position.y, transform.position.z);
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, directionVector);
            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject.GetComponent<SupplyZonesHelper>() != null)
                {
                    ContactInfo contact = new ContactInfo();
                    contact.contactPoint = hit.point;
                    contact.contactRay = directionVector.normalized;
                    contacts.Add(contact);
                }
            }
        }
    }

    void Update()
    {
        
    }
}
