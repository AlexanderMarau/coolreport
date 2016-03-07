using Microsoft.Reporting.WebForms;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;

namespace coolreport
{
    public class CoolReport
    {
        private ReportViewer reportViewer;
        private List<ReportParameter> listParameter;
        private byte[] bytePDF { get; set; }
        Warning[] warnings;
        private string[] streamids;
        private string mimeType;
        private string encoding;
        private string extension;
        private ReportType reporType;
        private string reportNameWithExtension;
        private string dataSourceName;
        public CoolReport(ReportType reporType, string reportNameWithExtension, string dataSourceName)
        {
            this.reporType = reporType;
            this.reportNameWithExtension = reportNameWithExtension;
            this.dataSourceName = dataSourceName;
        }
    }

}


public enum ReportType
{
    Word, PDF, Excel
}
   
}
