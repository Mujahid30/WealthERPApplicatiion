/****** Object:  StoredProcedure [dbo].[SP_UploadGetNewCustomersCams]    Script Date: 06/24/2009 12:07:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UploadGetNewCustomersCams]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UploadGetNewCustomersCams]
GO
/****** Object:  StoredProcedure [dbo].[SP_UploadGetNewCustomersCams]    Script Date: 06/24/2009 12:07:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UploadGetNewCustomersCams]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_UploadGetNewCustomersCams]
@processId INT 
AS
select Max(CMGCXPS_INV_NAME) CMGCXPS_INV_NAME,
Max(CMGCXPS_ADDRESS1) CMGCXPS_ADDRESS1,
Max(CMGCXPS_ADDRESS2) CMGCXPS_ADDRESS2,
Max(CMGCXPS_ADDRESS3) CMGCXPS_ADDRESS3,
Max(CMGCXPS_CITY) CMGCXPS_CITY,
Max(CMGCXPS_PINCODE) CMGCXPS_PINCODE,
Max(CMGCXPS_PHONE_OFF) CMGCXPS_PHONE_OFF,
Max(CMGCXPS_PHONE_RES) CMGCXPS_PHONE_RES,
MAX(CMGCXPS_EMAIL) CMGCXPS_EMAIL,
MAX(B.XCT_CustomerTypeCode) CustomerTypeCode,
MAX(B.XCST_CustomerSubTypeCode) CustomerSubTypeCode, 
CMGCXPS_PAN_NO,
count(*) [count] from CustomerMFCAMSXtrnlProfileStaging 
LEFT Outer JOIN WerpCAMSCustomerTypeDataTranslatorMapping B 
ON CMGCXPS_TAX_STATUS = B.WCCTDTM_TaxStatus    
where CMGCXPS_IsRejected=0 and CMGCXPS_IsCustomerNew=1 AND ADUL_ProcessId=@processId  group by CMGCXPS_PAN_NO' 
END
GO
