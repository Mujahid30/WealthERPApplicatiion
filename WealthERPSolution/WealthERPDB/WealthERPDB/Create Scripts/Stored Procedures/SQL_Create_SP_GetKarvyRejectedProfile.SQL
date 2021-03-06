/****** Object:  StoredProcedure [dbo].[SP_GetKarvyRejectedProfile]    Script Date: 06/24/2009 12:03:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetKarvyRejectedProfile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetKarvyRejectedProfile]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetKarvyRejectedProfile]    Script Date: 06/24/2009 12:03:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetKarvyRejectedProfile]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE SP_GetKarvyRejectedProfile
@RejectedId INT

AS

SET NOCOUNT ON

		SELECT
			*
		FROM
			dbo.CustomerMFKarvyXtrnlProfileStaging
		WHERE
			CMFKXPS_Id = @RejectedId

SET NOCOUNT OFF
' 
END
GO
