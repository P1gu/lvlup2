using UnityEngine;
using System.Collections.Generic;



public class GameManager : MonoBehaviour {
    private enum EtatDuJeu
    {
        AvantPreparation,Preparation, EnCombat, FinDuCombat
    }

    public int maxSbiresParLvl = 10;
    public int nbrDeSbireAuLvl1 = 4;


    public GameObject sbireArcher;
    public GameObject sbireMage;
    public GameObject sbireGuerrier;

    public GameObject spawnSbire;


    EtatDuJeu etatDuJeu = EtatDuJeu.AvantPreparation;

    public int tempsDeLaPreparationEnSec = 30;
    private float tempRestantPrepartion=30;


    int lvl = 1;

    int positionDeLaSelection = 0;

    int aventurierEnCombat = 0;
    List<GameObject> lesAventuriersDuLvl = new List<GameObject>();

    int sbireEnCours = 0;
    List<GameObject> lesSbiresDuLvl = new List<GameObject>();

    int nbrDeVieTotalAventurier = 1; 

	void Start () {
	    
	}
	
	void Update () {
        switch (etatDuJeu) {
            case EtatDuJeu.AvantPreparation:
                AvantPreparation();
                break;
            case EtatDuJeu.Preparation:
                Preparation();
                break;
            case EtatDuJeu.EnCombat:
                EnCombat();
                break;
            case EtatDuJeu.FinDuCombat:
                FinDuCombat();
                break;
        }
    }

    private void AvantPreparation() {
        tempRestantPrepartion = tempsDeLaPreparationEnSec;
        GenererLesAventuriersDuLvl();
        GenererLesSbiresDuLvl();


        etatDuJeu=EtatDuJeu.Preparation;
    }

    private void Preparation() {
        sbireEnCours = 0;
        int swap2 = -1;
        bool select = Input.GetKey("space");
        bool left = Input.GetKeyDown("left");
        bool right = Input.GetKeyDown("right");

        if (left && select) {
            swap2 = positionDeLaSelection == 0 ? lesSbiresDuLvl.Count - 1 : positionDeLaSelection - 1;
        } else if (right && select) {
            swap2 = positionDeLaSelection == lesSbiresDuLvl.Count - 1 ? 0 : positionDeLaSelection + 1;
        }

        if (swap2 > -1) { 
            SwapOrdreSbires(positionDeLaSelection, swap2);
            positionDeLaSelection = swap2;
        }

        checkFinDelaPreparation();
    }

    private void checkFinDelaPreparation() {
        tempRestantPrepartion -= Time.deltaTime;
        if (tempRestantPrepartion < 0)
        {
            etatDuJeu = EtatDuJeu.EnCombat;
            FairePopLeProchainSbire();
        }
    }

    private void EnCombat() {
      
    }

    private void FinDuCombat() {
        //todo: clear les mobs
        lvl++;
    }


    public void unSbireEstMort() {
        if (sbireEnCours < lesSbiresDuLvl.Count)
        {
            FairePopLeProchainSbire();
            
        }
        else {
            //GAME OVER
            //todo:changer de scene
        }

    }

    public void unAventurierEstMort() {
        if (aventurierEnCombat < lesAventuriersDuLvl.Count)
        {// il y a encore des ennemis
            FairePopLeProchainAventurier();
        }
        else
        {
            etatDuJeu = EtatDuJeu.FinDuCombat;            
        }
    }



    private void FairePopLeProchainAventurier() {
        //todo:
    }

    private void FairePopLeProchainSbire() {
        GameObject sbireEnCoursDeCombat = lesSbiresDuLvl[sbireEnCours];
        placerSurLeSpawnSbire(sbireEnCoursDeCombat);
        sbireEnCours++;
    }

    private void placerSurLeSpawnSbire(GameObject sbire) {
        sbire.transform.position = spawnSbire.transform.position;
        sbire.transform.rotation = spawnSbire.transform.rotation;
    }

    private void GenererLesSbiresDuLvl() {
        lesSbiresDuLvl = new List<GameObject>();
        for (int i = 0; i < (nbrDeSbireAuLvl1 + lvl - 1) % maxSbiresParLvl;i++) {
            int type= Random.Range(0, 2);
            switch (type) { //todo: definir la position
                case 0:
                    lesSbiresDuLvl.Add(Instantiate(sbireArcher));
                    break;
                case 1:
                    lesSbiresDuLvl.Add(Instantiate(sbireMage));
                    break;
                case 2:
                    lesSbiresDuLvl.Add(Instantiate(sbireGuerrier));
                    break;
            }
        }
    }

    private void GenererLesAventuriersDuLvl() {
        //todo:
    }

    public void SwapOrdreSbires(int s1, int s2) {
        GameObject tmp = lesSbiresDuLvl[s1];
        lesSbiresDuLvl[s1] = lesSbiresDuLvl[s2];
        lesAventuriersDuLvl[s2] = tmp;
    }
}