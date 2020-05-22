using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class Generate : MonoBehaviour
{
    //int i=0;
    private GameObject[] L1;
    private GameObject[] L2;
    private GameObject[] L3;
    private GameObject[] L4;
    private GameObject[] L5;
    private GameObject[] L6;
    public int nL3;
    public int nL4;
    public int nL5;
    public int nL6;
    private Transform local;
    Quaternion currentRotation;
    public bool generateNow=false;

    private void Awake()
    {

        local = gameObject.transform;

    }


    // Start is called before the first frame update
    void Start()
    {
       // Physics.gravity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(generateNow)
        {
            //generateLayers();
            spawnLogic();
            generateNow = false;
            StopAllCoroutines();
        }
        
    }

    void generateLayers()
    {        
        generateL1();
        generateL2();
        generateL3();
        generateL4();
        generateL5();
        generateL6();
    }

    void spawnLogic() //this function decides where to Instantiate the objects in the scene
    {
        int i;
        int j;
        GameObject furniture;
        GameObject ground;
        ground = generateL1();

        Instantiate(ground, local); //floor
        Instantiate(ground, new Vector3(local.position.x, 4.5f+local.position.y, local.position.z),new Quaternion(0,0,180,0)); //floor
        Instantiate(generateL2(), new Vector3(9 + local.position.x, 2.5f + local.position.y, 0 + local.position.z),
            generateL2().transform.rotation); //walls R
        Instantiate(generateL2(), new Vector3(-9 + local.position.x, 2.5f + local.position.y, 0 + local.position.z),
            generateL2().transform.rotation); //walls L


        for (i = 0; i <= nL4; i++) //ground
        {
            currentRotation.eulerAngles = new Vector3(0, Random.Range(-15, 15), 0);
            furniture = Instantiate(generateL4(),
                new Vector3(Random.Range(-8.7f, 8.7f) + local.position.x, 0f, Random.Range(-13.7f, 13.7f) + local.position.z),
                currentRotation);
            float z = Random.Range(0.1f, 1f);
            furniture.transform.localScale = new Vector3(z, z, z);

            //Debug.Log(furniture.transform.position); 
            for (j = 0; j <= nL6; j++)
            {
                currentRotation.eulerAngles = new Vector3(0, Random.Range(-30, 30), 0);
                GameObject go = Instantiate(generateL6(),
                    new Vector3(furniture.transform.position.x + Random.Range(-0.1f, 0.1f), 2f,
                        furniture.transform.position.z + Random.Range(-0.1f, 0.1f)), currentRotation);
                float s = Random.Range(0.1f, 0.2f);
                go.transform.localScale = new Vector3(s, s, s);
            }
        }

        for (i = 0; i <= nL5; i++)
        {
            currentRotation.eulerAngles = new Vector3(0, Random.Range(-30, 30), 0);
            furniture = generateL5();
            // Instantiate(furniture, new Vector3(Random.Range(-8, 8)+local.position.x, 0f, Random.Range(-13, 13) + local.position.z), currentRotation);


        }

        for (i = 0; i <= nL3; i++) //cieling
        {
            currentRotation.eulerAngles = new Vector3(0, Random.Range(-15, 15), 180);
            GameObject go = Instantiate(generateL3(),
                new Vector3(Random.Range(-8.7f, 8.7f) + local.position.x, 4.5f, Random.Range(-13, 13) + local.position.z),
                currentRotation);
            float s = Random.Range(0.1f, 1f);
            go.transform.localScale = new Vector3(s, s, s);
        }
    }

    #region Generate the first layer of objects
    GameObject generateL1() //this function will generate a game object containing a prefab from the designated folder
    {
        int i = 0;
        string path = Application.dataPath; //finding were there the build is located
        path += ("/Resources/Tunnel_A/L1");//adding the correct path for the layer
        var num = Directory.EnumerateFiles(path, "*.prefab", SearchOption.AllDirectories); //counting how many objects are in the folder to initialize the container, this uses a .NET library
        foreach (string currentFile in num)
        {
            i++;
        }
        L1 = new GameObject[i];
        i = 0;
        foreach (string currentFile in num)
        {

            string res = currentFile.Replace(Application.dataPath + "/Resources/", "").Replace(".prefab", "").Replace("\\", "/").Trim();// creating a path readable by the resources.load function
                                                                                                                                        // Debug.Log(res);
            
            L1[i] = Resources.Load<GameObject>(res);// each object in the folder L1 is beign recorded into the L1 gameobject array
            i++;
        }
        int j;
        j = Random.Range(0, i);
        return (L1[j]);// the function return a random prefab from the folder
        // Instantiate(L1[0], transform.parent);
    }
    #endregion

    #region Generate the second layer of objects
    GameObject generateL2()
    {
        int i = 0;
        string path = Application.dataPath;
        path += ("/Resources/Tunnel_A/L2");
        var num = Directory.EnumerateFiles(path, "*.prefab", SearchOption.AllDirectories);
        foreach (string currentFile in num)
        {
            i++;
        }
        L2 = new GameObject[i];
        i = 0;
        foreach (string currentFile in num)
        {

            string res = currentFile.Replace(Application.dataPath + "/Resources/", "").Replace(".prefab", "").Replace("\\", "/").Trim();           

            L2[i] = Resources.Load<GameObject>(res);
            i++;
        }
         int j;
         j = Random.Range(0,i);
        // Debug.Log(i);
        // Debug.Log(j);
        // Instantiate(L2[j], transform.parent);
        return (L2[j]);
    }
    #endregion

    #region Generate the third layer of objects
    GameObject generateL3()//cieling
    {
        int i = 0;
        string path = Application.dataPath;
        path += ("/Resources/LowPoly_Rocks/Prefabs/Stalagmite");
        var num = Directory.EnumerateFiles(path, "*.prefab", SearchOption.AllDirectories);
        foreach (string currentFile in num)
        {
            i++;
        }
        L3 = new GameObject[i];
        i = 0;
        foreach (string currentFile in num)
        {

            string res = currentFile.Replace(Application.dataPath + "/Resources/", "").Replace(".prefab", "").Replace("\\", "/").Trim();
           // Debug.Log(res);

            L3[i] = Resources.Load<GameObject>(res);
            if (L3[i].GetComponent<MeshCollider>())
                L3[i].GetComponent<MeshCollider>().convex = true;
            L3[i].GetComponent<Rigidbody>().isKinematic = true;
            i++;
        }
        int j;
        j = Random.Range(0, i);
        return (L3[j]);
        // Instantiate(L3[0], transform.parent);
    }
    #endregion

    #region Generate the fourth layer of objects
    GameObject generateL4()
    {
        int i = 0;
        string path = Application.dataPath;
        path += ("/Resources/LowPoly_Rocks/Prefabs/Round");
        var num = Directory.EnumerateFiles(path, "*.prefab", SearchOption.AllDirectories);
        foreach (string currentFile in num)
        {
            i++;
        }
        L4 = new GameObject[i];
        i = 0;
        foreach (string currentFile in num)
        {

            string res = currentFile.Replace(Application.dataPath + "/Resources/", "").Replace(".prefab", "").Replace("\\", "/").Trim();
           // Debug.Log(res);

            L4[i] = Resources.Load<GameObject>(res);
            if(L4[i].GetComponent<Rigidbody>()==null)
                L4[i].AddComponent<Rigidbody>();
            if (L4[i].GetComponent<MeshCollider>())
                L4[i].GetComponent<MeshCollider>().convex = true;
            L4[i].GetComponent<Rigidbody>().isKinematic = true;
            i++;
        }
        int j;
        j = Random.Range(0, i);
        return (L4[j]);
        // Instantiate(L4[0], transform.parent);
    }
    #endregion

    #region Generate the fifth layer of objects
    GameObject generateL5()
    {
        int i = 0;
        string path = Application.dataPath;
        path += ("/Resources/LowPoly_Rocks/Prefabs/Round");
        var num = Directory.EnumerateFiles(path, "*.prefab", SearchOption.AllDirectories);
        foreach (string currentFile in num)
        {
            i++;
        }
        L5 = new GameObject[i];
        i = 0;
        foreach (string currentFile in num)
        {

            string res = currentFile.Replace(Application.dataPath + "/Resources/", "").Replace(".prefab", "").Replace("\\", "/").Trim();
            
            //Debug.Log(res);

            L5[i] = Resources.Load<GameObject>(res);
            if (L5[i].GetComponent<Rigidbody>() == null)
            
                L5[i].AddComponent<Rigidbody>();
            //L5[i].GetComponent<Rigidbody>().useGravity = false;
            //L5[i].GetComponent<Rigidbody>().isKinematic = true;
            if (L5[i].GetComponent<MeshCollider>())
                L5[i].GetComponent<MeshCollider>().convex=true;
            i++;
        }
        int j;
        j = Random.Range(0, i);
        return (L5[j]);
        //  Instantiate(L5[0], transform.parent);
    }
    #endregion

    #region Generate the sixth layer of objects
    GameObject generateL6()
    {
        int i = 0;
        string path = Application.dataPath;
        path += ("/Resources/LowPoly_Rocks/Prefabs/Round");
        var num = Directory.EnumerateFiles(path, "*.prefab", SearchOption.AllDirectories);
        foreach (string currentFile in num)
        {
            i++;
        }
        L6 = new GameObject[i];
        i = 0;
        foreach (string currentFile in num)
        {

            string res = currentFile.Replace(Application.dataPath + "/Resources/", "").Replace(".prefab", "").Replace("\\", "/").Trim();

            //Debug.Log(res);

            L6[i] = Resources.Load<GameObject>(res);
            if (L6[i].GetComponent<Rigidbody>() == null)
                L6[i].AddComponent<Rigidbody>();
            if (L6[i].GetComponent<MeshCollider>())
                L6[i].GetComponent<MeshCollider>().convex = true;
            L4[i].GetComponent<Rigidbody>().isKinematic = false;

            i++;
        }
        int j;
        j = Random.Range(0, i);
        return (L6[j]);
        //  Instantiate(L5[0], transform.parent);
    }
    #endregion

    IEnumerator WaitTillnext()
    {
        
        yield return new WaitForSeconds(10);
        
    }
}
