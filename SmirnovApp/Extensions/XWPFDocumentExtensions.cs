using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmirnovApp.Extensions
{
    public static class XWPFDocumentExtensions
    {
        public static void ReplaceText(this XWPFDocument document, string from, string to)
        {
            document.Paragraphs.ToList().ForEach(paragraph => 
            {
                paragraph.Runs.ToList().ForEach(run => 
                {
                    run.SetText(run.Text.Replace(from, to));
                });
            });
        }
    }
}
