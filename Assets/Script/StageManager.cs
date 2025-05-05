using UnityEngine;

public enum TeaStep { Oxidation, Roasting, Brewing }

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public TeaStep currentStep = TeaStep.Oxidation;

    [SerializeField] private GameObject oxidationStage;
    [SerializeField] private GameObject roastingStage;
    [SerializeField] private GameObject brewingStage;
    [SerializeField] private Canvas canvas;

    public Color leafColor {get; private set;}

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        GoToStep(currentStep);
    }

    public void GoToStep(TeaStep step)
    {
        currentStep = step;

        oxidationStage.SetActive(false);
        roastingStage.SetActive(false);
        brewingStage.SetActive(false);

        switch (step)
        {
            case TeaStep.Oxidation: oxidationStage.SetActive(true); break;
            case TeaStep.Roasting: roastingStage.SetActive(true); break;
            case TeaStep.Brewing: brewingStage.SetActive(true); canvas.enabled = false; break;
        }

    }

    public void GoToNextStep()
    {
        int next = (int)currentStep + 1;
        if (next < System.Enum.GetValues(typeof(TeaStep)).Length)
            GoToStep((TeaStep)next);
    }

    public void SetOxidizedLeafColor(Color leafColor)
    {
        this.leafColor = leafColor;
    }
}

