using Microsoft.Reporting.WebForms;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

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
        private Type type;
        public CoolReport(ReportType reporType, Type type)
        {
            this.reporType = reporType;
            this.type = type;
        }

    }
    public enum ReportType
    {
        Word,PDF,Excel
    }
    public enum Type
    {
        WithParameters,WithoutParameters
    }
}
