/****** Object:  StoredProcedure [dbo].[SP_UploadGetNewCustomersKarvyCombo]    Script Date: 06/24/2009 12:07:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UploadGetNewCustomersKarvyCombo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UploadGetNewCustomersKarvyCombo]
GO
/****** Object:  StoredProcedure [dbo].[SP_UploadGetNewCustomersKarvyCombo]    Script Date: 06/24/2009 12:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UploadGetNewCustomersKarvyCombo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_UploadGetNewCustomersKarvyCombo]
AS

	select Max(CMFKXCS_InvestorName) CMFKXCS_InvestorName,Max(CMFKXCS_DateofBirth) CMFKXCS_DateofBirth,
Max(CMFKXCS_Address#1) CMFKXCS_Address#1,
Max(CMFKXCS_Address#2) CMFKXCS_Address#2,
Max(CMFKXCS_Address#3) CMFKXCS_Address#3,
Max(CMFKXCS_City) CMFKXCS_City,
Max(CMFKXCS_Country) CMFKXCS_Country,
Max(CMFKXCS_Pincode) CMFKXCS_Pincode,
Max(CMFKXCS_State) CMFKXCS_State,
Max(CMFKXCS_DateofBirth) CMFKXCS_DateofBirth,
Max(CMFKXCS_Email) CMFKXCS_Email,
Max(CMFKXCS_FaxResidence) CMFKXCS_FaxResidence,
Max(CMFKXCS_Mobile  ) CMFKXCS_Mobile  ,
Max(CMFKXCS_FaxOffice) CMFKXCS_FaxOffice,
Max(CMFKXCS_PhoneOffice) CMFKXCS_PhoneOffice,
CMFKXCS_PANNumber,
Max(CMFKXCS_PhoneResidence) CMFKXCS_PhoneResidence,
count(*) [count] from CustomerMFKarvyXtrnlCombinationStaging where CMFKXCS_IsRejected=0 and CMFKXCS_IsCustomerNew=1  group by CMFKXCS_PANNumber



' 
END
GO
