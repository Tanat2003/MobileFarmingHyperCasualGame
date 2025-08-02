using System.Collections;
using UnityEngine;

public class TransactionEffectManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private ParticleSystem coinPS;
    [SerializeField] private RectTransform coinRectTransform;
    [SerializeField] private float moveSpeed = 5f;
    public static TransactionEffectManager instance;
    [Header("Settings")]
    private int coinAmount;
    private Camera mainCamera;

    private void Awake()
    {
        instance = this;
    }
    private void Start() => mainCamera = Camera.main;

    [NaughtyAttributes.Button]
    private void TestCOinsPS()
    {
        PlayCoinParticles(100);
    }
    public void PlayCoinParticles(int amount)
    {   if (coinPS.isPlaying)
            return;
        // ตั้งค่าจำนวนอนุภาค
        var emission = coinPS.emission;
        var burst = emission.GetBurst(0);
        burst.count = amount;
        emission.SetBurst(0, burst);
        ParticleSystem.MainModule main = coinPS.main;
        main.gravityModifier = 2;


        // เริ่มเล่น Particle System
        coinPS.Play();
        coinAmount = amount;

        StartCoroutine(MoveParticlesToUICoroutine());
    }

    private IEnumerator MoveParticlesToUICoroutine()
    {
        yield return new WaitForSeconds(1f); //รอให้particleตกตามแรงโน้มถ่วง

        ParticleSystem.MainModule main = coinPS.main;
        main.gravityModifier = 0;

        // หาตำแหน่ง UI Icon ใน World Space
        Vector3[] worldCorners = new Vector3[4];
        coinRectTransform.GetWorldCorners(worldCorners);
        Vector3 targetWorldPosition = (worldCorners[0] + worldCorners[2]) / 2f;

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[coinAmount];
        coinPS.GetParticles(particles);

        while (coinPS.isPlaying)
        {
            int numParticlesAlive = coinPS.GetParticles(particles);
            
            for (int i = 0; i < numParticlesAlive; i++)
            {
                
                if (particles[i].remainingLifetime <= 0)
                    continue;

                
                particles[i].position = Vector3.MoveTowards(
                    particles[i].position,
                    targetWorldPosition,
                    moveSpeed * Time.deltaTime
                );
                if(Vector3.Distance(particles[i].position, targetWorldPosition)<0.8f)
                {
                    particles[i].position += Vector3.up * 100000;
                    CashManager.Instance.AddCoins(1);
                    AudioManager.instance.PlaySFX(0);


                }
            }

            coinPS.SetParticles(particles, numParticlesAlive);
            yield return null;
        }
    }
}