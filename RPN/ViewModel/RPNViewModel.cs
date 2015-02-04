using Codeplex.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.ViewModel
{
    public class RPNViewModel
    {
        public ReactiveProperty<string> Input { get; set; }
        public ReactiveProperty<float> Output { get; set; }

        private RPN.Model.RPNHaskell rpnModel { get; set; }

        public RPNViewModel()
        {
            this.Input = new ReactiveProperty<string>();
            this.Output = new ReactiveProperty<float>();

            this.rpnModel = new Model.RPNHaskell();
            //var res = rpnModel.solveRPN("10 4 3 + 2 * -");
            //this.text.Text = res.ToString();

            this.Input.Value = "10 4 3 + 2 * -";
            this.Input.Subscribe(s => this.Output.Value = this.rpnModel.solveRPN(s));
        }
    }
}
