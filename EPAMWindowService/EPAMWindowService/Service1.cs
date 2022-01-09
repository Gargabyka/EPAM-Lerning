using System;
using System.ServiceProcess;
using System.IO;
using System.Threading;

namespace EPAMWindowService
{
    public partial class Service1 : ServiceBase
    {
        private readonly Timer timer;
        private readonly string pathImages;
        private readonly string pathPdf;

        public Service1()
        {
            InitializeComponent();
            timer = new Timer(WorkService);
            pathImages = Path.GetDirectoryName($"{AppDomain.CurrentDomain.BaseDirectory}\\Images");
            pathPdf = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}\\Pdf\\", $"Image.pdf");
        }

        private void WorkService(object target)
        {
            var filters = new String[] { "jpg", "jpeg", "png" };
            string[] allFoundFiles = Directory.GetFiles(pathImages, String.Format("*.{0}", filters), SearchOption.AllDirectories);

            using (var doc = new iTextSharp.text.Document())
            {
                iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(pathPdf, FileMode.OpenOrCreate));
                doc.Open();
                foreach (var file in allFoundFiles)
                {
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(file);
                    doc.Add(image);
                }
            }
        }

        protected override void OnStart(string[] args)
        {
            timer.Change(0, 60 * 1000);
        }

        protected override void OnStop()
        {
            timer.Change(Timeout.Infinite, 0);
        }
    }
}
