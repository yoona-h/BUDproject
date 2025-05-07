using UnityEngine;

/// <summary>
/// LeafProcessingStage를 상속, 덖기 기능에 특화됨.
/// </summary>
public class RoastingStage : LeafProcessingStage
{   
    [SerializeField] private ParticleSystem smokeEffect;
    private DragAreaChecker dragAreaChecker;
    private Vector2 leafPos;
    private Color baseColor;

    void Start()
    {
        leafPos = leafSprite.transform.position;
        baseColor = MakeTeaManager.Instance.leafColor;
        leafSprite.color = baseColor;

        dragAreaChecker = leafSprite.GetComponent<DragAreaChecker>();
        dragAreaChecker.OnEnterArea += StartProcessing;
        dragAreaChecker.OnExitArea += StopProcessing;

        if (smokeEffect != null && smokeEffect.isPlaying)
            smokeEffect.Stop();
    }

    public override void StartProcessing()
    {
        base.StartProcessing();

        if (smokeEffect != null && !smokeEffect.isPlaying)
            smokeEffect.Play();
    }

    public override void StopProcessing()
    {
        base.StopProcessing();

        Debug.Log($"덖기 종료, 시간: {processTime:F2}s");

        DisplayTeaInfo("이름", "추가된 효과", "설명");

        if (smokeEffect != null && smokeEffect.isPlaying)
            smokeEffect.Stop();

        dragAreaChecker.enabled = false;
        leafSprite.transform.position = leafPos;
        Color finalRoastingColor = leafSprite.color;
        MakeTeaManager.Instance.SetOxidizedLeafColor(finalRoastingColor);
    }
}
