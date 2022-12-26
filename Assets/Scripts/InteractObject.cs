using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class InteractObject : MonoBehaviour
{
    public AudioClip doorClip;
    public AudioClip pickup;

    AudioSource audio;

    public float interactDistance = 5f;
    public Camera maincamera;
    private string eToCollect = "Press 'E' to steal";
    private string eToDoor = "Press 'E' to open/close door";
    private string noEnough = "Get at least $15000";
    private string eVictory = "Press 'E' to leave";
    private string eGetMissionItem = "You need to get the mission item";
    private string eToDestroy = "Press 'E' to destroy";
    private string eFindFenchCutter = "You need to find a Fench Cutter";
    private string eDeactive = "Press 'E' Deactive the laser";
    private string eDrawer = "Press Left Click to open/close drawer";

    public bool getMissionItem = false;
    public bool onTrigger1 = false;
    public bool keyCollected;
    public bool fuseBoxOpen=false;
    public bool doorOpen = false;
    public bool getWeapon = false;
    public bool isCut = false;

    public GameObject[] fence;

    public GameObject fuse;
    public GameObject laserManager;
    public GameObject fenchManager;
    public GameObject clearSpecial;
    public GameObject clearKey;
    public GameObject clearFuse;
    public GameObject clearWayToHouse;
   


    // Start is called before the first frame update
    void Start()
    {
        maincamera = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
        fuse = GameObject.Find("Fusedoor");
        laserManager = GameObject.Find("LaserManager");
        fenchManager = GameObject.Find("breakable fence ");
        fence = GameObject.FindGameObjectsWithTag("Fench");
        audio = GetComponent<AudioSource>();
        clearSpecial = GameObject.Find("specialBox");
        clearKey = GameObject.Find("FindKey");
        clearFuse = GameObject.Find("FuseBox");
        clearWayToHouse = GameObject.Find("wayToHouse");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.green);

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("Money") || hit.collider.CompareTag("key") || hit.collider.CompareTag("Gold") || hit.collider.CompareTag("GoldStack") || hit.collider.CompareTag("Wine")
                || hit.collider.CompareTag("Paint") || hit.collider.CompareTag("Coin") || hit.collider.CompareTag("Wallet") || hit.collider.CompareTag("Telephone") || hit.collider.CompareTag("Vase")
                || hit.collider.CompareTag("PC set") || hit.collider.CompareTag("Microwave") || hit.collider.CompareTag("Television")|| hit.collider.CompareTag("FenchCutter"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    audio.clip = pickup;
                    audio.Play();
                }


                HintText.message = eToCollect;
                HintText.textOn = true;

            }

            else if (hit.collider.CompareTag("LockedDoor"))
            {
                if (!keyCollected)
                {
                    HintText.message = "You need to find the key to open this door";
                    HintText.textOn = true;
                }
                else
                {
                    HintText.textOn = true;
                    HintText.message = eToDoor;
                }




            }

            else if (hit.collider.CompareTag("door")|| hit.collider.CompareTag("Fuse"))
            {

                if (!doorOpen)
                {
                HintText.message = eToDoor;
                HintText.textOn = true;
                }
                     
            }

            else if (hit.collider.CompareTag("Car"))
            {
                if(ScoringSystem.sManager.playerScore >= 15000 && getMissionItem)
                {
                    HintText.textOn = true;
                    HintText.message = eVictory;
                    SceneManager.LoadScene(4);

                }
                else if(ScoringSystem.sManager.playerScore < 15000)
                {
                    HintText.textOn = true;
                    HintText.message = noEnough;
                }

    

                else
                {
                    HintText.textOn = true;
                    HintText.message = eGetMissionItem;
                }

            }

            else if (hit.collider.CompareTag("MissionItem"))
            {   
                HintText.textOn = true;
                HintText.message = eToCollect;
            }

            else if (hit.collider.CompareTag("Fench"))
            {
                if (!isCut)
                {
                    if (getWeapon)
                    {
                        HintText.textOn = true;
                        HintText.message = eToDestroy;
                    }
                    else
                    {
                        HintText.textOn = true;
                        HintText.message = eFindFenchCutter;
                    }
                }
                
                    
                
            }

            else if (hit.collider.CompareTag("ButtonLaser"))
            {
                HintText.textOn = true;
                HintText.message = eDeactive;
            }

            else if (hit.collider.CompareTag("drawer"))
            {
                HintText.textOn = true;
                HintText.message = eDrawer;
            }

            else
            {
                HintText.textOn = false;
                HintText.message = "";
            }



            if (Input.GetKeyDown(KeyCode.E))
            {
               




                if (Physics.Raycast(ray, out hit, interactDistance))
                {


                    if (hit.collider.CompareTag("LockedDoor"))
                    {

                        gameDoor doorScript = hit.collider.transform.GetComponent<gameDoor>();
                        if (doorScript == null)
                        {
                            return;
                        }
                        if (Inventory.keys[doorScript.index] == true)
                        {
                            doorScript.ChangeDoorState();
                        }
                    }


                        if (hit.collider.CompareTag("door"))
                    {
                        
                        audio.clip = doorClip;
                        audio.Play();

                        gameDoor doorScript = hit.collider.transform.GetComponent<gameDoor>();
                        if (doorScript == null)
                        {
                            return;
                        }
                        if (Inventory.keys[doorScript.index] == true)
                        {
                            doorScript.ChangeDoorState();
                        }
                    }
                    else if (hit.collider.CompareTag("key"))
                    {
                        keyCollected = true;
                        Inventory.keys[hit.collider.GetComponent<Keys>().index] = true;
                        Destroy(hit.collider.gameObject);
                        clearKey.GetComponent<Text>().color = Color.green;
                    }

                    if (hit.collider.CompareTag("Money"))
                    {
                        
                        Debug.Log("Collide");
                        ScoringSystem.sManager.increaseMoney(100);
                        Destroy(hit.collider.gameObject);
                    }

                    if (hit.collider.CompareTag("Gold"))
                    {
                        
                        Debug.Log("Collide");
                        ScoringSystem.sManager.increaseMoney(300);
                        Destroy(hit.collider.gameObject);
                    }


                    if (hit.collider.CompareTag("GoldStack"))
                    {
                        
                        Debug.Log("Collide");
                        ScoringSystem.sManager.increaseMoney(4700);
                        Destroy(hit.collider.gameObject);
                    }

                    if (hit.collider.CompareTag("Wine"))
                    {
                        
                        Debug.Log("Collide");
                        ScoringSystem.sManager.increaseMoney(150);
                        Destroy(hit.collider.gameObject);
                    }

                    if (hit.collider.CompareTag("Paint"))
                    {
                      
                        Debug.Log("Collide");
                        ScoringSystem.sManager.increaseMoney(150);
                        Destroy(hit.collider.gameObject);
                    }

                    if (hit.collider.CompareTag("Coin"))
                    {
                        
                        Debug.Log("Collide");
                        ScoringSystem.sManager.increaseMoney(50);
                        Destroy(hit.collider.gameObject);
                    }

                    if (hit.collider.CompareTag("Wallet"))
                    {
                        
                        Debug.Log("Collide");
                        ScoringSystem.sManager.increaseMoney(100);
                        Destroy(hit.collider.gameObject);
                    }

                    if (hit.collider.CompareTag("Telephone"))
                    {
                        
                        Debug.Log("Collide");
                        ScoringSystem.sManager.increaseMoney(1000);
                        Destroy(hit.collider.gameObject);
                    }

                    if (hit.collider.CompareTag("Vase"))
                    {
                        
                        Debug.Log("Collide");
                        ScoringSystem.sManager.increaseMoney(100);
                        Destroy(hit.collider.gameObject);
                    }

                    if (hit.collider.CompareTag("PC set"))
                    {
                        
                        Debug.Log("Collide");
                        ScoringSystem.sManager.increaseMoney(500);
                        Destroy(hit.collider.gameObject);
                    }

                    if (hit.collider.CompareTag("Microwave"))
                    {
                       
                        Debug.Log("Collide");
                        ScoringSystem.sManager.increaseMoney(300);
                        Destroy(hit.collider.gameObject);
                    }

                    if (hit.collider.CompareTag("Television"))
                    {
                        
                        Debug.Log("Collide");
                        ScoringSystem.sManager.increaseMoney(1000);
                        Destroy(hit.collider.gameObject);
                    }

                    if (hit.collider.CompareTag("Fuse"))
                    {
                        if (!fuseBoxOpen)
                        {
                            fuseBoxOpen = true;
                            fuse.transform.Rotate(new Vector3(0, 0, -90));
                        }

                        else
                        {
                            fuseBoxOpen = false;
                            fuse.transform.Rotate(new Vector3(0, 0, 90));
                        }
                    }



                    if (hit.collider.CompareTag("ButtonLaser"))
                    {

                        laserManager.SetActive(false);
                        clearFuse.GetComponent<Text>().color = Color.green;

                    }

                    if (hit.collider.CompareTag("FenchCutter")) 
                    {
                        getWeapon = true;
                        Destroy(hit.collider.gameObject);

                    }
                    if (hit.collider.CompareTag("MissionItem"))
                    {
                        getMissionItem = true;
                        Destroy(hit.collider.gameObject);
                        clearSpecial.GetComponent<Text>().color = Color.green;
                    }


                    if (hit.collider.CompareTag("Fench"))
                    {
                        if (getWeapon)
                        {
                            
                            for(int i = 0; i < fence.Length; i++)
                            {
                                if(fence[i].name == hit.collider.gameObject.name)
                                {
                                    Destroy(hit.collider.gameObject);
                                    clearWayToHouse.GetComponent<Text>().color= Color.green;
                                    getWeapon = false;
                                    isCut = true;
                                }
                            }


                        }
                    }
                }
            }
        }
    }
}
