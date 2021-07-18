using NUnit.Framework;
using System.Collections.Generic;

namespace PacManDiv.UnitTests
{
    [TestFixture()]
    public class PacManTests
    {
        public List<PacManStackLayout> AllStacks { get; set; }
        public MainViewModel ViewModel { get; set; }

        [OneTimeSetUp]
        public void SetAllStacks()
        {
            AllStacks = new List<PacManStackLayout>();
            int rowMax = 6;
            int colMax = 6;

            for (int row = 0; row < rowMax; row++)
            {
                for (int col = 0; col < colMax; col++)
                {
                    PacManStackLayout pacStack = new PacManStackLayout()
                    {
                        HeightRequest = 10,
                        WidthRequest = 10,
                        Row = row,
                        Col = col
                    };
                    AllStacks.Add(pacStack);
                }
            }
        }

        [SetUp]
        public void Setup()
        {
            ViewModel = new MainViewModel();
        }

        [TestCase]
        public void Test_If_PacMan_Placed_Is_Valid()
        {
            ViewModel.PacManPlaced = true;
            var result = ViewModel.Validate();
            Assert.IsTrue(result);
        }

        [TestCase(0)]
        [TestCase(360)]
        public void Test_If_PacMan_Stack_Tapped_Place_11_Output_11(double angle)
        {
            ViewModel.CurrentAngle = angle;
            ViewModel.StackTapCommand.Execute(new PacManStackLayout() { Row = 1, Col = 1 });
            var outputText = ViewModel.ReportText;
            Assert.AreEqual("Output: 1, 1, East", outputText);
        }

        [TestCase(-270)]
        [TestCase(90)]
        public void Test_If_PacMan_Stack_Tapped_Place_11_Rotate_South(double angle)
        {
            ViewModel.CurrentAngle = angle;
            ViewModel.StackTapCommand.Execute(new PacManStackLayout() { Row = 1, Col = 1 });
            var outputText = ViewModel.ReportText;
            Assert.AreEqual("Output: 1, 1, South", outputText);
        }

        [TestCase(180)]
        [TestCase(-180)]
        public void Test_If_PacMan_Stack_Tapped_Place_11_Rotate_West(double angle)
        {
            ViewModel.CurrentAngle = angle;
            ViewModel.StackTapCommand.Execute(new PacManStackLayout() { Row = 1, Col = 1 });
            var outputText = ViewModel.ReportText;
            Assert.AreEqual("Output: 1, 1, West", outputText);
        }

        [TestCase(270)]
        [TestCase(-90)]
        public void Test_If_PacMan_Stack_Tapped_Place_11_Rotate_North(double angle)
        {
            ViewModel.CurrentAngle = angle;
            ViewModel.StackTapCommand.Execute(new PacManStackLayout() { Row = 1, Col = 1 });
            var outputText = ViewModel.ReportText;
            Assert.AreEqual("Output: 1, 1, North", outputText);
        }

        [TestCase]
        public void Test_If_PacMan_Stack_Tapped_Place_11_Turn_Left_To_Minus_90()
        {
            ViewModel.StackTapCommand.Execute(new PacManStackLayout() { Row = 1, Col = 1 });
            ViewModel.TurnLeftCommand.Execute(null);
            Assert.AreEqual(-90d, ViewModel.CurrentAngle);
        }

        [TestCase]
        public void Test_If_PacMan_Stack_Tapped_Place_11_Turn_Right_To_Plus_90()
        {
            ViewModel.StackTapCommand.Execute(new PacManStackLayout() { Row = 1, Col = 1 });
            ViewModel.TurnRightCommand.Execute(null);
            Assert.AreEqual(90d, ViewModel.CurrentAngle);
        }

        [TestCase]
        public void Test_If_PacMan_Stack_Tapped_Place_11_Turn_Right_Move_One_Step()
        {
            ViewModel.AllStacks = this.AllStacks;
            ViewModel.StackTapCommand.Execute(new PacManStackLayout() { Row = 1, Col = 1 });
            ViewModel.TurnRightCommand.Execute(null);
            ViewModel.MovePacManCommand.Execute(null);
            var outputText = ViewModel.ReportText;
            Assert.AreEqual(90d, ViewModel.CurrentAngle);
            Assert.AreEqual("Output: 2, 1, South", outputText);
        }

        [TestCase]
        public void Test_If_PacMan_Stack_Tapped_Place_33_Turn_Right_Move_One_Step()
        {
            ViewModel.AllStacks = this.AllStacks;
            ViewModel.StackTapCommand.Execute(new PacManStackLayout() { Row = 3, Col = 3 });
            ViewModel.TurnRightCommand.Execute(null);
            ViewModel.MovePacManCommand.Execute(null);
            var outputText = ViewModel.ReportText;
            Assert.AreEqual(90d, ViewModel.CurrentAngle);
            Assert.AreEqual("Output: 4, 3, South", outputText);
        }

        [TestCase]
        public void Test_If_PacMan_Stack_Tapped_Place_11_Turn_Left_Move_One_Step()
        {
            ViewModel.AllStacks = this.AllStacks ;
            ViewModel.StackTapCommand.Execute(new PacManStackLayout() { Row = 1, Col = 1 });
            ViewModel.TurnRightCommand.Execute(null);
            ViewModel.MovePacManCommand.Execute(null);
            var outputText = ViewModel.ReportText;
            Assert.AreEqual(90d, ViewModel.CurrentAngle);
            Assert.AreEqual("Output: 2, 1, South", outputText);
        }

        [TestCase]
        public void Test_If_PacMan_Stack_Tapped_Place_33_Turn_Left_Move_One_Step()
        {
            MainViewModel mainViewModel = new MainViewModel() { AllStacks = this.AllStacks };
            mainViewModel.StackTapCommand.Execute(new PacManStackLayout() { Row = 3, Col = 3 });
            mainViewModel.TurnRightCommand.Execute(null);
            mainViewModel.MovePacManCommand.Execute(null);
            var outputText = mainViewModel.ReportText;
            Assert.AreEqual(90d, mainViewModel.CurrentAngle);
            Assert.AreEqual("Output: 4, 3, South", outputText);
        }

        [TestCase]
        public void Test_If_PacMan_Stack_Tapped_Place_11_Turn_Right_Move_Two_Step()
        {
            ViewModel.AllStacks = this.AllStacks;
            ViewModel.StackTapCommand.Execute(new PacManStackLayout() { Row = 1, Col = 1 });
            ViewModel.TurnRightCommand.Execute(null);
            ViewModel.MovePacManCommand.Execute(null);
            ViewModel.MovePacManCommand.Execute(null);
            var outputText = ViewModel.ReportText;
            Assert.AreEqual(90d, ViewModel.CurrentAngle);
            Assert.AreEqual("Output: 3, 1, South", outputText);
        }

        [TestCase]
        public void Test_If_PacMan_Stack_Tapped_Place_13_Turn_Right_Move_Two_Step()
        {
            ViewModel.AllStacks = this.AllStacks;
            ViewModel.StackTapCommand.Execute(new PacManStackLayout() { Row = 1, Col = 3 });
            ViewModel.TurnRightCommand.Execute(null);
            ViewModel.MovePacManCommand.Execute(null);
            ViewModel.MovePacManCommand.Execute(null);
            var outputText = ViewModel.ReportText;
            Assert.AreEqual(90d, ViewModel.CurrentAngle);
            Assert.AreEqual("Output: 3, 3, South", outputText);
        }

        [TestCase]
        public void Test_If_PacMan_Stack_Tapped_Place_11_Turn_Left_Move_Two_Step()
        {
            ViewModel.AllStacks = this.AllStacks;
            ViewModel.StackTapCommand.Execute(new PacManStackLayout() { Row = 1, Col = 1 });
            ViewModel.TurnRightCommand.Execute(null);
            ViewModel.MovePacManCommand.Execute(null);
            ViewModel.MovePacManCommand.Execute(null);
            var outputText = ViewModel.ReportText;
            Assert.AreEqual(90d, ViewModel.CurrentAngle);
            Assert.AreEqual("Output: 3, 1, South", outputText);
        }

        [TestCase]
        public void Test_If_PacMan_Stack_Tapped_Place_13_Turn_Left_Move_Two_Step()
        {
            ViewModel.AllStacks = this.AllStacks;
            ViewModel.StackTapCommand.Execute(new PacManStackLayout() { Row = 1, Col = 3 });
            ViewModel.TurnRightCommand.Execute(null);
            ViewModel.MovePacManCommand.Execute(null);
            ViewModel.MovePacManCommand.Execute(null);
            var outputText = ViewModel.ReportText;
            Assert.AreEqual(90d, ViewModel.CurrentAngle);
            Assert.AreEqual("Output: 3, 3, South", outputText);
        }

        [TestCase]
        public void Test_Place_00N_Move_00W()
        {
            ViewModel.AllStacks = this.AllStacks;
            ViewModel.PlaceX = 0;
            ViewModel.PlaceY = 0;
            ViewModel.SelectedDirection = "North";
            ViewModel.PlaceCommand.Execute(null);
            ViewModel.TurnLeftCommand.Execute(null);
            Assert.AreEqual("Output: 0, 0, West", ViewModel.ReportText);
        }

        [TestCase]
        public void Test_Place_12E_Move_04N()
        {
            ViewModel.AllStacks = this.AllStacks;
            ViewModel.PlaceX = 1;
            ViewModel.PlaceY = 2;
            ViewModel.SelectedDirection = "East";
            ViewModel.PlaceCommand.Execute(null);

            ViewModel.MovePacManCommand.Execute(null);
            ViewModel.MovePacManCommand.Execute(null);
            ViewModel.TurnLeftCommand.Execute(null);
            ViewModel.MovePacManCommand.Execute(null);

            Assert.AreEqual("Output: 0, 4, North", ViewModel.ReportText);
        }

    }
}