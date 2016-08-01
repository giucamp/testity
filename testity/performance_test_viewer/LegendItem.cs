
//   Copyright Giuseppe Campana (giu.campana@gmail.com) 2016.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace performance_test_viewer
{
    public partial class LegendItem : UserControl
    {
        public LegendItem()
        {
            InitializeComponent();
        }

        public Color Color
        {
            get { return lblColorBox.BackColor; }
            set { lblColorBox.BackColor = value; }
        }

        public string SourceCode
        {
            get { return txtSourceCode.Text; }
            set
            {
                string code = value;
                code = code.Replace("    ", "  ");
                code = code.Replace("   ", "  ");
                code = code.Replace("\t", "  ");
                code = code.Replace("#nl#", "\r\n");
                txtSourceCode.Text = code;
            }
        }

        public string Percentage
        {
            get { return lblPercentage.Text; }
            set { lblPercentage.Text = value; }
        }
    }
}
