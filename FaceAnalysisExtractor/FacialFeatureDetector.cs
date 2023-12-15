using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace FaceAnalysisExtractor
{
    class FacialFeatureDetector
    {
        // Constructores y métodos para detectar características faciales.

        private CascadeClassifier faceDetector;
        private CascadeClassifier eyeDetector;
        private CascadeClassifier mouthDetector;
        private CascadeClassifier noseDetector;

        public FacialFeatureDetector()
        {
            string assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string assemblyPath = Path.GetDirectoryName(assemblyLocation);
            string haarcascadesDir = Path.Combine(assemblyPath, "haarcascades");

            faceDetector = new CascadeClassifier(Path.Combine(haarcascadesDir, "haarcascade_frontalface_default.xml"));
            eyeDetector = new CascadeClassifier(Path.Combine(haarcascadesDir, "haarcascade_eye.xml"));
            mouthDetector = new CascadeClassifier(Path.Combine(haarcascadesDir, "haarcascade_mcs_mouth.xml"));
            noseDetector = new CascadeClassifier(Path.Combine(haarcascadesDir, "haarcascade_mcs_nose.xml"));

        }

        /// <summary>
        /// Detecta y procesa las características faciales en una imagen.
        /// </summary>
        public void DetectAndProcessFeatures(Image<Bgr, byte> image, FeatureManager featureManager)
        {
            var grayImage = image.Convert<Gray, byte>();
            Rectangle[] faces = faceDetector.DetectMultiScale(grayImage, 1.1, 10, Size.Empty, Size.Empty);

            int personId = 0;
            foreach (var face in faces)
            {
                var faceROI = grayImage.Copy(face);
                var currentFeatures = ExtractFacialFeatures(faceROI, eyeDetector, mouthDetector, noseDetector);

                featureManager.UpdateBestFeatures(personId, "Mouth", currentFeatures.Mouth, image.Copy(face));
                featureManager.UpdateBestFeatures(personId, "Eyes", currentFeatures.Eyes, image.Copy(face));
                featureManager.UpdateBestFeatures(personId, "Nose", currentFeatures.Nose, image.Copy(face));

                personId++;
            }
        }

        private FacialFeatures ExtractFacialFeatures(Image<Gray, byte> faceROI, CascadeClassifier eyeDetector, CascadeClassifier mouthDetector, CascadeClassifier noseDetector)
        {
            var eyes = eyeDetector.DetectMultiScale(faceROI, 1.1, 10, Size.Empty, Size.Empty).FirstOrDefault();
            var mouth = mouthDetector.DetectMultiScale(faceROI, 1.1, 10, Size.Empty, Size.Empty).FirstOrDefault();
            var nose = noseDetector.DetectMultiScale(faceROI, 1.1, 10, Size.Empty, Size.Empty).FirstOrDefault();

            if (eyes != Rectangle.Empty) eyes.Location = new Point(eyes.X + faceROI.ROI.X, eyes.Y + faceROI.ROI.Y);
            if (mouth != Rectangle.Empty) mouth.Location = new Point(mouth.X + faceROI.ROI.X, mouth.Y + faceROI.ROI.Y);
            if (nose != Rectangle.Empty) nose.Location = new Point(nose.X + faceROI.ROI.X, nose.Y + faceROI.ROI.Y);

            return new FacialFeatures(eyes, mouth, nose);
        }
    }
}
