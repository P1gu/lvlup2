using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class GameManager : MonoBehaviour {
    private enum EtatDuJeu
    {
        AvantPreparation, Preparation, EnCombat, FinDuCombat
    }

    public float scalePersonnage = 2.5f;

    public int maxSbiresParLvl = 10;
    public int nbrDeSbireAuLvl1 = 4;

    public Text txt_niveau;
    public Text txt_sbireEnVie;
    public Text txt_tempRestant;
    public Text txt_score;

    public GameObject sbireArcher;
    public GameObject sbireMage;
    public GameObject sbireGuerrier;

    public GameObject aventurierArcher;
    public GameObject aventurierMage;
    public GameObject aventurierGuerrier;

    public GameObject bulleArcher;
    public GameObject bulleMage;
    public GameObject bulleAventurier;
    public GameObject positionPremierBulle;

    public GameObject gui;

    public GameObject spawnSbire;

    public GameObject spawnAventurier;

    private EtatDuJeu etatDuJeu = EtatDuJeu.AvantPreparation;

    public int tempsDeLaPreparationEnSec = 30;
    private float tempRestantPrepartion = 0;

    private int lvl = 1;

    private int positionDeLaSelection = 0;

    private int aventurierEnCours = 0;
    private List<GameObject> lesAventuriersDuLvl = new List<GameObject>();

    private int sbireEnCours = 0;
    private List<GameObject> lesSbiresDuLvl = new List<GameObject>();

    private int nbrDeVieTotalAventurier = 1;

    private int score = 0;

    void Start() {
        EventManager.OnUnAventurierEstMort += unAventurierEstMort;
        EventManager.OnUnSbireEstMort += unSbireEstMort;
    }

    void Update() {

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

        mettreAjourGUI();
    }

    private void mettreAjourGUI() {
        txt_score.text = "" + score;
        txt_tempRestant.text = "" + (int)tempRestantPrepartion;
        txt_sbireEnVie.text = "" + (lesSbiresDuLvl.Count - sbireEnCours - 1);
        txt_niveau.text = "" + lvl;
    }

    private void AvantPreparation() {
        positionDeLaSelection = 0;
        tempRestantPrepartion = tempsDeLaPreparationEnSec;
        GenererLesAventuriersDuLvl();
        GenererLesSbiresDuLvl();
        afficherLaProchaineVague();

        EventManager.playPhase1Start();

        sbireEnCours = -1;
        aventurierEnCours = 0;
        EventManager.playPhase1Start();
        etatDuJeu = EtatDuJeu.Preparation;
    }

    private bool keyDownLeft() {
        return true;
    }
    private bool keyDownRight() {
        return true;
    }


    private void Preparation() {
        
        int swap2 = -1;
        bool select = Input.GetKey(KeyCode.Space);
        bool right = Input.GetKeyDown(KeyCode.D);
        bool left = Input.GetKeyDown(KeyCode.A);
        bool skip = Input.GetKey(KeyCode.Q);

        if (skip) {
            tempRestantPrepartion = 0;
        }
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
        int offset = -2;
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
                scale.x = -scalePersonnage-1;
                scale.y = scalePersonnage+1;
                scale.z = scalePersonnage+1;
            }
            else {
                scale.x = -scalePersonnage;
                scale.y = scalePersonnage;
                scale.z = scalePersonnage;
            }
            sbire.transform.localScale = scale;

        }
    }

    private void afficherLaProchaineVague() {
        int offset = 0;
        GameObject bulle=null;
        foreach (GameObject go in lesAventuriersDuLvl) {
            if (go.GetComponent<Archer>() != null)
            {
                 bulle = Instantiate(bulleArcher); 
            }
            else if (go.GetComponent<Guerrier>() != null)
            {
                bulle = Instantiate(bulleAventurier);
            }
            else if (go.GetComponent<Mage>() != null)
            {
                bulle = Instantiate(bulleMage);
            }
            
            Vector3 v3= positionPremierBulle.transform.position;
            v3.y += offset * 3;
            bulle.transform.position = v3;
            offset--;
        }
    }

    private void checkFinDelaPreparation() {
        tempRestantPrepartion -= Time.deltaTime;
        if (tempRestantPrepartion < 0)
        {
            etatDuJeu = EtatDuJeu.EnCombat;
            sortirLesSbires();
            FairePopLeProchainSbire();
            FairePopLeProchainAventurier();

            EventManager.playPhase2Start();
        }
    }

    private void EnCombat() {
        GererLeSbire();
    }

    private void GererLeSbire() {
        lesSbiresDuLvl[sbireEnCours].GetComponent<PlayableCharacter>().Move(Input.GetAxis("Horizontal"));
        if(Input.GetButtonDown("Jump"))
            lesSbiresDuLvl[sbireEnCours].GetComponent<PlayableCharacter>().Jump();
        if (Input.GetButtonDown("Fire1"))
            lesSbiresDuLvl[sbireEnCours].GetComponent<PlayableCharacter>().Action1();
        if (Input.GetButtonDown("Fire2"))
            lesSbiresDuLvl[sbireEnCours].GetComponent<PlayableCharacter>().Action2();
    }

    private void FinDuCombat() {
        //todo: clear les sbires

        foreach (GameObject go in lesSbiresDuLvl) {
            DestroyImmediate(go);
        }

        lvl++;
        etatDuJeu=EtatDuJeu.AvantPreparation;
    }

    public void unSbireEstMort() {
        lesSbiresDuLvl[sbireEnCours].tag = "Untagged";
        FairePopLeProchainSbire();
    }

    public void unAventurierEstMort() {
        aventurierEnCours++;
        score += 154;
        if (aventurierEnCours < lesAventuriersDuLvl.Count)
        {// il y a encore des ennemis
            FairePopLeProchainAventurier();
        }
        else
        {
            etatDuJeu = EtatDuJeu.FinDuCombat;            
        }
    }

    private void FairePopLeProchainAventurier() {
            GameObject aventurierEnCoursDeCombat = lesAventuriersDuLvl[aventurierEnCours];
            placerSurLeSpawnAventurier(aventurierEnCoursDeCombat);
    }

    private void sortirLesSbires() {
        int offset = 0;
        foreach (GameObject sbire in lesSbiresDuLvl)
        {
            Vector3 v3 = spawnSbire.transform.position;
            v3.x += +40+offset;

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
            sbireEnCoursDeCombat.tag = "Sbire";
            placerSurLeSpawnSbire(sbireEnCoursDeCombat);
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

        scale.y = scalePersonnage;
        scale.x = scalePersonnage;
        scale.z = scalePersonnage;
        sbire.transform.localScale = scale;

    }


    private void placerSurLeSpawnAventurier(GameObject sbire)
    {
        sbire.transform.position = spawnAventurier.transform.position;
        sbire.transform.rotation = spawnAventurier.transform.rotation;

        Vector3 scale = sbire.transform.localScale;

        sbire.GetComponent<Rigidbody>().isKinematic = false;

        scale.y = scalePersonnage;
        scale.x = scalePersonnage;
        scale.z = scalePersonnage;
        sbire.transform.localScale = scale;

    }
    
    private void GenererLesSbiresDuLvl() {
        lesSbiresDuLvl = new List<GameObject>();
        GameObject leSbire =null ;
        for (int i = 0; i <( (nbrDeSbireAuLvl1 + lvl - 1) % maxSbiresParLvl)+1;i++) {
            int type= Random.Range(0, 3);
            switch (type) {
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
        lesAventuriersDuLvl = new List<GameObject>();
        //4 aventurier par pallier max premier palier c'est 1
        //int palier = (lvl / 16) + 1;
        int nbrDePointsDeVieTotal = lvl;
        int lvlDuPalier = (lvl-1 % 16)+1;
        int nbrAventurier=0;
        switch (lvlDuPalier) { 
            case 16:
            case 15:
            case 14:
            case 13:
                nbrAventurier = 4;
                break;

            case 12:
            case 11:
            case 10:
            case 9:
                nbrAventurier = Random.Range(3, 5);//nbr 3 -4
                break;
            case 8:
            case 7:
            case 6:
            case 5:
                nbrAventurier = Random.Range(2, 5);//nb 2-4
                break;
            case 4:
                nbrAventurier = Random.Range(1, 5);//1-4
                break;
            case 3:
                nbrAventurier = Random.Range(1, 4);//1-3
                break;
            case 2:
                nbrAventurier = Random.Range(1, 3);//1-2
                break;
            case 1:
                nbrAventurier = 1;// 1
                break;
        }

        GameObject aventurier = null;
        for (int i = 0; i < nbrAventurier; i++)
        {
            int type = Random.Range(0, 3);
            switch (type)
            {
                case 0:
                    aventurier = Instantiate(aventurierArcher);
                    break;
                case 1:
                    aventurier = (Instantiate(aventurierMage));
                    break;
                case 2:
                    aventurier = (Instantiate(aventurierGuerrier));
                    break;
            }
            aventurier.GetComponent<Rigidbody>().isKinematic = true;
            aventurier.GetComponent<CharacterBehaviour>().health= Random.Range(1, 5);
            lesAventuriersDuLvl.Add(aventurier);
        }


    }

    public void SwapOrdreSbires(int s1, int s2) {
        List<GameObject> lesSbireTmp = lesSbiresDuLvl;
        GameObject tmp = lesSbireTmp[s1];
        lesSbireTmp[s1] = lesSbireTmp[s2];
        lesSbireTmp[s2] = tmp;
        lesSbiresDuLvl = lesSbireTmp;
    }
}