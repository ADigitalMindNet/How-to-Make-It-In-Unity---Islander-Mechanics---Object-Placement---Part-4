using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
	public Material canPlaceMaterial;
	public Material cantPlaceMaterial;
	public GameObject objectToBePlaced;

	private bool _canPlace = false;
	private Renderer objectsRenderer;
	private Bounds objectBounds;
	private bool _collidingWithBuilding = false;
	private bool _objectPlaced = false;

    // Start is called before the first frame update
    void Start()
    {
		objectsRenderer = this.GetComponent<Renderer>();
		objectBounds = objectsRenderer.bounds;
    }

    // Update is called once per frame
    void Update()
    {
		SetObjectMaterial();
    }

	private void SetObjectMaterial()
	{
		if (_canPlace && !_collidingWithBuilding || _objectPlaced)
		{
			objectsRenderer.material = canPlaceMaterial;
		} else
		{
			objectsRenderer.material = cantPlaceMaterial;
		}
	}

	public void SetCanPlace(float normalValue)
	{
		if(normalValue == 1)
		{
			_canPlace = true;
		} else
		{
			_canPlace = false;
		}
	}

	public void SetObjectPlaced()
	{
		_objectPlaced = true;
	}

	public void PlaceObject(Vector3 place, Quaternion rotation)
	{
		if (_canPlace && !_collidingWithBuilding)
		{
			GameObject createdObject =  Instantiate(objectToBePlaced, place + new Vector3(0, objectBounds.extents.y, 0), rotation);
			createdObject.GetComponent<ObjectScript>().SetCanPlace(1);
			createdObject.GetComponent<ObjectScript>().SetObjectPlaced();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Building")
		{
			_collidingWithBuilding = true;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Building")
		{
			_collidingWithBuilding = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Building")
		{
			_collidingWithBuilding = false;
		}
	}
}
