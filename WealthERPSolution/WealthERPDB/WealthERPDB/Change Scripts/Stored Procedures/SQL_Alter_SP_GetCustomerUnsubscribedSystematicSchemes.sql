-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerUnsubscribedSystematicSchemes] 
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
 