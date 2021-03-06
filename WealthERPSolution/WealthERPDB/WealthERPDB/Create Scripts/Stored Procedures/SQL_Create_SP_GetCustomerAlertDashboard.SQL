/****** Object:  StoredProcedure [dbo].[SP_GetCustomerAlertDashboard]    Script Date: 06/24/2009 11:59:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerAlertDashboard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerAlertDashboard]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerAlertDashboard]    Script Date: 06/24/2009 11:59:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerAlertDashboard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerAlertDashboard]
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

		
END' 
END
GO
