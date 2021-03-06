/****** Object:  StoredProcedure [dbo].[SP_GetCustomerUnsubscribedSystematicSchemes]    Script Date: 06/24/2009 12:02:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerUnsubscribedSystematicSchemes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerUnsubscribedSystematicSchemes]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerUnsubscribedSystematicSchemes]    Script Date: 06/24/2009 12:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerUnsubscribedSystematicSchemes]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerUnsubscribedSystematicSchemes] 
	@C_CustomerId INT,
	@EventCode VARCHAR(5),
	@AEL_EventId SMALLINT
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT
		 CMFSS.* ,PASP.PASP_SchemePlanName
	FROM
	
		CustomerMutualFundSystematicSetup CMFSS
	INNER JOIN
		CustomerMutualFundAccount CMFA
	ON
		CMFSS.CMFA_AccountId=CMFA.CMFA_AccountId
	INNER JOIN
	 	CustomerPortfolio CP
	ON
		CMFA.CP_PortfolioId=CP.CP_PortfolioId
	INNER JOIN
		Customer C
	ON
		CP.C_CustomerId=C.C_CustomerId
	--LEFT OUTER JOIN
	--	AlertEventSetup AES
	--ON
	--	CMFSS.CMFA_AccountId=AES.AES_TargetId
	INNER JOIN
		ProductAMCSchemePlan PASP
	ON
		CMFSS.PASP_SchemePlanCode=PASP.PASP_SchemePlanCode
		
	WHERE
		C.C_CustomerId=@C_CustomerId
	AND
		CMFSS.XSTT_SystematicTypeCode=@EventCode
	AND
		CMFSS.PASP_SchemePlanCode NOT IN (
											SELECT AES_SchemeId
											FROM AlertEventSetup
											WHERE 
											AES_CreatedFor=@C_CustomerId
											AND
											AEL_EventId=@AEL_EventId
										)
										
	--	AES.AEL_EventId=1
	--AND
	--	--AES.AEL_EventId=@AEL_EventId
	--	AES.AES_EventSetupId is null

    SET NOCOUNT OFF
END
' 
END
GO
