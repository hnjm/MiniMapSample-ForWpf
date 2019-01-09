using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
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
            map.MapUnit = GeographyUnit.Meter;
            map.ZoomLevelSet = ThinkGeoCloudMapsOverlay.GetZoomLevelSet();

            ThinkGeoCloudMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudMapsOverlay() { MapType = ThinkGeoCloudMapsMapType.Aerial };
            map.Overlays.Add(thinkGeoCloudMapsOverlay);

            LayerOverlay layerOverlay = new LayerOverlay();
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
            miniMapAndornmentLayer.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.LightSlateGray)));
            miniMapAndornmentLayer.Layers.Add(shapeFileLayer);
            adornmentOverlay.Layers.Add(miniMapAndornmentLayer);
            map.AdornmentOverlay = adornmentOverlay;

            map.CurrentExtent = new RectangleShape(-13744767, 6805289, -8813661, 3239043);
            map.Refresh();
        }
    }
}
