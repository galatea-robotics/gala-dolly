using System.Drawing;
using Galatea.AI.Abstract;
using Galatea.AI.Characterization;
using Galatea.Imaging;

namespace Gala.Data.Databases
{
    internal class BinaryDataAccessManager : DataAccessManager
    {
        internal BinaryDataAccessManager(string connectionString) : base(connectionString)
        {
        }

        public ColorTemplateCollection GetColorTemplates()
        {
            ColorTemplateCollection result = new ColorTemplateCollection();
            try
            {
                result.Add(new ColorTemplate(10, "UVRed", "Red", Color.FromArgb(255, 0, 1)));
                result.Add(new ColorTemplate(11, "Red", "Red", Color.FromArgb(255, 0, 0)));
                result.Add(new ColorTemplate(12, "Yellow", "Yellow", Color.FromArgb(255, 255, 0)));
                result.Add(new ColorTemplate(13, "Green", "Green", Color.FromArgb(0, 255, 0)));
                result.Add(new ColorTemplate(14, "Blue", "Blue", Color.FromArgb(0, 0, 255)));
            }
            catch
            {
                result.Dispose();
                throw;
            }
            return result;
        }

        public ShapeTemplateCollection GetShapeTemplates()
        {
            ShapeTemplateCollection result = new ShapeTemplateCollection();
            try
            {
                result.Add(new ShapeTemplate(1, "Circle", "Round", GetRoundBlobPoints()));
                result.Add(new ShapeTemplate(2, "Triangle", "Triangular", GetTriangleBlobPoints()));
                result.Add(new ShapeTemplate(3, "Quadrilateral", "Four Corners", GetFourCornerBlobPoints()));
                result.Add(new ShapeTemplate(4, "Chevron", "Chevron", GetChevronBlobPoints()));

                //result.Add(new ShapeTemplate(5, "Pie", "Pie Shaped", GetPieBlobPoints()));
                //result.Add(new ShapeTemplate(6, "Four Points", "Four Points", GetFourStarBlobPoints()));
                //result.Add(new ShapeTemplate(7, "Five Points", "Five Points", GetFiveStarBlobPoints()));
                //result.Add(new ShapeTemplate(8, "Crescent", "Crescent", GetCrescentBlobPoints()));
            }
            catch
            {
                result.Dispose();
                throw;
            }
            return result;
        }

        #region Shape Template Members

        private static BlobPointList GetRoundBlobPoints()
        {
            BlobPointList blobPoints = new BlobPointList();
            blobPoints.IsRound = true;
            return blobPoints;
        }
        private static BlobPointList GetTriangleBlobPoints()
        {
            BlobPointList blobPoints = new BlobPointList();
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            return blobPoints;
        }
        private static BlobPointList GetFourCornerBlobPoints()
        {
            BlobPointList blobPoints = new BlobPointList();
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            return blobPoints;
        }
        private static BlobPointList GetChevronBlobPoints()
        {
            BlobPointList blobPoints = new BlobPointList();
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Convex));
            return blobPoints;
        }
        private static BlobPointList GetPieBlobPoints()
        {
            BlobPointList blobPoints = new BlobPointList();
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, true, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Convex));
            return blobPoints;
        }
        private static BlobPointList GetFourStarBlobPoints()
        {
            BlobPointList blobPoints = new BlobPointList();
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Convex));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Convex));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Convex));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Convex));
            return blobPoints;
        }
        private static BlobPointList GetFiveStarBlobPoints()
        {
            BlobPointList blobPoints = GetFourStarBlobPoints();
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Convex));
            return blobPoints;
        }

        private static BlobPointList GetCrescentBlobPoints()
        {
            BlobPointList blobPoints = new BlobPointList();
            blobPoints.Add(new BlobPoint(Point.Empty, -1, true, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, true, false, BlobPointTypes.Convex));
            blobPoints.Add(new BlobPoint(Point.Empty, -1, false, false, BlobPointTypes.Concave));
            return blobPoints;
        }

        #endregion

        public SymbolTemplateCollection GetSymbolTemplates()
        {
            SymbolTemplateCollection result = new SymbolTemplateCollection();

            result.Add(new SymbolTemplate(101, "A", "Letter A", "0001110000111100001111000011110001110110011111100111111111100111", 8, 8));
            result.Add(new SymbolTemplate(102, "B", "Letter B", "1111111011111111111001111111111011111111111001111111111111111110", 8, 8));
            result.Add(new SymbolTemplate(103, "C", "Letter C", "0001111001111111011100101110000011100000111000000111111100111110", 8, 8));
            result.Add(new SymbolTemplate(104, "D", "Letter D", "1111110011111110111001111110011111100111111001111111111111111110", 8, 8));
            result.Add(new SymbolTemplate(105, "E", "Letter E", "1111111111111111111000001111111111111111111000001111111111111111", 8, 8));
            result.Add(new SymbolTemplate(106, "F", "Letter F", "1111111111111111111000001111111011111110111000001110000011100000", 8, 8));
            result.Add(new SymbolTemplate(107, "G", "Letter G", "0011111001111111111000101110000011000111111000110111111100111110", 8, 8));
            result.Add(new SymbolTemplate(108, "H", "Letter H", "1110001111100011111000111111111111111111111000111110001111100011", 8, 8));
            result.Add(new SymbolTemplate(109, "I", "Letter I", "111111111111111111111111", 3, 8));
            result.Add(new SymbolTemplate(110, "J", "Letter J", "0000011100000111000001110000011100000111111001111111111101111110", 8, 8));
            result.Add(new SymbolTemplate(111, "K", "Letter K", "1100011111001110111111001111110011111100111011101100011011000111", 8, 8));
            result.Add(new SymbolTemplate(112, "L", "Letter L", "1110000011100000111000001110000011100000111000001111111111111111", 8, 8));
            result.Add(new SymbolTemplate(113, "M", "Letter M", "1110011111100111111101111111111111011111110111111101101111011011", 8, 8));
            result.Add(new SymbolTemplate(114, "N", "Letter N", "1110001111110011111100111111111111111111111011111110011111100111", 8, 8));
            result.Add(new SymbolTemplate(115, "O", "Letter O", "0011110001111110111001111100001111000011111001110111111000111100", 8, 8));
            result.Add(new SymbolTemplate(116, "P", "Letter P", "1111111011111111111001111110111111111110111000001110000011100000", 8, 8));
            result.Add(new SymbolTemplate(117, "Q", "Letter Q", "0011110001111110111001111100001111100011111011110111111100000011", 8, 8));
            result.Add(new SymbolTemplate(118, "R", "Letter R", "1111110011111110111001101110111011111100111011101110111011100111", 8, 8));
            result.Add(new SymbolTemplate(119, "S", "Letter S", "0011110001111110011001000111111000011111111001110111011101111110", 8, 8));
            result.Add(new SymbolTemplate(120, "T", "Letter T", "1111111111111111000111000001100000011000000110000001100000011000", 8, 8));
            result.Add(new SymbolTemplate(121, "U", "Letter U", "1110011111100011111000111110001111100011111001110111111100111110", 8, 8));
            result.Add(new SymbolTemplate(122, "V", "Letter V", "1100001101100111011001100111011000110110001111000011110000011100", 8, 8));
            result.Add(new SymbolTemplate(123, "W", "Letter W", "1101100111011011010110110111111101111110011101100110011001100110", 8, 8));
            result.Add(new SymbolTemplate(124, "X", "Letter X", "0110011001110110001111000011110000111100001111100111011011100111", 8, 8));
            result.Add(new SymbolTemplate(125, "Y", "Letter Y", "0110001101100110001111100011110000011000000110000001100000011000", 8, 8));
            result.Add(new SymbolTemplate(126, "Z", "Letter Z", "0111111101111111000011100001110000111000011100001111111111111111", 8, 8));
            result.Add(new SymbolTemplate(131, "1", "Number 1", "0000000000000000000000000011100000011000000010000000100000000000", 8, 8));
            result.Add(new SymbolTemplate(132, "2", "Number 2", "0000000000000000000000000011110000001100001110000011110000000000", 8, 8));
            result.Add(new SymbolTemplate(133, "3", "Number 3", "0000000000000000000000000010110000011100000011000011100000000000", 8, 8));
            result.Add(new SymbolTemplate(134, "4", "Number 4", "0000000000000000000000000001100000101000011111000000100000000000", 8, 8));
            result.Add(new SymbolTemplate(135, "5", "Number 5", "0000000000000000000000000011110000111100000011000011100000000000", 8, 8));
            result.Add(new SymbolTemplate(136, "6", "Number 6", "0000000000000000000000000011110000111100001001000011110000000000", 8, 8));
            result.Add(new SymbolTemplate(137, "7", "Number 7", "0000000000000000000000000011110000011000000100000001000000000000", 8, 8));
            result.Add(new SymbolTemplate(138, "8", "Number 8", "0000111000011010000111100001101000001100000000000000000000000000", 8, 8));
            result.Add(new SymbolTemplate(139, "9", "Number 9", "0000000000000000000000000001011000011110000011100000110000000000", 8, 8));
            result.Add(new SymbolTemplate(201, "a", "lowercase a", "0011110001111110011001110001111101111111111001111111111101111111", 8, 8));
            result.Add(new SymbolTemplate(202, "b", "lowercase b", "1110000011100000111111101111111111100111111001111111111111111110", 8, 8));
            result.Add(new SymbolTemplate(203, "c", "lowercase c", "0011110001111110111001111110000011100000111001110111111100111110", 8, 8));
            result.Add(new SymbolTemplate(204, "d", "lowercase d", "0000011100000111001111110111111111100111111001111111111101111111", 8, 8));
            result.Add(new SymbolTemplate(205, "e", "lowercase e", "0011110001111110111001111111111111111111111000000111111100111110", 8, 8));
            result.Add(new SymbolTemplate(206, "f", "lowercase f", "0011111100111101111111101111111000111000001110000011100000111000", 8, 8));
            result.Add(new SymbolTemplate(207, "g", "lowercase g", "0011101111111111111001111110011101111111000001111110011101111110", 8, 8));
            result.Add(new SymbolTemplate(208, "h", "lowercase h", "1110000011100000111011101111111111100111111001111110011111100111", 8, 8));
            result.Add(new SymbolTemplate(209, "i", "lowercase i", "111000000111111111111111", 3, 8));
            result.Add(new SymbolTemplate(210, "j", "lowercase j", "0001111100000000000111110001111100011111000111110001111111111110", 8, 8));
            result.Add(new SymbolTemplate(211, "k", "lowercase k", "1110000011100000111001101110111011111100111111001110111011100111", 8, 8));
            result.Add(new SymbolTemplate(213, "m", "lowercase m", "1101001011111111111111111101101111011011110110111101101111011011", 8, 8));
            result.Add(new SymbolTemplate(214, "n", "lowercase n", "1110111011111111111111111110011111100111111001111110011111100111", 8, 8));
            result.Add(new SymbolTemplate(215, "o", "lowercase o", "0011110001111110111001111110011111100111111001110111111000111100", 8, 8));
            result.Add(new SymbolTemplate(216, "p", "lowercase p", "1110110011111111111001111110011111111111111111101110000011100000", 8, 8));
            result.Add(new SymbolTemplate(217, "q", "lowercase q", "0011101101111111111001111110011111111111011111110000011100000111", 8, 8));
            result.Add(new SymbolTemplate(218, "r", "lowercase r", "1111011111111111111111101111000011110000111100001111000011110000", 8, 8));
            result.Add(new SymbolTemplate(219, "s", "lowercase s", "0011110001111110011000000111111000111111000001111111111101111110", 8, 8));
            result.Add(new SymbolTemplate(220, "t", "lowercase t", "0000000000001000001110000111111000111000001110000011111000000000", 8, 8));
            result.Add(new SymbolTemplate(221, "u", "lowercase u", "0000000001100110011001100110011001100110011111100111111000000000", 8, 8));
            result.Add(new SymbolTemplate(222, "v", "lowercase v", "0110011001101110011011000011110000111000001110000000000000000000", 8, 8));
            result.Add(new SymbolTemplate(223, "w", "lowercase w", "0000000000000000000000000111101000111110001111100011011000000000", 8, 8));
            result.Add(new SymbolTemplate(224, "x", "lowercase x", "0000000001100110011111000011110000111100001111000110011000000000", 8, 8));
            result.Add(new SymbolTemplate(225, "y", "lowercase y", "0110001101100111011101100011111000111100000111000011100001111000", 8, 8));
            result.Add(new SymbolTemplate(226, "z", "lowercase z", "0000000000111110000111100001110000111000001111100011111000000000", 8, 8));

            return result;
        }

        protected internal override void InitializeMemoryBank()
        {
            this.Clear();
            Memory.Reset();

            // Initialize
            ColorTemplateCollection colorTemplates = GetColorTemplates();
            ShapeTemplateCollection shapeTemplates = GetShapeTemplates();
            SymbolTemplateCollection symbolTemplates = GetSymbolTemplates();

            this.Add(colorTemplates);
            this.Add(shapeTemplates);
            this.Add(symbolTemplates);

            this.Add(new NamedEntityCollection());

            // Feedback Table
            FeedbackCounterTable feedbackCounterTable = new FeedbackCounterTable();
            this.SetFeedbackCounterTable(feedbackCounterTable);
        }

        public override void SaveAll()
        {
            //throw new NotImplementedException();
            Engine.Debugger.Log(Galatea.Diagnostics.DebuggerLogLevel.Error, "NOT IMPLEMENTED");
        }
    }
}
