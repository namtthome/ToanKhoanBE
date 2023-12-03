using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using vn.com.pnsuite.common.models;

namespace vn.com.pnsuite.common
{
    public class ExcelParamer
    {
        public uint Row { get; set; }
        public string ColumnName { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; } = "Text";
        public bool FontBold { get; set; } = false;
    }
    public class ExcelHelper
    {
        public static List<ExcelValue> ReadExcel(string filepath)
        {
            List<ExcelValue> ret = new List<ExcelValue>();
            try
            {
                var openSettings = new OpenSettings
                {
                    RelationshipErrorHandlerFactory = package =>
                    {
                        return new UriRelationshipErrorHandler();
                    }
                };
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filepath, true, openSettings))
                {

                    //create the object for workbook part  
                    WorkbookPart wbPart = doc.WorkbookPart;

                    //statement to get the count of the worksheet  
                    int worksheetcount = doc.WorkbookPart.Workbook.Sheets.Count();

                    //statement to get the sheet object  
                    Sheet mysheet = (Sheet)doc.WorkbookPart.Workbook.Sheets.ChildElements.GetItem(0);

                    //statement to get the worksheet object by using the sheet id  
                    Worksheet Worksheet = ((WorksheetPart)wbPart.GetPartById(mysheet.Id)).Worksheet;

                    //Note: worksheet has 8 children and the first child[1] = sheetviewdimension,....child[4]=sheetdata  
                    var stringTable = wbPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();

                    //statement to get the sheetdata which contains the rows and cell in table  
                    SheetData rows = (SheetData)Worksheet.Elements<SheetData>().First();
                    //worksheetPart.Worksheet.Elements<SheetData>().First();
                    foreach (Row row in rows)
                    {
                        foreach (Cell cell in row.Elements())
                        {
                            ExcelValue cellValue = new ExcelValue()
                            {
                                Row = GetRowIndex(cell.CellReference),
                                Column = GetColumnIndex(cell.CellReference),
                                CellAddress = cell.CellReference,
                                CellValue = GetCellValue(stringTable, cell)
                            };
                            if (cellValue.CellValue.Trim().Length > 0)
                                ret.Add(cellValue);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

            return ret;
        }
        public static List<ExcelValue> ReadExcel(Stream fileStream)
        {
            List<ExcelValue> ret = new List<ExcelValue>();
            try
            {
                var openSettings = new OpenSettings
                {
                    RelationshipErrorHandlerFactory = package =>
                    {
                        return new UriRelationshipErrorHandler();
                    }
                };
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fileStream, true, openSettings))
                {

                    //create the object for workbook part  
                    WorkbookPart wbPart = doc.WorkbookPart;

                    //statement to get the count of the worksheet  
                    int worksheetcount = doc.WorkbookPart.Workbook.Sheets.Count();

                    //statement to get the sheet object  
                    Sheet mysheet = (Sheet)doc.WorkbookPart.Workbook.Sheets.ChildElements.GetItem(0);

                    //statement to get the worksheet object by using the sheet id  
                    Worksheet Worksheet = ((WorksheetPart)wbPart.GetPartById(mysheet.Id)).Worksheet;

                    //Note: worksheet has 8 children and the first child[1] = sheetviewdimension,....child[4]=sheetdata  
                    var stringTable = wbPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();

                    //statement to get the sheetdata which contains the rows and cell in table  
                    SheetData rows = (SheetData)Worksheet.Elements<SheetData>().First();
                    //worksheetPart.Worksheet.Elements<SheetData>().First();
                    foreach (Row row in rows)
                    {
                        foreach (Cell cell in row.Elements())
                        {
                            ExcelValue cellValue = new ExcelValue()
                            {
                                Row = GetRowIndex(cell.CellReference),
                                Column = GetColumnIndex(cell.CellReference),
                                CellAddress = cell.CellReference,
                                CellValue = GetCellValue(stringTable, cell)
                            };
                            if (cellValue.CellValue != null && cellValue.CellValue.Trim().Length > 0)
                                ret.Add(cellValue);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

            return ret;
        }
        private static string GetColumnIndex(string cellReference)
        {
            if (string.IsNullOrEmpty(cellReference))
            {
                return null;
            }

            string columnReference = Regex.Replace(cellReference.ToUpper(), @"[\d]", string.Empty);

            return columnReference;
        }
        private static int GetRowIndex(string cellReference)
        {
            // Create a regular expression to match the row index portion the cell name.
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(cellReference);

            return int.Parse(match.Value);
        }
        private static string GetCellValue(SharedStringTablePart stringTable, Cell cell)
        {
            string ret = cell.CellValue != null ? cell.CellValue.Text : null;

            if (cell.CellReference.ToString().StartsWith("BC"))
            {
                Console.WriteLine(cell.ToString());
            }

            if (cell.DataType != null)
            {
                switch (cell.DataType.Value)
                {
                    case CellValues.SharedString:
                        if (stringTable != null)
                        {
                            ret = stringTable.SharedStringTable
                                        .ElementAt(int.Parse(ret)).InnerText;
                        }
                        break;
                    case CellValues.Boolean:
                        switch (ret)
                        {
                            case "0":
                                ret = "FALSE";
                                break;
                            default:
                                ret = "TRUE";
                                break;
                        }
                        break;
                    case CellValues.Date:
                        ret = cell.InnerText;
                        break;
                }
            }

            return ret;
        }
        public static MemoryStream ExportData<T>(List<T> dataModel, string filePath, int startRow = 10, List<ExcelParamer> parammeter = null, int dateStyleIndex = 14, int stringStyleIndex = 5)
        {
            try
            {
                var table = ToDataTable(dataModel);

                byte[] templateBytes = File.ReadAllBytes(filePath);
                MemoryStream des = new MemoryStream();
                des.Write(templateBytes, 0, templateBytes.Length);
                using (var myWorkbook = SpreadsheetDocument.Open(des, true))
                {
                    var workbookPart = myWorkbook.WorkbookPart;
                    var Sheets = myWorkbook.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    var relationshipId = Sheets.First().Id.Value;
                    var worksheetPart = (WorksheetPart)myWorkbook.WorkbookPart.GetPartById(relationshipId);
                    var stringTable = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                    var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                    //var table = ds.Tables[0];
                    UInt32Value row = Convert.ToUInt32(startRow);
                    var no = 1;
                    if (parammeter != null)
                    {
                        foreach (var item in parammeter)
                        {
                            Cell cell = GetCell(worksheetPart.Worksheet, item.ColumnName, item.Row);
                            cell.CellValue = new CellValue(item.Value);
                            cell.DataType = CellValues.String;
                        }
                    }
                    foreach (DataRow dsrow in table.Rows)
                    {
                        var newRow = new Row { RowIndex = row++ };
                        var cell = new Cell();


                        //cell.StyleIndex = currentFormat.StyleIndex;
                        //cell.CellValue = new CellValue((no++).ToString());
                        //newRow.AppendChild(cell);
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            Cell cellType = (Cell)sheetData.Elements<Row>().ElementAt(0).ChildElements[i];
                            Cell cellFormat = cellType;

                            String type = cellType.CellValue.InnerText;
                            if (stringTable != null)
                            {
                                type =
                                    stringTable.SharedStringTable
                                    .ElementAt(int.Parse(type)).InnerText;
                            }

                            var value = dsrow[i].ToString();
                            cell = new Cell();
                            float tempf = 0;

                            value = dsrow[i].ToString();
                            if (type == "Number")
                            {
                                float.TryParse(value, out tempf);
                                value = value.Replace(",", ".");
                                if (cellFormat != null)
                                {
                                    cell.StyleIndex = cellFormat.StyleIndex;
                                }

                                if (!(table.Columns[i].DataType.Name == "String" && value == "0"))
                                {
                                    cell.DataType = CellValues.Number;
                                    cell.CellValue = new CellValue(tempf);
                                }
                            }
                            else if (type == "Date")
                            {
                                if (cellFormat != null)
                                {
                                    cell.StyleIndex = cellFormat.StyleIndex;
                                }

                                //cell.DataType = CellValues.Date;


                                DateTime dateTime = new DateTime();
                                try
                                {
                                    dateTime = DateTime.Parse(value);
                                    cell.CellValue = new CellValue(dateTime.ToOADate());
                                }
                                catch (Exception dateEx)
                                {
                                    //cell.DataType = CellValues.String;
                                    cell.CellValue = new CellValue(value);
                                }
                            }
                            else
                            {
                                cell.DataType = CellValues.String;
                                cell.StyleIndex = cellFormat.StyleIndex;
                                //String cellValue = value.Length > 0 && value.Substring(0, 1) == "0" ? "'" + value : value;
                                cell.CellValue = new CellValue(value);
                            }


                            newRow.AppendChild(cell);
                        }
                        sheetData.AppendChild(newRow);
                    }

                    IEnumerable<Row> rows = sheetData.Elements<Row>();
                    Row firstRow = rows.FirstOrDefault();
                    firstRow.Remove();

                    firstRow.Hidden = new BooleanValue(true); ;

                    workbookPart.Workbook.Save();
                }

                return des;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
        }
        public static MemoryStream ExportData(string json, string filePath, int startRow = 10, List<ExcelParamer> parammeter = null, int dateStyleIndex = 14, int stringStyleIndex = 5)
        {
            try
            {
                byte[] templateBytes = File.ReadAllBytes(filePath);
                MemoryStream des = new MemoryStream();
                des.Write(templateBytes, 0, templateBytes.Length);
                using (var myWorkbook = SpreadsheetDocument.Open(des, true))
                {
                    var workbookPart = myWorkbook.WorkbookPart;
                    var Sheets = myWorkbook.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    var relationshipId = Sheets.First().Id.Value;
                    var worksheetPart = (WorksheetPart)myWorkbook.WorkbookPart.GetPartById(relationshipId);
                    var stringTable = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                    var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                    //var table = ds.Tables[0];

                    //var styles = myWorkbook.WorkbookPart.WorkbookStylesPart;
                    //var stylesheet = styles.Stylesheet;

                    //var availableFonts = stylesheet.Fonts.Cast<Font>();

                    //var font = new Font();
                    //font.Append(new FontSize() { Val = 13 });
                    //font.Append(new FontName() { Val = "Times New Romans" });
                    //stylesheet.Fonts.Append(font);

                    //stylesheet.CellFormats.Append(new CellFormat()
                    //{
                    //    FontId = stylesheet.Fonts.Count - 1
                    //});

                    

                    UInt32Value row = Convert.ToUInt32(startRow);
                    var no = 1;

                    //CellFormat cellFormat = new CellFormat();
                    //Font font = new Font();
                    //font.FontName = new FontName() { Val = "Time New Romans" };
                    //font.FontSize = new FontSize() { Val = 13 };
                    //font.Bold = new Bold();
                    Cell cellTemp = GetCell(worksheetPart.Worksheet, "B", 2);
                    int cols = sheetData.Elements<Row>().ElementAt(0).Count();

                    JArray array = JArray.Parse(json);
                    foreach (JObject obj in array.Children<JObject>())
                    {
                        var newRow = new Row { RowIndex = row++ };
                        var cell = new Cell();

                        int col = 0;
                        for (col = 0; col < cols; col++)
                        {
                            Cell cellType = (Cell)sheetData.Elements<Row>().ElementAt(0).ChildElements[col];
                            //Cell cellFormat = cellType;
                            //CellFormat cellFormat = cellType.StyleIndex != null ? workbookPart.WorkbookStylesPart.Stylesheet.CellFormats.ChildElements[(int)cellType.StyleIndex.Value] : new CellFormat();
                            
                            Cell cellColl = (Cell)sheetData.Elements<Row>().ElementAt(1).ChildElements[col];

                            String type = cellType.CellValue.InnerText;
                            if (stringTable != null)
                            {
                                type =
                                    stringTable.SharedStringTable
                                    .ElementAt(int.Parse(type)).InnerText;
                            }
                            String colname = cellColl.CellValue.InnerText;
                            if (stringTable != null)
                            {
                                colname =
                                    stringTable.SharedStringTable
                                    .ElementAt(int.Parse(colname)).InnerText;
                            }
                            
                            var value = obj.Property(colname) == null ? "" : obj.Property(colname).Value.ToString();
                            cell = new Cell();
                            float tempf = 0;

                            if (type == "Number")
                            {
                                float.TryParse(value, out tempf);
                                value = value.Replace(",", ".");
                                if (cellType != null)
                                {
                                    cell.StyleIndex = cellType.StyleIndex;
                                    
                                }

                                cell.DataType = CellValues.Number;
                                cell.CellValue = new CellValue(tempf);
                            }
                            else if (type == "Date")
                            {
                                if (cellType != null)
                                {
                                    cell.StyleIndex = cellType.StyleIndex;
                                }

                                DateTime dateTime = new DateTime();
                                try
                                {
                                    dateTime = DateTime.Parse(value);
                                    cell.CellValue = new CellValue(dateTime.ToOADate());
                                }
                                catch (Exception dateEx)
                                {
                                    //cell.CellValue = new CellValue(value);
                                }
                            }
                            else
                            {
                                cell.DataType = CellValues.String;
                                cell.StyleIndex = cellType.StyleIndex;
                                //String cellValue = value.Length > 0 && value.Substring(0, 1) == "0" ? "'" + value : value;
                                cell.CellValue = new CellValue(value);
                            }


                            newRow.AppendChild(cell);
                        }

                        sheetData.AppendChild(newRow);
                    }

                    if (parammeter != null)
                    {
                        foreach (var item in parammeter)
                        {
                            if (item.Row > 0)
                            {
                                Cell cell = GetCell(worksheetPart.Worksheet, item.ColumnName, item.Row);
                                cell.CellValue = new CellValue(item.Value);
                                if (item.ValueType == "Number")
                                {
                                    cell.DataType = CellValues.Number;
                                }
                                else if (item.ValueType == "Date")
                                {
                                    cell.DataType = CellValues.Date;
                                }
                                else
                                {
                                    cell.DataType = CellValues.String;
                                }
                                //cell.DataType = CellValues.String;
                            } else if (item.Row == 0)
                            {
                                Cell cell = GetCell(worksheetPart.Worksheet, item.ColumnName, row + 2);
                                if (cell == null) {
                                    Row newRow = new Row();
                                    newRow.RowIndex = (row + 2);
                                    cell = new Cell();
                                    cell.CellReference = item.ColumnName + (row + 2);
                                    cell.CellValue = new CellValue(item.Value);
                                    cell.DataType = CellValues.String;
                                    cell.StyleIndex = cellTemp.StyleIndex;
                                    newRow.AppendChild(cell);
                                    sheetData.AppendChild(newRow);
                                } else
                                {
                                    cell.CellValue = new CellValue(item.Value);
                                    cell.DataType = CellValues.String;
                                    cell.StyleIndex = 0;
                                }
                                
                                
                                
                            }
                        }
                    }
                    //if (parammeter != null)
                    //{

                    //    for (int i = 0; i < 2; i++)
                    //    {
                    //        var newRow = new Row { RowIndex = row++ };
                    //        var cell = new Cell();
                    //        if (i == 1)
                    //        {
                    //            for (int j = 0; j < cols; j++)
                    //            {
                    //                String value = null;
                    //                foreach (var item in parammeter)
                    //                {
                    //                    if (item.Row == 0)
                    //                    {
                    //                        if (((Char)(j + 65)) == item.ColumnName[0])
                    //                        {
                    //                            value = item.Value;
                    //                        }
                    //                    }
                    //                }

                    //                cell = new Cell();
                    //                cell.DataType = CellValues.String;

                    //                if (value == null)
                    //                {
                    //                    cell.CellValue = null;
                    //                }
                    //                else
                    //                {
                    //                    cell.CellValue = new CellValue(value);
                    //                }


                    //                newRow.AppendChild(cell);
                    //            }
                    //        }

                    //        sheetData.AppendChild(newRow);
                    //    }
                    //}

                    IEnumerable<Row> rows = sheetData.Elements<Row>();
                    Row firstRow = rows.FirstOrDefault();
                    firstRow.Remove();
                    Row secondRow = rows.ElementAt(0);
                    secondRow.Remove();

                    firstRow.Hidden = new BooleanValue(true); 
                    secondRow.Hidden = new BooleanValue(false);

                    workbookPart.Workbook.Save();
                }

                return des;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
        }
        private static Stylesheet createStylesheet()
        {
           
            
            var cellFormats = new CellFormats() { Count = 4U };
            var fonts = new Fonts() { Count = 1U, KnownFonts = true };
            Font font = new Font();
            font.Append(new FontSize() { Val = 13D });
            font.Append(new FontName() { Val = "Times New Romans" });
            font.Append(new FontFamilyNumbering() { Val = 2 });
            font.Append(new Bold());
            // add the created font to the fonts collection
            // since this is the first added font it will gain the id 1U
            fonts.Append(font);

            cellFormats.AppendChild(new CellFormat() { FontId = 0U, FillId = 0U });
            
            var stylesheet = new Stylesheet(fonts, cellFormats);

            return stylesheet;
        }
        private static Cell GetCell(Worksheet ws, string columnName, uint rowIndex)
        {
            Row row = GetRow(ws, rowIndex);
            if (row == null) return null;
            return row.Elements<Cell>().FirstOrDefault(c => string.Compare(c.CellReference.Value, columnName + rowIndex, true) == 0);
        }
        private static Row GetRow(Worksheet ws, uint rowIndex)
        {
            return ws.GetFirstChild<SheetData>().Elements<Row>().FirstOrDefault(r => r.RowIndex == rowIndex);
        }
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType.Name.Contains("Nullable") ? typeof(String) : prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
    public class UriRelationshipErrorHandler : RelationshipErrorHandler
    {
        public override string Rewrite(Uri partUri, string id, string uri)
        {
            return "https://broken-link";
        }
    }
}
