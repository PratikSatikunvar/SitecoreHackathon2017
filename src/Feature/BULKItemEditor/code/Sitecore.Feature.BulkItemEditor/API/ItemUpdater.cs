using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Data;
using Sitecore.Globalization;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Web.Hosting;
using Sitecore.Data.Fields;

namespace Sitecore.Feature.BulkItemEditor.API
{
    public class ItemUpdater
    {
        public void UploadExcelFile(string path)
        {
            var fullFileName = string.Format("{0}ExcelUploads\\{1}", HostingEnvironment.ApplicationPhysicalPath, "SitecoreItemDataUpload" + DateTime.UtcNow);
        }

        public void DownloadExcel(string parentItemId, string language)
        {
            Sitecore.Data.Database master = Sitecore.Data.Database.GetDatabase("master");

            Language languageItem;
            Language.TryParse(language, out languageItem);
            var parentItem = master.GetItem(parentItemId, languageItem);
            if (parentItem != null)
            {
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Customers.xls"));
                HttpContext.Current.Response.ContentType = "application/ms-excel";

                var children = parentItem.Children;
                string fieldLabel = string.Empty, fieldValue = string.Empty;
                foreach (Item item in children)
                {
                    HttpContext.Current.Response.Write(item.ID.ToString() + "\n");
                    fieldLabel = fieldValue = string.Empty;
                    item.Fields.ReadAll();
                    foreach (Field field in item.Fields.Where(d => !d.Shared && !d.Name.StartsWith("__") && d.Name.Trim() != ""))
                    {
                        fieldLabel += field.Name + "\t";
                        fieldValue += item[field.Name] + "\t";
                    }
                    HttpContext.Current.Response.Write(fieldLabel + "\n");
                    HttpContext.Current.Response.Write(fieldValue + "\n");
                }

                HttpContext.Current.Response.End();
            }
        }

        public void SaveExcelData(string path)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;


            for (int i = 1; i < xlRange.Row; i++)
            {
                for (int j = 1; j < xlRange.Column; j++)
                {

                }
            }

        }

        public void createDirectory(string FilePath)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(FilePath);
            fileInfo.Directory.Create(); // If the directory already exists, this method does nothing.
        }
    }
}