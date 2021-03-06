/****** Object:  StoredProcedure [dbo].[SP_DeleteAlertNotification]    Script Date: 06/24/2009 11:14:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteAlertNotification]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeleteAlertNotification]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteAlertNotification]    Script Date: 06/24/2009 11:14:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteAlertNotification]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[SP_DeleteAlertNotification]
@NotificationID BIGINT

AS

BEGIN

	SET NOCOUNT ON
	
	DELETE 
	FROM
		dbo.AlertEventNotification
	WHERE
		AEN_EventQueueID = @NotificationID 
	
	SET NOCOUNT OFF
	
END' 
END
GO
