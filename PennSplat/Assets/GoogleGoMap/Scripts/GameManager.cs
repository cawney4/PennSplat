﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

	[HideInInspector]
	public bool locationServicesIsRunning = false;

	public GameObject mainMap;
	public GameObject newMap;

	public GameObject player;
	public GeoPoint playerGeoPosition;
	public PlayerLocationService player_loc;


	public Vector3 lastPosition;
	public float distance;
	public Text trueHeadingText;
	public Text distanceText;

	public float time;

	public enum PlayerStatus { TiedToDevice, FreeFromDevice }

	private PlayerStatus _playerStatus;
	public PlayerStatus playerStatus
	{
		get { return _playerStatus; }
		set { _playerStatus = value; }
	}

	void Awake (){

		Time.timeScale = 1;
		playerStatus = PlayerStatus.TiedToDevice;

		player_loc = player.GetComponent<PlayerLocationService>();
		newMap.GetComponent<MeshRenderer>().enabled = false;
		newMap.SetActive (false);


	}

	public GoogleStaticMap getMainMapMap () {
		return mainMap.GetComponent<GoogleStaticMap> ();
	}

	public GoogleStaticMap getNewMapMap () {
		return newMap.GetComponent<GoogleStaticMap> ();
	}

	IEnumerator Start () {


		getMainMapMap ().initialize ();
		yield return StartCoroutine (player_loc._StartLocationService ());
		StartCoroutine (player_loc.RunLocationService ());

		locationServicesIsRunning = player_loc.locServiceIsRunning;
		Debug.Log ("Player loc from GameManager: " + player_loc.loc);
		getMainMapMap ().centerMercator = getMainMapMap ().tileCenterMercator (player_loc.loc);
		getMainMapMap ().DrawMap ();

		mainMap.transform.localScale = Vector3.Scale (
			new Vector3 (getMainMapMap ().mapRectangle.getWidthMeters (), getMainMapMap ().mapRectangle.getHeightMeters (), 1.0f),
			new Vector3 (getMainMapMap ().realWorldtoUnityWorldScale.x, getMainMapMap ().realWorldtoUnityWorldScale.y, 1.0f));

		player.GetComponent<ObjectPosition> ().setPositionOnMap (player_loc.loc);

		GameObject[] objectsOnMap = GameObject.FindGameObjectsWithTag ("ObjectOnMap");

		foreach (GameObject obj in objectsOnMap) {
			obj.GetComponent<ObjectPosition> ().setPositionOnMap ();
		}

		lastPosition = player.transform.position;
		distance = 0.0f;
		trueHeadingText.text = "";
		distanceText.text = "";
    }

    void Update () {
		if(!locationServicesIsRunning){

			//TODO: Show location service is not enabled error. 
			return;
		}

		// playerGeoPosition = getMainMapMap ().getPositionOnMap(new Vector2(player.transform.position.x, player.transform.position.z));
		playerGeoPosition = new GeoPoint();
		// GeoPoint playerGeoPosition = getMainMapMap ().getPositionOnMap(new Vector2(player.transform.position.x, player.transform.position.z));
		if (playerStatus == PlayerStatus.TiedToDevice) {
			playerGeoPosition = player_loc.loc;
			player.GetComponent<ObjectPosition> ().setPositionOnMap (playerGeoPosition);
		} else if (playerStatus == PlayerStatus.FreeFromDevice){
			playerGeoPosition = getMainMapMap ().getPositionOnMap(new Vector2(player.transform.position.x, player.transform.position.z));
		}


		// Rotate the charactor direction
		float playerDirection = player_loc.trueHeading;
		trueHeadingText.text = playerDirection.ToString();
		Vector3 controllerRotation = new Vector3 (Mathf.Cos (playerDirection * Mathf.PI / 180.0f), 1, Mathf.Sin (playerDirection * Mathf.PI / 180.0f));
		player.transform.rotation = Quaternion.Euler (controllerRotation);

		// Calculate the distance
		float dis = Vector3.Distance(player.transform.position, lastPosition);
		distance += dis;
		lastPosition = player.transform.position;
		distanceText.text = distance.ToString();

		var tileCenterMercator = getMainMapMap ().tileCenterMercator (playerGeoPosition);
		if(!getMainMapMap ().centerMercator.isEqual(tileCenterMercator)) {

			newMap.SetActive(true);
			getNewMapMap ().initialize ();
			getNewMapMap ().centerMercator = tileCenterMercator;

			getNewMapMap ().DrawMap ();

			getNewMapMap ().transform.localScale = Vector3.Scale(
				new Vector3 (getNewMapMap ().mapRectangle.getWidthMeters (), getNewMapMap ().mapRectangle.getHeightMeters (), 1.0f),
				new Vector3(getNewMapMap ().realWorldtoUnityWorldScale.x, getNewMapMap ().realWorldtoUnityWorldScale.y, 1.0f));	

			Vector2 tempPosition = GameManager.Instance.getMainMapMap ().getPositionOnMap (getNewMapMap ().centerLatLon);
			newMap.transform.position = new Vector3 (tempPosition.x, 0, tempPosition.y);


			GameObject temp = newMap;
			newMap = mainMap;
			mainMap = temp;

		}
		if(getMainMapMap().isDrawn && mainMap.GetComponent<MeshRenderer>().enabled == false){
			mainMap.GetComponent<MeshRenderer>().enabled = true;
			newMap.GetComponent<MeshRenderer>().enabled = false;
			newMap.SetActive(false);
		}
	}

	public Vector3 ScreenPointToMapPosition(Vector2 point){
		var ray = Camera.main.ScreenPointToRay(point);
		//RaycastHit hit;
		// create a plane at 0,0,0 whose normal points to +Y:
		Plane hPlane = new Plane(Vector3.up, Vector3.zero);
		float distance = 0; 
		if (!hPlane.Raycast (ray, out distance)) {
			// get the hit point:
			return new Vector3(0, 0, 0);
		}
		Vector3 location = ray.GetPoint (distance);
		return location;
	}

}
