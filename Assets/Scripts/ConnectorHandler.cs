using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

//using UnityEngine.UI;


public class ConnectorHandler : MonoBehaviour
{
    [SerializeField] private string url = "https://opentdb.com/api_category.php";
    [SerializeField] public TMP_Dropdown tmpDropdown;
    [SerializeField] public Dropdown dropdown;
    
    private TriviaCat triviaCat;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
       //dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
       //dropdown = dropdwn.GetComponent<Dropdown>();

       StartCoroutine(FetchDataFromAPI()); 
    }

    IEnumerator FetchDataFromAPI()
    {
       
       Debug.Log("Connection Started"); 
       UnityWebRequest request = UnityWebRequest.Get("https://opentdb.com/api_category.php");
       yield return request.SendWebRequest();
       while (!request.isDone)
       {
           Debug.Log("Waiting for request to finish");
       }

       if (request.result != UnityWebRequest.Result.Success)
       {
           Debug.LogError("Error while fetching data from API: " + request.error);
       }
       else
       {
            triviaCat = JsonUtility.FromJson<TriviaCat>(request.downloadHandler.text);
            UpdateDropdownOptions();
       }

    }

    void UpdateDropdownOptions()
    {
        //dropdown.ClearOptions();
        tmpDropdown.ClearOptions();
        foreach (TriviaCategories category in triviaCat.trivia_categories)
        {
            Dropdown.OptionData option = new Dropdown.OptionData(category.name);
            //dropdown.
            tmpDropdown.options.Add(new TMP_Dropdown.OptionData(category.name));
            //dropdown.options.Add(option);
        }
    }

}



[System.Serializable]
public class TriviaCategories
{
    public int id;
    public string name;
}

[System.Serializable]
public class TriviaCat
{
    public TriviaCategories[] trivia_categories;
}
