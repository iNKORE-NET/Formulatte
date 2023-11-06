using Formulatte.Engine.Common;
using Formulatte.Engine.Controls;
using iNKORE.UI.WPF.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Formulatte.Engine.Controls
{
    /// <summary>
    /// Interaction logic for MathToolbar.xaml
    /// </summary>
    public partial class EquationToolBar : UserControl, IEditorToolbar
    {
        public event EventHandler CommandCompleted = (x, y) => { };
        Dictionary<object, ButtonPanel> buttonPanelMapping = new Dictionary<object, ButtonPanel>();
        ButtonPanel visiblePanel = null;

        public EditorHandler EditorHandler { get; set; }

        public EquationToolBar()
        {
            InitializeComponent();
        }

        private void toolBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (visiblePanel != null)
            {
                visiblePanel.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (buttonPanelMapping[sender].Visibility != System.Windows.Visibility.Visible)
            {
                buttonPanelMapping[sender].Visibility = System.Windows.Visibility.Visible;
                visiblePanel = buttonPanelMapping[sender];
            }
        }

        public void HideVisiblePanel()
        {
            if (visiblePanel != null)
            {
                visiblePanel.Visibility = System.Windows.Visibility.Collapsed;
                visiblePanel = null;
            }
        }

        private void toolBarButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeActivePanel(sender);
        }
        
        private void toolBarButton_GotFocus(object sender, RoutedEventArgs e)
        {
            ChangeActivePanel(sender);
        }

        void ChangeActivePanel(object sender)
        {
            if (visiblePanel != null)
            {
                visiblePanel.Visibility = System.Windows.Visibility.Collapsed;
                buttonPanelMapping[sender].Visibility = System.Windows.Visibility.Visible;
                visiblePanel = buttonPanelMapping[sender];
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CreateBracketsPanel();
            CreateSumsProductsPanel();
            CreateIntegralsPanel();
            CreateSubAndSuperPanel();
            CreateDivAndRootsPanel();
            CreateCompositePanel();
            CreateDecoratedEquationPanel();
            CreateDecoratedCharacterPanel();
            CreateArrowEquationPanel();
            CreateBoxEquationPanel();
            CreateMatrixPanel();
        }

        void CreatePanel(List<CommandDetails> list, Button toolBarButton, int columns, int margin)
        {
            ButtonPanel bp = new ButtonPanel(list, columns, margin, this);
            bp.ButtonClick += (x, y) => { CommandCompleted(this, EventArgs.Empty); visiblePanel = null; };
            mainToolBarPanel.Children.Add(bp);
            Canvas.SetTop(bp, mainToolBarPanel.Height);
            Vector offset = VisualTreeHelper.GetOffset(toolBarButton);
            Canvas.SetLeft(bp, offset.X + 2);
            bp.Visibility = Visibility.Collapsed;
            buttonPanelMapping.Add(toolBarButton, bp);
        }

        void CreateImagePanel(Uri[] imageUris, CommandType[] commands, object[] paramz, Button toolBarButton, int columns)
        {
            OpacityMaskedImage[] items = new OpacityMaskedImage[imageUris.Count()];
            for (int i = 0; i < items.Count(); i++)
            {
                items[i] = new OpacityMaskedImage();
                BitmapImage bmi = new BitmapImage(imageUris[i]);
                items[i].Source = bmi;
                items[i].Width = 16;
                items[i].Height = 16;
            }
            List<CommandDetails> list = new List<CommandDetails>();
            for (int i = 0; i < items.Count(); i++)
            {
                list.Add(new CommandDetails { Image = items[i], CommandType = commands[i], CommandParam = paramz[i] });
            }
            CreatePanel(list, toolBarButton, columns, 0);
        }

        Uri CreateImageUri(string subFolder, string imageFileName)
        {
            return new Uri("pack://application:,,,/Formulatte.Engine;component/Resources/Images/Commands/" + subFolder + "/" + imageFileName, UriKind.Absolute);
        }

        void CreateBracketsPanel()
        {
            Uri[] imageUris = { CreateImageUri("Brackets", "SingleBar.png"),
                                CreateImageUri("Brackets", "DoubleBar.png"),
                                CreateImageUri("Brackets", "Floor.png"),
                                CreateImageUri("Brackets", "Ceiling.png"),
                                CreateImageUri("Brackets", "CurlyBracket.png"),
                                CreateImageUri("Brackets", "RightRightSquareBracket.png"),
                                CreateImageUri("Brackets", "Parentheses.png"),
                                CreateImageUri("Brackets", "SquareBracket.png"),
                                CreateImageUri("Brackets", "AngleBar.png"),
                                CreateImageUri("Brackets", "BarAngle.png"),
                                CreateImageUri("Brackets", "SquareBar.png"),
                                CreateImageUri("Brackets", "ParenthesisSquare.png"),
                                CreateImageUri("Brackets", "SquareParenthesis.png"),
                                CreateImageUri("Brackets", "LeftLeftSquareBracket.png"),
                                CreateImageUri("Brackets", "PointingAngles.png"),
                                CreateImageUri("Brackets", "RightLeftSquareBracket.png"),
                                CreateImageUri("Brackets", "LeftCurlyBracket.png"),
                                CreateImageUri("Brackets", "RightCurlyBracket.png"),
                                CreateImageUri("Brackets", "LeftDoubleBar.png"),
                                CreateImageUri("Brackets", "RightDoubleBar.png"),
                                CreateImageUri("Brackets", "LeftParenthesis.png"),
                                CreateImageUri("Brackets", "RightParenthesis.png"),
                                CreateImageUri("Brackets", "LeftSquareBar.png"),
                                CreateImageUri("Brackets", "RightSquareBar.png"),
                                CreateImageUri("Brackets", "LeftSquareBracket.png"),
                                CreateImageUri("Brackets", "RightSquareBracket.png"),
                                CreateImageUri("Brackets", "LeftAngle.png"),
                                CreateImageUri("Brackets", "RightAngle.png"),
                                CreateImageUri("Brackets", "LeftBar.png"),
                                CreateImageUri("Brackets", "RightBar.png"),
                                CreateImageUri("Brackets", "TopCurlyBracket.png"),
                                CreateImageUri("Brackets", "BottomCurlyBracket.png"),
                                CreateImageUri("Brackets", "TopSquareBracket.png"),
                                CreateImageUri("Brackets", "BottomSquareBracket.png"),
                                CreateImageUri("Brackets", "DoubleArrowBarBracket.png"),                                
                               };

            CommandType[] commands = { CommandType.LeftRightBracket, CommandType.LeftRightBracket, CommandType.LeftRightBracket, 
                                       CommandType.LeftRightBracket, CommandType.LeftRightBracket, CommandType.LeftRightBracket,
                                       CommandType.LeftRightBracket, CommandType.LeftRightBracket, CommandType.LeftRightBracket, 
                                       CommandType.LeftRightBracket, CommandType.LeftRightBracket, CommandType.LeftRightBracket,
                                       CommandType.LeftRightBracket, CommandType.LeftRightBracket, CommandType.LeftRightBracket,
                                       CommandType.LeftRightBracket,
                                       CommandType.LeftBracket,      CommandType.RightBracket, 
                                       CommandType.LeftBracket,      CommandType.RightBracket, 
                                       CommandType.LeftBracket,      CommandType.RightBracket, 
                                       CommandType.LeftBracket,      CommandType.RightBracket, 
                                       CommandType.LeftBracket,      CommandType.RightBracket, 
                                       CommandType.LeftBracket,      CommandType.RightBracket, 
                                       CommandType.LeftBracket,      CommandType.RightBracket, 
                                       CommandType.TopBracket,  CommandType.BottomBracket,
                                       CommandType.TopBracket, CommandType.BottomBracket, 
                                       CommandType.DoubleArrowBarBracket,                                       
                                     };
            object[] paramz = { 
                                   new BracketSignType [] {BracketSignType.LeftBar,       BracketSignType.RightBar},
                                   new BracketSignType [] {BracketSignType.LeftDoubleBar, BracketSignType.RightDoubleBar},
                                   new BracketSignType [] {BracketSignType.LeftFloor,     BracketSignType.RightFloor},
                                   new BracketSignType [] {BracketSignType.LeftCeiling,   BracketSignType.RightCeiling},
                                   new BracketSignType [] {BracketSignType.LeftCurly,     BracketSignType.RightCurly},
                                   new BracketSignType [] {BracketSignType.RightSquare,   BracketSignType.RightSquare},
                                   new BracketSignType [] {BracketSignType.LeftRound,     BracketSignType.RightRound},
                                   new BracketSignType [] {BracketSignType.LeftSquare,    BracketSignType.RightSquare},
                                   new BracketSignType [] {BracketSignType.LeftAngle,     BracketSignType.RightBar},
                                   new BracketSignType [] {BracketSignType.LeftBar,       BracketSignType.RightAngle},
                                   new BracketSignType [] {BracketSignType.LeftSquareBar, BracketSignType.RightSquareBar},
                                   new BracketSignType [] {BracketSignType.LeftRound,     BracketSignType.RightSquare},
                                   new BracketSignType [] {BracketSignType.LeftSquare,    BracketSignType.RightRound},
                                   new BracketSignType [] {BracketSignType.LeftSquare,    BracketSignType.LeftSquare},
                                   new BracketSignType [] {BracketSignType.LeftAngle,     BracketSignType.RightAngle},                                   
                                   new BracketSignType [] {BracketSignType.RightSquare,   BracketSignType.LeftSquare},

                                   BracketSignType.LeftCurly,
                                   BracketSignType.RightCurly, 
                                   BracketSignType.LeftDoubleBar,
                                   BracketSignType.RightDoubleBar, 
                                   BracketSignType.LeftRound,
                                   BracketSignType.RightRound, 
                                   BracketSignType.LeftSquareBar,
                                   BracketSignType.RightSquareBar, 
                                   BracketSignType.LeftSquare,
                                   BracketSignType.RightSquare, 
                                   BracketSignType.LeftAngle,
                                   BracketSignType.RightAngle,
                                   BracketSignType.LeftBar,
                                   BracketSignType.RightBar, 
                                   HorizontalBracketSignType.TopCurly,
                                   HorizontalBracketSignType.BottomCurly,
                                   HorizontalBracketSignType.ToSquare,
                                   HorizontalBracketSignType.BottomSquare,
                                   0,
                              };

            CreateImagePanel(imageUris, commands, paramz, bracketsButton, 4);
        }

        void CreateSumsProductsPanel()
        {
            Uri[] imageUris = {   
                                  CreateImageUri("SumsProducts", "sum.png"),
                                  CreateImageUri("SumsProducts", "sumSub.png"),
                                  CreateImageUri("SumsProducts", "sumSubSuper.png"),
                                  CreateImageUri("SumsProducts", "sumBottom.png"),
                                  CreateImageUri("SumsProducts", "sumBottomTop.png"),                                  

                                  CreateImageUri("SumsProducts", "product.png"),
                                  CreateImageUri("SumsProducts", "productSub.png"),
                                  CreateImageUri("SumsProducts", "productSubSuper.png"),
                                  CreateImageUri("SumsProducts", "productBottom.png"),
                                  CreateImageUri("SumsProducts", "productBottomTop.png"),

                                  CreateImageUri("SumsProducts", "coProduct.png"),
                                  CreateImageUri("SumsProducts", "coProductSub.png"),
                                  CreateImageUri("SumsProducts", "coProductSubSuper.png"),
                                  CreateImageUri("SumsProducts", "coProductBottom.png"),
                                  CreateImageUri("SumsProducts", "coProductBottomTop.png"),
                                  
                                  CreateImageUri("SumsProducts", "intersection.png"),
                                  CreateImageUri("SumsProducts", "intersectionSub.png"),
                                  CreateImageUri("SumsProducts", "intersectionSubSuper.png"),
                                  CreateImageUri("SumsProducts", "intersectionBottom.png"),
                                  CreateImageUri("SumsProducts", "intersectionBottomTop.png"),
                                  
                                  CreateImageUri("SumsProducts", "union.png"),
                                  CreateImageUri("SumsProducts", "unionSub.png"),
                                  CreateImageUri("SumsProducts", "unionSubSuper.png"),
                                  CreateImageUri("SumsProducts", "unionBottom.png"),
                                  CreateImageUri("SumsProducts", "unionBottomTop.png"),
                              };
            CommandType[] commands = Enumerable.Repeat(CommandType.SignComposite, imageUris.Length).ToArray();
            object[] paramz = { 
                                  new object [] {Position.None,    SignCompositeSymbol.Sum} ,
                                  new object [] {Position.Sub,       SignCompositeSymbol.Sum} ,
                                  new object [] {Position.SubAndSuper,  SignCompositeSymbol.Sum} ,
                                  new object [] {Position.Bottom,    SignCompositeSymbol.Sum} ,
                                  new object [] {Position.BottomAndTop, SignCompositeSymbol.Sum} ,

                                  new object [] {Position.None,    SignCompositeSymbol.Product} ,
                                  new object [] {Position.Sub,       SignCompositeSymbol.Product} ,
                                  new object [] {Position.SubAndSuper,  SignCompositeSymbol.Product} ,
                                  new object [] {Position.Bottom,    SignCompositeSymbol.Product} ,
                                  new object [] {Position.BottomAndTop, SignCompositeSymbol.Product} ,

                                  new object [] {Position.None,    SignCompositeSymbol.CoProduct} ,
                                  new object [] {Position.Sub,       SignCompositeSymbol.CoProduct} ,
                                  new object [] {Position.SubAndSuper,  SignCompositeSymbol.CoProduct} ,
                                  new object [] {Position.Bottom,    SignCompositeSymbol.CoProduct} ,
                                  new object [] {Position.BottomAndTop, SignCompositeSymbol.CoProduct} ,

                                  new object [] {Position.None,    SignCompositeSymbol.Intersection} ,
                                  new object [] {Position.Sub,       SignCompositeSymbol.Intersection} ,
                                  new object [] {Position.SubAndSuper,  SignCompositeSymbol.Intersection} ,
                                  new object [] {Position.Bottom,    SignCompositeSymbol.Intersection} ,
                                  new object [] {Position.BottomAndTop, SignCompositeSymbol.Intersection} ,

                                  new object [] {Position.None,    SignCompositeSymbol.Union} ,
                                  new object [] {Position.Sub,       SignCompositeSymbol.Union} ,
                                  new object [] {Position.SubAndSuper,  SignCompositeSymbol.Union} ,
                                  new object [] {Position.Bottom,    SignCompositeSymbol.Union} ,
                                  new object [] {Position.BottomAndTop, SignCompositeSymbol.Union} ,
                              };

            CreateImagePanel(imageUris, commands, paramz, sumsProductsButton, 5);
        }

        void CreateIntegralsPanel()
        {
            Uri[] imageUris = { 
                                  CreateImageUri("Integrals/Single", "Simple.png"),
                                  CreateImageUri("Integrals/Single", "Sub.png"),
                                  CreateImageUri("Integrals/Single", "SubSuper.png"),
                                  CreateImageUri("Integrals/Single", "Bottom.png"),
                                  CreateImageUri("Integrals/Single", "BottomTop.png"),                                  

                                  CreateImageUri("Integrals/Double", "Simple.png"),
                                  CreateImageUri("Integrals/Double", "Sub.png"),
                                  CreateImageUri("Integrals/Double", "SubSuper.png"),
                                  CreateImageUri("Integrals/Double", "Bottom.png"),
                                  CreateImageUri("Integrals/Double", "BottomTop.png"),

                                  CreateImageUri("Integrals/Triple", "Simple.png"),
                                  CreateImageUri("Integrals/Triple", "Sub.png"),
                                  CreateImageUri("Integrals/Triple", "SubSuper.png"),
                                  CreateImageUri("Integrals/Triple", "Bottom.png"),
                                  CreateImageUri("Integrals/Triple", "BottomTop.png"),
                                  
                                  CreateImageUri("Integrals/Contour", "Simple.png"),
                                  CreateImageUri("Integrals/Contour", "Sub.png"),
                                  CreateImageUri("Integrals/Contour", "SubSuper.png"),
                                  CreateImageUri("Integrals/Contour", "Bottom.png"),
                                  CreateImageUri("Integrals/Contour", "BottomTop.png"),

                                  CreateImageUri("Integrals/Surface", "Simple.png"),
                                  CreateImageUri("Integrals/Surface", "Sub.png"),
                                  CreateImageUri("Integrals/Surface", "SubSuper.png"),
                                  CreateImageUri("Integrals/Surface", "Bottom.png"),
                                  CreateImageUri("Integrals/Surface", "BottomTop.png"),

                                  CreateImageUri("Integrals/Volume", "Simple.png"),
                                  CreateImageUri("Integrals/Volume", "Sub.png"),
                                  CreateImageUri("Integrals/Volume", "SubSuper.png"),
                                  CreateImageUri("Integrals/Volume", "Bottom.png"),
                                  CreateImageUri("Integrals/Volume", "BottomTop.png"),

                                  CreateImageUri("Integrals/Clock", "Simple.png"),
                                  CreateImageUri("Integrals/Clock", "Sub.png"),
                                  CreateImageUri("Integrals/Clock", "SubSuper.png"),
                                  CreateImageUri("Integrals/Clock", "Bottom.png"),
                                  CreateImageUri("Integrals/Clock", "BottomTop.png"),

                                  CreateImageUri("Integrals/AntiClock", "Simple.png"),
                                  CreateImageUri("Integrals/AntiClock", "Sub.png"),
                                  CreateImageUri("Integrals/AntiClock", "SubSuper.png"),
                                  CreateImageUri("Integrals/AntiClock", "Bottom.png"),
                                  CreateImageUri("Integrals/AntiClock", "BottomTop.png"),
                               };

            CommandType[] commands = Enumerable.Repeat(CommandType.SignComposite, imageUris.Length).ToArray();

            object[] paramz = { 
                                  new object [] {Position.None,    SignCompositeSymbol.Integral},
                                  new object [] {Position.Sub,       SignCompositeSymbol.Integral},
                                  new object [] {Position.SubAndSuper,  SignCompositeSymbol.Integral},
                                  new object [] {Position.Bottom,    SignCompositeSymbol.Integral},
                                  new object [] {Position.BottomAndTop, SignCompositeSymbol.Integral},
                                  

                                  new object [] {Position.None,    SignCompositeSymbol.DoubleIntegral},
                                  new object [] {Position.Sub,       SignCompositeSymbol.DoubleIntegral},
                                  new object [] {Position.SubAndSuper,  SignCompositeSymbol.DoubleIntegral},
                                  new object [] {Position.Bottom,    SignCompositeSymbol.DoubleIntegral},
                                  new object [] {Position.BottomAndTop, SignCompositeSymbol.DoubleIntegral},
                                  
                                  
                                  new object [] {Position.None,    SignCompositeSymbol.TripleIntegral},
                                  new object [] {Position.Sub,       SignCompositeSymbol.TripleIntegral},
                                  new object [] {Position.SubAndSuper,  SignCompositeSymbol.TripleIntegral},
                                  new object [] {Position.Bottom,    SignCompositeSymbol.TripleIntegral},
                                  new object [] {Position.BottomAndTop, SignCompositeSymbol.TripleIntegral},

                                  new object [] {Position.None,    SignCompositeSymbol.ContourIntegral},
                                  new object [] {Position.Sub,       SignCompositeSymbol.ContourIntegral},
                                  new object [] {Position.SubAndSuper,  SignCompositeSymbol.ContourIntegral},
                                  new object [] {Position.Bottom,    SignCompositeSymbol.ContourIntegral},
                                  new object [] {Position.BottomAndTop, SignCompositeSymbol.ContourIntegral},

                                 new object [] {Position.None,    SignCompositeSymbol.SurfaceIntegral},
                                  new object [] {Position.Sub,       SignCompositeSymbol.SurfaceIntegral},
                                  new object [] {Position.SubAndSuper,  SignCompositeSymbol.SurfaceIntegral},
                                  new object [] {Position.Bottom,    SignCompositeSymbol.SurfaceIntegral},
                                  new object [] {Position.BottomAndTop, SignCompositeSymbol.SurfaceIntegral},

                                  new object [] {Position.None,    SignCompositeSymbol.VolumeIntegral},
                                  new object [] {Position.Sub,       SignCompositeSymbol.VolumeIntegral},
                                  new object [] {Position.SubAndSuper,  SignCompositeSymbol.VolumeIntegral},
                                  new object [] {Position.Bottom,    SignCompositeSymbol.VolumeIntegral},
                                  new object [] {Position.BottomAndTop, SignCompositeSymbol.VolumeIntegral},

                                  new object [] {Position.None,    SignCompositeSymbol.ClockContourIntegral},
                                  new object [] {Position.Sub,       SignCompositeSymbol.ClockContourIntegral},
                                  new object [] {Position.SubAndSuper,  SignCompositeSymbol.ClockContourIntegral},
                                  new object [] {Position.Bottom,    SignCompositeSymbol.ClockContourIntegral},
                                  new object [] {Position.BottomAndTop, SignCompositeSymbol.ClockContourIntegral},

                                  new object [] {Position.None,    SignCompositeSymbol.AntiClockContourIntegral},
                                  new object [] {Position.Sub,       SignCompositeSymbol.AntiClockContourIntegral},
                                  new object [] {Position.SubAndSuper,  SignCompositeSymbol.AntiClockContourIntegral},
                                  new object [] {Position.Bottom,    SignCompositeSymbol.AntiClockContourIntegral},
                                  new object [] {Position.BottomAndTop, SignCompositeSymbol.AntiClockContourIntegral},                                 
                              };

            CreateImagePanel(imageUris, commands, paramz, integralsButton, 5);
        }

        void CreateSubAndSuperPanel()
        {
            Uri[] imageUris = { 
                                  CreateImageUri("SubSuper", "Sub.png"),   
                                  CreateImageUri("SubSuper", "Super.png"),   
                                  CreateImageUri("SubSuper", "SubSuper.png"),   
                                  CreateImageUri("SubSuper", "SubLeft.png"),   
                                  CreateImageUri("SubSuper", "SuperLeft.png"),   
                                  CreateImageUri("SubSuper", "SubSuperLeft.png"),   
                               };
            CommandType[] commands = { CommandType.Sub, CommandType.Super, CommandType.SubAndSuper,
                                       CommandType.Sub, CommandType.Super, CommandType.SubAndSuper};

            object[] paramz = { Position.Right, Position.Right, Position.Right,
                                Position.Left, Position.Left, Position.Left,};

            CreateImagePanel(imageUris, commands, paramz, subAndSuperButton, 3);
        }

        void CreateCompositePanel()
        {
            Uri[] imageUris = { 
                                  CreateImageUri("Composite", "CompositeBottom.png"),  
                                  CreateImageUri("Composite", "CompositeTop.png"),  
                                  CreateImageUri("Composite", "CompositeBottomTop.png"),                            
                                  CreateImageUri("Composite", "BigBottom.png"),  
                                  CreateImageUri("Composite", "BigTop.png"),  
                                  CreateImageUri("Composite", "BigBottomTop.png"),                            
                                  CreateImageUri("Composite", "BigSub.png"),  
                                  CreateImageUri("Composite", "BigSuper.png"),  
                                  CreateImageUri("Composite", "BigSubSuper.png"),                        
                               };
            CommandType[] commands = { 
                                         CommandType.Composite, CommandType.Composite, CommandType.Composite,
                                         CommandType.CompositeBig,    CommandType.CompositeBig, CommandType.CompositeBig, 
                                         CommandType.CompositeBig, CommandType.CompositeBig, CommandType.CompositeBig,                                          
                                     };

            object[] paramz = { 
                                  Position.Bottom, Position.Top, Position.BottomAndTop,
                                  Position.Bottom, Position.Top, Position.BottomAndTop,
                                  Position.Sub, Position.Super, Position.SubAndSuper,
                              };

            CreateImagePanel(imageUris, commands, paramz, compositeButton, 3);
        }

        void CreateDecoratedEquationPanel()
        {
            Uri[] imageUris = { 
                                  CreateImageUri("Decorated/Equation", "hat.png"),  
                                  CreateImageUri("Decorated/Equation", "tilde.png"),  
                                  CreateImageUri("Decorated/Equation", "parenthesis.png"),                            
                                  CreateImageUri("Decorated/Equation", "tortoise.png"),  
                                  CreateImageUri("Decorated/Equation", "topBar.png"),  
                                  CreateImageUri("Decorated/Equation", "topDoubleBar.png"),                            
                                  CreateImageUri("Decorated/Equation", "topRightArrow.png"),  
                                  CreateImageUri("Decorated/Equation", "topLeftArrow.png"),  
                                  CreateImageUri("Decorated/Equation", "topRightHalfArrow.png"),                            
                                  CreateImageUri("Decorated/Equation", "topLeftHalfArrow.png"),  
                                  CreateImageUri("Decorated/Equation", "topDoubleArrow.png"),  
                                    
                                  CreateImageUri("Decorated/Equation", "topDoubleArrow.png"),  //to be left empty

                                  CreateImageUri("Decorated/Equation", "bottomBar.png"),                            
                                  CreateImageUri("Decorated/Equation", "bottomDoubleBar.png"),  
                                  CreateImageUri("Decorated/Equation", "bottomRightArrow.png"),  
                                  CreateImageUri("Decorated/Equation", "bottomLeftArrow.png"),  
                                  CreateImageUri("Decorated/Equation", "bottomRightHalfArrow.png"),  
                                  CreateImageUri("Decorated/Equation", "bottomLeftHalfArrow.png"),  
                                  CreateImageUri("Decorated/Equation", "bottomDoubleArrow.png"),                 
           
                                  CreateImageUri("Decorated/Equation", "bottomDoubleArrow.png"),  //to be left empty
                                  
                                  CreateImageUri("Decorated/Equation", "cross.png"),  
                                  CreateImageUri("Decorated/Equation", "leftCross.png"),  
                                  CreateImageUri("Decorated/Equation", "rightCross.png"),                            
                                  CreateImageUri("Decorated/Equation", "strikeThrough.png"),  
                               };
            CommandType[] commands = Enumerable.Repeat(CommandType.Decorated, imageUris.Length).ToArray();
            commands[11] = CommandType.None; //empty cell
            commands[19] = CommandType.None; //empty cell

            object[] paramz = {                                   
                                  new object [] {DecorationType.Hat,                    Position.Top },
                                  new object [] {DecorationType.Tilde,                  Position.Top },
                                  new object [] {DecorationType.Parenthesis,            Position.Top },
                                  new object [] {DecorationType.Tortoise,               Position.Top },
                                  new object [] {DecorationType.Bar,                    Position.Top },
                                  new object [] {DecorationType.DoubleBar,              Position.Top },
                                  new object [] {DecorationType.RightArrow,             Position.Top },
                                  new object [] {DecorationType.LeftArrow,              Position.Top },
                                  new object [] {DecorationType.RightHarpoonUpBarb,     Position.Top },
                                  new object [] {DecorationType.LeftHarpoonUpBarb,      Position.Top },
                                  new object [] {DecorationType.DoubleArrow,            Position.Top },
                                  0, //empty cell                                  
                                  new object [] {DecorationType.Bar,                    Position.Bottom },
                                  new object [] {DecorationType.DoubleBar,              Position.Bottom },
                                  new object [] {DecorationType.RightArrow,             Position.Bottom },
                                  new object [] {DecorationType.LeftArrow,              Position.Bottom },
                                  new object [] {DecorationType.RightHarpoonDownBarb,   Position.Bottom },
                                  new object [] {DecorationType.LeftHarpoonDownBarb,    Position.Bottom },
                                  new object [] {DecorationType.DoubleArrow,            Position.Bottom },
                                  0, //empty cell
                                  new object [] {DecorationType.Cross,          Position.Middle },
                                  new object [] {DecorationType.LeftCross,      Position.Middle },
                                  new object [] {DecorationType.RightCross,     Position.Middle },
                                  new object [] {DecorationType.StrikeThrough,  Position.Middle },
                              };
            CreateImagePanel(imageUris, commands, paramz, decoratedEquationButton, 4);
        }
        
        void CreateDecoratedCharacterPanel()
        {
            Uri[] imageUris = {   
                                  CreateImageUri("Decorated/Character", "None.png"),
                                  CreateImageUri("Decorated/Character", "StrikeThrough.png"),
                                  CreateImageUri("Decorated/Character", "DoubleStrikeThrough.png"),                                  
                                  CreateImageUri("Decorated/Character", "LeftCross.png"), 
                                  CreateImageUri("Decorated/Character", "RightCross.png"),         
                                  CreateImageUri("Decorated/Character", "Cross.png"),
                                  CreateImageUri("Decorated/Character", "VstrikeThrough.png"),
                                  CreateImageUri("Decorated/Character", "VDoubleStrikeThrough.png"), 
                                  CreateImageUri("Decorated/Character", "LeftUprightCross.png"), 
                                  CreateImageUri("Decorated/Character", "RightUprightCross.png"), 
                                  
                                  CreateImageUri("Decorated/Character", "Prime.png"), 
                                  CreateImageUri("Decorated/Character", "DoublePrime.png"),                                   
                                  CreateImageUri("Decorated/Character", "TriplePrime.png"),
                                  CreateImageUri("Decorated/Character", "ReversePrime.png"),
                                  CreateImageUri("Decorated/Character", "ReverseDoublePrime.png"),

                                  CreateImageUri("Decorated/Character", "AcuteAccent.png"), 
                                  CreateImageUri("Decorated/Character", "GraveAccent.png"),                                   
                                  CreateImageUri("Decorated/Character", "TopRing.png"),
                                  CreateImageUri("Decorated/Character", "TopRightRing.png"),
                                  CreateImageUri("Decorated/Character", "ReverseDoublePrime.png"), //Empty

                                  CreateImageUri("Decorated/Character", "TopBar.png"), 
                                  CreateImageUri("Decorated/Character", "TopTilde.png"),                                   
                                  CreateImageUri("Decorated/Character", "TopBreve.png"),
                                  CreateImageUri("Decorated/Character", "TopInvertedBreve.png"),
                                  CreateImageUri("Decorated/Character", "TopCircumflex.png"),

                                  CreateImageUri("Decorated/Character", "BottomBar.png"), 
                                  CreateImageUri("Decorated/Character", "BottomTilde.png"),                                   
                                  CreateImageUri("Decorated/Character", "BottomBreve.png"),
                                  CreateImageUri("Decorated/Character", "BottomInvertedBreve.png"),
                                  CreateImageUri("Decorated/Character", "TopCaron.png"),

                                  CreateImageUri("Decorated/Character", "TopRightArrow.png"), 
                                  CreateImageUri("Decorated/Character", "TopLeftArrow.png"),                                   
                                  CreateImageUri("Decorated/Character", "TopDoubleArrow.png"),
                                  CreateImageUri("Decorated/Character", "TopRightHarpoon.png"),
                                  CreateImageUri("Decorated/Character", "TopLeftHarpoon.png"),


                                  CreateImageUri("Decorated/Character", "BottomRightArrow.png"), 
                                  CreateImageUri("Decorated/Character", "BottomLeftArrow.png"),                                   
                                  CreateImageUri("Decorated/Character", "BottomDoubleArrow.png"),
                                  CreateImageUri("Decorated/Character", "BottomRightHarpoon.png"),
                                  CreateImageUri("Decorated/Character", "BottomLeftHarpoon.png"),

                                  CreateImageUri("Decorated/Character", "TopDot.png"), 
                                  CreateImageUri("Decorated/Character", "TopDDot.png"),                                   
                                  CreateImageUri("Decorated/Character", "TopTDot.png"),
                                  CreateImageUri("Decorated/Character", "TopFourDot.png"),
                                  CreateImageUri("Decorated/Character", "TopFourDot.png"), //Empty
                                  
                                  CreateImageUri("Decorated/Character", "BottomDot.png"), 
                                  CreateImageUri("Decorated/Character", "BottomDDot.png"),                                   
                                  CreateImageUri("Decorated/Character", "BottomTDot.png"),
                                  CreateImageUri("Decorated/Character", "BottomFourDot.png"),
                                  CreateImageUri("Decorated/Character", "BottomFourDot.png"), //Empty
                                                                  
                               };
            CommandType[] commands = Enumerable.Repeat(CommandType.DecoratedCharacter, imageUris.Length).ToArray();
            commands[19] = CommandType.None; //empty cell 
            commands[44] = CommandType.None; //empty cell           
            commands[49] = CommandType.None; //empty cell  

            object[] paramz = {                                   
                                  new object [] {CharacterDecorationType.None,                  Position.Over, null},
                                  new object [] {CharacterDecorationType.StrikeThrough,         Position.Over, null},
                                  new object [] {CharacterDecorationType.DoubleStrikeThrough,   Position.Over, null},
                                  new object [] {CharacterDecorationType.LeftCross,             Position.Over, null},
                                  new object [] {CharacterDecorationType.RightCross,            Position.Over, null},
                                  new object [] {CharacterDecorationType.Cross,                 Position.Over, null},
                                  new object [] {CharacterDecorationType.VStrikeThrough,        Position.Over, null},
                                  new object [] {CharacterDecorationType.VDoubleStrikeThrough,  Position.Over, null},  
                                  new object [] {CharacterDecorationType.LeftUprightCross,      Position.Over, null},
                                  new object [] {CharacterDecorationType.RightUprightCross,     Position.Over, null},  
                                  
                                  new object [] {CharacterDecorationType.Unicode, Position.TopRight,  "\u2032"}, //Prime
                                  new object [] {CharacterDecorationType.Unicode, Position.TopRight,  "\u2033"}, //Double prime
                                  new object [] {CharacterDecorationType.Unicode, Position.TopRight,  "\u2034"}, //Triple prime
                                  new object [] {CharacterDecorationType.Unicode, Position.TopLeft,   "\u2035"}, //Reversed prime
                                  new object [] {CharacterDecorationType.Unicode, Position.TopLeft,   "\u2036"}, //Double reversed prime

                                  new object [] {CharacterDecorationType.Unicode, Position.Top,  "\u02CA"}, // Acute
                                  new object [] {CharacterDecorationType.Unicode, Position.Top,  "\u02CB"}, //Grave
                                  new object [] {CharacterDecorationType.Unicode, Position.Top,  "\u030A"}, //Ring
                                  new object [] {CharacterDecorationType.Unicode, Position.TopRight,  "\u030A"}, //Ring
                                  0, //Empty
                                  
                                  new object [] {CharacterDecorationType.Unicode, Position.Top,  "\u0332"}, //Bar or line
                                  new object [] {CharacterDecorationType.Unicode, Position.Top,  "\u0334"}, //Tilde
                                  new object [] {CharacterDecorationType.Unicode, Position.Top, "\u0306"}, //Breve
                                  new object [] {CharacterDecorationType.Unicode, Position.Top, "\u0311"}, //Inverted Breve
                                  new object [] {CharacterDecorationType.Unicode, Position.Top, "\u02C6"}, //Circumflex

                                  new object [] {CharacterDecorationType.Unicode, Position.Bottom, "\u0332"}, //Bar or line
                                  new object [] {CharacterDecorationType.Unicode, Position.Bottom, "\u0334"}, //Tilde
                                  new object [] {CharacterDecorationType.Unicode, Position.Bottom, "\u0306"}, //Breve
                                  new object [] {CharacterDecorationType.Unicode, Position.Bottom, "\u0311"}, //Inverted breve
                                  new object [] {CharacterDecorationType.Unicode, Position.Top, "\u02C7"}, //Caron or check

                                  new object [] {CharacterDecorationType.Unicode, Position.Top, "\u20D7"}, //left arrow
                                  new object [] {CharacterDecorationType.Unicode, Position.Top, "\u20D6"}, //right arrow
                                  new object [] {CharacterDecorationType.Unicode, Position.Top, "\u20E1"}, //double arrow
                                  new object [] {CharacterDecorationType.Unicode, Position.Top, "\u20D1"}, //top right harpoon
                                  new object [] {CharacterDecorationType.Unicode, Position.Top, "\u20D0"}, //top left harpoon

                                  new object [] {CharacterDecorationType.Unicode, Position.Bottom, "\u20D7"}, //left arrow
                                  new object [] {CharacterDecorationType.Unicode, Position.Bottom, "\u20D6"}, //right arrow
                                  new object [] {CharacterDecorationType.Unicode, Position.Bottom, "\u20E1"}, //double arrow
                                  new object [] {CharacterDecorationType.Unicode, Position.Bottom, "\u20EC"}, //bottom right harpoon
                                  new object [] {CharacterDecorationType.Unicode, Position.Bottom, "\u20ED"}, //bottom left harpoon

                                  new object [] {CharacterDecorationType.Unicode, Position.Top, "\u0323"},  //dot
                                  new object [] {CharacterDecorationType.Unicode, Position.Top, "\u0324"},  //two dots
                                  new object [] {CharacterDecorationType.Unicode, Position.Top, "\u20DB" }, //three dots
                                  new object [] {CharacterDecorationType.Unicode, Position.Top, "\u20DC" }, //four dots
                                  0, //Empty
                                  new object [] {CharacterDecorationType.Unicode, Position.Bottom, "\u0323"},  //dot
                                  new object [] {CharacterDecorationType.Unicode, Position.Bottom, "\u0324"},  //two dots
                                  new object [] {CharacterDecorationType.Unicode, Position.Bottom, "\u20DB" }, //three dots
                                  new object [] {CharacterDecorationType.Unicode, Position.Bottom, "\u20DC" }, //four dots
                                  0, //Empty
                              };
            CreateImagePanel(imageUris, commands, paramz, decoratedCharacterButton, 5);
        }

        void CreateArrowEquationPanel()
        {
            Uri[] imageUris = {                                                                                                 
                                  CreateImageUri("Decorated/Arrow", "LeftTop.png"),                            
                                  CreateImageUri("Decorated/Arrow", "LeftBottom.png"),                            
                                  CreateImageUri("Decorated/Arrow", "LeftBottomTop.png"),    

                                  CreateImageUri("Decorated/Arrow", "RightTop.png"),                            
                                  CreateImageUri("Decorated/Arrow", "RightBottom.png"),                            
                                  CreateImageUri("Decorated/Arrow", "RightBottomTop.png"),    

                                  CreateImageUri("Decorated/Arrow", "DoubleTop.png"),                            
                                  CreateImageUri("Decorated/Arrow", "DoubleBottom.png"),                            
                                  CreateImageUri("Decorated/Arrow", "DoubleBottomTop.png"),    

                                  CreateImageUri("Decorated/Arrow", "RightLeftTop.png"),                            
                                  CreateImageUri("Decorated/Arrow", "RightLeftBottom.png"),                            
                                  CreateImageUri("Decorated/Arrow", "RightLeftBottomTop.png"),    

                                  CreateImageUri("Decorated/Arrow", "RightSmallLeftTop.png"),                            
                                  CreateImageUri("Decorated/Arrow", "RightSmallLeftBottom.png"),                            
                                  CreateImageUri("Decorated/Arrow", "RightSmallLeftBottomTop.png"),    

                                  CreateImageUri("Decorated/Arrow", "SmallRightLeftTop.png"),                            
                                  CreateImageUri("Decorated/Arrow", "SmallRightLeftBottom.png"),                            
                                  CreateImageUri("Decorated/Arrow", "SmallRightLeftBottomTop.png"),

                                  CreateImageUri("Decorated/Arrow", "RightLeftHarpTop.png"),                            
                                  CreateImageUri("Decorated/Arrow", "RightLeftHarpBottom.png"),                            
                                  CreateImageUri("Decorated/Arrow", "RightLeftHarpBottomTop.png"),    

                                  CreateImageUri("Decorated/Arrow", "RightSmallLeftHarpTop.png"),                            
                                  CreateImageUri("Decorated/Arrow", "RightSmallLeftHarpBottom.png"),                            
                                  CreateImageUri("Decorated/Arrow", "RightSmallLeftHarpBottomTop.png"),    

                                  CreateImageUri("Decorated/Arrow", "SmallRightLeftHarpTop.png"),                            
                                  CreateImageUri("Decorated/Arrow", "SmallRightLeftHarpBottom.png"),                            
                                  CreateImageUri("Decorated/Arrow", "SmallRightLeftHarpBottomTop.png"),
                               };
            CommandType[] commands = Enumerable.Repeat(CommandType.Arrow, imageUris.Length).ToArray();

            object[] paramz = {   
                                  new object [] {ArrowType.LeftArrow,               Position.Top },
                                  new object [] {ArrowType.LeftArrow,               Position.Bottom },
                                  new object [] {ArrowType.LeftArrow,               Position.BottomAndTop },

                                  new object [] {ArrowType.RightArrow,              Position.Top },
                                  new object [] {ArrowType.RightArrow,              Position.Bottom },
                                  new object [] {ArrowType.RightArrow,              Position.BottomAndTop },

                                  
                                  new object [] {ArrowType.DoubleArrow,             Position.Top },
                                  new object [] {ArrowType.DoubleArrow,             Position.Bottom },
                                  new object [] {ArrowType.DoubleArrow,             Position.BottomAndTop },

                                  
                                  new object [] {ArrowType.RightLeftArrow,          Position.Top },
                                  new object [] {ArrowType.RightLeftArrow,          Position.Bottom },
                                  new object [] {ArrowType.RightLeftArrow,          Position.BottomAndTop },

                                  
                                  new object [] {ArrowType.RightSmallLeftArrow,     Position.Top },
                                  new object [] {ArrowType.RightSmallLeftArrow,     Position.Bottom },
                                  new object [] {ArrowType.RightSmallLeftArrow,     Position.BottomAndTop },

                                  
                                  new object [] {ArrowType.SmallRightLeftArrow,     Position.Top },
                                  new object [] {ArrowType.SmallRightLeftArrow,     Position.Bottom },
                                  new object [] {ArrowType.SmallRightLeftArrow,     Position.BottomAndTop },

                                  
                                  new object [] {ArrowType.RightLeftHarpoon,        Position.Top },
                                  new object [] {ArrowType.RightLeftHarpoon,        Position.Bottom },
                                  new object [] {ArrowType.RightLeftHarpoon,        Position.BottomAndTop },

                                  
                                  new object [] {ArrowType.RightSmallLeftHarpoon,     Position.Top },
                                  new object [] {ArrowType.RightSmallLeftHarpoon,     Position.Bottom },
                                  new object [] {ArrowType.RightSmallLeftHarpoon,     Position.BottomAndTop },

                                  
                                  new object [] {ArrowType.SmallRightLeftHarpoon,    Position.Top },
                                  new object [] {ArrowType.SmallRightLeftHarpoon,    Position.Bottom },
                                  new object [] {ArrowType.SmallRightLeftHarpoon,    Position.BottomAndTop },

                              };
            CreateImagePanel(imageUris, commands, paramz, arrowEquationButton, 3);
        }
        
        void CreateDivAndRootsPanel()
        {
            Uri[] imageUris = { 
                                  CreateImageUri("DivAndRoots", "SqRoot.png"),  
                                  CreateImageUri("DivAndRoots", "nRoot.png"),  
                                  CreateImageUri("DivAndRoots", "DivMath.png"),  
                                  CreateImageUri("DivAndRoots", "DivMathWithTop.png"),  

                                  CreateImageUri("DivAndRoots", "Div.png"),  
                                  CreateImageUri("DivAndRoots", "DivDoubleBar.png"),  
                                  CreateImageUri("DivAndRoots", "DivTripleBar.png"),
                                  CreateImageUri("DivAndRoots", "SmallDiv.png"),

                                  CreateImageUri("DivAndRoots", "DivSlant.png"),  
                                  CreateImageUri("DivAndRoots", "SmallDivSlant.png"),
                                  CreateImageUri("DivAndRoots", "DivHoriz.png"),
                                  CreateImageUri("DivAndRoots", "SmallDivHoriz.png"),

                                  CreateImageUri("DivAndRoots", "DivMathInverted.png"),  
                                  CreateImageUri("DivAndRoots", "DivMathInvertedWithBottom.png"),
                                  CreateImageUri("DivAndRoots", "DivTriangleFixed.png"),
                                  CreateImageUri("DivAndRoots", "DivTriangleExpanding.png"),
                               };
            CommandType[] commands = { 
                                         CommandType.SquareRoot, CommandType.nRoot, 
                                         CommandType.Division, CommandType.Division, CommandType.Division,
                                         CommandType.Division, CommandType.Division, CommandType.Division,
                                         CommandType.Division, CommandType.Division, CommandType.Division,
                                         CommandType.Division, CommandType.Division, CommandType.Division,
                                          CommandType.Division, CommandType.Division,
                                     };
            object[] paramz = { 
                                  0, 0, //square root and nRoot
                                  DivisionType.DivMath, DivisionType.DivMathWithTop,
                                  DivisionType.DivRegular, DivisionType.DivDoubleBar, DivisionType.DivTripleBar,
                                  DivisionType.DivRegularSmall, DivisionType.DivSlanted, DivisionType.DivSlantedSmall, 
                                  DivisionType.DivHoriz, DivisionType.DivHorizSmall, DivisionType.DivMathInverted,
                                  DivisionType.DivInvertedWithBottom, DivisionType.DivTriangleFixed,
                                  DivisionType.DivTriangleExpanding,
                              };
            CreateImagePanel(imageUris, commands, paramz, divAndRootsButton, 4);
        }

        void CreateBoxEquationPanel()
        {
            Uri[] imageUris = { 
                                  CreateImageUri("Box", "leftTop.png"),  
                                  CreateImageUri("Box", "leftBottom.png"),  
                                  CreateImageUri("Box", "rightTop.png"),  
                                  CreateImageUri("Box", "rightBottom.png"),  
                                  CreateImageUri("Box", "all.png"),
                               };
            CommandType[] commands = Enumerable.Repeat<CommandType>(CommandType.Box, imageUris.Length).ToArray();
            object[] paramz = { BoxType.LeftTop, BoxType.LeftBottom, BoxType.RightTop, BoxType.RightBottom, BoxType.All };
            CreateImagePanel(imageUris, commands, paramz, boxButton, 2);
        }

        void CreateMatrixPanel()
        {
            Uri[] imageUris = { 
                                  CreateImageUri("Matrix", "2cellRow.png"),  
                                  CreateImageUri("Matrix", "2cellColumn.png"),  
                                  CreateImageUri("Matrix", "2Square.png"),

                                  CreateImageUri("Matrix", "3cellRow.png"),  
                                  CreateImageUri("Matrix", "3cellColumn.png"),  
                                  CreateImageUri("Matrix", "3Square.png"),
                                  
                                  CreateImageUri("Matrix", "row.png"),
                                  CreateImageUri("Matrix", "column.png"),
                                  CreateImageUri("Matrix", "custom.png"),
                               };
            CommandType[] commands = Enumerable.Repeat<CommandType>(CommandType.Matrix, imageUris.Length).ToArray();
            commands[6] = CommandType.CustomMatrix;
            commands[7] = CommandType.CustomMatrix;
            commands[8] = CommandType.CustomMatrix;
            object[] paramz = {
                                  new [] {1, 2},
                                  new [] {2, 1},
                                  new [] {2, 2},
                                  new [] {1, 3},
                                  new [] {3, 1},
                                  new [] {3, 3},
                                  new [] {1, 4},
                                  new [] {4, 1},
                                  new [] {4, 4},
                              };
            CreateImagePanel(imageUris, commands, paramz, matrixButton, 3);
        }
    }
}
