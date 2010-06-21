-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerAlertDashboard]
@CustomerID INT
AS

BEGIN
	

		SELECT
			EL.AEL_EventID,
			EL.AEL_EventCode,
			EL.AEL_EventType,
			EL.AEL_Reminder,
			dbo.FN_CheckEventSubscription(EL.AEL_EventID, @CustomerID) AS SubscriptionStatus
		FROM 
			dbo.AlertEventLookup AS EL
		WHERE
			EL.AEL_IsAvailable = 1
			and
			EL.AEL_Reminder <> 1

		
END 