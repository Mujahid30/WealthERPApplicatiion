-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

ALTER PROCEDURE [dbo].[SP_GetSystemAlerts]

AS

BEGIN
		SET NOCOUNT ON
		
		SELECT 
			AEL_EventID,
			AEL_EventCode,
			AEL_EventType,
			AEL_Reminder

		FROM 
			dbo.AlertEventLookup
		WHERE
			AEL_IsAvailable = 1
			AND
			AEL_Reminder = 0
			
		SET NOCOUNT OFF
END 