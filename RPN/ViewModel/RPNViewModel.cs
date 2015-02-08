using Codeplex.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RPN.ViewModel
{
    public class RPNViewModel
    {
        public ReactiveProperty<string> Input { get; set; }
        public ReactiveProperty<float> Output { get; set; }
        public ReactiveCommand AddChar { get; private set; }

        private RPN.Model.RPNHaskell rpnModel { get; set; }

        public RPNViewModel()
        {
            Input = new ReactiveProperty<string>();
            Output = new ReactiveProperty<float>();

            AddChar = new ReactiveCommand();
            AddChar.Subscribe(o => addChar(((Button)o).Content as string));

            rpnModel = new Model.RPNHaskell();

            Input.Value = "10 4 3 + 2 * -";
            Input.Subscribe(s => Output.Value = rpnModel.solveRPN(s));
        }

        private void addChar(string str)
        {
            // TODO: Cover all of buttons
            if (str == "3")
            {
                Input.Value += " 3";
            }
            if (str == "+")
            {
                Input.Value += " +";
            }
        }
    }
}
