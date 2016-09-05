using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chordgen
{
    public partial class BindableToolStripMenuItem : ToolStripMenuItem, IBindableComponent
    {
        public BindableToolStripMenuItem()
        {
            InitializeComponent();
        }

        public BindableToolStripMenuItem(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        private BindingContext m_bindingContext;
        private ControlBindingsCollection m_dataBindings;
        [Browsable(false)]
        public BindingContext BindingContext
        {
            get
            {
                if (m_bindingContext == null)
                    m_bindingContext = new BindingContext();
                return m_bindingContext;
            }

            set
            {
                m_bindingContext = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlBindingsCollection DataBindings
        {
            get
            {
                if (m_dataBindings == null)
                    m_dataBindings = new ControlBindingsCollection(this);
                return m_dataBindings;
            }
        }
    }
}
