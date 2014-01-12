using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace SkyMovie.Model.Export
{
    class ExportToExcel
    {
        public ExportToExcel()
        {
            
        }
        /// <summary>
        /// Export All DB's in XLSX (One WorkSheet per DB).
        /// </summary>
        /// <param name="_filename">xlsx filename (as ExportMovieDB.xlsx)</param>
        public void ExportDbToFile(string _filename, ObservableCollection<Movie> _movieDB, ObservableCollection<Movie> _wishedDB)
        {
            var workbook = new XLWorkbook();
            var possesedMovieWorksheet = workbook.Worksheets.Add("Liste des films");
            var wantedMovieWorksheet = workbook.Worksheets.Add("Liste des souhaits");
            var gapBetweenFirstRowAndFilmTable = 3;

            // Part 1 : Owned film
            possesedMovieWorksheet.Cell("D2").Value = "Liste des films possedés";

            for (var i = 0; i <_movieDB.Count;i++ )
            {
                string cellToWrite = (i + gapBetweenFirstRowAndFilmTable).ToString();
                possesedMovieWorksheet.Cell("B" + cellToWrite).Value = _movieDB[i].Id;

                possesedMovieWorksheet.Cell("C" + cellToWrite).Value = _movieDB[i].Nom;
                possesedMovieWorksheet.Cell("D" + cellToWrite).Value = _movieDB[i].Genre;
                possesedMovieWorksheet.Cell("E" + cellToWrite).Value = _movieDB[i].Duree;
                possesedMovieWorksheet.Cell("F" + cellToWrite).Value = _movieDB[i].Overview;
                possesedMovieWorksheet.Cell("G" + cellToWrite).Value = _movieDB[i].Seen;
             }
            //PART 2 : Wanted Film

            wantedMovieWorksheet.Cell("D2").Value = "Liste des films souhaités";

            for (var i = 0; i < _wishedDB.Count; i++)
            {
                string cellToWrite = (i + gapBetweenFirstRowAndFilmTable).ToString();
                wantedMovieWorksheet.Cell("B" + cellToWrite).Value = _movieDB[i].Id;
                wantedMovieWorksheet.Cell("C" + cellToWrite).Value = _movieDB[i].Nom;
                wantedMovieWorksheet.Cell("D" + cellToWrite).Value = _movieDB[i].Genre;
                wantedMovieWorksheet.Cell("E" + cellToWrite).Value = _movieDB[i].Duree;
                wantedMovieWorksheet.Cell("F" + cellToWrite).Value = _movieDB[i].Overview;
                wantedMovieWorksheet.Cell("G" + cellToWrite).Value = _movieDB[i].Seen;
            }

            try
            {
                workbook.SaveAs(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/SkyMovie/" + _filename);
                MessageBox.Show("Le fichier à correctement été exporter à l'emplacement /Mes Documents/SkyMovie/" + _filename);
            }
            catch (Exception)
            {
                MessageBox.Show("Impossible d'enregistrer le fichier, vérifier que vous ne l'avez pas ouvert");
                
            }
        }
        
    }
}
