using Emgu.CV;
using Emgu.CV.Structure;

namespace FaceAnalysisExtractor
{
    class VideoProcessor
    {
        private string videoPath;
        private string outputFolder;
        private string changesFolder;

        /// <summary>
        /// Inicializa una nueva instancia de VideoProcessor con las rutas especificadas.
        /// </summary>
        public VideoProcessor(string videoPath, string outputFolder, string changesFolder)
        {
            // Configuración inicial.
            this.videoPath = videoPath;
            this.outputFolder = outputFolder;
            this.changesFolder = changesFolder;
            Directory.CreateDirectory(outputFolder);
            Directory.CreateDirectory(changesFolder);
        }

        /// <summary>
        /// Procesa el video, extrayendo frames y detectando características faciales.
        /// </summary>
        public void ProcessVideo()
        {
            var capture = new VideoCapture(videoPath);
            var featureManager = new FeatureManager();
            var facialFeatureDetector = new FacialFeatureDetector();
            int frameCount = 0;

            while (true)
            {
                var frame = capture.QueryFrame();
                if (frame == null) break;

                var image = frame.ToImage<Bgr, byte>();
                facialFeatureDetector.DetectAndProcessFeatures(image, featureManager);

                string frameFileName = Path.Combine(outputFolder, $"frame_{frameCount++}.jpg");
                image.Save(frameFileName);
            }

            featureManager.SaveBestFeatures(changesFolder);
            Console.WriteLine("Procesamiento de video completado.");
        }
    }
}
