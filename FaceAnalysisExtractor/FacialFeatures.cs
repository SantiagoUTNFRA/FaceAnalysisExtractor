using System.Drawing;

namespace FaceAnalysisExtractor
{
    /// <summary>
    /// Representa las características faciales detectadas, como ojos, boca y nariz.
    /// </summary>
    internal class FacialFeatures
    {
        public Rectangle Eyes { get; private set; }
        public Rectangle Mouth { get; private set; }
        public Rectangle Nose { get; private set; }

        /// <summary>
        /// Constructor para inicializar las características faciales.
        /// </summary>
        public FacialFeatures(Rectangle eyes, Rectangle mouth, Rectangle nose)
        {
            Eyes = eyes;
            Mouth = mouth;
            Nose = nose;
        }
    }
}
