-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerSubscribedConfirmationAlerts]
@AES_CreatedFor INT,
@AEL_EventID INT,
@AlertType Varchar(50)

AS

BEGIN
	SET NOCOUNT ON

	IF(@AlertType='Transactional')
	
	BEGIN
	
		SELECT 
			 ES.AES_EventSetupID,
			 ES.AEL_EventID,
			 ES.AES_TargetID,
			 ES.AES_EventMessage,
			 EL.AEL_EventCode,
			 ES.AES_SchemeID,
			 PASP.PASP_SchemePlanName
		FROM
		AlertEventSetup ES
		INNER JOIN
		AlertEventLookup EL
		ON
		ES.AEL_EventID=EL.AEL_EventID
		INNER JOIN
		ProductAMCSchemePlan PASP
		ON
		ES.AES_SchemeID=PASP.PASP_SchemePlanCode
		WHERE
		ES.AES_CreatedFor=@AES_CreatedFor
		AND
		ES.AEL_EventID=@AEL_EventID
	
	END
	SET NOCOUNT OFF
END
 