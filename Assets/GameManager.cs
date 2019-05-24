using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject theCube;
	public LayerMask groundLayer;
	public Material canPlaceMaterial;
	public Material cantPlaceMaterial;

	private ObjectScript cubeScript;

    // Start is called before the first frame update
    void Start()
    {
		cubeScript = theCube.GetComponent<ObjectScript>();
    }

    // Update is called once per frame
    void Update()
    {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		Renderer cubeRender = theCube.GetComponent<Renderer>();
		Bounds cubeBounds = cubeRender.bounds;

		if (Physics.Raycast(ray,out hit,100f,groundLayer))
		{
			theCube.SetActive(true);
			cubeScript.SetCanPlace(hit.normal.y);

			theCube.transform.position = hit.point + new Vector3(0, cubeBounds.extents.y, 0);

			if (Input.GetMouseButtonDown(0) && hit.normal.y == 1)
			{
				cubeScript.PlaceObject(hit.point, Quaternion.identity);
			}

		} else
		{
			theCube.SetActive(false);
		}



    }
}
