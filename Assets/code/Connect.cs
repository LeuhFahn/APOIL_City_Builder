using UnityEngine;
using System.Collections;

public class Connect : MonoBehaviour {
	
	public string matchIP = "127.0.0.1"; //127.0.0.1 est l'adresse locale universelle.
	public string matchPort="8000";
	public int maxClients=32;
	//GUI
	public int labelW=80;
	public int fieldW=100;
	public int elementsH=35;
	public int fieldX=80;
	
	void OnGUI(){
		//Regarde si on est déconnecté, le serveur, un client connecté, ou bien en train de se connecter
		switch(Network.peerType){
				//ce qui est affiché lorsqu'on démarre le jeu, ou lorsqu'on est déconnecté
			case NetworkPeerType.Disconnected :
				Disconnected_GUI();
				break;
				//ce qui est affiché lorsqu'on est connecté au serveur en tant que Client
			case NetworkPeerType.Client :
				Client_GUI();
				break;
				//ce qui est affiché lorsqu'on est celui qui a initialisé le serveur
			case NetworkPeerType.Server :
				Server_GUI();
				break;
				//ce qui est affiché lorsqu'on est entrain de se connecter au serveur.
			case NetworkPeerType.Connecting :
				Connecting_GUI();
				break;
		}
	}
	
	//creation des champs d'entrée
	void Disconnected_GUI(){
		//IP
		GUI.Label(new Rect(10,10,labelW,elementsH),"Server IP");
		matchIP=GUI.TextField(new Rect(fieldX, 10, fieldW, elementsH), matchIP);
		//Port
		GUI.Label(new Rect(10,50,labelW,elementsH),"Server Port");
		matchPort=GUI.TextField(new Rect(fieldX, 50, fieldW, elementsH), matchPort);
		//conversion de string en int pour les éléments nécessaires
        /*	int connectPort = int.Parse(matchPort);
            //Bouton de connexion
            if(GUI.Button(new Rect(10,120,150,30),"Connect to server")){
                Network.Connect(matchIP,connectPort); 
            }
		//Bouton d'initialisation du serveur
		if(GUI.Button(new Rect(10,150,150,30),"Start a server")){
			bool useNat = !Network.HavePublicAddress();
			Network.InitializeServer(maxClients, connectPort, useNat); 
		}*/
    }
	
	void Client_GUI(){
		GUI.Label(new Rect(10,10,500,100),"Connecte au serveur :"+matchIP);
	}
	
	void Server_GUI(){
		GUI.Label(new Rect(10,10,500,100),"Nombre de clients : "+Network.connections.Length);
	}
	
	void Connecting_GUI(){
		GUI.Label(new Rect(10,10,500,100),"Connexion au serveur...");
	}


    public void ConnectToServer()
    {
        int connectPort = int.Parse(matchPort);
        Network.Connect(matchIP, connectPort);
    }

    public void StartAServer()
    {
        int connectPort = int.Parse(matchPort);
        bool useNat = !Network.HavePublicAddress();
        Network.InitializeServer(maxClients, connectPort, useNat); 
    }

}