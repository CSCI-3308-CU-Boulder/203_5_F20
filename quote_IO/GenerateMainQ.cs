using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System;

public class GenerateMainQ : MonoBehaviour {

	public Text TextBox;

	public void readTextFile(){
		System.Random rnd = new System.Random();
		//System.Collections.Generic.IEnumerable<string> lines = File.ReadLines("resources/QuoteListNotepad.txt");
		string[] lines = File.ReadAllLines("c:\\Users\\User\\Documents\\TT Games\\Assets\\Resources\\QuoteListNotepad.txt").ToArray();

		// int randIndex = rnd.Next(lines);
		int arrayLength = lines.Length;
		int randIndex = rnd.Next(arrayLength);

		// TextBox.Text = textArray [rowsToReadFrom [randIndex]];
		TextBox.text = lines[randIndex];
		for (var i = 0; i < arrayLength; i++) {
			Console.WriteLine (lines[i]);
		}
		/*
		foreach (var line in lines)
		{
			Console.WriteLine("\t" + line);
		}
		*/
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;

public class GenerateMainQ : MonoBehaviour {

	public Text TextBox;

	public static void Main()
	{
		string path = @"c:\\Users\\User\\Documents\\TT Games\\Assets\\Resources\\QuoteListNotepad.txt";

		// This text is added only once to the file.
		if (!File.Exists(path))
		{
			// Create a file to write to.
			Console.Write("Oof, no file");
		}

		// This text is always added, making the file longer over time
		// if it is not deleted.
		//string appendText = "This is extra text" + Environment.NewLine;
		//File.AppendAllText(path, appendText);

		// Open the file to read from.
		string[] readText = File.ReadAllLines(path);
		foreach (string s in readText)
		{
			Console.WriteLine(s);
		}
		TextBox.text = readText[0];
	}
}
*/