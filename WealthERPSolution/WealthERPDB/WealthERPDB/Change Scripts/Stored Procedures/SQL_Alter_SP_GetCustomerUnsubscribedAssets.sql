-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerUnsubscribedAssets]
	@C_CustomerId INT,
	@EventCode VARCHAR(20)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@EventCode='Property')
	BEGIN
	
		SELECT 
			CPNP.CPNP_PropertyNPId AssetId,
			CPNP.CPA_AccountId TargetId,
			CPNP.CPNP_Name Name,
			CPNP.CPNP_CurrentValue CurrentValue
		FROM
			CustomerPropertyNetPosition CPNP
		INNER JOIN
			CustomerPropertyAccount CPA
		ON
			CPNP.CPA_AccountId=CPA.CPA_AccountId
		INNER JOIN
			CustomerPortfolio CP
		ON
			CP.CP_PortfolioId=CPA.CP_PortfolioId
		INNER JOIN
			Customer C
		ON
			C.C_CustomerId=CP.C_CustomerId
		LEFT OUTER JOIN
			AlertEventSetup AES
		ON
			CPA.CPA_AccountId=AES.AES_TargetId
		WHERE
			C.C_CustomerId=@C_CustomerId
		AND
			AES.AES_EventSetupId is null
	
	END
	
	IF(@EventCode='Personal')
	BEGIN
		
		SELECT 
			CPNP.CPNP_PersonalNPId AssetId,
			CPNP.CP_PortfolioId TargetId,
			CPNP.CPNP_Name Name,
			CPNP.CPNP_CurrentValue CurrentValue
		FROM
			CustomerPersonalNetPosition CPNP
		INNER JOIN
			CustomerPortfolio CP
		ON
			CP.CP_PortfolioId=CPNP.CP_PortfolioId
		INNER JOIN
			Customer C
		ON
			C.C_CustomerId=CP.C_CustomerId
		LEFT OUTER JOIN
			AlertEventSetup AES
		ON
			CP.CP_PortfolioId=AES.AES_TargetId
		WHERE
			C.C_CustomerId=@C_CustomerId
		AND
			AES.AES_EventSetupId is null
	
	END
	
	SET NOCOUNT OFF

END
 