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

namespace Sitecore.Feature.BulkItemEditor.API
{
    public class ItemUpdater
    {
        public void UploadExcelFile(string path)
        {
            var fullFileName = string.Format("{0}ExcelUploads\\{1}", HostingEnvironment.ApplicationPhysicalPath, "SitecoreItemDataUpload" + DateTime.UtcNow);

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(fullFileName);
            fileInfo.Directory.Create(); // If the directory already exists, this method does nothing.

        }


        public void DownloadExcel(string parentItemId, string language)
        {
            HttpContext.Current.Response.Write()
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
    }
}