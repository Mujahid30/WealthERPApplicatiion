-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerSubscribedReminderAlerts]
@AES_CreatedFor INT,
@AEL_EventId INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--Select query for SIP reminder
	IF(@AEL_EventId=1)
	BEGIN
	WITH Alerts AS (
	SELECT 
		ES.AES_EventSetupId AS ParentSetupId,
		ES1.AES_EventSetupId AS EventSetupId,
		ES.AEL_EventId AS EventId,
		ES.AES_SchemeId AS SchemeId,
		ES1.AES_EventMessage AS EventMessage,
		EL.AEL_EventCode AS EventCode,
		PASP.PASP_SchemePlanName AS Name,
		ES.AES_NextOccurence AS EventDate,
		ES1.AES_NextOccurence AS NextOccurence,
		ROW_NUMBER() OVER (PARTITION BY ES.AES_EventSetupId 
							ORDER BY ES1.AES_NextOccurence ASC) AS rn

	
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
		INNER JOIN
		AlertEventSetup ES1
		ON
		ES1.AES_ParentEventSetupId=ES.AES_EventSetupId

		
	WHERE
		ES.AES_CreatedFor=@AES_CreatedFor
		AND 
		ES.AEL_EventId=1
	)
	
	Select 		
		* 
	FROM
		Alerts
	WHERE 
		rn=1
	END
	
	--Select query for DOB reminder
	IF(@AEL_EventId=4)
	BEGIN
	WITH Alerts AS (
	SELECT 
		ES.AES_EventSetupId AS ParentSetupId,
		ES1.AES_EventSetupId AS EventSetupId,
		ES.AEL_EventId AS EventId,
		ES.AES_SchemeId AS SchemeId,
		ES1.AES_EventMessage AS EventMessage,
		EL.AEL_EventCode AS EventCode,
		'Birthday' AS Name,
		ES.AES_NextOccurence AS EventDate,
		ES1.AES_NextOccurence AS NextOccurence,
		ROW_NUMBER() OVER (PARTITION BY ES.AES_EventSetupId 
							ORDER BY ES1.AES_NextOccurence ASC) AS rn

	
	FROM
		AlertEventSetup ES
		INNER JOIN
		AlertEventLookup EL
		ON
		ES.AEL_EventID=EL.AEL_EventID
		INNER JOIN
		AlertEventSetup ES1
		ON
		ES1.AES_ParentEventSetupId=ES.AES_EventSetupId

		
	WHERE
		ES.AES_CreatedFor=@AES_CreatedFor
		AND 
		ES.AEL_EventId=4
	)
	
	Select 		
		* 
	FROM
		Alerts
	WHERE 
		rn=1
	END
	SET NOCOUNT OFF
END 