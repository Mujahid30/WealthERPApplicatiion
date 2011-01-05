using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFFormFilling
{
    public class TestClass:ICloneable
    {
        private string m_Name;

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        private int m_Age;

        public int Age
        {
            get { return m_Age; }
            set { m_Age = value; }
        }

        public TestClass Clone()
        {
            TestClass testClass = new TestClass();
            testClass.Name = m_Name;
            testClass.Age = m_Age;
            return testClass;
        }
        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }

}
