using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace FaceAnalysisExtractor
{
    /// <summary>
    /// Almacena los datos de una característica facial específica, como su ubicación y confianza.
    /// </summary>
    class FeatureData
    {
        public Image<Bgr, byte> Image { get; set; }
        public Rectangle FeatureRect { get; set; }
        public double Confidence { get; set; }

        /// <summary>
        /// Constructor para inicializar los datos de la característica.
        /// </summary>
        public FeatureData(Image<Bgr, byte> image, Rectangle featureRect)
        {
            Image = image;
            FeatureRect = featureRect;
            Confidence = featureRect.Width * featureRect.Height;
        }
    }
}
