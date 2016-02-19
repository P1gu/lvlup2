using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

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

    public GameObject gui;
    
    public GameObject spawnSbire;

    private EtatDuJeu etatDuJeu = EtatDuJeu.AvantPreparation;

    public int tempsDeLaPreparationEnSec = 4;
    private float tempRestantPrepartion=0;

    private int lvl = 1;

    private int positionDeLaSelection = 0;

    private int aventurierEnCombat = 0;
    private List<GameObject> lesAventuriersDuLvl = new List<GameObject>();

    private int sbireEnCours = 0;
    private List<GameObject> lesSbiresDuLvl = new List<GameObject>();

    private int nbrDeVieTotalAventurier = 1; 

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

        EventManager.playPhase1Start();
        etatDuJeu =EtatDuJeu.Preparation;
    }

    private void Preparation() {
        sbireEnCours = -1;
        int swap2 = -1;
        bool select = Input.GetKey(KeyCode.Space);
        bool left = Input.GetKeyDown(KeyCode.D);
        bool right = Input.GetKeyDown(KeyCode.A);

        if (left && select)
        {
            swap2 = positionDeLaSelection == 0 ? lesSbiresDuLvl.Count - 1 : positionDeLaSelection - 1;
        }
        else if (right && select)
        {
            swap2 = positionDeLaSelection == lesSbiresDuLvl.Count - 1 ? 0 : positionDeLaSelection + 1;
        }
        else if (left)
        {
            positionDeLaSelection = positionDeLaSelection == 0 ? lesSbiresDuLvl.Count - 1 : positionDeLaSelection - 1;
        }
        else if (right) {
            positionDeLaSelection = positionDeLaSelection == lesSbiresDuLvl.Count - 1 ? 0 : positionDeLaSelection + 1;
        }

        if (swap2 > -1) {
            Debug.Log("Swap - position "+positionDeLaSelection+" swap2 "+swap2);
            SwapOrdreSbires(positionDeLaSelection, swap2);
            positionDeLaSelection = swap2;
        }

        afficherLesSbiresPourLaPreparation();

        checkFinDelaPreparation();
    }

    private void afficherLesSbiresPourLaPreparation()
    {
        int offset = 2;
        Vector3 v3 = spawnSbire.transform.position;
        GameObject sbire;
        for ( int i= lesSbiresDuLvl.Count-1; i >= 0;i-- ) {
            sbire = lesSbiresDuLvl[i];
            v3.x += offset;
            
            sbire.transform.position = v3;
            sbire.transform.rotation = spawnSbire.transform.rotation;

            // permet de mettre en evidence le personnage selectoinner
            Vector3 scale = sbire.transform.localScale;
            if (positionDeLaSelection == i) {
                scale.x = 3;
                scale.y = 3;
                scale.z = 3;
            }
            else {
                scale.x = 2;
                scale.y = 2;
                scale.z = 2;
            }
            sbire.transform.localScale = scale;

        }
    }

    private void checkFinDelaPreparation() {
        tempRestantPrepartion -= Time.deltaTime;
        if (tempRestantPrepartion < 0)
        {
            etatDuJeu = EtatDuJeu.EnCombat;
            sortirLesSbires();
            FairePopLeProchainSbire();
            EventManager.playPhase2Start();
        }
    }

    private void EnCombat() {
        GererLeSbire();
        
    }

    private void GererLeSbire() {
        lesSbiresDuLvl[sbireEnCours].GetComponent<PlayableCharacter>().Move(Input.GetAxis("Horizontal"));
        if(Input.GetKey("space"))
            lesSbiresDuLvl[sbireEnCours].GetComponent<PlayableCharacter>().Jump();
        if (Input.GetButtonDown("Fire1"))
            lesSbiresDuLvl[sbireEnCours].GetComponent<PlayableCharacter>().Action1();
        if (Input.GetButtonDown("Fire2"))
            lesSbiresDuLvl[sbireEnCours].GetComponent<PlayableCharacter>().Action2();
    }

    private void FinDuCombat() {
        //todo: clear les mobs
        lvl++;
    }

    public void unSbireEstMort() {

        FairePopLeProchainSbire();
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

    private void sortirLesSbires() {
        int offset = 0;
        foreach (GameObject sbire in lesSbiresDuLvl)
        {
            Vector3 v3 = spawnSbire.transform.position;
            v3.x += -40+offset;

            sbire.GetComponent<Rigidbody>().isKinematic = true;

            sbire.transform.position = v3;
            sbire.transform.rotation = spawnSbire.transform.rotation;
            offset += 2;


        }
    }

    private void FairePopLeProchainSbire() {

        sbireEnCours++;
        if (sbireEnCours < lesSbiresDuLvl.Count)
        {
            GameObject sbireEnCoursDeCombat = lesSbiresDuLvl[sbireEnCours];
            placerSurLeSpawnSbire(sbireEnCoursDeCombat);

            //todo remove in prod
            Invoke("unSbireEstMort", 5);
        }
        else {
            Application.LoadLevel("GameOver");
        }

       
    }

    private void placerSurLeSpawnSbire(GameObject sbire) {
        sbire.transform.position = spawnSbire.transform.position;
        sbire.transform.rotation = spawnSbire.transform.rotation;

        Vector3 scale = sbire.transform.localScale;

        sbire.GetComponent<Rigidbody>().isKinematic = false;

        scale.y = 2;
        scale.x = 2;
        scale.z = 2;
        sbire.transform.localScale = scale;

    }

    private void GenererLesSbiresDuLvl() {
        lesSbiresDuLvl = new List<GameObject>();
        GameObject leSbire =null ;
        for (int i = 0; i < (nbrDeSbireAuLvl1 + lvl - 1) % maxSbiresParLvl;i++) {
            int type= Random.Range(0, 2);
            switch (type) { //todo: definir la position
                case 0:
                    leSbire = Instantiate(sbireArcher);
                    
                    break;
                case 1:
                    leSbire=(Instantiate(sbireMage));
                    break;
                case 2:
                    leSbire=(Instantiate(sbireGuerrier));
                    break;
            }
            lesSbiresDuLvl.Add(leSbire);           
        }
    }

    private void GenererLesAventuriersDuLvl() {
        //todo:


    }

    public void SwapOrdreSbires(int s1, int s2) {
        List<GameObject> lesSbireTmp = lesSbiresDuLvl;
        GameObject tmp = lesSbireTmp[s1];
        lesSbireTmp[s1] = lesSbireTmp[s2];
        lesSbireTmp[s2] = tmp;
        lesSbiresDuLvl = lesSbireTmp;
    }
}