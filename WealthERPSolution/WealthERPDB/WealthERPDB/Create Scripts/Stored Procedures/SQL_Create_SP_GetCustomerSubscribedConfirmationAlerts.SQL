/****** Object:  StoredProcedure [dbo].[SP_GetCustomerSubscribedConfirmationAlerts]    Script Date: 06/24/2009 12:01:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerSubscribedConfirmationAlerts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerSubscribedConfirmationAlerts]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerSubscribedConfirmationAlerts]    Script Date: 06/24/2009 12:01:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerSubscribedConfirmationAlerts]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerSubscribedConfirmationAlerts]
@AES_CreatedFor INT,
@AEL_EventID INT,
@AlertType Varchar(50)

AS

BEGIN
	SET NOCOUNT ON

	IF(@AlertType=''Transactional'')
	
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
' 
END
GO
