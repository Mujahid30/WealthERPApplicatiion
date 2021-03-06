/****** Object:  StoredProcedure [dbo].[SP_GetProfileRejectedCustomers]    Script Date: 06/24/2009 12:04:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetProfileRejectedCustomers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetProfileRejectedCustomers]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProfileRejectedCustomers]    Script Date: 06/24/2009 12:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetProfileRejectedCustomers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GetProfileRejectedCustomers]
	-- Add the parameters for the stored procedure here
	@adviserId int
AS
	SET NOCOUNT ON
	
	(select CMFKXPS_Id AS Id,CMFKXPS_InvestorName as Name,''KARVY'' as [Type],CMFKXPS_RejectReason as Remark from CustomerMFKarvyXtrnlProfileStaging
	where CMFKXPS_IsRejected=1 and A_AdviserId = @adviserId
	union all
	select CMGCXPS_Id AS Id,CMGCXPS_INV_NAME as Name,''CAMS'' as [Type],CMGCXPS_RejectReason as Remark from CustomerMFCAMSXtrnlProfileStaging
	where CMGCXPS_IsRejected=1 and A_AdviserId = @adviserId
	union all
	select CEXPS_Id AS Id,CEXPS_LastName as Name,''WERP Equity'' as [Type],CEXPS_RejectedRemark as Remark from CustomerEquityXtrnlProfileStaging
	where CEXPS_IsRejected=1 and A_AdviserId = @adviserId
	union all 
	select CMFXPS_Id AS Id,CMFXPS_LastName as Name,''WERP MF'' as [Type],CMFXPS_RejectedRemark as Remark from CustomerMFXtrnlProfileStaging
	where CMFXPS_IsRejected=1 and A_AdviserId = @adviserId)
	order by TYPE
	
	SET NOCOUNT OFF
' 
END
GO
