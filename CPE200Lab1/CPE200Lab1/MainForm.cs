﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        private bool hasDot;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterEqual;
        private bool isMemOn;
        private bool isAfterMem;
        private string firstOperand;
        private string operate;
        private string preOperate;
        private string memStore;
        private string memSign;
        private CalculatorEngine engine;
        

        private void resetAll()
        {
            lblDisplay.Text = "0";
            isAllowBack = true;
            hasDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
            isAfterMem = false;
        }

        

        public MainForm()
        {
            InitializeComponent();

            resetAll();
            engine = new CalculatorEngine();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (isAfterOperater || isAfterMem)
            {
                lblDisplay.Text = "0";
            }
            if(lblDisplay.Text.Length is 8)
            {
                return;
            }
            isAllowBack = true;
            string digit = ((Button)sender).Text;
            if(lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            isAfterOperater = false;
            isAfterMem = false;
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                return;
            }
            operate = ((Button)sender).Text;
            switch (operate)
            {
                case "+":
                case "-":
                case "X":
                case "÷":
                    preOperate = operate;
                    firstOperand = lblDisplay.Text;
                    isAfterOperater = true;
                    break;
                case "%":

                    lblDisplay.Text = engine.calculate(operate, firstOperand, lblDisplay.Text);
                    
                    operate = preOperate;
                    // your code here
                    break;
                case "√":
                    firstOperand = lblDisplay.Text;
                    
                    break;
                case "1/x":
                    firstOperand = lblDisplay.Text;

                    break;
            }
            isAllowBack = false;
            
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            string secondOperand = lblDisplay.Text;
            string result = engine.calculate(operate, firstOperand, secondOperand);
            if (result is "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
            }
            isAfterEqual = true;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (!hasDot)
            {
                lblDisplay.Text += ".";
                hasDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if(lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            } else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if(lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if(rightMost is '.')
                {
                    hasDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if(lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }

        private void btnMem_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterMem)
            {
                return;
            }
            memSign = ((Button)sender).Text;
            switch (memSign)
            {
                case "MC":
                    if (isMemOn)
                    {
                        memStore = "0";
                    }
                    isMemOn = false;
                    isAfterOperater = true;
                    break;
                case "MR":
                    if (isMemOn)
                    {
                        lblDisplay.Text = memStore;
                    }
                    isAfterMem = true;
                    
                    break;
                case "MS":
                    memStore = lblDisplay.Text;
                    isMemOn = true;
                    isAfterOperater = true;
                    break;

                case "M+":
                    operate = "+";
                    firstOperand = lblDisplay.Text;
                    
                    memStore = engine.calculate(operate, firstOperand, memStore);
                    isMemOn = true;
                    isAfterOperater = true;
                    break;
                case "M-":
                    operate = "-";
                    firstOperand = lblDisplay.Text;
                    memStore = engine.calculate(operate, memStore, firstOperand);
                    isMemOn = true;
                    isAfterOperater = true;
                    break;
            }
            
            
        }
    }
}
