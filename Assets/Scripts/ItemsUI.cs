using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsUI : MonoBehaviour
{
    // Start is called before the first frame update

    public Player player;
    public GameObject lighter;
    public GameObject drawerKey;
    public GameObject penDrive;
    public GameObject corkscrew;
    public GameObject Cup;
    public GameObject bathroomKey;
    public GameObject knife;
    public GameObject exitKey;

    void Start()
    {
        gameObject.SetActive(true);
        lighter.SetActive(false);
        drawerKey.SetActive(false);
        penDrive.SetActive(false);
        corkscrew.SetActive(false);
        Cup.SetActive(false);
        bathroomKey.SetActive(false);
        knife.SetActive(false);
        exitKey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        lighter.SetActive(player.hasItem("Lighter"));
        drawerKey.SetActive(player.hasItem("Drawer Key"));
        penDrive.SetActive(player.hasItem("Pen Drive"));
        corkscrew.SetActive(player.hasItem("Corkscrew"));
        Cup.SetActive(player.hasItem("Cup"));
        bathroomKey.SetActive(player.hasItem("Bathroom Key"));
        knife.SetActive(player.hasItem("Knife"));
        exitKey.SetActive(player.hasItem("Exit Key"));
    }
}
