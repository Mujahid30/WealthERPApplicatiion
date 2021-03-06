/****** Object:  StoredProcedure [dbo].[SP_UploadGetNewCustomersWerp]    Script Date: 06/24/2009 12:08:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UploadGetNewCustomersWerp]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UploadGetNewCustomersWerp]
GO
/****** Object:  StoredProcedure [dbo].[SP_UploadGetNewCustomersWerp]    Script Date: 06/24/2009 12:08:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UploadGetNewCustomersWerp]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_UploadGetNewCustomersWerp]
AS

	select Max(CEXPS_FirstName) CEXPS_FirstName,
Max(CEXPS_MiddleName) CEXPS_MiddleName,
Max(CEXPS_LastName) CEXPS_LastName,
Max(CEXPS_Gender) CEXPS_Gender,
Max(CEXPS_DOB) CEXPS_DOB,
Max(CEXPS_Type) CEXPS_Type,
Max(CEXPS_SubType) CEXPS_SubType,
Max(CEXPS_Salutation) CEXPS_Salutation,
Max(CEXPS_Adr1Line1) CEXPS_Adr1Line1,
Max(CEXPS_Adr1Line2) CEXPS_Adr1Line2,
Max(CEXPS_Adr1Line3) CEXPS_Adr1Line3,
MAX(CEXPS_Adr1PinCode) CEXPS_Adr1PinCode,
MAX(CEXPS_Adr1City) CEXPS_Adr1City,
MAX(CEXPS_Adr1State) CEXPS_Adr1State,
MAX(CEXPS_Adr1Country) CEXPS_Adr1Country,
Max(CEXPS_Adr2Line1) CEXPS_Adr2Line1,
Max(CEXPS_Adr2Line2) CEXPS_Adr2Line2,
Max(CEXPS_Adr2Line3) CEXPS_Adr2Line3,
MAX(CEXPS_Adr2PinCode) CEXPS_Adr2PinCode,
MAX(CEXPS_Adr2City) CEXPS_Adr2City,
MAX(CEXPS_Adr2State) CEXPS_Adr2State,
MAX(CEXPS_Adr2Country) CEXPS_Adr2Country,
MAX(CEXPS_ResISDCode) CEXPS_ResISDCode,
MAX(CEXPS_ResSTDCode) CEXPS_ResSTDCode,
MAX(CEXPS_ResPhoneNum) CEXPS_ResPhoneNum,
MAX(CEXPS_OfcISDCode) CEXPS_OfcISDCode,
MAX(CEXPS_OfcSTDCode) CEXPS_OfcSTDCode,
MAX(CEXPS_OfcPhoneNum) CEXPS_OfcPhoneNum,
MAX(CEXPS_Email) CEXPS_Email,
MAX(CEXPS_AltEmail) CEXPS_AltEmail,
MAX(CEXPS_Mobile1) CEXPS_Mobile1,
MAX(CEXPS_Mobile2) CEXPS_Mobile2,
MAX(CEXPS_ISDFax) CEXPS_ISDFax,
MAX(CEXPS_STDFax) CEXPS_STDFax,
MAX(CEXPS_Fax) CEXPS_Fax,
MAX(CEXPS_OfcFax) CEXPS_OfcFax,
MAX(CEXPS_OfcFaxISD) CEXPS_OfcFaxISD,
MAX(CEXPS_OfcFaxSTD) CEXPS_OfcFaxSTD,
MAX(CEXPS_Occupation) CEXPS_Occupation,
MAX(CEXPS_Qualification) CEXPS_Qualification,
MAX(CEXPS_MarriageDate) CEXPS_MarriageDate,
MAX(CEXPS_MaritalStatus) CEXPS_MaritalStatus,
MAX(CEXPS_Nationality) CEXPS_Nationality,
MAX(CEXPS_RBIRefNum) CEXPS_RBIRefNum,
MAX(CEXPS_RBIApprovalDate) CEXPS_RBIApprovalDate,
MAX(CEXPS_CompanyName) CEXPS_CompanyName,
MAX(CEXPS_OfcAdrLine1) CEXPS_OfcAdrLine1,
MAX(CEXPS_OfcAdrLine2) CEXPS_OfcAdrLine2,
MAX(CEXPS_OfcAdrLine3) CEXPS_OfcAdrLine3,
MAX(CEXPS_OfcAdrPinCode) CEXPS_OfcAdrPinCode,
MAX(CEXPS_OfcAdrCity) CEXPS_OfcAdrCity,
MAX(CEXPS_OfcAdrState) CEXPS_OfcAdrState,
MAX(CEXPS_OfcAdrCountry) CEXPS_OfcAdrCountry,
MAX(CEXPS_RegistrationDate) CEXPS_RegistrationDate,
MAX(CEXPS_CommencementDate) CEXPS_CommencementDate,
MAX(CEXPS_RegistrationPlace) CEXPS_RegistrationPlace,
MAX(CEXPS_RegistrationNum) CEXPS_RegistrationNum,
MAX(CEXPS_CompanyWebsite) CEXPS_CompanyWebsite,
CEXPS_PANNum,
count(*) [count] from CustomerEquityXtrnlProfileStaging where CEXPS_IsRejected = 0 and CEXPS_IsCustomerNew = 1  group by CEXPS_PANNum

' 
END
GO
