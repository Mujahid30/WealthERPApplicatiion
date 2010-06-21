using System;
using System.Net.Mail;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PCGMailLib
{
    /// <summary>
    /// This class is used to Searlize the mail .. so that if the server is not up we can store msgs in some tem location befor sending them .
    /// Schedular can be plug in any time to ...send sechudule specfic engine as ann when reqired 
    /// </summary>
    public class SerializableMailMessage : IXmlSerializable
    {


        public MailMessage Email { get; set; }


        public SerializableMailMessage()
        {
            this.Email = new MailMessage();
        }


        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(reader);

            // Properties
            XmlNode rootNode = GetConfigSection(xml, "SerializableMailMessage/MailMessage");
            this.Email.IsBodyHtml = Convert.ToBoolean(rootNode.Attributes["IsBodyHtml"].Value);
            this.Email.Priority = (MailPriority)Convert.ToInt16(rootNode.Attributes["Priority"].Value);

            // From
            XmlNode fromNode = GetConfigSection(xml, "SerializableMailMessage/MailMessage/From");
            string fromDisplayName = string.Empty;
            if (fromNode.Attributes["DisplayName"] != null)
                fromDisplayName = fromNode.Attributes["DisplayName"].Value;
            MailAddress fromAddress = new MailAddress(fromNode.InnerText, fromDisplayName);
            this.Email.From = fromAddress;

            // To
            XmlNode toNode = GetConfigSection(xml, "SerializableMailMessage/MailMessage/To/Addresses");
            foreach (XmlNode node in toNode.ChildNodes)
            {
                string toDisplayName = string.Empty;
                if (node.Attributes["DisplayName"] != null)
                    toDisplayName = node.Attributes["DisplayName"].Value;
                MailAddress toAddress = new MailAddress(node.InnerText, toDisplayName);
                this.Email.To.Add(toAddress);
            }

            // CC
            XmlNode ccNode = GetConfigSection(xml, "SerializableMailMessage/MailMessage/CC/Addresses");
            if (ccNode != null)
            {
                foreach (XmlNode node in ccNode.ChildNodes)
                {
                    string ccDisplayName = string.Empty;
                    if (node.Attributes["DisplayName"] != null)
                        ccDisplayName = node.Attributes["DisplayName"].Value;
                    MailAddress ccAddress = new MailAddress(node.InnerText, ccDisplayName);
                    this.Email.CC.Add(ccAddress);
                }
            }

            // Bcc
            XmlNode bccNode = GetConfigSection(xml, "SerializableMailMessage/MailMessage/Bcc/Addresses");
            if (bccNode != null)
            {
                foreach (XmlNode node in bccNode.ChildNodes)
                {
                    string bccDisplayName = string.Empty;
                    if (node.Attributes["DisplayName"] != null)
                        bccDisplayName = node.Attributes["DisplayName"].Value;
                    MailAddress bccAddress = new MailAddress(node.InnerText, bccDisplayName);
                    this.Email.Bcc.Add(bccAddress);
                }
            }

            // Subject
            XmlNode subjectNode = GetConfigSection(xml, "SerializableMailMessage/MailMessage/Subject");
            this.Email.Subject = subjectNode.InnerText;

            // Body
            XmlNode bodyNode = GetConfigSection(xml, "SerializableMailMessage/MailMessage/Body");
            this.Email.Body = bodyNode.InnerText;
        }

        public void WriteXml(XmlWriter writer)
        {
            if (this.Email != null)
            {
                writer.WriteStartElement("MailMessage");
                writer.WriteAttributeString("Priority", Convert.ToInt16(this.Email.Priority).ToString());
                writer.WriteAttributeString("IsBodyHtml", this.Email.IsBodyHtml.ToString());

                // From
                writer.WriteStartElement("From");
                if (!string.IsNullOrEmpty(this.Email.From.DisplayName))
                    writer.WriteAttributeString("DisplayName", this.Email.From.DisplayName);
                writer.WriteRaw(this.Email.From.Address);
                writer.WriteEndElement();

                // To
                writer.WriteStartElement("To");
                writer.WriteStartElement("Addresses");
                foreach (MailAddress address in this.Email.To)
                {
                    writer.WriteStartElement("Address");
                    if (!string.IsNullOrEmpty(address.DisplayName))
                        writer.WriteAttributeString("DisplayName", address.DisplayName);
                    writer.WriteRaw(address.Address);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndElement();

                // CC
                if (this.Email.CC.Count > 0)
                {
                    writer.WriteStartElement("CC");
                    writer.WriteStartElement("Addresses");
                    foreach (MailAddress address in this.Email.CC)
                    {
                        writer.WriteStartElement("Address");
                        if (!string.IsNullOrEmpty(address.DisplayName))
                            writer.WriteAttributeString("DisplayName", address.DisplayName);
                        writer.WriteRaw(address.Address);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }

                // Bcc
                if (this.Email.Bcc.Count > 0)
                {
                    writer.WriteStartElement("Bcc");
                    writer.WriteStartElement("Addresses");
                    foreach (MailAddress address in this.Email.Bcc)
                    {
                        writer.WriteStartElement("Address");
                        if (!string.IsNullOrEmpty(address.DisplayName))
                            writer.WriteAttributeString("DisplayName", address.DisplayName);
                        writer.WriteRaw(address.Address);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }

                // Subject
                writer.WriteStartElement("Subject");
                writer.WriteRaw(this.Email.Subject);
                writer.WriteEndElement();

                // Body
                writer.WriteStartElement("Body");
                writer.WriteCData(this.Email.Body);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }




        public XmlNode GetConfigSection(XmlDocument xml, string nodePath)
        {
            return xml.SelectSingleNode(nodePath);
        }


    }
}
