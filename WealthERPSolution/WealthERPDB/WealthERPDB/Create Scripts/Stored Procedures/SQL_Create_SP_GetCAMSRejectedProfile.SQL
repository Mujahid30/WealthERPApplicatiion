/****** Object:  StoredProcedure [dbo].[SP_GetCAMSRejectedProfile]    Script Date: 06/24/2009 11:58:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCAMSRejectedProfile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCAMSRejectedProfile]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCAMSRejectedProfile]    Script Date: 06/24/2009 11:58:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCAMSRejectedProfile]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE SP_GetCAMSRejectedProfile
@RejectedId INT

AS

SET NOCOUNT ON

	SELECT 
		* 
	FROM
		CustomerMFCAMSXtrnlProfileStaging
	WHERE
		CMGCXPS_Id = @RejectedId
	

SET NOCOUNT OFF
' 
END
GO
