using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

[RequireComponent(typeof(TextMesh))]
public class GenerateQuestion : MonoBehaviour {

	public GameObject TextBox;
	public GameObject option01;
	public GameObject option02;
	public GameObject option03;
	public GameObject option04;
	public int choiceMade;

	public static List<string> textArray;
	private bool display;
	private TextMesh textComp;

	[SerializeField]
	public int[] rowsToReadFrom;
	public string FileName;

	private TextAsset textAsset;

	void start(){
		textAsset = Resources.Load (FileName) as TextAsset;
		textComp = GetComponent<TextMesh> ();
		readTextFile ();
	}


	public void readTextFile(){
		System.Random rnd = new System.Random();
		char[] delimiterChars = { '|', '\n' };
		textArray = textAsset.text.Split (delimiterChars).ToList ();
		for (int i = 0; i < rowsToReadFrom.Length; i++) {
			if (rowsToReadFrom [0] < 0 || rowsToReadFrom.Length == 0) {
				textComp.text = textAsset.text;
			}
			else{
				textComp.text += textArray [rowsToReadFrom [i]] + "\n";
			}
		}
		int randIndex = rnd.Next(rowsToReadFrom.Length);
		// MessageBox.show (randIndex);
		// TextBox.Text = textArray [rowsToReadFrom [randIndex]];
		TextBox.GetComponent<Text> ().text = textArray [rowsToReadFrom [randIndex]];
		//option01.GetComponent<Text>().text = textArray [rowsToReadFrom [randIndex]];
		//option02.GetComponent<Text>().text = textArray [rowsToReadFrom [randIndex]];
		//option03.GetComponent<Text>().text = textArray [rowsToReadFrom [randIndex]];
		//option04.GetComponent<Text>().text = textArray [rowsToReadFrom [randIndex]];
	}

	// Update is called once per frame
	/*
	 void Update () {
		if (choiceMade >= 1) {
			option01.SetActive (false);
			option02.SetActive (false);
			option03.SetActive (false);
			option04.SetActive (false);
		}
	}
	*/
}


/*
	string winDir = System.Environment.GetEnvironmentVariable ("windir");
	public GameObject TextBox;
	public GameObject option01;
	public GameObject option02;
	public GameObject option03;
	public GameObject option04;
	public int choiceMade;

	StreamReader reader = new StreamReader (winDir + "\\system.ini");
	try {
		do {
			addListItem(reader.ReadLine());
		}
		while(reader.Peek() != -1);
	}
	catch {
		addListItem("File is empty");
	}
	finally {
		reader.Close();
	}
*/

	/*
class GFG { 
      
    class WriteToFile { 
          
        public void Data() 
        { 
            // This will create a file named sample.txt 
            // at the specified location  
            StreamWriter sw = new StreamWriter("H://geeksforgeeks.txt"); 
              
            // To write on the console screen 
            Console.WriteLine("Enter the Text that you want to write on File");  
              
            // To read the input from the user 
            string str = Console.ReadLine();  
              
            // To write a line in buffer 
            sw.WriteLine(str);  
              
            // To write in output stream 
            sw.Flush();  
              
            // To close the stream 
            sw.Close();  
        } 
    } 
      
    // Main Method 
    static void Main(string[] args) 
    { 
        WriteToFile wr = new WriteToFile(); 
        wr.Data(); 
        Console.ReadKey(); 
    } 
} 
	 */

/*
	private void addListItem(string value) {
		this.listBox1.Items.Add(value);
	}

	public void ChoiceOption1 () {
		option01.GetComponent<Text>().text = "You clearly chose the first option here.";
		choiceMade = 1;
	}

	public void ChoiceOption2 () {
		option02.GetComponent<Text>().text = "You clearly chose the second option here.";
		choiceMade = 2;
	}

	public void ChoiceOption3 () {
		option03.GetComponent<Text>().text = "You clearly chose the third option here.";
		choiceMade = 3;
	}

	public void ChoiceOption4 () {
		option04.GetComponent<Text>().text = "You clearly chose the fourth option here.";
		choiceMade = 4;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (choiceMade >= 1) {
			option01.SetActive (false);
			option02.SetActive (false);
			option03.SetActive (false);
			option04.SetActive (false);
		}
	}
}
*/