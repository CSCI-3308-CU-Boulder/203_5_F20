using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using System.IO;
// using Excel = Microsoft.Office.Interop.Excel;       //Microsoft Excel 14 object in references-> COM tab

/*
namespace Sandbox
{
	public class Read_From_Excel
	{
		public static void getExcelFile()
		{

			//Create COM Objects. Create a COM object for everything that is referenced
			Excel.Application xlApp = new Excel.Application();
			Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\User\Desktop\Schoolwork\TT Games - 3308\Quote_list.xlsx");
			Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
			Excel.Range xlRange = xlWorksheet.UsedRange;

			int rowCount = xlRange.Rows.Count;
			int colCount = xlRange.Columns.Count;

			//iterate over the rows and columns and print to the console as it appears in the file
			//excel is not zero based!!
			for (int i = 1; i <= rowCount; i++)
			{
				for (int j = 1; j <= colCount; j++)
				{
					//new line
					if (j == 1)
						Console.Write("\r\n");

					//write the value to the console
					if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
						Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
				}
			}

			//cleanup
			GC.Collect();
			GC.WaitForPendingFinalizers();

			//rule of thumb for releasing com objects:
			//  never use two dots, all COM objects must be referenced and released individually
			//  ex: [somthing].[something].[something] is bad

			//release com objects to fully kill excel process from running in the background
			Marshal.ReleaseComObject(xlRange);
			Marshal.ReleaseComObject(xlWorksheet);

			//close and release
			xlWorkbook.Close();
			Marshal.ReleaseComObject(xlWorkbook);

			//quit and release
			xlApp.Quit();
			Marshal.ReleaseComObject(xlApp);
		}
	}
}   
*/


public class ChoiceScript : MonoBehaviour {

	public GameObject TextBox;
	public GameObject option01;
	public GameObject option02;
	public GameObject option03;
	public GameObject option04;
	public int choiceMade;

	public void ChoiceOption1 () {
		TextBox.GetComponent<Text>().text = "You clearly chose the first option here.";
		choiceMade = 1;
	}

	public void ChoiceOption2 () {
		TextBox.GetComponent<Text>().text = "You clearly chose the second option here.";
		choiceMade = 2;
	}

	public void ChoiceOption3 () {
		TextBox.GetComponent<Text>().text = "You clearly chose the third option here.";
		choiceMade = 3;
	}

	public void ChoiceOption4 () {
		TextBox.GetComponent<Text>().text = "You clearly chose the fourth option here.";
		choiceMade = 4;
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
