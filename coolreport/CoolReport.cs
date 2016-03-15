using Microsoft.Reporting.WebForms;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Security;
using System.Security.Permissions;

namespace coolreport
{
    public class CoolReport
    {
        private ReportViewer reportViewer;
        private byte[] bytePDF { get; set; }
        Warning[] warnings;
        private string[] streamids;
        private string mimeType;
        private string encoding;
        private string extension;
        private string dataSourceName;
        private static string extensionName;

        public CoolReport(ReportType reportType, string reportNameWithExtension, string dataSourceName, ProcessingMode processingMode = ProcessingMode.Local)
        {
            reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = processingMode;
            this.dataSourceName = dataSourceName;
            extensionName = reportType == ReportType.Excel ? ".Excel" : reportType == ReportType.Word ? ".Word" : ".pdf";        
            reportViewer.LocalReport.ReportPath = reportNameWithExtension;
            reportViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(new PermissionSet(PermissionState.Unrestricted));
        }
        public void OpenReportWithoutParameters(IEnumerable _query, string reportName = "Report")
        {
            ReportDataSource list = ReportQuery(_query);
            reportViewer.LocalReport.DataSources.Add(list);
            CreateReport(reportName);

        }
        public void OpenReportWithoutQuery(List<ReportParameter> listParameter, string reportName = "Report")
        {
            reportViewer.LocalReport.SetParameters(listParameter);
            CreateReport(reportName);
        }
        public void OpenReportWithQueryAndParameters(IEnumerable _query, List<ReportParameter> listParameter, string reportName = "Report")
        {
            ReportDataSource list = ReportQuery(_query);
            reportViewer.LocalReport.DataSources.Add(list);
            reportViewer.LocalReport.SetParameters(listParameter);
            CreateReport(reportName);
        }
        private void CreateReport(string reportName)
        {
            byte[] bytePDF = reportViewer.LocalReport.Render(extensionName = extensionName.Contains(".") ? extensionName.Remove(0, 1) : extensionName, null, out mimeType, out encoding, out extension, out streamids, out warnings);
            FileStream fileStream = null;
            string fileName = null;
            fileName = GeneratePath(reportName);
            fileStream = new FileStream(fileName, FileMode.Create);
            fileStream.Write(bytePDF, 0, bytePDF.Length);
            fileStream.Close();
            Process.Start(fileName);
        }
        private ReportDataSource ReportQuery(IEnumerable _query)
        {
            return new ReportDataSource(name: dataSourceName, dataSourceValue: _query);
        }
        private static string GeneratePath(string reportName)
        {
            var extensionType = extensionName.Contains(ReportType.Excel.ToString()) ? ReportExtension.xls.ToString() : extensionName;
            return $"{Path.GetTempPath()} {reportName} - {DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}.{extensionType}";
        }
      
    }

}


public enum ReportType
{
    Word, PDF, Excel
}

internal enum ReportExtension
{
    xls
}

