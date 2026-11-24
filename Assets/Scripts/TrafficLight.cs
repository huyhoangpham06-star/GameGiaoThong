
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public static bool IsRed { get; private set; }

    private Renderer redLight;
    private Renderer yellowLight;
    private Renderer greenLight;

    private float timer;
    private float greenTime = 10f;
    private float yellowTime = 3f;
    private float redTime = 10f;

    private Material redMat;
    private Material yellowMat;
    private Material greenMat;
    private Material offMat;

    void Start()
    {
        CreateMaterials();
        CreateLightModel();
    }

    void Update()
    {
        timer += Time.deltaTime;

        float cycle = greenTime + yellowTime + redTime;
        float time = timer % cycle;

        if (time < greenTime)
        {
            SetLight(false, false, true);
            IsRed = false;
        }
        else if (time < greenTime + yellowTime)
        {
            SetLight(false, true, false);
            IsRed = false;
        }
        else
        {
            SetLight(true, false, false);
            IsRed = true;
        }
    }

    void CreateMaterials()
    {
        redMat = CreateMat(Color.red);
        yellowMat = CreateMat(Color.yellow);
        greenMat = CreateMat(Color.green);
        offMat = CreateMat(Color.black);
    }

    Material CreateMat(Color color)
    {
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");

        if (shader == null)
            shader = Shader.Find("Standard");

        Material mat = new Material(shader);
        mat.color = color;
        return mat;
    }

    void CreateLightModel()
    {
        GameObject pole = GameObject.CreatePrimitive(
            PrimitiveType.Cylinder
        );

        pole.name = "Cot den";
        pole.transform.SetParent(transform);
        pole.transform.localPosition = new Vector3(0, 1.3f, 0);
        pole.transform.localScale = new Vector3(0.15f, 1.3f, 0.15f);

        Destroy(pole.GetComponent<Collider>());

        GameObject box = GameObject.CreatePrimitive(
            PrimitiveType.Cube
        );

        box.name = "Hop den";
        box.transform.SetParent(transform);
        box.transform.localPosition = new Vector3(0, 2.8f, 0);
        box.transform.localScale = new Vector3(0.65f, 1.5f, 0.4f);
        box.GetComponent<Renderer>().material = offMat;

        Destroy(box.GetComponent<Collider>());

        redLight = CreateBulb("Den do", 3.25f, redMat);
        yellowLight = CreateBulb("Den vang", 2.8f, yellowMat);
        greenLight = CreateBulb("Den xanh", 2.35f, greenMat);
    }

    Renderer CreateBulb(
        string name, float height, Material material)
    {
        GameObject bulb = GameObject.CreatePrimitive(
            PrimitiveType.Sphere
        );

        bulb.name = name;
        bulb.transform.SetParent(transform);
        bulb.transform.localPosition =
            new Vector3(0, height, -0.22f);

        bulb.transform.localScale = Vector3.one * 0.32f;

        bulb.GetComponent<Renderer>().material = material;

        Destroy(bulb.GetComponent<Collider>());

        return bulb.GetComponent<Renderer>();
    }

    void SetLight(bool red, bool yellow, bool green)
    {
        if (redLight == null) return;

        redLight.material = red ? redMat : offMat;
        yellowLight.material = yellow ? yellowMat : offMat;
        greenLight.material = green ? greenMat : offMat;
    }
}
