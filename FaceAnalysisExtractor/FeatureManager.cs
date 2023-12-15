using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAnalysisExtractor
{
    class FeatureManager
    {
        // Constructores y métodos para manejar las características faciales.

        private Dictionary<int, Dictionary<string, FeatureData>> bestFeatures;

        public FeatureManager()
        {
            bestFeatures = new Dictionary<int, Dictionary<string, FeatureData>>();
        }

        /// <summary>
        /// Actualiza las mejores características faciales detectadas.
        /// </summary
        public void UpdateBestFeatures(int personId, string featureName, Rectangle featureRect, Image<Bgr, byte> image)
        {
            if (featureRect != Rectangle.Empty)
            {
                var currentData = new FeatureData(image, featureRect);

                if (!bestFeatures.ContainsKey(personId))
                {
                    bestFeatures[personId] = new Dictionary<string, FeatureData>();
                }

                if (!bestFeatures[personId].ContainsKey(featureName) || bestFeatures[personId][featureName].Confidence < currentData.Confidence)
                {
                    bestFeatures[personId][featureName] = currentData;
                }
            }
        }

        /// <summary>
        /// Guarda las mejores características faciales detectadas.
        /// </summary>
        public void SaveBestFeatures(string folder)
        {
            foreach (var personEntry in bestFeatures)
            {
                foreach (var featureEntry in personEntry.Value)
                {
                    var featureData = featureEntry.Value;
                    featureData.Image.Draw(featureData.FeatureRect, new Bgr(Color.Blue), 2);
                    string path = Path.Combine(folder, $"Person_{personEntry.Key}_{featureEntry.Key}_{DateTime.Now.Ticks}.jpg");
                    featureData.Image.Save(path);
                }
            }
        }
    }
}
