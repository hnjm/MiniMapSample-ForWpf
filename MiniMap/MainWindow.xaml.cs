using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace MiniMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void map_Loaded(object sender, RoutedEventArgs e)
        {
            map.MapUnit = GeographyUnit.DecimalDegree;

            LayerOverlay layerOverlay = new LayerOverlay();
            WorldStreetsAndImageryRasterLayer worldLayer = new WorldStreetsAndImageryRasterLayer();
            layerOverlay.Layers.Add(worldLayer);
            ShapeFileFeatureLayer shapeFileLayer = new ShapeFileFeatureLayer(@"..\..\AppData\states.shp");
            shapeFileLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = WorldStreetsAreaStyles.Military();
            shapeFileLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(shapeFileLayer);
            map.Overlays.Add(layerOverlay);
            
            AdornmentOverlay adornmentOverlay = new AdornmentOverlay();
            MiniMapAdornmentLayer miniMapAndornmentLayer = new MiniMapAdornmentLayer(200, 160);
            miniMapAndornmentLayer.Location = AdornmentLocation.UpperRight;
            miniMapAndornmentLayer.XOffsetInPixel = -20;
            miniMapAndornmentLayer.YOffsetInPixel = 20;
            miniMapAndornmentLayer.Layers.Add(worldLayer);
            miniMapAndornmentLayer.Layers.Add(shapeFileLayer);
            adornmentOverlay.Layers.Add(miniMapAndornmentLayer);
            map.AdornmentOverlay = adornmentOverlay;

            shapeFileLayer.Open();
            map.CurrentExtent = shapeFileLayer.GetBoundingBox();
            map.Refresh();
        }
    }
}
