-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_UpdateCustomer]
@C_CustomerId int,
@C_FirstName varchar(25),
@C_MiddleName varchar(25),
@C_LastName varchar(25),
@C_Gender varchar(10),
@XCST_CustomerSubTypeCode varchar(25) = null,
@C_Salutation varchar(5),
@C_PANNum varchar(10),
@C_CustCode varchar(10),
@C_ProfilingDate	DATETIME ,
@C_DOB	DATETIME ,
@C_Adr1Line1 varchar(25),
@C_Adr1Line2 varchar(25),
@C_Adr1Line3 varchar(25),
@C_Adr1PinCode numeric(6,0),
@C_Adr1City varchar(25),
@C_Adr1State varchar(25),
@C_Adr1Country varchar(25),
@C_Adr2Line1 varchar(20),
@C_Adr2Line2 varchar(20),
@C_Adr2Line3 varchar(20),
@C_Adr2PinCode numeric(6,0),
@C_Adr2City varchar(20),
@C_Adr2State varchar(20),
@C_Adr2Country varchar(20),
@C_ResISDCode numeric(4,0),
@C_ResSTDCode numeric(4,0),
@C_ResPhoneNum numeric(8,0),
@C_OfcISDCode numeric(4,0),
@C_OfcSTDCode numeric(4,0),
@C_OfcPhoneNum numeric(8,0),
@C_Email varchar(25),
@C_AltEmail varchar(25),
@C_Mobile1 numeric(10,0),
@C_Mobile2 numeric(10,0),
@C_ISDFax numeric(4,0),
@C_STDFax numeric(4,0),
@C_Fax numeric(8,0),
@C_OfcFaxISD numeric(4,0),
@C_OfcFaxSTD numeric(4,0),
@C_OfcFax numeric(8,0),
@XO_OccupationCode varchar(25),
@XQ_QualificationCode varchar(25),
@XMS_MaritalStatusCode varchar(25),
@XN_NationalityCode varchar(25),
@C_RBIRefNum varchar(25),
@C_RBIApprovalDate datetime,
@C_CompanyName varchar(50),
@C_OfcAdrLine1 varchar(25),
@C_OfcAdrLine2 varchar(25),
@C_OfcAdrLine3 varchar(25),
@C_OfcAdrPinCode numeric(6,0),
@C_OfcAdrCity varchar(25),
@C_OfcAdrState varchar(25),
@C_OfcAdrCountry varchar(25),
@C_RegistrationDate datetime,
@C_CommencementDate datetime,
@C_RegistrationPlace varchar(20),
@C_RegistrationNum varchar(25),
@C_CompanyWebsite varchar(25)

AS

SET NOCOUNT ON

if (@XCST_CustomerSubTypeCode is null)
begin
	update Customer set 
			   C_FirstName=@C_FirstName
			   ,C_MiddleName=@C_MiddleName
			   ,C_LastName=@C_LastName
			   ,C_Gender=@C_Gender
			   ,C_Salutation=@C_Salutation
			   ,C_PANNum=@C_PANNum
			   ,C_DOB=@C_DOB
			   ,C_ProfilingDate=@C_ProfilingDate
			   ,C_CustCode=@C_CustCode
			   ,C_Adr1Line1=@C_Adr1Line1
			   ,C_Adr1Line2=@C_Adr1Line2
			   ,C_Adr1Line3=@C_Adr1Line3
			   ,C_Adr1PinCode=@C_Adr1PinCode
			   ,C_Adr1City=@C_Adr1City
			   ,C_Adr1State=@C_Adr1State
			   ,C_Adr1Country=@C_Adr1Country
			   ,C_Adr2Line1=@C_Adr2Line1
			   ,C_Adr2Line2=@C_Adr2Line2
			   ,C_Adr2Line3=@C_Adr2Line3
			   ,C_Adr2PinCode=@C_Adr2PinCode
			   ,C_Adr2City=@C_Adr2City
			   ,C_Adr2State=@C_Adr2State
			   ,C_Adr2Country=@C_Adr2Country
			   ,C_ResISDCode=@C_ResISDCode
			   ,C_ResSTDCode=@C_ResSTDCode
			   ,C_ResPhoneNum=@C_ResPhoneNum
			   ,C_OfcISDCode=@C_OfcISDCode
			   ,C_OfcSTDCode=@C_OfcSTDCode
			   ,C_OfcPhoneNum=@C_OfcPhoneNum
			   ,C_Email=@C_Email
			   ,C_AltEmail=@C_AltEmail
			   ,C_Mobile1=@C_Mobile1
			   ,C_Mobile2=@C_Mobile2
			   ,C_ISDFax=@C_Mobile2
			   ,C_STDFax=@C_STDFax
			   ,C_Fax=@C_Fax
			   ,C_OfcFaxISD=@C_OfcFaxISD
			   ,C_OfcFaxSTD=@C_OfcFaxSTD
			   ,C_OfcFax=@C_OfcFax
			   ,XO_OccupationCode=@XO_OccupationCode
			   ,XQ_QualificationCode=@XQ_QualificationCode
			   ,XMS_MaritalStatusCode=@XMS_MaritalStatusCode
			   ,XN_NationalityCode=@XN_NationalityCode
			   ,C_RBIRefNum=@C_RBIRefNum
			   ,C_RBIApprovalDate=@C_RBIApprovalDate
			   ,C_CompanyName=@C_CompanyName
			   ,C_OfcAdrLine1=@C_OfcAdrLine1
			   ,C_OfcAdrLine2=@C_OfcAdrLine2
			   ,C_OfcAdrLine3=@C_OfcAdrLine3
			   ,C_OfcAdrPinCode=@C_OfcAdrPinCode
			   ,C_OfcAdrCity=@C_OfcAdrCity
			   ,C_OfcAdrState=@C_OfcAdrState
			   ,C_OfcAdrCountry=@C_OfcAdrCountry
			   ,C_RegistrationDate=@C_RegistrationDate
			   ,C_CommencementDate=@C_CommencementDate
			   ,C_RegistrationPlace=@C_RegistrationPlace
			   ,C_RegistrationNum=@C_RegistrationNum
			   ,C_CompanyWebsite=@C_CompanyWebsite
	           
	where C_CustomerId=@C_CustomerId
end
else
begin

	update Customer set 
			   C_FirstName=@C_FirstName
			   ,C_MiddleName=@C_MiddleName
			   ,C_LastName=@C_LastName
			   ,C_Gender=@C_Gender
			   ,XCST_CustomerSubTypeCode=@XCST_CustomerSubTypeCode
			   ,C_Salutation=@C_Salutation
			   ,C_PANNum=@C_PANNum
			   ,C_CustCode=@C_CustCode
			   ,C_ProfilingDate=@C_ProfilingDate
			   ,C_DOB=@C_DOB
			   
			   ,C_Adr1Line1=@C_Adr1Line1
			   ,C_Adr1Line2=@C_Adr1Line2
			   ,C_Adr1Line3=@C_Adr1Line3
			   ,C_Adr1PinCode=@C_Adr1PinCode
			   ,C_Adr1City=@C_Adr1City
			   ,C_Adr1State=@C_Adr1State
			   ,C_Adr1Country=@C_Adr1Country
			   ,C_Adr2Line1=@C_Adr2Line1
			   ,C_Adr2Line2=@C_Adr2Line2
			   ,C_Adr2Line3=@C_Adr2Line3
			   ,C_Adr2PinCode=@C_Adr2PinCode
			   ,C_Adr2City=@C_Adr2City
			   ,C_Adr2State=@C_Adr2State
			   ,C_Adr2Country=@C_Adr2Country
			   ,C_ResISDCode=@C_ResISDCode
			   ,C_ResSTDCode=@C_ResSTDCode
			   ,C_ResPhoneNum=@C_ResPhoneNum
			   ,C_OfcISDCode=@C_OfcISDCode
			   ,C_OfcSTDCode=@C_OfcSTDCode
			   ,C_OfcPhoneNum=@C_OfcPhoneNum
			   ,C_Email=@C_Email
			   ,C_AltEmail=@C_AltEmail
			   ,C_Mobile1=@C_Mobile1
			   ,C_Mobile2=@C_Mobile2
			   ,C_ISDFax=@C_Mobile2
			   ,C_STDFax=@C_STDFax
			   ,C_Fax=@C_Fax
			   ,C_OfcFaxISD=@C_OfcFaxISD
			   ,C_OfcFaxSTD=@C_OfcFaxSTD
			   ,C_OfcFax=@C_OfcFax
			   ,XO_OccupationCode=@XO_OccupationCode
			   ,XQ_QualificationCode=@XQ_QualificationCode
			   ,XMS_MaritalStatusCode=@XMS_MaritalStatusCode
			   ,XN_NationalityCode=@XN_NationalityCode
			   ,C_RBIRefNum=@C_RBIRefNum
			   ,C_RBIApprovalDate=@C_RBIApprovalDate
			   ,C_CompanyName=@C_CompanyName
			   ,C_OfcAdrLine1=@C_OfcAdrLine1
			   ,C_OfcAdrLine2=@C_OfcAdrLine2
			   ,C_OfcAdrLine3=@C_OfcAdrLine3
			   ,C_OfcAdrPinCode=@C_OfcAdrPinCode
			   ,C_OfcAdrCity=@C_OfcAdrCity
			   ,C_OfcAdrState=@C_OfcAdrState
			   ,C_OfcAdrCountry=@C_OfcAdrCountry
			   ,C_RegistrationDate=@C_RegistrationDate
			   ,C_CommencementDate=@C_CommencementDate
			   ,C_RegistrationPlace=@C_RegistrationPlace
			   ,C_RegistrationNum=@C_RegistrationNum
			   ,C_CompanyWebsite=@C_CompanyWebsite
	           
	where C_CustomerId=@C_CustomerId
end

SET NOCOUNT OFF 