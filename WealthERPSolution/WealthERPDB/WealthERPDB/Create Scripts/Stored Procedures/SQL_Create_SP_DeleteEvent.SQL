/****** Object:  StoredProcedure [dbo].[SP_DeleteEvent]    Script Date: 06/24/2009 11:14:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteEvent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeleteEvent]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteEvent]    Script Date: 06/24/2009 11:14:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteEvent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_DeleteEvent]
@EventSetupID BIGINT

AS

BEGIN
	SET NOCOUNT ON
	
	DELETE 
	FROM 
		dbo.AlertEventSetup
	WHERE
		AES_EventSetupID = @EventSetupID
		 
	SET NOCOUNT OFF
	
END' 
END
GO
