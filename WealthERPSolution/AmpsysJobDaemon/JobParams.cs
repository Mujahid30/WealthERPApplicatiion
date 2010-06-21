using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;

namespace AmpsysJobDaemon
{
    class JobParams
    {
        private Hashtable _Params = new Hashtable();

        public void Init(string Params)
        {
            if (Params != "")
            {
                XmlDocument XD = new XmlDocument();
                XD.LoadXml(Params);

                XmlNodeList XNL = XD.SelectNodes("params/param");

                foreach (XmlNode XN in XNL)
                {
                    _Params.Add(XN.Attributes["name"].Value, XN.Attributes["value"].Value);
                }
            }
        }

        public void AddParameter(string Name, string Value)
        {
            if (_Params.ContainsKey(Name))
                _Params.Remove(Name);

            _Params.Add(Name, Value);
        }

        public string GetXML()
        {
            XmlDocument XD = new XmlDocument();

            XmlElement XE = XD.CreateElement("params");
            XmlNode XN = XD.AppendChild(XE);

            foreach (string Name in _Params.Keys)
            {
                string Value = _Params[Name].ToString();

                XmlElement XEParam = XD.CreateElement("param");
                XmlAttribute XAName = XD.CreateAttribute("name");
                XAName.Value = Name;
                XEParam.Attributes.Append(XAName);

                XmlAttribute XAValue = XD.CreateAttribute("value");
                XAValue.Value = Value;
                XEParam.Attributes.Append(XAValue);

                XN.AppendChild(XEParam);
            }

            return XD.OuterXml;
        }

        public string GetParam(string Name)
        {
            if (_Params.ContainsKey(Name))
                return _Params[Name].ToString();

            return "";
        }
    }
}
