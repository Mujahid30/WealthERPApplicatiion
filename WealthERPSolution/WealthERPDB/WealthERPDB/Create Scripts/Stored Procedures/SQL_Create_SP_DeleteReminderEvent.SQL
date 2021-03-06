/****** Object:  StoredProcedure [dbo].[SP_DeleteReminderEvent]    Script Date: 06/24/2009 11:15:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteReminderEvent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeleteReminderEvent]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteReminderEvent]    Script Date: 06/24/2009 11:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteReminderEvent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_DeleteReminderEvent]
@ParentEventSetupID BIGINT

AS

BEGIN
	SET NOCOUNT ON
	
	DELETE 
	FROM 
		dbo.AlertEventSetup
	WHERE
		AES_ParentEventSetupId = @ParentEventSetupID
		 
	SET NOCOUNT OFF
	
END' 
END
GO
