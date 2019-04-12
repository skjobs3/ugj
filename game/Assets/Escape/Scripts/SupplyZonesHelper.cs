using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyZonesHelper : MonoBehaviour
{
    struct ContactInfo
    {
        public Vector3 contactPoint;
        public Vector3 contactRay;
        public SupplyZone refZone;
    };

    public CameraController cameraController;
    public GameObject supplyZoneMarker;
    public GameObject supplyZoneFuelMarker;
    public List<SupplyZone> supplyZones;
    public float startFadeDistance = 5.0f;

    private List<ContactInfo> contacts = new List<ContactInfo>();
    private List<GameObject> contactMarkers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(var zone in supplyZones)
        {
            if (zone.Type == SupplyZone.SupplyType.Ammo)
            {
                GameObject marker = Instantiate(supplyZoneMarker);
                marker.SetActive(false);
                contactMarkers.Add(marker);
            }
            else
            {
                GameObject marker = Instantiate(supplyZoneFuelMarker);
                marker.SetActive(false);
                contactMarkers.Add(marker);
            }
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
            Vector3 directionVector = new Vector3(transform.position.x - zone.transform.position.x, transform.position.y - zone.transform.position.y, transform.position.z);
            RaycastHit2D[] hits = Physics2D.RaycastAll(zone.transform.position, directionVector);
            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject.GetComponent<SupplyZonesHelper>() != null)
                {
                    ContactInfo contact = new ContactInfo();
                    contact.contactPoint = hit.point;
                    contact.contactRay = (-directionVector).normalized;
                    contact.refZone = zone;
                    contacts.Add(contact);
                    break; //ignores second 
                }
            }
        }
    }

    void Update()
    {
        transform.position = Camera.main.transform.position;
        for(int i = 0; i < contacts.Count; i++)
        {
            var contact = contacts[i];
            var marker = contactMarkers[i];
            marker.SetActive(true);

            float angle = Vector2.Angle(Vector2.up, contact.contactRay);
            marker.transform.position = contact.contactPoint;
            marker.transform.eulerAngles = new Vector3(0, 0, contact.refZone.transform.position.x > transform.position.x ? -angle + 90 : angle + 90);

            var distance = Vector3.Distance(marker.transform.position, contact.refZone.transform.position);
            if(distance < startFadeDistance)
            {
                var spriteColor = marker.GetComponent<SpriteRenderer>().color;
                spriteColor.a = distance / startFadeDistance;
                marker.GetComponent<SpriteRenderer>().color = spriteColor;
            }
        }
    }
}
