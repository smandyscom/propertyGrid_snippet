using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Design;
namespace grid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = new MyClass();
        }
    }

    [TypeConverter(typeof(optionConvertor))]
    enum MyEnum
    {
        OPTION1,
        OPTION2,
    }

    class MyClass
    {
        [Description("The input value range is 16-19")] //offered the description which would showed on the bar below
        [Category("Catagory A")]            // offered the folded categoring method
        [DisplayName("Integer Parameter")]  // offered the customised display name
        [ReadOnly(true)]
        public int Property1 
        {
            get
            {
                return __innerVariableInteger;
            }
            set 
            {
                // constrained range
                if (value > 10 && value < 20)
                    __innerVariableInteger = value;
            }
        }

        [Browsable(true)]  //wont be showed on PropertyGrid
        [EditorAttribute(typeof(stringUIEditor),typeof(UITypeEditor))]
        public string Property2
        {
            get
            {
                return __innerVariableString;
            }
            set
            {
                __innerVariableString = value;
            }
        }

        [DisplayName("想要選什麼？")] // much more human-readable?
        [TypeConverter(typeof(optionConvertor))]
        public MyEnum Property3 { get; set; }

        protected string __innerVariableString = "";
        protected int __innerVariableInteger = 15;

    }


    class stringUIEditor :
        UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Form __form = new Form();       // allocating a form , used to show-up editable facility 
            TextBox __tb = new TextBox();
            __form.Controls.Add(__tb);
            __form.AutoSize=true;
            __form.ShowDialog(); // show-up the form 
            return __tb.Text; //return the text value the user just input.
        }
    }


    class optionConvertor :
        EnumConverter
    {
        /// <summary>
        /// Convert From String To Enum Type
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            switch (value.ToString())
            {
                case "選項1":
                    return MyEnum.OPTION1;
                case "選項2":
                    return MyEnum.OPTION2;
                default:
                    return null;
            }
        }
        /// <summary>
        /// Convert From Enum Type To String Type
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            switch ((MyEnum)value)
            {
                case MyEnum.OPTION1:
                    return "選項1";
                case MyEnum.OPTION2:
                    return "選項2";
                default:
                    return null;
            }
        }

       public optionConvertor(Type type):base(type)
       {
       }
    }
}
