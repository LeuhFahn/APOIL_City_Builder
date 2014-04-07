using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {
	
	//On crée une liste de players
	public List<PlayerManager> playersList = new List<PlayerManager>();
	
	public GameObject playerPrefab; //On demande la prefab de player
	
	void OnServerInitialized() {
		
		//on apelle la fonction qui nous permettra de faire apparaître le joueur de l'admin.
		Debug.Log ("server initialized");
		spawnPlayer(Network.player);
	}

	void OnPlayerConnected(NetworkPlayer player)	
	{
		spawnPlayer(player);	
	}

	void spawnPlayer(NetworkPlayer player)	
	{
		//On lui trouve un id unique afin de pouvoir le supprimer plus tard dans le Network
		int playerNumber = int.Parse(player+"");
		
		//On l'instancie
		GameObject playerInstance = Network.Instantiate(playerPrefab,transform.position,transform.rotation,playerNumber) as GameObject;
		playerInstance.transform.Translate(10 * playerNumber, 0,0);
		//On récupère son playerManager afin de l'ajouter à la liste (Et non pas directement le GameObject afin de le manipuler plus facilement).
		PlayerManager playerInstanceManager = playerInstance.GetComponent<PlayerManager>();
		
		playersList.Add(playerInstanceManager);
	}



	
}