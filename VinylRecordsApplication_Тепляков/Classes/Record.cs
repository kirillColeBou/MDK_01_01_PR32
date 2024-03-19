using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylRecordsApplication_Тепляков.Classes
{
    public class Record
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int Format { get; set; }
        public int Size { get; set; }
        public int IdManufacturer { get; set; }
        public float Price { get; set; }
        public int IdState { get; set; }
        public string Description { get; set; }
        public static IEnumerable<Record> AllRecords()
        {
            List<Record> records = new List<Record>();
            DataTable recordQuery = Classes.DBConnection.Connection("SELECT * FROM [dbo].[Record]");
            foreach (DataRow row in recordQuery.Rows)
            {
                records.Add(new Record()
                {
                    Id = Convert.ToInt32(row[0]),
                    Name = row[1].ToString(),
                    Year = Convert.ToInt32(row[2]),
                    Format = Convert.ToInt32(row[3]),
                    Size = Convert.ToInt32(row[4]),
                    IdManufacturer = Convert.ToInt32(row[5]),
                    Price = float.Parse(row[6].ToString()),
                    IdState = Convert.ToInt32(row[7]),
                    Description = row[8].ToString()
                });
            }
            return records;
        }
        public void Save(bool Update = false)
        {
            string CorrectPrice = this.Price.ToString().Replace(",", ".");
            if (Update == false)
            {
                Classes.DBConnection.Connection(
                    "INSERT INTO " +
                    "[dbo].[Record](" +
                    "[Name], " +
                    "[Year]" +
                    "[Format]" +
                    "[Size]" +
                    "[IdManufacturer], " +
                    "[Price]" +
                    "[IdState]" +
                    "[Description]) " +
                    "VALUES(" +
                    $"N'{this.Name}', " +
                    $"{this.Year}', " +
                    $"{this.Format}, " +
                    $"{this.Size}," +
                    $"{this.IdManufacturer}," +
                    $"{CorrectPrice}, " +
                    $"{this.IdState}, " +
                    $"N'{this.Description}');");
                this.Id = Record.AllRecords().Where(
                    x => x.Name == this.Name &&
                    x.Year == this.Year &&
                    x.Format == this.Format &&
                    x.Size == this.Size &&
                    x.IdManufacturer == this.IdManufacturer &&
                    x.IdState == this.IdState &&
                    x.Description == this.Description).First().Id;
            }
            else
                Classes.DBConnection.Connection(
                    "UPDATE [dbo].[Record] " +
                    $"SET [Name] = N'{this.Name}'," +
                    $"[Year] = {this.Year}, " +
                    $"[Format] = {this.Format}, " +
                    $"[Size] = {this.Size}, " +
                    $"[IdManufacturer] = {this.IdManufacturer}, " +
                    $"[Price] = {CorrectPrice}, " +
                    $"[IdState] = {this.IdState}, " +
                    $"[Description] = N'{this.Description}' " +
                    $"WHERE [Id] = {this.Id}");
        }
        public void Delete() => Classes.DBConnection.Connection($"DELETE FROM [dbo].[Record] WHERE [Id] = {this.Id};");

        public static void Export(string filePath, IEnumerable<Record> records)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Записи");
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "Название";
                worksheet.Cells[1, 3].Value = "Год";
                worksheet.Cells[1, 4].Value = "Формат";
                worksheet.Cells[1, 5].Value = "Размер";
                worksheet.Cells[1, 6].Value = "Id производителя";
                worksheet.Cells[1, 7].Value = "Цена";
                worksheet.Cells[1, 8].Value = "Id состояния";
                worksheet.Cells[1, 9].Value = "Описание";
                int row = 2;
                foreach (var record in records)
                {
                    worksheet.Cells[row, 1].Value = record.Id;
                    worksheet.Cells[row, 2].Value = record.Name;
                    worksheet.Cells[row, 3].Value = record.Year;
                    worksheet.Cells[row, 4].Value = record.Format;
                    worksheet.Cells[row, 5].Value = record.Size;
                    worksheet.Cells[row, 6].Value = record.IdManufacturer;
                    worksheet.Cells[row, 7].Value = record.Price;
                    worksheet.Cells[row, 8].Value = record.IdState;
                    worksheet.Cells[row, 9].Value = record.Description;
                    row++;
                }
                FileInfo fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);
            }
        }
    }
}
