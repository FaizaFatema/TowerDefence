using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public int totalWaves = 3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(showWaveStart(1));
    }
    IEnumerator showWaveStart(int wave)
    {
        waveText.text= $"Wave {1} Starting!";
        waveText.gameObject.SetActive(true);
        waveText.alpha = 0f;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 2;
            waveText.alpha = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        t = 0;
        while(t < 1)
        {
            t += Time.deltaTime * 2;
            waveText.alpha = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }
        waveText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
