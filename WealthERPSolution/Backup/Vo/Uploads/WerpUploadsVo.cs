using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoUploads
{
    public class WerpUploadsVo
    {
        #region fields

        private string m_FirstName;

        public string FirstName
        {
            get { return m_FirstName; }
            set { m_FirstName = value; }
        }
        private string m_MiddleName;

        public string MiddleName
        {
            get { return m_MiddleName; }
            set { m_MiddleName = value; }
        }
        private string m_LastName;

        public string LastName
        {
            get { return m_LastName; }
            set { m_LastName = value; }
        }
        private string m_Gender;

        public string Gender
        {
            get { return m_Gender; }
            set { m_Gender = value; }
        }
        private string m_DOB;

        public string DOB
        {
            get { return m_DOB; }
            set { m_DOB = value; }
        }
        private string m_Type;

        public string Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }
        private string m_SubType;

        public string SubType
        {
            get { return m_SubType; }
            set { m_SubType = value; }
        }
        private string m_Salutation;

        public string Salutation
        {
            get { return m_Salutation; }
            set { m_Salutation = value; }
        }
        private string m_PanNumber;

        public string PanNumber
        {
            get { return m_PanNumber; }
            set { m_PanNumber = value; }
        }
        private string m_Address1Line1;

        public string Address1Line1
        {
            get { return m_Address1Line1; }
            set { m_Address1Line1 = value; }
        }
        private string m_Address1Line2;

        public string Address1Line2
        {
            get { return m_Address1Line2; }
            set { m_Address1Line2 = value; }
        }
        private string m_Address1Line3;

        public string Address1Line3
        {
            get { return m_Address1Line3; }
            set { m_Address1Line3 = value; }
        }
        private string m_Address1Pincode;

        public string Address1Pincode
        {
            get { return m_Address1Pincode; }
            set { m_Address1Pincode = value; }
        }
        private string m_Address1City;

        public string Address1City
        {
            get { return m_Address1City; }
            set { m_Address1City = value; }
        }
        private string m_Address1State;

        public string Address1State
        {
            get { return m_Address1State; }
            set { m_Address1State = value; }
        }
        private string m_Address1Country;

        public string Address1Country
        {
            get { return m_Address1Country; }
            set { m_Address1Country = value; }
        }
        private string m_Address2Line1;

        public string Address2Line1
        {
            get { return m_Address2Line1; }
            set { m_Address2Line1 = value; }
        }
        private string m_Address2Line2;

        public string Address2Line2
        {
            get { return m_Address2Line2; }
            set { m_Address2Line2 = value; }
        }
        private string m_Address2Line3;

        public string Address2Line3
        {
            get { return m_Address2Line3; }
            set { m_Address2Line3 = value; }
        }
        private string m_Address2Pincode;

        public string Address2Pincode
        {
            get { return m_Address2Pincode; }
            set { m_Address2Pincode = value; }
        }
        private string m_Address2City;

        public string Address2City
        {
            get { return m_Address2City; }
            set { m_Address2City = value; }
        }
        private string m_Address2State;

        public string Address2State
        {
            get { return m_Address2State; }
            set { m_Address2State = value; }
        }
        private string m_Address2Country;

        public string Address2Country
        {
            get { return m_Address2Country; }
            set { m_Address2Country = value; }
        }
        private string m_ResISDCode;

        public string ResISDCode
        {
            get { return m_ResISDCode; }
            set { m_ResISDCode = value; }
        }
        private string m_ResSTDCode;

        public string ResSTDCode
        {
            get { return m_ResSTDCode; }
            set { m_ResSTDCode = value; }
        }
        private string m_ResPhoneNumber;

        public string ResPhoneNumber
        {
            get { return m_ResPhoneNumber; }
            set { m_ResPhoneNumber = value; }
        }
        private string m_OfcISDCode;

        public string OfcISDCode
        {
            get { return m_OfcISDCode; }
            set { m_OfcISDCode = value; }
        }
        private string m_OfcSTDCode;

        public string OfcSTDCode
        {
            get { return m_OfcSTDCode; }
            set { m_OfcSTDCode = value; }
        }
        private string m_OfcPhoneNumber;

        public string OfcPhoneNumber
        {
            get { return m_OfcPhoneNumber; }
            set { m_OfcPhoneNumber = value; }
        }
        private string m_Email;

        public string Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }
        private string m_AltEmail;

        public string AltEmail
        {
            get { return m_AltEmail; }
            set { m_AltEmail = value; }
        }
        private string m_Mobile1;

        public string Mobile1
        {
            get { return m_Mobile1; }
            set { m_Mobile1 = value; }
        }
        private string m_Mobile2;

        public string Mobile2
        {
            get { return m_Mobile2; }
            set { m_Mobile2 = value; }
        }
        private string m_ISDFax;

        public string ISDFax
        {
            get { return m_ISDFax; }
            set { m_ISDFax = value; }
        }
        private string m_STDFax;

        public string STDFax
        {
            get { return m_STDFax; }
            set { m_STDFax = value; }
        }
        private string m_Fax;

        public string Fax
        {
            get { return m_Fax; }
            set { m_Fax = value; }
        }
        private string m_OfcFax;

        public string OfcFax
        {
            get { return m_OfcFax; }
            set { m_OfcFax = value; }
        }
        private string m_OfcFaxISD;

        public string OfcFaxISD
        {
            get { return m_OfcFaxISD; }
            set { m_OfcFaxISD = value; }
        }
        private string m_OfcFaxSTD;

        public string OfcFaxSTD
        {
            get { return m_OfcFaxSTD; }
            set { m_OfcFaxSTD = value; }
        }
        private string m_Occupation;

        public string Occupation
        {
            get { return m_Occupation; }
            set { m_Occupation = value; }
        }
        private string m_Qualification;

        public string Qualification
        {
            get { return m_Qualification; }
            set { m_Qualification = value; }
        }
        private string m_MarriageDate;

        public string MarriageDate
        {
            get { return m_MarriageDate; }
            set { m_MarriageDate = value; }
        }
        private string m_MaritalStatus;

        public string MaritalStatus
        {
            get { return m_MaritalStatus; }
            set { m_MaritalStatus = value; }
        }
        private string m_Nationality;

        public string Nationality
        {
            get { return m_Nationality; }
            set { m_Nationality = value; }
        }
        private string m_RBIRefNumber;

        public string RBIRefNumber
        {
            get { return m_RBIRefNumber; }
            set { m_RBIRefNumber = value; }
        }
        private string m_RBIApprovalDate;

        public string RBIApprovalDate
        {
            get { return m_RBIApprovalDate; }
            set { m_RBIApprovalDate = value; }
        }
        private string m_CompanyName;

        public string CompanyName
        {
            get { return m_CompanyName; }
            set { m_CompanyName = value; }
        }
        private string m_OfcAddressLine1;

        public string OfcAddressLine1
        {
            get { return m_OfcAddressLine1; }
            set { m_OfcAddressLine1 = value; }
        }
        private string m_OfcAddressLine2;

        public string OfcAddressLine2
        {
            get { return m_OfcAddressLine2; }
            set { m_OfcAddressLine2 = value; }
        }
        private string m_OfcAddressLine3;

        public string OfcAddressLine3
        {
            get { return m_OfcAddressLine3; }
            set { m_OfcAddressLine3 = value; }
        }
        private string m_OfcAddressPincode;

        public string OfcAddressPincode
        {
            get { return m_OfcAddressPincode; }
            set { m_OfcAddressPincode = value; }
        }
        private string m_OfcAddressCity;

        public string OfcAddressCity
        {
            get { return m_OfcAddressCity; }
            set { m_OfcAddressCity = value; }
        }
        private string m_OfcAddressState;

        public string OfcAddressState
        {
            get { return m_OfcAddressState; }
            set { m_OfcAddressState = value; }
        }
        private string m_OfcAddressCountry;

        public string OfcAddressCountry
        {
            get { return m_OfcAddressCountry; }
            set { m_OfcAddressCountry = value; }
        }
        private string m_RegistrationDate;

        public string RegistrationDate
        {
            get { return m_RegistrationDate; }
            set { m_RegistrationDate = value; }
        }
        private string m_CommencementDate;

        public string CommencementDate
        {
            get { return m_CommencementDate; }
            set { m_CommencementDate = value; }
        }
        private string m_RegistrationPlace;

        public string RegistrationPlace
        {
            get { return m_RegistrationPlace; }
            set { m_RegistrationPlace = value; }
        }
        private string m_RegistrationNumber;

        public string RegistrationNumber
        {
            get { return m_RegistrationNumber; }
            set { m_RegistrationNumber = value; }
        }
        private string m_CompanyWebsite;

        public string CompanyWebsite
        {
            get { return m_CompanyWebsite; }
            set { m_CompanyWebsite = value; }
        }

        #endregion fields
    }
}
