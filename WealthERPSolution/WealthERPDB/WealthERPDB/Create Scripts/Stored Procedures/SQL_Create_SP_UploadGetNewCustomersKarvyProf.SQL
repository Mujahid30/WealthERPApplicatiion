/****** Object:  StoredProcedure [dbo].[SP_UploadGetNewCustomersKarvyProf]    Script Date: 06/24/2009 12:08:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UploadGetNewCustomersKarvyProf]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UploadGetNewCustomersKarvyProf]
GO
/****** Object:  StoredProcedure [dbo].[SP_UploadGetNewCustomersKarvyProf]    Script Date: 06/24/2009 12:08:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UploadGetNewCustomersKarvyProf]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SP_UploadGetNewCustomersKarvyProf]
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
Max(CMFKXPS_OccupationDescription) CMFKXPS_OccupationDescription,
CMFKXPS_PANNumber,
count(*) [count] from CustomerMFKarvyXtrnlProfileStaging where CMFKXPS_IsRejected=0 and CMFKXPS_IsCustomerNew=1  group by CMFKXPS_PANNumber


' 
END
GO
