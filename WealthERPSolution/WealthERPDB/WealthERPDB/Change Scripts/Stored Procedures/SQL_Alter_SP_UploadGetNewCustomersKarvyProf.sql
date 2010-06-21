

ALTER PROCEDURE [dbo].[SP_UploadGetNewCustomersKarvyProf]
@processId INT
AS
select 
Max(CMFKXPS_InvestorName) CMFKXPS_InvestorName,
Max(CMFKXPS_Mobile) CMFKXPS_Mobile,
Max(CMFKXPS_Email) CMFKXPS_Email,
Max(CMFKXPS_PhoneOffice) CMFKXPS_PhoneOffice,
Max(CMFKXPS_FaxResidence) CMFKXPS_FaxResidence,
Max(CMFKXPS_FaxOffice) CMFKXPS_FaxOffice,
Max(CMFKXPS_PhoneResidence) CMFKXPS_PhoneResidence,
Max(CMFKXPS_Address#1) CMFKXPS_Address#1,
Max(CMFKXPS_Address#1) CMFKXPS_Address#2,
Max(CMFKXPS_Address#1) CMFKXPS_Address#3,
Max(CMFKXPS_City) CMFKXPS_City,
Max(CMFKXPS_Pincode) CMFKXPS_Pincode,
Max(CMFKXPS_State) CMFKXPS_State,
Max(CMFKXPS_Country) CMFKXPS_Country,
Max(CMFKXPS_DateofBirth) CMFKXPS_DateofBirth,
Max(B.XO_OccupationCode) OccupationCode,
MAX(C.XCT_CustomerTypeCode) CustomerTypeCode,
MAX(C.XCST_CustomerSubTypeCode) CustomerSubTypeCode, 
MAX(D.XBAT_BankAccountTypeCode) BankAccountTypeCode,
MAX(E.XMOH_ModeOfHoldingCode) ModeOfHoldingCode,
CMFKXPS_PANNumber,
count(*) [count] from CustomerMFKarvyXtrnlProfileStaging A
INNER JOIN WerpKarvyOccupationDataTransalatorMapping B ON 
A.CMFKXPS_OccCode = B.WKODTM_OccCode
INNER JOIN WerpKarvyCustomerTypeDataTranslatorMapping C ON
A.CMFKXPS_TaxStatus = C.WKCTDTM_TaxStatus
INNER JOIN WerpKarvyBankAccountTypeDataTranslatorMapping D ON
A.CMFKXPS_AccountType = D.WKBATDTM_AccountType
INNER JOIN WerpKarvyBankModeOfHoldingDataTranslatorMapping E ON
A.CMFKXPS_ModeofHolding = E.WKBMOHDTM_ModeofHolding
where CMFKXPS_IsRejected=0 and CMFKXPS_IsCustomerNew=1 AND A.ADUL_ProcessId = @processId group by CMFKXPS_PANNumber


 