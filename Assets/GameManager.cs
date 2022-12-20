using System.Collections;
using UnityEngine;
using System.Net;
using Assets;
using Assets.Pendu;
using Pendu;
using Newtonsoft.Json.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform link;
    [SerializeField]
    private Transform cage;
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private BoxCollider cageFloorCollider;
    [SerializeField]
    private Material lavaShader;
    [SerializeField]
    private Material waterShader;
    [SerializeField]
    private MeshRenderer planeRenderer;
    [SerializeField]
    private AudioSource winSound;
    [SerializeField]
    private AudioSource looseSound;
    [SerializeField]
    private Transform characterParent;
    [SerializeField]
    private DifficultyManager difficultyManager;
    [SerializeField]
    private UIManager UIManager;

    private int faultCount = 0;
    private bool gameStarted;
    private Word wordword;
    private WrittenCharacters writtenCharacters;
    private string actualStr;
    private bool canType = true;

    private void Awake()
    {
        ChangeWaterToLava();
    }

    private void ChangeLavaToWater()
    {
        planeRenderer.material = waterShader;
    }

    private void ChangeWaterToLava()
    {
        planeRenderer.material = lavaShader;
    }

    void Update()
    {
        if (gameStarted && wordword != null && wordword.Name == UIManager.GetWordText)
        {
            gameStarted = false;
            print("FIN");
            Win();
            ChangeLavaToWater();
        }
        else if (canType && wordword != null && Input.inputString != null && Input.inputString.Length > 0)
        {
            char c = Input.inputString[0];
            if (char.IsLetter(c))
            {
                Debug.Log($"character {c}");
                if (wordword.Name.Contains(c))
                {
                    actualStr = wordword.UpdateDisplayedWord(actualStr, c);
                    UIManager.SetWordText(actualStr);
                }
                else
                {
                    if (writtenCharacters.AddChar(c))
                    {
                        UIManager.SetLettersText(writtenCharacters.GetCharacters);
                        Fault();
                    }
                }
            }
        }
    }

    public async void GetWord(int _minLength, int _maxLength)
    {
        using (var wb = new WebClient())
        {
            var @object = await wb.DownloadStringTaskAsync($"https://api.dicolink.com/v1/mots/motauhasard?avecdef=true&minlong={_minLength}&maxlong={_maxLength}&verbeconjugue=false&api_key=rON7EygLDvbBstbmAZISOfwEv14WtVw4");
            @object = @object.TrimStart('[').TrimEnd(']');
            print(@object);
            var json = JObject.Parse(@object);
            var word = (string)json.GetValue("mot");
            wordword = new Word(word);
            writtenCharacters = new WrittenCharacters();
            wordword.Name = Utils.RemoveSpecialChars(word);
            actualStr = wordword.HiddenWord;
            UIManager.SetWordText(actualStr);
            UIManager.SetFaultsText($"{6 - faultCount} essais restants");
            print(wordword.Name);
            gameStarted = true;
        }
    }

    private void Fault()
    {
        print("Fault");
        link.position = new Vector3(link.position.x, link.position.y - 5, link.position.z);
        faultCount++;
        UIManager.SetFaultsText($"{6 - faultCount} essais restants");
        if (faultCount == 6)
        {
            Loose();
            faultCount = 0;
        }
    }

    private void SetCageInitialPosition()
    {
        Destroy(character.GetComponent<Rigidbody>());
        character.transform.parent = characterParent;
        character.transform.localPosition = new Vector3(-0.01596069f, -0.8220062f, 0.04272461f);
        character.transform.localEulerAngles = new Vector3(0, 180, 0);
        canType = true;
    }

    private void Loose()
    {
        canType = false;
        looseSound.Play();
        UIManager.SetLettersText($"Le mot était \"{wordword.Name}\" !");
        UIManager.SetDifficultyText(string.Empty);
        UIManager.SetWordText(string.Empty);
        UIManager.SetFaultsText(string.Empty);
        var rigidbody = character.AddComponent<Rigidbody>();
        rigidbody.mass = 1;
        rigidbody.AddForce(new Vector3(0, -10, 0), ForceMode.Impulse);
        character.transform.parent = null;
        character.transform.eulerAngles = new Vector3(0, 180, 0);
        cageFloorCollider.enabled = false;
        UIManager.EnableRetryChoice();
    }

    private void Win()
    {
        winSound.Play();
        UIManager.SetLettersText($"Bien joué, tu as trouvé le mot \"{wordword.Name}\" en faisait {faultCount} fautes !");
        UIManager.SetDifficultyText(string.Empty);
        UIManager.SetFaultsText(string.Empty);
        UIManager.SetWordText(string.Empty);
        UIManager.EnableRetryChoice();
    }

    public void Retry()
    {
        UIManager.SetLettersText(string.Empty);
        UIManager.DisableRetryChoice();
        UIManager.EnableDifficultyChoices();
        StartCoroutine(DoFade(link, link.localPosition.y, 230.71f, 5));
    }

    IEnumerator DoFade(Transform transform, float _start, float _end, float _duration)
    {
        float counter = 0f;

        while (counter < _duration)
        {
            counter += Time.deltaTime;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(_start, _end, counter / _duration), transform.localPosition.z);

            yield return null;
        }
        SetCageInitialPosition();
    }
}
