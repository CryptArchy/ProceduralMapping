using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonExtensions.EnumerableExtensions;
using PCG.Library;
using System.Drawing.Drawing2D;
using CommonExtensions.NumericExtensions;
using PCG.Library.Intelligence;
using PCG.Library.Extensions;
using PCG.Library.Generators;
using System.IO;
using PCG.Library.Core;

namespace ProceduralMapping
{
    public partial class frmMapViewer : Form
    {
        private const int DEFAULT_GRID_SIZE = 64;
        private const int DEFAULT_CELL_SIZE = 10;
        private const int DEFAULT_FLAT_COLOR = Int32.MinValue;
        private const string DEFAULT_GENERATOR = "Binary Division";
        private const string DEFAULT_SOLVER = "AStar";
        private Size initialWindowSize;

        private List<Bitmap> Layers = new List<Bitmap>();
        private Dictionary<string, Func<IMapGenerator<int>>> MapGenerators;
        private Dictionary<string, Func<IPathFinder>> PathFinders;

        private IRandom Rng = new SysRandom(42);

        public frmMapViewer()
        {
            InitializeComponent();

            MapGenerators = new Dictionary<string, Func<IMapGenerator<int>>>();
            MapGenerators.Add("Empty (Rectangle)", () => new EmptyMapGenerator());
            MapGenerators.Add("Empty (Circle)", () => new EmptyCircleMapGenerator());
            MapGenerators.Add("Empty (Ellipse)", () => new EmptyEllipseMapGenerator());
            MapGenerators.Add("Noise", () => new NoiseMapGenerator());
            MapGenerators.Add("GrowingTree (Stack)", () => GrowingTreeMazeGenerator.TakeNewestCellStrategy);
            MapGenerators.Add("GrowingTree (Queue)", () => GrowingTreeMazeGenerator.TakeOldestCellStrategy);
            MapGenerators.Add("GrowingTree (Random)", () => GrowingTreeMazeGenerator.TakeRandomCellStrategy);
            MapGenerators.Add("GrowingTree (Mix)", () => new GrowingTreeMazeGenerator(new GrowingTreeMazeGenerator.MixedStrategy(0.2,0.4,0.4)));
            MapGenerators.Add("Binary Division", () => new BinaryDivisionMapGenerator());
            MapGenerators.Add("Simple (RH-Spiral)", () => SimpleMapGenerator<int>.RightHandSpiral);
            MapGenerators.Add("Simple (RHI-Spiral)", () => SimpleMapGenerator<int>.RightHandInOutSpiral);
            MapGenerators.Add("Simple (LH-Spiral)", () => SimpleMapGenerator<int>.LeftHandSpiral);
            MapGenerators.Add("Simple (LHI-Spiral)", () => SimpleMapGenerator<int>.LeftHandInOutSpiral);
            MapGenerators.Add("Simple (Verticals)", () => SimpleMapGenerator<int>.VerticalLines);
            MapGenerators.Add("Simple (Horizontals)", () => SimpleMapGenerator<int>.HorizontalLines);
            MapGenerators.Add("Template (Regions)", () => new TemplateRegionMapGenerator());
            MapGenerators.Add("Template (Binary)", () => new TemplateBinaryDivisionMapGenerator());
            MapGenerators.Add("Kruskal's Algorithm", () => new KruskalsMapGenerator());
            MapGenerators.Add("Kruskal's Loopy", () => new KruskalsLoopyMapGenerator());
            MapGenerators.Add("Cellular Automata", () => new CellularAutomataMapGenerator<int>());
            
            PathFinders = new Dictionary<string, Func<IPathFinder>>();
            PathFinders.Add("None", () => new EmptyPathFinder());
            PathFinders.Add("AStar", () => new AStarPathFinder());
            PathFinders.Add("Right-Hand Rule", () => WallFollower.RightHandRule);
            PathFinders.Add("Left-Hand Rule", () => WallFollower.LeftHandRule);
            PathFinders.Add("Random Walk", () => new RandomWalkPathFinder(Rng));

            cmbAlgorithm.DataSource = new List<string>(MapGenerators.Keys);
            cmbSolver.DataSource = new List<string>(PathFinders.Keys);

            diaSaveFile.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG" 
                               + "|All files (*.*)|*.*";

            txtFlatDispColor.Text = DEFAULT_FLAT_COLOR.ToString();
            numCellHeight.Value = DEFAULT_CELL_SIZE;
            numCellWidth.Value = DEFAULT_CELL_SIZE;
            numGridHeight.Value = DEFAULT_GRID_SIZE;
            numGridWidth.Value = DEFAULT_GRID_SIZE;
            cmbAlgorithm.SelectedItem = DEFAULT_GENERATOR;
            cmbSolver.SelectedItem = DEFAULT_SOLVER;
            initialWindowSize = this.Size;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var seed = (int)numSeed.Value;
            Rng = new SysRandom(seed);

            var gridSize = new Size3((int)numGridWidth.Value, (int)numGridHeight.Value, (int)numGridDepth.Value);

            var cellWidth = (int)numCellWidth.Value;
            var cellHeight = (int)numCellHeight.Value;

            var layer = (int)numLayer.Value;

            var imageWidth = gridSize.Width * cellWidth + 1;
            var imageHeight = gridSize.Height * cellHeight + 1;
            this.Width = imageWidth + groupBox1.Width + 55;
            this.Height = (imageHeight + 75).GreaterOf(initialWindowSize.Height);

            var mapGeneratorName = (string)cmbAlgorithm.SelectedItem;
            var pathFinderName = (string)cmbSolver.SelectedItem;
            IMapGenerator<int> gen = MapGenerators[mapGeneratorName].Invoke();
            IPathFinder pathFinder = PathFinders[pathFinderName].Invoke();

            var map = gen.Generate(gridSize, Rng);

            Point3 pntPathFinderStart, pntPathFinderEnd;
            try
            {
                pntPathFinderStart = Point3.Parse(txtStartPoint.Text);
                pntPathFinderEnd = Point3.Parse(txtEndPoint.Text);
            }
            catch
            {
                pntPathFinderStart = new Point3(0, 0, 0);
                pntPathFinderEnd = new Point3(gridSize.Width - 1, gridSize.Height - 1, gridSize.Depth - 1);

                var arePointsValid = false;
                var counter = 0;
                while (!arePointsValid && counter < 1000)
                {
                    pntPathFinderStart = Rng.NextPoint3(0, gridSize.Width, gridSize.Height, gridSize.Depth);
                    pntPathFinderEnd = Rng.NextPoint3(0, gridSize.Width, gridSize.Height, gridSize.Depth);
                    counter++;
                    arePointsValid = (map[pntPathFinderStart].Edges != Direction.Center &&
                                      map[pntPathFinderEnd].Edges != Direction.Center);
                }

                txtStartPoint.Text = pntPathFinderStart.ToString();
                txtEndPoint.Text = pntPathFinderEnd.ToString();
            }                       

            var path = pathFinder.FindRoute(map, pntPathFinderStart, pntPathFinderEnd);

            CellMapDisplayMode dispMode;
            if (rdDispValue.Checked)
                dispMode = CellMapDisplayMode.CellValue;
            else if (rdDispValueBytes.Checked)
                dispMode = CellMapDisplayMode.CellValueByte;
            else if (rdDispEdges.Checked)
                dispMode = CellMapDisplayMode.EdgeValue;
            else
                dispMode = CellMapDisplayMode.FlatColor;

            Layers = map.Draw(false, cellWidth, cellHeight, dispMode, Int32.Parse(txtFlatDispColor.Text));
            Layers.DrawPath(path, pntPathFinderStart, pntPathFinderEnd, cellWidth, cellHeight);

            if (layer >= gridSize.Depth) 
                layer = gridSize.Depth - 1;
            pctCanvas.Image = Layers[layer];
        }        

        private void numGridWidth_ValueChanged(object sender, EventArgs e)
        {
            if (chkGridLink.Checked && numGridHeight.Value != numGridWidth.Value)
                numGridHeight.Value = numGridWidth.Value;

            txtStartPoint.Text = string.Empty;
            txtEndPoint.Text = string.Empty;
        }

        private void numGridHeight_ValueChanged(object sender, EventArgs e)
        {
            if (chkGridLink.Checked && numGridWidth.Value != numGridHeight.Value)
                numGridWidth.Value = numGridHeight.Value;

            txtStartPoint.Text = string.Empty;
            txtEndPoint.Text = string.Empty;
        }

        private void numCellWidth_ValueChanged(object sender, EventArgs e)
        {
            if (chkCellLink.Checked && numCellHeight.Value != numCellWidth.Value)
                numCellHeight.Value = numCellWidth.Value;
        }

        private void numCellHeight_ValueChanged(object sender, EventArgs e)
        {
            if (chkCellLink.Checked && numCellWidth.Value != numCellHeight.Value)
                numCellWidth.Value = numCellHeight.Value;
        }

        private void numLayer_ValueChanged(object sender, EventArgs e)
        {
            var gridDepth = (int)numGridDepth.Value;
            var layer = (int)numLayer.Value;
            if (layer >= gridDepth) layer = gridDepth - 1;

            pctCanvas.Image = Layers[layer];
        }

        private void btnOpenFileDialog_Click(object sender, EventArgs e)
        {
            diaSaveFile.ShowDialog();
        }

        private void diaSaveFile_FileOk(object sender, CancelEventArgs e)
        {
            var cntLayer = 0;
            foreach (var layer in Layers)
            {
                cntLayer++;
                var filename = Path.GetFileNameWithoutExtension(diaSaveFile.FileName);
                var fileext = Path.GetExtension(diaSaveFile.FileName);
                var filepath = Path.GetDirectoryName(diaSaveFile.FileName);
                var fullpath = Path.Combine(filepath, filename + cntLayer.ToString() + fileext);
                layer.Save(fullpath);
            }
        }

        private void rdDispFlatValue_CheckedChanged(object sender, EventArgs e)
        {
            txtFlatDispColor.Enabled = rdDispFlatValue.Checked;
        }

        private void cmbAlgorithm_SelectedValueChanged(object sender, EventArgs e)
        {
            var mapGeneratorName = (string)cmbAlgorithm.SelectedItem;
            IMapGenerator<int> gen = MapGenerators[mapGeneratorName].Invoke();
            var config = gen.GetDefaultConfig();

        }        
    }
}
