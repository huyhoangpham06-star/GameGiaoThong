
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TrafficMap : MonoBehaviour
{
    public Material roadMat;
    public Material grassMat;
    public Material whiteMat;
    public Material buildingMat;
    public Material treeMat;

    void Start()
    {
        CreateMaterials();
        CreateGround();
        CreateRoad();
        CreateSidewalk();
        CreateRoadLines();
        CreateCrosswalks();
        CreateBuildings();
        CreateTrees();
        CreatePlayer();
        CreateTrafficLights();
        CreateCamera();
        CreateLighting();
        CreateGameManager();
    }
    void CreateGameManager()
    {
        GameObject manager = new GameObject("GameManager");

        manager.AddComponent<GameManager>();
        manager.AddComponent<TrafficUI>();
    }
    void CreateMaterials()
    {
        roadMat = MakeMaterial(new Color(0.16f, 0.17f, 0.19f));
        grassMat = MakeMaterial(new Color(0.25f, 0.55f, 0.22f));
        whiteMat = MakeMaterial(Color.white);
        buildingMat = MakeMaterial(new Color(0.7f, 0.65f, 0.55f));
        treeMat = MakeMaterial(new Color(0.1f, 0.4f, 0.12f));
    }


    Material MakeMaterial(Color color)
    {
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");

        if (shader == null)
            shader = Shader.Find("Standard");

        if (shader == null)
        {
            Debug.LogError("Khong tim thay Shader!");
            return null;
        }

        Material mat = new Material(shader);
        mat.color = color;

        return mat;
    }


    void CreateObject(
        string name, Vector3 position,
        Vector3 scale, Material material
    )
    {
        GameObject obj = GameObject.CreatePrimitive(
            PrimitiveType.Cube
        );
        Destroy(obj.GetComponent<Collider>());

        obj.name = name;
        obj.transform.position = position;
        obj.transform.localScale = scale;

        if (material != null)
            obj.GetComponent<Renderer>().material = material;
    }

    void CreateGround()
    {
        CreateObject(
            "Mat co", new Vector3(0, -0.2f, 0),
            new Vector3(50, 0.2f, 50), grassMat
        );
    }

    void CreateRoad()
    {
        CreateObject(
            "Duong ngang", new Vector3(0, 0, 0),
            new Vector3(30, 0.1f, 8), roadMat
        );

        CreateObject(
            "Duong doc", new Vector3(0, 0.02f, 0),
            new Vector3(8, 0.1f, 30), roadMat
        );
    }

    void CreateSidewalk()
    {
        CreateObject("Via he 1",
            new Vector3(0, 0.02f, 4.7f),
            new Vector3(30, 0.3f, 1.4f), whiteMat);

        CreateObject("Via he 2",
            new Vector3(0, 0.02f, -4.7f),
            new Vector3(30, 0.3f, 1.4f), whiteMat);

        CreateObject("Via he 3",
            new Vector3(4.7f, 0.02f, 0),
            new Vector3(1.4f, 0.3f, 30), whiteMat);

        CreateObject("Via he 4",
            new Vector3(-4.7f, 0.02f, 0),
            new Vector3(1.4f, 0.3f, 30), whiteMat);
    }

    void CreateRoadLines()
    {
        for (int i = -13; i <= 13; i += 3)
        {
            if (Mathf.Abs(i) < 5) continue;

            CreateObject("Vach ngang",
                new Vector3(i, 0.09f, 0),
                new Vector3(1.5f, 0.02f, 0.12f),
                whiteMat);

            CreateObject("Vach doc",
                new Vector3(0, 0.09f, i),
                new Vector3(0.12f, 0.02f, 1.5f),
                whiteMat);
        }
    }

    void CreateCrosswalks()
    {
        for (int i = -3; i <= 3; i++)
        {
            CreateObject("Vach qua duong",
                new Vector3(i, 0.1f, 5.8f),
                new Vector3(0.55f, 0.03f, 1.2f),
                whiteMat);

            CreateObject("Vach qua duong",
                new Vector3(i, 0.1f, -5.8f),
                new Vector3(0.55f, 0.03f, 1.2f),
                whiteMat);

            CreateObject("Vach qua duong",
                new Vector3(5.8f, 0.1f, i),
                new Vector3(1.2f, 0.03f, 0.55f),
                whiteMat);

            CreateObject("Vach qua duong",
                new Vector3(-5.8f, 0.1f, i),
                new Vector3(1.2f, 0.03f, 0.55f),
                whiteMat);
        }
    }

    void CreateBuildings()
    {
        Vector3[] positions =
        {
            new Vector3(12, 2, 12),
            new Vector3(-12, 2, 12),
            new Vector3(12, 2, -12),
            new Vector3(-12, 2, -12)
        };

        foreach (Vector3 p in positions)
        {
            CreateObject(
                "Nha cao tang", p,
                new Vector3(5, 4, 5), buildingMat
            );
        }
    }

    void CreateTrees()
    {
        for (int i = -12; i <= 12; i += 4)
        {
            if (Mathf.Abs(i) < 6) continue;

            CreateObject("Than cay",
                new Vector3(i, 0.8f, 7),
                new Vector3(0.4f, 1.6f, 0.4f),
                buildingMat);

            CreateObject("Tan cay",
                new Vector3(i, 2f, 7),
                new Vector3(1.8f, 1.8f, 1.8f),
                treeMat);
        }
    }

    void CreatePlayer()
    {
        GameObject car = new GameObject("PlayerCar");
        car.transform.position = new Vector3(0, 0.5f, -10);

        GameObject body = GameObject.CreatePrimitive(
            PrimitiveType.Cube
        );

        body.name = "Than xe";
        body.transform.SetParent(car.transform);
        body.transform.localPosition = Vector3.zero;
        body.transform.localScale = new Vector3(1.5f, 0.7f, 2.5f);
        body.GetComponent<Renderer>().material =
            MakeMaterial(Color.red);
        Destroy(body.GetComponent<Collider>());

        GameObject roof = GameObject.CreatePrimitive(
            PrimitiveType.Cube
        );
        

        roof.name = "Noc xe";
        roof.transform.SetParent(car.transform);
        roof.transform.localPosition = new Vector3(0, 0.55f, -0.1f);
        roof.transform.localScale = new Vector3(1, 0.5f, 1.2f);
        roof.GetComponent<Renderer>().material =
            MakeMaterial(Color.black);
        Destroy(roof.GetComponent<Collider>());

        for (int x = -1; x <= 1; x += 2)
        {
            for (int z = -1; z <= 1; z += 2)
            {
                GameObject wheel = GameObject.CreatePrimitive(
                    PrimitiveType.Cylinder
                );

                wheel.transform.SetParent(car.transform);
                wheel.transform.localPosition =
                    new Vector3(x * 0.75f, -0.25f, z * 0.7f);

                wheel.transform.localScale =
                    new Vector3(0.3f, 0.15f, 0.3f);

                wheel.transform.localRotation =
                    Quaternion.Euler(0, 0, 90);

                wheel.GetComponent<Renderer>().material =
                    MakeMaterial(Color.black);
                Destroy(wheel.GetComponent<Collider>());
            }
            
        }
        


        car.AddComponent<BoxCollider>();
        car.AddComponent<Rigidbody>();
        car.GetComponent<Rigidbody>().isKinematic = true;

        car.tag = "Player";

        car.AddComponent<CarController>();
        car.AddComponent<TrafficCollision>();
    }

    void CreateTrafficLights()
    {
        Vector3[] positions =
        {
            new Vector3(5, 0, -4),
            new Vector3(-5, 0, 4),
            new Vector3(4, 0, 5),
            new Vector3(-4, 0, -5)
        };

        foreach (Vector3 p in positions)
        {
            GameObject light = new GameObject("Den giao thong");
            light.transform.position = p;

            light.AddComponent<TrafficLight>();
        }
    }

    void CreateCamera()
    {
        Camera cam = Camera.main;

        if (cam == null)
        {
            GameObject cameraObj = new GameObject("Main Camera");
            cam = cameraObj.AddComponent<Camera>();
            cameraObj.tag = "MainCamera";
        }

        cam.transform.position = new Vector3(0, 24, -16);
        cam.transform.rotation = Quaternion.Euler(55, 0, 0);
        cam.orthographic = true;
        cam.orthographicSize = 17;
    }

    void CreateLighting()
    {
        GameObject light = new GameObject("Sun");
        Light sun = light.AddComponent<Light>();

        sun.type = LightType.Directional;
        sun.intensity = 1.2f;

        light.transform.rotation = Quaternion.Euler(50, -30, 0);
    }
}
