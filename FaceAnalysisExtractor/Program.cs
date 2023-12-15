namespace FaceAnalysisExtractor
{
    /// <summary>
    /// Punto de entrada principal para la aplicación.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Método principal que inicia el procesamiento del video.
        /// </summary>
        static void Main(string[] args)
        {
            // Creación e inicio del procesamiento de video.
            var videoProcessor = new VideoProcessor("videoTest.mp4", "FramesOutput", "FacialChanges");
            videoProcessor.ProcessVideo();
        }
    }
}