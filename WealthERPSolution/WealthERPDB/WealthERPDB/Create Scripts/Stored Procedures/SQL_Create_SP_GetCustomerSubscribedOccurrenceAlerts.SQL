/****** Object:  StoredProcedure [dbo].[SP_GetCustomerSubscribedOccurrenceAlerts]    Script Date: 06/24/2009 12:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerSubscribedOccurrenceAlerts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerSubscribedOccurrenceAlerts]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerSubscribedOccurrenceAlerts]    Script Date: 06/24/2009 12:01:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerSubscribedOccurrenceAlerts]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerSubscribedOccurrenceAlerts]
@AES_CreatedFor INT,
@AEL_EventId INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@AEL_EventId=11)
	BEGIN
	SELECT 
		--ES.*, EL.*, ADCS.*
		ES.AES_EventSetupId,
		ES.AEL_EventId,
		ES.AES_SchemeId,
		ES.AES_EventMessage,
		EL.AEL_EventCode,
		ADCS.ADCS_Condition,
		ADCS.ADCS_PresetValue,
		CPNP.CPNP_Name AS Name,
		CPNP.CPNP_CurrentValue AS CurrentValue
	
	FROM
		AlertEventSetup ES
		INNER JOIN
		AlertEventLookup EL
		ON
		ES.AEL_EventID=EL.AEL_EventID
		INNER JOIN
		AlertDataConditionSetup ADCS
		ON
		(ES.AES_CreatedFor=ADCS.ADCS_UserId 
		AND 
		ES.AES_SchemeId=ADCS.ADCS_SchemeId 
		AND 
		ES.AEL_EventId=ADCS.AEL_EventId)
		INNER JOIN
		CustomerPropertyNetPosition CPNP
		ON
		ES.AES_SchemeId=CPNP.CPNP_PropertyNPId
		
	WHERE
		ES.AES_CreatedFor=@AES_CreatedFor
		AND
		ES.AEL_EventId=@AEL_EventId

	END
	
	IF(@AEL_EventId=15)
	BEGIN
	
	SELECT 
		--ES.*, EL.*, ADCS.*
		ES.AES_EventSetupId,
		ES.AEL_EventId,
		ES.AES_SchemeId,
		ES.AES_EventMessage,
		EL.AEL_EventCode,
		ADCS.ADCS_Condition,
		ADCS.ADCS_PresetValue,
		CPNP.CPNP_Name AS Name,
		CPNP.CPNP_CurrentValue AS CurrentValue
	
	FROM
		AlertEventSetup ES
		INNER JOIN
		AlertEventLookup EL
		ON
		ES.AEL_EventID=EL.AEL_EventID
		INNER JOIN
		AlertDataConditionSetup ADCS
		ON
		(ES.AES_CreatedFor=ADCS.ADCS_UserId 
		AND 
		ES.AES_SchemeId=ADCS.ADCS_SchemeId 
		AND 
		ES.AEL_EventId=ADCS.AEL_EventId)
		INNER JOIN
		CustomerPersonalNetPosition CPNP
		ON
		ES.AES_SchemeId=CPNP.CPNP_PersonalNPId
		
	WHERE
		ES.AES_CreatedFor=@AES_CreatedFor
		AND
		ES.AEL_EventId=@AEL_EventId
	
	END
	SET NOCOUNT OFF

		
END
' 
END
GO
